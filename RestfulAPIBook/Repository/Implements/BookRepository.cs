using Microsoft.EntityFrameworkCore;
using RestfulAPIBook.Data;
using RestfulAPIBook.Models.Domain;
using RestfulAPIBook.Repository.Interface;

namespace RestfulAPIBook.Repository.Implements
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Book> AddAsync(Book book)
        {
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> DeleteAsync(Guid id)
        {
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBook != null)
            {
                return null;
            }
            dbContext.Books.Remove(existingBook);
            await dbContext.SaveChangesAsync();
            return existingBook;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetById(Guid id)
        {
            return await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book?> UpdateAsync(Book book)
        {
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
            if (existingBook != null)
            {
                dbContext.Entry(existingBook).CurrentValues.SetValues(book);
                await dbContext.SaveChangesAsync();
                return existingBook;
            }
            return null;
        }
    }
}
