using jiffyOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiffyOnline.Entity
{
    [Serializable]
    public class ItemOrderEntity : ItemDetailEntity
    {
        public int ListId { get; set; }
        public int ItemId { get; set; }
        public int Size { get; set; }
        public int Color { get; set; }
        public decimal Amount { get; set; }
        public bool WithList { get; set; }

        public decimal TotalSummary { get; set; }

        public string PRICEDISPLAY
        {
            get
            {
                return StringHelpers.FormatDecimal(PRICE);
            }

        }


    }
}