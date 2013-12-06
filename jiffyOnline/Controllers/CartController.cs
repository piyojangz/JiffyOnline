using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jiffyOnline.Entity;
using jiffyOnline.Models;
using jiffyOnline.DataAccess;
using Newtonsoft.Json;

namespace jiffyOnline.Controllers
{
    public class CartController : Controller
    {
        private string[] ALPHABETINDEX = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private List<CategoriesEntity> CATEGORY;
        private DataFactory _DataFactory = new DataFactory();
        private SessionModels objSession;
        public CartController()
        {
            CATEGORY = _DataFactory.GetCategories();
            ViewBag.ALPHABETINDEX = this.ALPHABETINDEX;
            ViewBag.CATEGORY = this.CATEGORY;
            objSession = new SessionModels();
        }
        public ActionResult Index()
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.PRODUCTRECOMMEND = _DataFactory.GetProductRecommend().Take(5);
            ApplicationHelpers.getCookieOrders(this);
            ViewBag.OrderDetail = null;
            if (Session["Orders"] != null)
            {
                ViewBag.OrderDetail = (List<ItemOrderEntity>)Session["Orders"];
            }

            ViewBag.CurentPage = "Cart";
            ViewBag.Title = "SHOP A HORIC By Jiffy | Cart";

            OrderDetailEntity _OrderDetailEntity = new OrderDetailEntity();


            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(List<ItemOrderEntity> model, string button)
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.PRODUCTRECOMMEND = _DataFactory.GetProductRecommend().Take(5);
            ApplicationHelpers.getCookieOrders(this);
            ViewBag.OrderDetail = null;
            switch (button)
            {
                case "Update":
                    if (Session["Orders"] != null)
                    {

                        List<ItemOrderEntity> objOrders = (List<ItemOrderEntity>)Session["Orders"];
                        foreach (var item in model)
                        {
                            ItemOrderEntity Orders = objOrders.Where(p => p.ItemId == item.ItemId).FirstOrDefault();
                            Orders.Amount = item.Amount;
                            ViewBag.OrderDetail = objOrders;
                        }

                    }
                    break;
                case "More":
                    return RedirectToAction("Index", "Home");
                    break;
                default:
                    break;
            }




            ViewBag.CurentPage = "Cart";
            ViewBag.Title = "SHOP A HORIC By Jiffy | Cart";

            OrderDetailEntity _OrderDetailEntity = new OrderDetailEntity();

            return View();
        }

        [AllowAnonymous]
        public ActionResult RemoveItem(int Id)
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            RedirectToRouteResult r = RedirectToAction("Index", "Cart");
            if (Session["Orders"] != null)
            {
                ItemOrderEntity objOrderEntity = new ItemOrderEntity();
                List<ItemOrderEntity> objOrderDetail = (List<ItemOrderEntity>)Session["Orders"];
                objOrderEntity = objOrderDetail.Where(p => p.ItemId == Id).FirstOrDefault();
                objOrderDetail.Remove(objOrderEntity);
                Session["Orders"] = objOrderDetail;
                var json = JsonConvert.SerializeObject(objOrderDetail);
                var orderCookie = new HttpCookie("OrdersCookie", json);
                orderCookie.Expires.AddDays(365);
                this.ControllerContext.HttpContext.Response.SetCookie(orderCookie);
            }


            return r;
        }



        [AllowAnonymous]
        public ActionResult Checkout()
        {

            ViewBag.CurentPage = "Checkout";
            ViewBag.Title = "SHOP A HORIC By Jiffy | Checkout";
            return View();
        }
    }
}
