using Eshop.Dto.CategoryModel;
using Eshop.Dto.ProductModel;
using Eshop.Service;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Eshop.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoryService)
        {
            _categoriesService = categoryService;
        }
        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] UpdateCategoryDto categoryDto,CancellationToken cancellationToken)
        {
            var result = await _categoriesService.UpdateCategory(id, categoryDto, cancellationToken);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto request, CancellationToken cancellationToken)
        {
            var response = await _categoriesService.CreateCategory(request,cancellationToken);
            if (!response.IsSuccess)
                return BadRequest(response);
            return Ok(response);
        }

        [Consumes("multipart/form-data")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id,CancellationToken cancellationToken)
        {
            var response = await _categoriesService.GetCategoryById(id,cancellationToken);

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

        [Consumes("multipart/form-data")]
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoriesService.GetAllCategories();
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [Consumes("multipart/form-data")]
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _categoriesService.DeleteCategory(id, cancellationToken);
            return Ok(response);
        }
    }
}
