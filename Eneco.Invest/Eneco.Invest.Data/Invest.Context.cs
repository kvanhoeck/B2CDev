﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class InvestEntities : DbContext
    {
        public InvestEntities()
            : base("name=InvestEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Axapta> Axapta { get; set; }
        public DbSet<AxaptaIsabel> AxaptaIsabel { get; set; }
        public DbSet<Isabel> Isabel { get; set; }
        public DbSet<Log> Log { get; set; }
    }
}
