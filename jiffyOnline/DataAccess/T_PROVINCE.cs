//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace jiffyOnline.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_PROVINCE
    {
        public T_PROVINCE()
        {
            this.T_AMPHUR = new HashSet<T_AMPHUR>();
        }
    
        public int ID { get; set; }
        public string CODE { get; set; }
        public string NAME_TH { get; set; }
        public int GEO_ID { get; set; }
        public string NAME_EN { get; set; }
    
        public virtual ICollection<T_AMPHUR> T_AMPHUR { get; set; }
    }
}
