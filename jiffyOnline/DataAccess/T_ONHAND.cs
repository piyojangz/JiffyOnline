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
    
    public partial class T_ONHAND
    {
        public int ID { get; set; }
        public int PRODUCT_ID { get; set; }
        public string LOT { get; set; }
        public string SN { get; set; }
        public decimal QUANTITY { get; set; }
        public System.DateTime CREATE_DATE { get; set; }
        public string CREATE_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public Nullable<System.DateTime> DELETE_DATE { get; set; }
        public string DELETE_BY { get; set; }
        public Nullable<decimal> COST { get; set; }
    
        public virtual T_PRODUCTS T_PRODUCTS { get; set; }
    }
}