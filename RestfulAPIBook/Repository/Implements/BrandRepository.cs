using Microsoft.EntityFrameworkCore;
using RestfulAPIBook.Data;
using RestfulAPIBook.Models.Domain;
using RestfulAPIBook.Repository.Interface;

namespace RestfulAPIBook.Repository.Implements
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BrandRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Brand> AddAsync(Brand brand)
        {
            await dbContext.Brands.AddAsync(brand);
            await dbContext.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand?> DeleteAsync(Guid id)
        {
            var existingBrand = await dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBrand is null)
            {
                return null;
            }
            dbContext.Brands.Remove(existingBrand);
            await dbContext.SaveChangesAsync();
            return existingBrand;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await dbContext.Brands.ToListAsync();
        }

        public async Task<Brand?> GetById(Guid id)
        {
            return await dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Brand> UpdateAsync(Brand brand)
        {
            var existingBrand = await dbContext.Brands.FirstOrDefaultAsync(x => x.Id == brand.Id);
            if (existingBrand is not null)
            {
             dbContext.Entry(existingBrand).CurrentValues.SetValues(brand);
            await dbContext.SaveChangesAsync();
            return brand;
            }
             return null;
        }
    }
}
