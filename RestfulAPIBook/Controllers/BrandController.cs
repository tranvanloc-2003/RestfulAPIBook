using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPIBook.Models.Domain;
using RestfulAPIBook.Models.Dto.Brand;
using RestfulAPIBook.Repository.Implements;
using RestfulAPIBook.Repository.Interface;

namespace RestfulAPIBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandRequestDto createBrandRequestDto)
        {
            //
            var brand = new Brand
            {
                NameBrand = createBrandRequestDto.NameBrand,
                featuredImage = createBrandRequestDto.featuredImage,
                UrlHandle = createBrandRequestDto.UrlHandle,
                

            };
            //
            await brandRepository.AddAsync(brand);
            var response = new BrandDto
            {
                Id = brand.Id,
                NameBrand = brand.NameBrand,
                UrlHandle = brand.UrlHandle,
                featuredImage = brand.featuredImage,

            };
            return Ok(response);
        }

        //GET:
        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            // lay db
           var brands =  await brandRepository.GetAllAsync();
            var response = new List<BrandDto>();
            foreach(var brand in brands)
            {
                response.Add(new BrandDto
                {
                    Id = brand.Id,
                    NameBrand = brand.NameBrand,
                    featuredImage = brand.featuredImage,
                    UrlHandle = brand.UrlHandle,
                });
                
            }
            return Ok(response);
        }
        //GET by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBrandById([FromRoute] Guid id)
        {
           var existingBrand = await brandRepository.GetById(id);
            if(existingBrand is null)
            {
                return NotFound();
            }
            var response = new BrandDto
            {
                Id = existingBrand.Id,
                NameBrand = existingBrand.NameBrand,
                featuredImage = existingBrand.featuredImage,
                UrlHandle = existingBrand.UrlHandle
            };
            return Ok(response);

        }
        //PUT
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBrand([FromRoute] Guid id,UpdateBrandRequestDto updateBrandRequestDto)
        {
            var brands = new Brand
            {
                Id = id,
                NameBrand = updateBrandRequestDto.NameBrand,
                featuredImage = updateBrandRequestDto.featuredImage,
                UrlHandle = updateBrandRequestDto.UrlHandle,
            };
            var existingBrands = await brandRepository.UpdateAsync(brands);
            if(existingBrands is null)
            {
                return NotFound();
            }
            var response = new BrandDto
            {
                Id = existingBrands.Id,
                NameBrand = existingBrands.NameBrand,
                featuredImage = existingBrands.featuredImage,
                UrlHandle = existingBrands.UrlHandle,
            };
            return Ok(response);
        }
        //DELETE:
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var existingBrands = await brandRepository.DeleteAsync(id);
            if(existingBrands is null)
            {
                return NotFound();
            }
            var response = new BrandDto
            {
                Id = existingBrands.Id,
                NameBrand = existingBrands.NameBrand,
                featuredImage = existingBrands.featuredImage,
                UrlHandle = existingBrands.UrlHandle
            };
            return Ok(response);
        }
    }
}
