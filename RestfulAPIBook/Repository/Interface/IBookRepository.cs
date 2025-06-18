using RestfulAPIBook.Models.Domain;

namespace RestfulAPIBook.Repository.Interface
{
    public interface IBookRepository
    {
        Task<Book> AddAsync(Book book);
        Task<Book?> UpdateAsync(Book book);
        Task<Book?> DeleteAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetById(Guid id);
    }
}
