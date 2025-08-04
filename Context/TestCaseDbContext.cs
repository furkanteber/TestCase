using Microsoft.EntityFrameworkCore;
using TestCase.Entities;

namespace TestCase.Context
{
    public class TestCaseDbContext : DbContext
    {
    
        public TestCaseDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
