using jiffyOnline.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiffyOnline.Entity
{
     [Serializable]
    public class DistrictEntity
    {
         public string Text { get; set; }
         public int Value { get; set; }
    }
}