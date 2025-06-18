using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPIBook.Models.Domain;
using RestfulAPIBook.Models.Dto;
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



        // ============================
        // 3. LẤY DANH MỤC THEO ID (GET)
        // ============================
        // Endpoint: GET /api/categories/{id}



        // ============================
        // 4. CẬP NHẬT DANH MỤC THEO ID (PUT)
        // ============================
        // Endpoint: PUT /api/categories/{id}




        // ============================
        // 5. XÓA DANH MỤC THEO ID (DELETE)
        // ============================
        // Endpoint: DELETE /api/categories/{id}
    }
}
