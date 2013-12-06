using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jiffyOnline.Entity;
using jiffyOnline.Models;
using jiffyOnline.DataAccess;
using PagedList;
using PagedList.Mvc;
using Newtonsoft.Json;
namespace jiffyOnline.Controllers
{


    public class ItemController : Controller
    {
        private string[] ALPHABETINDEX = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private List<CategoriesEntity> CATEGORY;
        private List<CategoriesEntity> menusource; // get your menus here
        private ItemDetailEntity ITEMDETAILS;
        private List<ItemDetailEntity> ITEMRELATES;
        private List<DropdownEntity> ITEMPROPERTY;
        public DataFactory _DataFactory = new DataFactory();
        private SessionModels objSession;
        private int IsAddwishList { get; set; }
        public ItemController()
        {
            CATEGORY = _DataFactory.GetCategories();
            ViewBag.ALPHABETINDEX = this.ALPHABETINDEX;
            ViewBag.CATEGORY = this.CATEGORY;
            objSession = new SessionModels();
            menusource = this.CATEGORY;
            ViewBag.Menus = _DataFactory.CreateVM(113321003, menusource);
           
        }
        public ActionResult Index(int Id)
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.CurentPage = "Cart";
            ViewBag.Id = Id;
            ViewBag.Title = "SHOP A HORIC By Jiffy | Item | พาเล็ทเครื่องสำอาง Primping With The Stars";
            return View();
        }

        public ActionResult ProductCategory()
        {

            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            List<BannerEntity> BannerS = _DataFactory.GetBannerS();
         
            ViewBag.Title = "SHOP A HORIC By Jiffy | Product Categories";
            return View();
        }

        public ActionResult Detail(int Id, ItemDetailEntity Model)
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.IsAddwishList = Session["IsAddWishlist"];
            Session["IsAddWishlist"] = null;
            ITEMDETAILS = _DataFactory.GetItemDetail().Where(t => t.ID == Id).FirstOrDefault();
            ITEMRELATES = _DataFactory.GetProductRelate().Where(t => t.ID == Id).Take(3).ToList();
            ITEMPROPERTY = _DataFactory.GetItemProperty(Id);
            ViewBag.ITEMRECENTVIEW = Session["RecentView"];
            ViewBag.ITEMDETAILS = ITEMDETAILS;
            ViewBag.ITEMRELATES = ITEMRELATES;
            ViewBag.ITEMPROPERTY = ITEMPROPERTY;
            ViewBag.ITEMIMGS = _DataFactory.GetItemImgs(ITEMDETAILS.ID);
            ViewBag.CATEGORY = _DataFactory.GetCategories().Where(p => p.ID == ITEMDETAILS.CATEGORIES_ID).FirstOrDefault();
            ViewBag.CurentPage = "Cart";
            ViewBag.Id = Id;
            ViewBag.Title = "SHOP A HORIC By Jiffy | Item | " + ITEMDETAILS.NAME_EN;

            if (Session["RecentView"] == null)
            {
                Queue<ItemDetailEntity> RecentViewList = new Queue<ItemDetailEntity>();
                RecentViewList.Enqueue(ITEMDETAILS);
                Session["RecentView"] = RecentViewList;
            }
            else
            {
                Queue<ItemDetailEntity> RecentViewList = (Queue<ItemDetailEntity>)Session["RecentView"];
                if (RecentViewList.Count(p => p.ID == Id) == 0)
                {
                    if (RecentViewList.Count < 3)
                    {
                        RecentViewList.Enqueue(ITEMDETAILS);
                    }
                    else
                    {
                        RecentViewList.Dequeue();
                        RecentViewList.Enqueue(ITEMDETAILS);
                        Session["RecentView"] = RecentViewList;
                    }
                }


            }

            return View();
        }
        [HttpPost]
        public ActionResult Detail(int Id, ItemOrderEntity model, String button)
        {

            RedirectToRouteResult r = RedirectToAction("Index", "Cart");
            T_CUSTOMER objCustomer = objSession.GetSessionCustomer(this);
            switch (button)
            {
                case "Add":
                    r = RedirectToAction("Index", "Cart");
                    addItemToCart(Id, model);
                    break;
                case "Wishlist":
                    if (objCustomer != null)
                    {
                        r = RedirectToAction("Detail", "Item");
                        addItemToWislist(Id, objCustomer.ID);
                    }
                    else
                    {
                        r = RedirectToAction("Login", "Home");
                    }

                    break;
                default:

                    break;
            }

            return r;
        }


        public void addItemToWislist(int ProductId, int CustomerId)
        {

            if (_DataFactory.SetWishlist(ProductId, CustomerId))
            {
                Session["IsAddWishlist"] = 1;
            }
            else
            {
                Session["IsAddWishlist"] = 0;
            }
        }

        public void addItemToCart(int Id, ItemOrderEntity model)
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ITEMDETAILS = _DataFactory.GetItemDetail().Where(t => t.ID == Id).FirstOrDefault();
            model.ItemId = Id;
            model.NAME_EN = ITEMDETAILS.NAME_EN;
            model.NAME_TH = ITEMDETAILS.NAME_TH;
            model.PRICE = ITEMDETAILS.PRICE;
            model.SHORT_DESCR_EN = ITEMDETAILS.SHORT_DESCR_EN;
            model.SHORT_DESCR_TH = ITEMDETAILS.SHORT_DESCR_TH;
            model.IMAGES = ITEMDETAILS.IMAGES;
            List<ItemOrderEntity> objOrderDetail = new List<ItemOrderEntity>();

            if (Session["Orders"] == null)
            {
                objOrderDetail.Add(model);
                Session["Orders"] = objOrderDetail;
                var json = JsonConvert.SerializeObject(objOrderDetail);
                var orderCookie = new HttpCookie("OrdersCookie", json);
                orderCookie.Expires.AddDays(365);
                this.ControllerContext.HttpContext.Response.SetCookie(orderCookie);

            }
            else
            {
                objOrderDetail = (List<ItemOrderEntity>)Session["Orders"];
                //Check items
                var currentItem = objOrderDetail.Where(p => p.ItemId == Id && p.Color == model.Color && p.Size == model.Size).FirstOrDefault();

                if (currentItem != null)
                {
                    //  currentItem.ForEach(p => p.Amount += model.Amount);
                    objOrderDetail.Where(p => p.ItemId == Id && p.Color == model.Color && p.Size == model.Size).ToList().ForEach(p => p.Amount += model.Amount);
                }
                else
                {
                    objOrderDetail.Add(model);
                }

                Session["Orders"] = objOrderDetail;
                var json = JsonConvert.SerializeObject(objOrderDetail);
                var orderCookie = new HttpCookie("OrdersCookie", json);
                orderCookie.Expires.AddDays(365);
                this.ControllerContext.HttpContext.Response.SetCookie(orderCookie);
            }
        }
        public ActionResult List(int Id)
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.CurentPage = "Cart";
            ViewBag.Title = "SHOP A HORIC By Jiffy | Item list | xxxx";
            return View();
        }

        public ActionResult Categories(int Id, int page = 1, ItemOrderEntity model = null)
        {
            List<ProductsEntity> objProducts = null;
            if (model.NAME_EN != null)
            {
                Session["CURRENTSORT"] = model.NAME_EN;
            }
            else
            {
                if (Session["CURRENTSORT"] == null)
                {
                    Session["CURRENTSORT"] = "";
                }
                model.NAME_EN = Session["CURRENTSORT"].ToString();
            }

            switch (model.NAME_EN)
            {
                case "Newest":
                    objProducts = _DataFactory.GetProductByCateId(Id).Where(p=>p.NEW_ARRIVAL_FLAG == 1).ToList();
                    break;
                case "OrderByAZ":
                    objProducts = _DataFactory.GetProductByCateId(Id).OrderBy(p => p.NAME_EN).ToList();
                    break;
                case "OrderByZA":
                    objProducts = _DataFactory.GetProductByCateId(Id).OrderByDescending(p => p.NAME_EN).ToList();
                    break;
                case "OrderByHL":
                    objProducts = _DataFactory.GetProductByCateId(Id).OrderBy(p => p.PRICE).ToList();
                    break;
                case "OrderByLH":
                    objProducts = _DataFactory.GetProductByCateId(Id).OrderByDescending(p => p.PRICE).ToList();
                    break;
                default:
                    objProducts = _DataFactory.GetProductByCateId(Id).OrderByDescending(p => p.ID).ToList();
                    break;
            }
            
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            List<BannerEntity> Banners = _DataFactory.GetBanner();
            ViewBag.BANNER = Banners.Where(p => p.Type.ToUpper() == "CATEGORY" && p.RefId == Id).FirstOrDefault();
            if ( _DataFactory.GetCategoriesById(Id) != null)
            {
                ViewBag.Title = "SHOP A HORIC By Jiffy | Categories | " + _DataFactory.GetCategoriesById(Id).NAME_EN.ToString();
            }
          
            ViewBag.objProducts = objProducts;
            return View(objProducts.ToPagedList(page, 20));
        }
        public ActionResult NewArrivals(int page = 1)
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            List<ProductsEntity> objProducts = _DataFactory.GetProduct().Where(p => p.NEW_ARRIVAL_FLAG == 1).ToList();
            ViewBag.Title = "SHOP A HORIC By Jiffy | New Arrivals ";
            ViewBag.objProducts = objProducts;
            return View(objProducts.OrderByDescending(p => p.ID).ToPagedList(page, 20));
        }

        public ActionResult BestSeller(int page = 1)
        {
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            List<ProductsEntity> objProducts = _DataFactory.GetProduct().Where(p => p.NEW_ARRIVAL_FLAG == 1).ToList();
            ViewBag.Title = "SHOP A HORIC By Jiffy | Best Seller ";
            ViewBag.objProducts = objProducts;
            return View(objProducts.OrderByDescending(p => p.ID).ToPagedList(page, 20));
        }

        public List<CategoriesEntity> getCategoryByPrefix(string prefix)
        {
             return _DataFactory.GetCategories().Where(p=>p.NAME_EN.Substring(0,1).Contains(prefix)).ToList();
        }
    }
}
