using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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

       

        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
           return await dbContext.Category.ToListAsync();
        }

        public async Task<Categories?> GetById(Guid id)
        {
            return await dbContext.Category.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Categories?> UpdateAsync(Categories categories)
        {
            var existingCategory = await dbContext.Category.FirstOrDefaultAsync(x => x.Id == categories.Id);
            if(existingCategory is not null)
            {
                dbContext.Entry(existingCategory).CurrentValues.SetValues(categories);
                await dbContext.SaveChangesAsync();
                return categories;
            }
            return null;
        } 
        public async Task<Categories?> DeleteAsync(Guid id)
        {
            var existingCategories =  await dbContext.Category.FirstOrDefaultAsync(x => x.Id == id);
            if(existingCategories is null)
            {
                return null;
            }
            dbContext.Category.Remove(existingCategories);
            await dbContext.SaveChangesAsync();
            return existingCategories;

        }
    }
}
