using jiffyOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiffyOnline.Entity
{
     [Serializable]
    public class OrderDetailEntity
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Images{ get; set; }
        public string ImagesDisplay
        {
            get
            {
                if (Images != null)
                {
                    return StringHelpers.ImagePath + Images;
                }
                else
                {
                    return "~/Content/img/no-imge.png";
                }
            }
        }
        public string UnitPriceDisPlay
        {
            get
            {
                return StringHelpers.FormatDecimal(UnitPrice);
            }

        }
    










    }
  
}