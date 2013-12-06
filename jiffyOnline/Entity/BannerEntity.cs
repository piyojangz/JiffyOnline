using jiffyOnline.DataAccess;
using jiffyOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiffyOnline.Entity
{
     [Serializable]
    public class BannerEntity : T_BANNER
    {
        public int ID { get; set; }
        public int? RefId { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string Active { get; set; }
        public int? Priorrity { get; set; }
        public string Position { get; set; }
        public string Img { get; set; }
        public string ImgDisplay
        {
            get
            {
                if (Img == null || Img == "")
                {
                    return StringHelpers.ImagePath + "/pic/noimage.jpg";
                }
                return StringHelpers.ImagePath + Img;
            }

        }
    }
}