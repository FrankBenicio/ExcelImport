
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class ExcelContext : DbContext
    {
        public ExcelContext(DbContextOptions options) : base(options)
        {
            //
        }


        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>(x =>
            {
                x.Property(x => x.Name)
                .HasMaxLength(50)
                .HasColumnType("varchar(50)")
                .IsRequired();

                x.Property(x => x.PriceUnit)
                .IsRequired();

                x.Property(x => x.Quantity)
               .IsRequired();

                x.Property(x => x.DeliveryDate)
               .IsRequired();
            });

        }
    }
}
