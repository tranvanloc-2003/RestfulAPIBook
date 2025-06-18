using RestfulAPIBook.Models.Domain;

namespace RestfulAPIBook.Repository.Interface
{
    public interface ICategoriesRepository
    {
        Task<Categories> CreateAsync(Categories categories);
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories?> GetById(Guid id);

        Task<Categories?> UpdateAsync(Categories categories);
        Task<Categories?> DeleteAsync(Guid id);
    }
}
