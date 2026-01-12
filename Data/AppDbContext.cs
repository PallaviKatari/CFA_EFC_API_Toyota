using CFA_EFC_API.DTO;
using CFA_EFC_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CFA_EFC_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        //Product - Database - Singular form
        //Products - .NET - Plural form
        public DbSet<Product> Product => Set<Product>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentDto>().HasNoKey();
        }
    }

}
