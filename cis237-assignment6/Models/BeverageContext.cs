// Edrick Tamayo
// Thursday 3:30PM
// 12/18/20
namespace cis237_assignment6.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BeverageContext : DbContext
    {
        public BeverageContext()
            : base("DefaultConnection")
        {
        }

        public virtual DbSet<Beverage> Beverages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beverage>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Beverage>()
                .Property(e => e.pack)
                .IsFixedLength();

            modelBuilder.Entity<Beverage>()
                .Property(e => e.price)
                .HasPrecision(19, 4);
        }
    }
}
