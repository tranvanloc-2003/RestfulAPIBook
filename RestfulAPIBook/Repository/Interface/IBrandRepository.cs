using RestfulAPIBook.Models.Domain;

namespace RestfulAPIBook.Repository.Interface
{
    public interface IBrandRepository
    {
        Task<Brand> AddAsync(Brand brand);
        Task<IEnumerable<Brand>> GetAllAsync();
        Task<Brand?> GetById(Guid id);
        Task<Brand> UpdateAsync(Brand brand);
        Task<Brand?> DeleteAsync(Guid id);
    }
}
