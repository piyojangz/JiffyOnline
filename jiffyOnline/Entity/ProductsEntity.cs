using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jiffyOnline.DataAccess;
using jiffyOnline.Models;

namespace jiffyOnline.Entity
{
    [Serializable]
    public class ProductsEntity
    {
        public int ID { get; set; }
        public string NAME_TH { get; set; }
        public string NAME_EN { get; set; }
        public string DESCR_TH { get; set; }
        public string DESCR_EN { get; set; }
        public int CATEGORIES_ID { get; set; }
        public string PRODUCT_SKU { get; set; }
        public int VENDOR_ID { get; set; }
        public decimal COST { get; set; }
        public decimal PRICE { get; set; }
        public short ACTIVE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public Nullable<System.DateTime> DELETE_DATE { get; set; }
        public string DELETE_BY { get; set; }
        public string SHORT_DESCR_TH { get; set; }
        public string SHORT_DESCR_EN { get; set; }
        public Nullable<decimal> OLD_PRICE { get; set; }
        public short BUY_FLAG { get; set; }
        public short WISH_LIST { get; set; }
        public short CALL_PRICE_FLAG { get; set; }
        public short SHIPPING_FLAG { get; set; }
        public decimal SHIPPING_CHARGE { get; set; }
        public decimal TAX_VALUE { get; set; }
        public string TAX_TYPE { get; set; }
        public decimal MIN_QTY { get; set; }
        public Nullable<decimal> MAX_QTY { get; set; }
        public decimal STOCK_QTY { get; set; }
        public short DISPLAY_QTY_FLAG { get; set; }
        public Nullable<decimal> MIN_STOCK_QTY { get; set; }
        public Nullable<short> NEW_ARRIVAL_FLAG { get; set; }
        public int BRAND_ID { get; set; }
        public Nullable<short> RECOMMEND_FLAG { get; set; }
        public Nullable<decimal> WEIGTH { get; set; }
        public string IMAGES { get; set; }
        public string IMAGESDisplay
        {
            get
            {
                if (IMAGES != null)
                {
                    return StringHelpers.ImagePath + IMAGES;
                }
                else
                {
                    return "~/Content/img/no-imge.png";
                }
            }
        }


    }
}