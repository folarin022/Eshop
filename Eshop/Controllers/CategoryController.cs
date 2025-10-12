using System;
using System.Threading;
using System.Threading.Tasks;
using Eshop.Service;
using Eshop.Service.Inteterface;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] Data.Category category, CancellationToken cancellationToken)
        {
            if (id != category.Id)
                return BadRequest("ID in route and category object do not match.");

            await _categoryService.UpdateCategory(category, cancellationToken);
            return Ok("Category updated successfully.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]Data.Category category, CancellationToken cancellationToken)
        {
            await _categoryService.CreateCategory(category, cancellationToken);
            return Ok("Category created successfully.");
        }

        [HttpGet("Category:{id:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellationToken)
        {
            var response = await _categoryService.GetCategoryById(id, cancellationToken);

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
        //public async Task<IActionResult> GetAllCategories()
        //{
        //    var response = await _categoryService.GetAllCategories();
        //    return Ok(response);
        //}   



        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
        {
            await _categoryService.DeleteCategory(id, cancellationToken);

            return Ok("Category deleted successfully.");
        }
    }
}
