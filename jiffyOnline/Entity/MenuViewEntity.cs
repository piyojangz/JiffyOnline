using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jiffyOnline.Entity
{
     [Serializable]
    public class MenuViewEntity
    {
         public int MenuId { get; set; }
         public string Name { get; set; }

         public List<MenuViewEntity> Children { get; set; }
    }
}