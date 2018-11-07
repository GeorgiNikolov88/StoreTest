using Microsoft.EntityFrameworkCore;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Context
{
    class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<StoreLog> StoreLog { get; set; }
        public DbSet<StoreMoney> StoreMoney { get; set; }
        public DbSet<ProductTypes> ProductTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-S7J30B6\SQLEXPRESS;DataBase=StoreDB;Trusted_Connection=True;");
        }
    }
}
