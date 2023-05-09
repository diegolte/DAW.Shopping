using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UESAN.Shopping.Core.DTOs;
using UESAN.Shopping.Core.Interfaces;

namespace UESAN.Shopping.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productService.GetAll();
            return Ok(product);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductInsertDTO product)
        {
            var result = await _productService.Insert(product);
            if (!result)
                return BadRequest();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductUpdateDTO product)
        {
            if (id != product.Id)
                return NotFound();

            var result = await _productService.Update(product);
            if (!result)
                return BadRequest();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            if (!result)
                return BadRequest();

            return NoContent();
        }
    }
}
