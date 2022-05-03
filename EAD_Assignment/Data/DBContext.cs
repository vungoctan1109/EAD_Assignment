using EAD_Assignment.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EAD_Assignment.Data
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public DbSet<Articles> Articles { get; set; }
        public DbSet<Sources> Sources { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articles>().Property(m => m.CreatedAt).IsOptional();
            modelBuilder.Entity<Articles>().Property(m => m.UpdatedAt).IsOptional();
            modelBuilder.Entity<Articles>().Property(m => m.Status).IsOptional();
            modelBuilder.Entity<Articles>().Property(m => m.Category).IsOptional();
            base.OnModelCreating(modelBuilder);
        }
    }
}