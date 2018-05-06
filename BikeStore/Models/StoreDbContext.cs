using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BikeStore.Models
{
    public class StoreDbContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Product>().HasMany(c => c.Comments);
        }
    }
    //public class StoreDbInitializer : CreateDatabaseIfNotExists<StoreDbContext>
    public class StoreDbInitializer : DropCreateDatabaseAlways<StoreDbContext>
{
  protected override void Seed(StoreDbContext db)
  {
      var catElectro = new Category { Name = "Electro" };
      db.Categories.Add(catElectro);
    db.Categories.Add(new Category { Name = "Old"});
    var catSport = new Category { Name = "Sport" };
    db.Categories.Add(catSport);

    var vendorXiaomi = new Vendor { Name = "Xiaomi" };
    db.Vendors.Add(vendorXiaomi);
    var vendorComanche = new Vendor { Name = "Comanche" };
      db.Vendors.Add(vendorComanche);

    db.Products.Add(new Product { Name = "Uma Mini", Description = "Dont buy this shit any more )))", 
        Vendor = vendorXiaomi, Price= 10000,
        Category = catElectro });
    db.Products.Add(new Product
    {
        Name = "C1",
        Description = "Dont buy this.",
        Price = 10000,
        Vendor = vendorXiaomi,
        Category = catElectro
    });
    db.Products.Add(new Product
    {
        Name = "M17",
        Price = 12000,
        Description = "asda",
        Vendor = vendorComanche,
        Category = catSport
    });
    db.Products.Add(new Product
    {
        Name = "M19",
        Price = 15000,
        Description = "fdas",
        Vendor = vendorComanche,
        Category = catSport
    });
    db.Products.Add(new Product
    {
        Name = "M19",
        Price = 20000,
        Description = "fdas",
        Vendor = vendorComanche,
        Category = catSport
    });
    db.Products.Add(new Product
    {
        Name = "PRARIE 17",
        Price = 30000,
        Description = "fdas",
        Vendor = vendorComanche,
        Category = catSport
    });
    db.Products.Add(new Product
    {
        Name = "PRARIE M17",
        Price = 31000,
        Description = "fdas",
        Vendor = vendorComanche,
        Category = catSport
    });
    db.Products.Add(new Product
    {
        Name = "ONTARIO M19",
        Description = "fdas",
        Price = 45000,
        Vendor = vendorComanche,
        Category = catSport
    });


    db.Users.Add(new User
    {
        Name = "Admin",
        Email = "email@mail.com",
        Password = "password"
    });

    base.Seed(db);
  }
}
}