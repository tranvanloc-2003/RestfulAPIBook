using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPIBook.Models.Domain;
using RestfulAPIBook.Models.Dto.Categories;
using RestfulAPIBook.Repository.Interface;

namespace RestfulAPIBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        // ============================
        // 1. TẠO MỚI DANH MỤC (POST)
        // ============================
        // Endpoint: POST /api/categories

        //POST: api/book
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoriesRequestDto categoriesRequestDto)
        {
            // Tạo object domain từ DTO nhận vào
            var category = new Categories
            {
                Name = categoriesRequestDto.Name,
                UrlHandle = categoriesRequestDto.UrlHandle,
            };
            // Lưu vào database thông qua repository
            await categoriesRepository.CreateAsync(category);
            // Trả kết quả về dạng DTO
            var response = new CategoriesDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };


            return Ok(response);

        }
        // ============================
        // 2. LẤY DANH SÁCH DANH MỤC (GET)
        // ============================
        // Endpoint: GET /api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            // Lấy danh sách từ DB
            var categories = await categoriesRepository.GetAllAsync();
            // Chuyển về DTO trả về client
            var response = new List<CategoriesDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoriesDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);
        }


        // ============================
        // 3. LẤY DANH MỤC THEO ID (GET)
        // ============================
        // Endpoint: GET /api/categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            // Lấy danh sách từ DB
            var existingCategory = await categoriesRepository.GetById(id);
            if (existingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoriesDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle,
            };
            return Ok(response);
        }


        // ============================
        // 4. CẬP NHẬT DANH MỤC THEO ID (PUT)
        // ============================
        // Endpoint: PUT /api/categories/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategories([FromRoute] Guid id, UpdateCategoriesRequestDto categories)
        {
            // Tạo object domain từ DTO nhận vào
            var Categories = new Categories
            {
                Id = id,
                Name = categories.Name,
                UrlHandle = categories.UrlHandle
            };
            var category = await categoriesRepository.UpdateAsync(Categories);
            //kiểm tra danh mục có trống không?

            if (category is null)
            {
                return NotFound();
            }
            // Chuyển về DTO trả về client
            var response = new CategoriesDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);

        }



        // ============================
        // 5. XÓA DANH MỤC THEO ID (DELETE)
        // ============================
        // Endpoint: DELETE /api/categories/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategoriesById([FromRoute] Guid id)
        {
            //
           var existingCategory =  await categoriesRepository.DeleteAsync(id);
            //
            if(existingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoriesDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle
            };
            return Ok(response);
        }

    }
}
