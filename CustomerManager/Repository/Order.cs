//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CustomerManager.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int OrderID { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public System.DateTime Date { get; set; }
        public int CustomerID { get; set; }
        public bool FulFilled { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
