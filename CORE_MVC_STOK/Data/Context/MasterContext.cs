using CORE_MVC_STOK.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_MVC_STOK.Data.Context
{
    public class MasterContext : DbContext
    {
        #region Member

        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sales> Sales { get; set; }

        #endregion

        #region Constructor

        //Dependency Injection yapısı gereği yapıcı metod oluşturduk.
        public MasterContext()
        {
               
        }

        //Dependency Injection yapısı gereği yapıcı metod oluşturduk.
        public MasterContext(DbContextOptions<MasterContext> options) : base(options)
        {

        }

        #endregion


        //Bağlantı ayarlarını buradanda verebiliriz.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Örnek kullanım.
            //optionsBuilder.UseMySQL("server=localhost;database=coremvcstok;user=root;password=root");
        }

        //Veri tabanı oluşturulurken yapmak istediğimiz ayarlar.Foreign key bağlantıları vs.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(x => x.CategoryId);
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasOne(d => d.Customer)
                .WithMany(p => p.Sales)
                .HasForeignKey(x => x.CustomerId);
            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasOne(d => d.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(x => x.ProductId);
            });

               
        }
    }
}
