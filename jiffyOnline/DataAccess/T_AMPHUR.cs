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
    
    public partial class T_AMPHUR
    {
        public T_AMPHUR()
        {
            this.T_AMPHUR_POSTCODE = new HashSet<T_AMPHUR_POSTCODE>();
            this.T_DISTRICT = new HashSet<T_DISTRICT>();
        }
    
        public int ID { get; set; }
        public string CODE { get; set; }
        public string NAME_TH { get; set; }
        public int GEO_ID { get; set; }
        public int PROVINCE_ID { get; set; }
        public string NAME_EN { get; set; }
    
        public virtual ICollection<T_AMPHUR_POSTCODE> T_AMPHUR_POSTCODE { get; set; }
        public virtual T_PROVINCE T_PROVINCE { get; set; }
        public virtual ICollection<T_DISTRICT> T_DISTRICT { get; set; }
    }
}
