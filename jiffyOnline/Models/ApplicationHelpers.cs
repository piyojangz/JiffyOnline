using System;
using System.IO;
using System.Web.UI;
using System.Web.Hosting;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using jiffyOnline.Entity;
using System.Linq;
using System.Data.SqlClient;

namespace jiffyOnline.Models
{
    public class ApplicationHelpers
    {
        const int START_YEAR = 2013;



        public static string GetJDate() { return GetJDate(DateTime.Now); }

        public static string GetJDate(DateTime zdate)
        {
            return "1" + zdate.Year.ToString().Substring(2, 2) + zdate.DayOfYear.ToString("000");
        }

        public static decimal? ConvertToDecimal(String str)
        {
            decimal result;
            bool no_error = decimal.TryParse(str, out result);
            return no_error ? result : new Nullable<decimal>();
        }

        public static short? ConvertToShort(String str)
        {
            short result;
            bool no_error = short.TryParse(str, out result);
            return no_error ? result : new Nullable<short>();
        }

        public static int? ConvertToInt(String str)
        {
            int result;
            bool no_error = int.TryParse(str, out result);
            return no_error ? result : new Nullable<int>();
        }

        public static string GetDataField(string fieldName, GridViewRowEventArgs e)
        {
            var data = DataBinder.Eval(e.Row.DataItem, fieldName);
            return data != null ? data.ToString() : null;
        }

        public static decimal? ParseDecimal(String str)
        {
            string result = str;

            if(str.IndexOf(",") != -1) {
                result = result.Trim().Replace(",", string.Empty);
            }

            return ConvertToDecimal(result);
        }


        public static void getCookieOrders(Controller me)
        {
            if (me.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("OrdersCookie"))
            {
                 //me.ControllerContext.HttpContext.Request.Cookies["OrdersCookie"].Expires = DateTime.Now.AddDays(-1D);
                 //me.Response.Cookies.Add(me.ControllerContext.HttpContext.Request.Cookies["OrdersCookie"]);

                var obJectOrders = JsonConvert.DeserializeObject<List<ItemOrderEntity>>(me.ControllerContext.HttpContext.Request.Cookies["OrdersCookie"].Value);
               me.Session["Orders"] = obJectOrders;
            }

        }


      
        #region "Decimal Format"

        public static string FormatDecimal(decimal d)
        {
            return d.ToString("#,##0.00");
        }

        public static string FormatDecimal(decimal? d)
        {
            string result = string.Empty;

            if(d.HasValue) {
                result = d.Value.ToString("#,##0.00");
            }

            return result;
        }

        #endregion

        #region "Convert Area Format"

        public static decimal? ConvertWaToNgan(decimal? Wa)
        {
            Double Ngan;

            if (Wa >= 100)
            {
                Ngan = Convert.ToDouble(Wa) / 100;
            }
            else
            {
                Ngan = 0;
            }

            return Convert.ToDecimal(Ngan);
        }

        public static decimal? ConvertNganToRai(decimal? Ngan)
        {
            Double Rai;

            if (Ngan >= 4)
            {
                Rai = Convert.ToDouble(Ngan) / 4;
            }
            else
            {
                Rai = 0;
            }

            return Convert.ToDecimal(Rai);
        }

        #endregion

        #region "???"

        public static decimal? GetAgreementId(System.Web.Routing.RouteData routeData)
        {
            decimal? result = null;

            if (routeData.Values["agreementid"] != null)
            {
                result = ApplicationHelpers.ConvertToDecimal(routeData.Values["agreementid"].ToString());
            }

            return result;
        }

        public static decimal? GetAssetId(System.Web.Routing.RouteData routeData)
        {
            decimal? result = null;

            if (routeData.Values["assetid"] != null)
            {
                result = ApplicationHelpers.ConvertToDecimal(routeData.Values["assetid"].ToString());
            }

            return result;
        }

        public static decimal? GetPrimaryId(System.Web.Routing.RouteData routeData)
        {
            decimal? result = null;

            if (routeData.Values["id"] != null)
            {
                result = ApplicationHelpers.ConvertToDecimal(routeData.Values["id"].ToString());
            }

            return result;
        }

        public static string GetAgreementType(System.Web.Routing.RouteData routeData)
        {
            string result = string.Empty;

            if (routeData.Values["agreementtype"] != null)
            {
                result = routeData.Values["agreementtype"].ToString();
            }

            return result;
        }

        public static string GetAssetId(string str)
        {
            IList<string> result = StringHelpers.ConvertStringToList(str, ':');
            return result.Count == 2 ? result[1] : result[0];
        }

        public static string GetAgreementIdToString(System.Web.Routing.RouteData routeData)
        {
            return StringHelpers.ConvertToString(GetAgreementId(routeData));
        }

        public static decimal? GetAssetIdToDecimal(string str)
        {
            return ApplicationHelpers.ConvertToDecimal(GetAssetId(str));
        }

        #endregion
    }
}