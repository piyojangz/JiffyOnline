using jiffyOnline.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiffyOnline.Entity
{
     [Serializable]
    public class PostcodeEntity
    {
         public string Text
         {
             get
             {
                 return Convert.ToString(Value);
             }
         }
         public int Value { get; set; }
    }
}