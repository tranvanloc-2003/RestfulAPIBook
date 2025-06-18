using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPIBook.Models.Domain;
using RestfulAPIBook.Models.Dto.Book;
using RestfulAPIBook.Repository.Implements;
using RestfulAPIBook.Repository.Interface;

namespace RestfulAPIBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookRequestDto createBookRequestDto)
        {
            var books = new Book
            {
                Title = createBookRequestDto.Title,
                Author = createBookRequestDto.Author,
                Content = createBookRequestDto.Content,
                Description = createBookRequestDto.Description,
                FeaturedImage = createBookRequestDto.FeaturedImage,
                IsVisible = createBookRequestDto.IsVisible,
                Price = createBookRequestDto.Price,
                PublicshedDate = createBookRequestDto.PublicshedDate,
                UrlHandle = createBookRequestDto.UrlHandle,
            };
            await bookRepository.AddAsync(books);
            var response = new BookDto
            {
                Id = books.Id,
                Title = books.Title,
                Author = books.Author,
                Content = books.Content,
                Description = books.Description,
                FeaturedImage = books.FeaturedImage,
                IsVisible = books.IsVisible,
                Price = books.Price,
                PublicshedDate = books.PublicshedDate,
                UrlHandle = books.UrlHandle,
            };
            return Ok(response);
        }
        //GET
        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            var books = await bookRepository.GetAllAsync();
            var response = new List<BookDto>();
            foreach (var book in books)
            {
                response.Add(new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Content = book.Content,
                    Description = book.Description,
                    FeaturedImage = book.FeaturedImage,
                    IsVisible = book.IsVisible,
                    Price = book.Price,
                    PublicshedDate = book.PublicshedDate,
                    UrlHandle = book.UrlHandle,


                });

            }
            return Ok(response);

        }
        //GET ID
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBookById([FromRoute] Guid id)
        {
            var books = await bookRepository.GetById(id);
            if(books is null)
            {
                return NotFound();
            }
            var response = new BookDto
            {
                Id = books.Id,
                Title = books.Title,
                Author = books.Author,
                Content = books.Content,
                Description = books.Description,
                FeaturedImage = books.FeaturedImage,
                IsVisible = books.IsVisible,
                Price = books.Price,
                PublicshedDate = books.PublicshedDate,
                UrlHandle = books.UrlHandle,
            };
            return Ok(response);
        }
        //PUT:
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBook([FromRoute] Guid id, UpdateBookRequestDto updateBookRequestDto)
        {
            var books = new Book
            {
                Id = id,
                Title = updateBookRequestDto.Title,
                Author = updateBookRequestDto.Author,
                Content = updateBookRequestDto.Content,
                Description = updateBookRequestDto.Description,
                FeaturedImage = updateBookRequestDto.FeaturedImage,
                IsVisible = updateBookRequestDto.IsVisible,
                Price = updateBookRequestDto.Price,
                PublicshedDate = updateBookRequestDto.PublicshedDate,
                UrlHandle = updateBookRequestDto.UrlHandle,
            };
            var existingBook = await bookRepository.UpdateAsync(books);
            if(existingBook is null)
            {
                return NotFound();
            }
            var response = new BookDto
            {
                Id = books.Id,
                Title = books.Title,
                Author = books.Author,
                Content = books.Content,
                Description = books.Description,
                FeaturedImage = books.FeaturedImage,
                IsVisible = books.IsVisible,
                Price = books.Price,
                PublicshedDate = books.PublicshedDate,
                UrlHandle = books.UrlHandle,
            };
            return Ok(response);
        }
    }
}
