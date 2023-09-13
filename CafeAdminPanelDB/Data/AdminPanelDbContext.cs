using CafeAdminPanelDB.Data.TypeConfigurations;
using CafeAdminPanelDB.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CafeAdminPanelDB.Data
{
    public class AdminPanelDbContext : DbContext
    {

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Table> Tables { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Product>(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration<Order>(new OrderTypeConfiguration());
            modelBuilder.ApplyConfiguration<OrderDetail>(new OrderDetailTypeConfiguration());
            modelBuilder.ApplyConfiguration<Table>(new TableTypeConfiguration());
        }
        public AdminPanelDbContext(DbContextOptions<AdminPanelDbContext> options) : base(options)
        {

        }
    }
}
//NOT: BURADA ON MODEL CREATING İLE YUKLEME YAPMADIK. BUNU  YERİNE appsettings.json DA BAĞLANTI BELİRLEDİK VE program.cs DE (builder.Services.AddDbContext<AdminPanelDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BaglantiAdi"))); ŞEKLİNDE DATABASE NİN ADRESİNİ TANIMLAMIŞ OLDUK. ADD-MIGRATION FIRST.... VE UPDATE-DATABASE YAPINCA SQL DE VERİLER YÜKLENMİŞ OLUYOR.

    

