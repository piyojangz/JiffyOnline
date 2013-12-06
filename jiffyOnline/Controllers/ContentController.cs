using jiffyOnline.DataAccess;
using jiffyOnline.Entity;
using jiffyOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jiffyOnline.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Content/
        private string[] ALPHABETINDEX = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private List<CategoriesEntity> CATEGORY;
        DataFactory _DataFactory = new DataFactory();
        private SessionModels objSession;
        public ContentController()
        {
            CATEGORY = _DataFactory.GetCategories();
            ViewBag.ALPHABETINDEX = this.ALPHABETINDEX;
            ViewBag.CATEGORY = this.CATEGORY;
            objSession = new SessionModels();

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Promotion()
        {
            ViewBag.CONTENT = _DataFactory.GetContents().Where(p => p.TYPE == "PROMOTION").FirstOrDefault();
            return View();
        }
        public ActionResult News()
        {
            ViewBag.CONTENT = _DataFactory.GetContents().Where(p => p.TYPE == "NEWS").FirstOrDefault();
            return View();
        }
        public ActionResult ContactUs()
        {
            ViewBag.CONTENT = _DataFactory.GetContents().Where(p => p.TYPE == "CONTACT US").FirstOrDefault();
            return View();
        }

    }
}
