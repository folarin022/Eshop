using Eshop.Dto.ProductModel;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers 
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;

        public ProductsController(IProductsService productService)
        {
            _productService = productService;
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id:guid}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromForm] UpdateProductDto productDto, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateProduct(id, productDto, cancellationToken);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto request, CancellationToken cancellationToken)
        {
            var response = await _productService.AddProduct(request, cancellationToken);
            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }


        [Authorize]
        [HttpGet("get-by-id/{id:guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _productService.GetProductById(id, HttpContext.RequestAborted);

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

        [Authorize]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProduct();
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken )
        {
            var response = await _productService.DeleteProduct(id, HttpContext.RequestAborted);
            return Ok(response);
        }
    }
}
