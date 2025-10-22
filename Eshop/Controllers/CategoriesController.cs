using Eshop.Dto.CategoryModel;
using Eshop.Dto.ProductModel;
using Eshop.Service;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoryService)
        {
            _categoriesService = categoryService;
        }    



        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromRoute] UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var result = await _categoriesService.UpdateCategory(id, categoryDto, cancellationToken);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto request, CancellationToken cancellationToken)
        {
            await _categoriesService.CreateCategory(request, cancellationToken);
            return Ok("Category created successfully.");
        }

        [HttpGet("{id:guid}")]

        public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _categoriesService.GetCategoryById(id, cancellationToken);

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

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoriesService.GetAllCategories();
            return Ok(response);
        }



        [HttpDelete("delete{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            await _categoriesService.DeleteCategory(id, cancellationToken);

            return Ok("Category deleted successfully.");
        }
    }
}
