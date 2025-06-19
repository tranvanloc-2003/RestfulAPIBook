using Microsoft.AspNetCore.Mvc;
using RestfulAPIBook.Models.Domain;
using RestfulAPIBook.Models.Dto.Book;
using RestfulAPIBook.Models.Dto.Brand;
using RestfulAPIBook.Models.Dto.Categories;
using RestfulAPIBook.Repository.Implements;
using RestfulAPIBook.Repository.Interface;

namespace RestfulAPIBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepository;
        private readonly ICategoriesRepository categoriesRepository;
        private readonly IBrandRepository brandRepository;

        public BookController(IBookRepository bookRepository, ICategoriesRepository categoriesRepository,IBrandRepository brandRepository)
        {
            this.bookRepository = bookRepository;
            this.categoriesRepository = categoriesRepository;
            this.brandRepository = brandRepository;
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
                Categories = new List<Categories>(),
                Brands = new List<Brand>()

            };
          
            //lay theo id category
            foreach (var GuidCategoriesId in createBookRequestDto.CategoriesId)
            {
                var categoriesRelationship = await categoriesRepository.GetById(GuidCategoriesId);
                if(categoriesRelationship is not null)
                {
                    books.Categories.Add(categoriesRelationship);
                }
            }
            // lay id theo brand
            foreach(var GuidBrandId in createBookRequestDto.BrandId)
            {
              var brandRelationship =  await brandRepository.GetById(GuidBrandId);
                if(brandRelationship is not null)
                {
                    books.Brands.Add(brandRelationship);
                }
            } 
            var relationshipBook =  await bookRepository.AddAsync(books);
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
                CategoriesDtos = books.Categories.Select(x => new CategoriesDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),
                BrandDtos = books.Brands.Select(x => new BrandDto
                {
                    Id = x.Id,
                    NameBrand = x.NameBrand,
                    UrlHandle = x.UrlHandle,
                    featuredImage = x.featuredImage,
                }).ToList(),

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
                    CategoriesDtos = book.Categories.Select(x => new CategoriesDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList(),
                    BrandDtos = book.Brands.Select(x => new BrandDto
                    {
                        Id = x.Id,
                        NameBrand = x.NameBrand,
                        UrlHandle = x.UrlHandle,
                        featuredImage = x.featuredImage,
                    }).ToList(),
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
            if (books is null)
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
                CategoriesDtos = books.Categories.Select(x => new CategoriesDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),
                BrandDtos = books.Brands.Select(x => new BrandDto
                {
                    Id = x.Id,
                    NameBrand = x.NameBrand,
                    UrlHandle = x.UrlHandle,
                    featuredImage = x.featuredImage,
                }).ToList(),

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
                Categories = new List<Categories>(),
                Brands = new List<Brand>()
            };
            //lay id cua categories
            foreach(var idCategories in updateBookRequestDto.CategoriesId)
            {
                var relationshipCategories = await categoriesRepository.GetById(idCategories);
                if(relationshipCategories is not null)
                {
                    books.Categories.Add(relationshipCategories);
                }
            }
            //lay id cua brand
            foreach (var idBrand in updateBookRequestDto.BrandId)
            {
                var relationshipBrand = await brandRepository.GetById(idBrand);
                if(relationshipBrand is not null)
                {
                    books.Brands.Add(relationshipBrand);
                }
            }
            var existingBook = await bookRepository.UpdateAsync(books);

            if (existingBook is null)
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
                CategoriesDtos = books.Categories.Select(x => new CategoriesDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList(),
                BrandDtos = books.Brands.Select(x => new BrandDto
                {
                    Id = x.Id,
                    NameBrand = x.NameBrand,
                    UrlHandle = x.UrlHandle,
                    featuredImage = x.featuredImage,
                }).ToList(),
            };
            return Ok(response);
        }
    }
}
