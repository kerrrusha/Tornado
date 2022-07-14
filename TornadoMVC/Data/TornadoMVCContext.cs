using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TornadoMVC.Models;

namespace TornadoMVC.Data
{
    public class TornadoMVCContext : DbContext
    {
        public TornadoMVCContext (DbContextOptions<TornadoMVCContext> options)
            : base(options)
        {
        }

        public DbSet<TornadoMVC.Models.Category>? Category { get; set; }

        public DbSet<TornadoMVC.Models.Product>? Product { get; set; }

        public DbSet<TornadoMVC.Models.User>? User { get; set; }
    }
}
