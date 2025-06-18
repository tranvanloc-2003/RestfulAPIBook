using RestfulAPIBook.Data;
using RestfulAPIBook.Models.Domain;
using RestfulAPIBook.Repository.Interface;

namespace RestfulAPIBook.Repository.Implements
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CategoriesRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Categories> CreateAsync(Categories categories)
        {
             await dbContext.Category.AddAsync(categories);
            await dbContext.SaveChangesAsync();
            return categories;
        }
    }
}
