using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jiffyOnline.Entity;
using jiffyOnline.DataAccess;
using jiffyOnline.Models;
using System.Web.Security;
using Newtonsoft.Json;
namespace jiffyOnline.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private string[] ALPHABETINDEX = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private List<CategoriesEntity> CATEGORY;
        DataFactory _DataFactory = new DataFactory();
        private SessionModels objSession;
        public HomeController()
        {
            CATEGORY = _DataFactory.GetCategories();
            ViewBag.ALPHABETINDEX = this.ALPHABETINDEX;
            ViewBag.CATEGORY = this.CATEGORY;
            objSession = new SessionModels();

        }
        public ActionResult Index()
        {

            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            List<BannerEntity> BannerS = _DataFactory.GetBannerS();
            ViewBag.BANNER_A = BannerS.Where(p => p.Position.ToUpper() == "A" && p.Type == "OTHER").ToList();
            ViewBag.BANNER_B = BannerS.Where(p => p.Position.ToUpper() == "B" && p.Type == "OTHER").FirstOrDefault();
            ViewBag.BANNER_C = BannerS.Where(p => p.Position.ToUpper() == "C" && p.Type == "OTHER").FirstOrDefault();
            ViewBag.BANNER_D = BannerS.Where(p => p.Position.ToUpper() == "D" && p.Type == "OTHER").FirstOrDefault();
            ViewBag.BANNER_E = BannerS.Where(p => p.Position.ToUpper() == "E" && p.Type == "OTHER").FirstOrDefault();
            ViewBag.BANNER_F = BannerS.Where(p => p.Position.ToUpper() == "F" && p.Type == "OTHER").FirstOrDefault();
            ViewBag.BANNER_G = BannerS.Where(p => p.Position.ToUpper() == "G" && p.Type == "OTHER").FirstOrDefault();
            ViewBag.Title = "SHOP A HORIC By Jiffy | Home";
            return View();
        }
        public ActionResult Login()
        {
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);

            if (ModelState.IsValid)
            {
                T_CUSTOMER objCustomer = _DataFactory.GetAuthenCustomer(model);
                if (objCustomer != null)
                {
                    if (model.RememberMe)
                    {
                        var json = JsonConvert.SerializeObject(objCustomer);
                        var userCookie = new HttpCookie("user", json);
                        userCookie.Expires.AddDays(365);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(userCookie);
                    }
                    else
                    {
                        var json = JsonConvert.SerializeObject(objCustomer);
                        var userCookie = new HttpCookie("user", json);
                        userCookie.Expires.AddDays(1);
                        this.ControllerContext.HttpContext.Response.Cookies.Add(userCookie);
                    }
                    Session.Add("IsAuthen", 1);
                    Session.Add("Customer", objCustomer);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username / Pasword is wrong");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("user"))
            {
                HttpCookie userCookie = this.ControllerContext.HttpContext.Request.Cookies["user"];
                userCookie.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(userCookie);
            }

            //Session.Abandon();
            return RedirectToActionPermanent("Index");

        }
        public ActionResult Register()
        {
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            if (objSession.GetSessionCustomer(this) != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.PROVINCELIST = _DataFactory.GetProvince();
            return View();
        }
        public ActionResult Profile()
        {
            if (objSession.GetSessionCustomer(this)==null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.Wisjlist = _DataFactory.GetProductByCateId(113321001);
            return View();
        }
        public ActionResult ProfileEdit()
        {
            if (objSession.GetSessionCustomer(this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);
            ViewBag.Wisjlist = _DataFactory.GetProductByCateId(113321001);
            return View();
        }
        public ActionResult Wishlist()
        {
            if (objSession.GetSessionCustomer(this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            T_CUSTOMER objCustomer = objSession.GetSessionCustomer(this);
            ViewBag.CustomerDetail = objCustomer;
            ViewBag.Wisjlist = _DataFactory.GetWishList(objCustomer.ID);
            return View();
        }
        public ActionResult RegisterCompleted()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model, String Button)
        {
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            if (Button == "Register")
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (_DataFactory.SetCustomer(model))
                        {
                            return RedirectToAction("RegisterCompleted", "Home");
                            
                        }
                        else
                        {
                          //  return RedirectToAction("RegisterCompleted", "Home");
                        }
                    }
                    catch (MembershipCreateUserException e)
                    {
                        ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                    }

                }

            }

            ViewBag.PROVINCELIST = _DataFactory.GetProvince();
            return View();
        }
        public ActionResult ErrorPage(object sender, EventArgs e)
        {
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            ViewBag.CustomerDetail = objSession.GetSessionCustomer(this);

            ViewBag.Title = "SHOP A HORIC By Jiffy | Error page";
            ViewBag.ALPHABETINDEX = this.ALPHABETINDEX;
            ViewBag.CATEGORY = this.CATEGORY;
            return View();
        }
        [HttpGet]
        public ActionResult LoadDistrist(int aumphuId)
        {
            List<DistrictEntity> Districts = _DataFactory.GetDistrict(aumphuId);
            return Json(Districts, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult LoadAumphur(int provinceId)
        {
            List<AumphurEntity> Aumphurs = _DataFactory.GetAumphur(provinceId);
            return Json(Aumphurs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadPostCode(int aumphuId)
        {
            List<PostcodeEntity> PostCodes = _DataFactory.GetPostcode(aumphuId);
            return Json(PostCodes, JsonRequestBehavior.AllowGet);
        }


        public ActionResult OrderStatus()
        {
            if (objSession.GetSessionCustomer(this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            T_CUSTOMER objCustomer = objSession.GetSessionCustomer(this);
            ViewBag.CustomerDetail = objCustomer;
            ViewBag.Wisjlist = _DataFactory.GetWishList(objCustomer.ID);
            return View();
        }

        public ActionResult OrderHistory()
        {
            if (objSession.GetSessionCustomer(this) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.OrderDetail = objSession.GetSessionOrder(this);
            T_CUSTOMER objCustomer = objSession.GetSessionCustomer(this);
            ViewBag.CustomerDetail = objCustomer;
            ViewBag.Wisjlist = _DataFactory.GetWishList(objCustomer.ID);
            return View();
        }
















        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
