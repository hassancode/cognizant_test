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
    
    public partial class State
    {
        public State()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public int StateID { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
