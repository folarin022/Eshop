
using System;
using System.Threading;
using System.Threading.Tasks;
using Eshop.Data;
using Eshop.Dto;
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
        public async Task<IActionResult> UpdateProduct(Guid id, [FromRoute] Data.Product product, CancellationToken cancellationToken)
        {
            if (id != product.Id)
                return BadRequest("ID in route and product object do not match.");

            await _productService.UpdateProduct(product, cancellationToken);
            return Ok("Product updated successfully.");
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CreateProductDto product)
        {
            var newProduct = _productService.AddProduct(product); 
            return Ok(new BaseResponse<bool>
            {
                IsSuccess = true,
                Data = newProduct, 
                Message = "Product created successfully"
            });
        }
 
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id)
        {
            var response = await _productService.GetProductById(id);

            if (response == null)
            {
                return NotFound(new
                {
                    message = $"Product with ID {id} was not found."
                });
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProdct(Guid id, CancellationToken cancellationToken)
        {
            await _productService.DeleteProduct(id, cancellationToken);
            return Ok("Category deleted successfully.");
        }
    }
}
