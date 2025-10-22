using Eshop.Dto;
using Eshop.Dto.ProductModel;
using Eshop.Service;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productService;

        public ProductsController(IProductsService productService)
        {
            _productService = productService;
        }



        [HttpPut("update{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto productDto, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateProduct(id, productDto, cancellationToken);
            return Ok(result);
        }



        [HttpPost("create")]
            public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto product, CancellationToken cancellationToken)
            {
                if (!ModelState.IsValid)
                    return BadRequest(new BaseResponse<bool>
                    {
                        IsSuccess = false,
                        Message = "Invalid product data"
                    });

                var response = await _productService.AddProduct(product, cancellationToken);

                if (!response.IsSuccess)
                    return BadRequest(response);

                return Ok(response);
            }


        [HttpGet("get-by-id{id:guid}")]
        public async Task<IActionResult> GetProductById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _productService.GetProductById(id, cancellationToken);

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

        [HttpGet("get-all  ")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _productService.GetAllProduct();
            return Ok(response);
        }


        [HttpDelete("delete{id:guid}")]
        public async Task<IActionResult> DeleteProdct(Guid id, CancellationToken cancellationToken)
        {
            await _productService.DeleteProduct(id, cancellationToken);
            return Ok("Category deleted successfully.");
        }
    }
}
