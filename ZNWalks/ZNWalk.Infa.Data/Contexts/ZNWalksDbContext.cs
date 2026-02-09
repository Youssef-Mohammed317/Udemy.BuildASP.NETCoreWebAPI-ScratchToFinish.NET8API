using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Infa.Data.Seads;
using NZWalks.Domain.Models;

namespace NZWalks.Infa.Data.Contexts
{
    public class ZNWalksDbContext : DbContext
    {
        public ZNWalksDbContext() : base()
        {
        }
        public ZNWalksDbContext(DbContextOptions<ZNWalksDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeadZNWalksDb.SeadDifficulties(modelBuilder);
            SeadZNWalksDb.SeadRegions(modelBuilder);
        }

        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Walk> Walk { get; set; }
        public DbSet<Image> Image { get; set; }

    }
}
