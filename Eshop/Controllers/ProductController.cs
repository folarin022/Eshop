
using System;
using System.Threading;
using System.Threading.Tasks;
using Eshop.Dto.ProductModel;
using Eshop.Service;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Data.Product product, CancellationToken cancellationToken)
        {
            if (id != product.Id)
                return BadRequest("ID in route and product object do not match.");

            await _productService.UpdateProduct(product, cancellationToken);
            return Ok("Category updated successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto request)
        {
           if (request == null)
            {
                return BadRequest("Product cannot be empty");
            }

           var response = await _productService.AddProduct(request);

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);

        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProdct(Guid id, CancellationToken cancellationToken)
        {
            await _productService.DeleteProduct(id, cancellationToken);
            return Ok("Category deleted successfully.");
        }
    }
}
