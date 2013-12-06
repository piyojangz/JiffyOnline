using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Security;
using jiffyOnline.Entity;
using System.Linq;
using jiffyOnline.DataAccess;
namespace jiffyOnline.Models
{
    public class SessionModels : System.Web.UI.Page 
    {
        public List<ItemOrderEntity> GetSessionOrder(Controller me)
        {
            if (me.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OrdersCookie"))
            {
                var obJectOrders = JsonConvert.DeserializeObject<List<ItemOrderEntity>>(me.ControllerContext.HttpContext.Request.Cookies["OrdersCookie"].Value);
                this.Session["Orders"] = obJectOrders;
            }
            else
            {
                this.Session["Orders"] = null;
            }
            return (List<ItemOrderEntity>)this.Session["Orders"];
        }

        public T_CUSTOMER GetSessionCustomer(Controller me)
        {
            if (me.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("user"))
            {
                var obJectCustomer = JsonConvert.DeserializeObject<T_CUSTOMER>(me.ControllerContext.HttpContext.Request.Cookies["user"].Value);
                this.Session["Customer"] = obJectCustomer;
            }
            else
            {                this.Session["Customer"] = null;
            }
            return (T_CUSTOMER)this.Session["Customer"];
        }
    }
}
