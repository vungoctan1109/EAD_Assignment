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

        public DbSet<Article> Articles { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().Property(m => m.CreatedAt).IsOptional();
            modelBuilder.Entity<Article>().Property(m => m.UpdatedAt).IsOptional();
            modelBuilder.Entity<Article>().Property(m => m.Status).IsOptional();
            base.OnModelCreating(modelBuilder);
        }
    }
}