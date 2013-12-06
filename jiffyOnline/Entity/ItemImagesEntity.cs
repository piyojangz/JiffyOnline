using jiffyOnline.DataAccess;
using jiffyOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiffyOnline.Entity
{
    [Serializable]
    public class ItemImagesEntity
    {
        public int ID { get; set; }
        public int RefId { get; set; }
        public string Img { get; set; }
        public string ImageDisplay
        {
            get
            {
                if (Img != null)
                {
                    return StringHelpers.ImagePath + Img;
                }
                else
                {
                    return "~/Content/img/no-imge.png";
                }
              
            }
        }
    }
}