using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApp.Models.XHouseInventory> XHouseInventory { get; set; }
        public DbSet<WebApp.Models.XOrder> XOrders{ get; set; }

    }
}
