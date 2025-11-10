using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Eshop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly PhotoService _photoService;

        public PhotosController(PhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadPhoto( IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided");

            try
            {
                var url = await _photoService.UploadImageAsync(file);
                return Ok(new { Url = url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }
}
