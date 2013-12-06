using jiffyOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jiffyOnline.DataAccess;

namespace jiffyOnline.Entity
{
    [Serializable]
    public class ItemDetailEntity
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
        public System.DateTime? CREATE_DATE { get; set; }
        public string CREATE_BY { get; set; }
        public System.DateTime? UPDATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public System.DateTime? DELETE_DATE { get; set; }
        public string DELETE_BY { get; set; }
        public string SHORT_DESCR_TH { get; set; }
        public string SHORT_DESCR_EN { get; set; }
        public decimal? OLD_PRICE { get; set; }
        public short BUY_FLAG { get; set; }
        public short WISH_LIST { get; set; }
        public short CALL_PRICE_FLAG { get; set; }
        public short SHIPPING_FLAG { get; set; }
        public decimal SHIPPING_CHARGE { get; set; }
        public decimal TAX_VALUE { get; set; }
        public string TAX_TYPE { get; set; }
        public decimal? MIN_QTY { get; set; }
        public decimal? MAX_QTY { get; set; }
        public decimal STOCK_QTY { get; set; }
        public short DISPLAY_QTY_FLAG { get; set; }
        public decimal? MIN_STOCK_QTY { get; set; }
        public short? NEW_ARRIVAL_FLAG { get; set; }
        public int BRAND_ID { get; set; }
        public short? RECOMMEND_FLAG { get; set; }
        public decimal? WEIGTH { get; set; }
        public string IMAGES { get; set; }
        public string IMAGESDisplay
        {
            get
            {
                return StringHelpers.ImagePath + IMAGES;
            }
        }
        public string PRICEDISPLAY
        {
            get
            {
                return StringHelpers.FormatDecimal(PRICE);
            }

        }


    }
}