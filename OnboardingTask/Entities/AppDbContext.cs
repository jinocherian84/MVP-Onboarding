using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnboardingTask.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContextConn") { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<ProductSold> ProductSold { get; set; }
    }
}