﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TamagotchiHotel.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TamagotchiHotelEntities : DbContext
    {
        public TamagotchiHotelEntities()
            : base("name=TamagotchiHotelEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<HotelKamer> HotelKamer { get; set; }
        public virtual DbSet<Tamagotchi> Tamagotchi { get; set; }
        public virtual DbSet<Type> Type { get; set; }
    }
}
