//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eneco.Invest.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Axapta
    {
        public Axapta()
        {
            this.AxaptaIsabel = new HashSet<AxaptaIsabel>();
        }
    
        public int Id { get; set; }
        public string CUNumber { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string ContractNumber { get; set; }
        public string ProductName { get; set; }
        public decimal Investment { get; set; }
        public System.DateTime StartDate { get; set; }
        public string Status { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
    
        public virtual ICollection<AxaptaIsabel> AxaptaIsabel { get; set; }
    }
}
