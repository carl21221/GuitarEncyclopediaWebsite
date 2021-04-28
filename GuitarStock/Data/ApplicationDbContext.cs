using GuitarStock.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Guitar> Guitars { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Image> Images { get; set; }
    }
}
