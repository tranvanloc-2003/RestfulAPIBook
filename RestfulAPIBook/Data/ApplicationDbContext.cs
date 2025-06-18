using Microsoft.EntityFrameworkCore;
using RestfulAPIBook.Models.Domain;

namespace RestfulAPIBook.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Categories> Category { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
