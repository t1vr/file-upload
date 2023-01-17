using FileUploadPlaygroundProject.Models;
using FileUploadPlaygroundProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadPlaygroundProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IFileService _fileService;

        public FileUploadController(ILogger<WeatherForecastController> logger, IFileService fileService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024 * 8)]
        public async Task<IActionResult> Upload(IFormFile[] images)
        {
            if (images.Length > 10)
            {
                return BadRequest();
            }

            await _fileService.Process(images.Select(i => new ImageInputModel
            {
                Name = i.FileName,
                Type = i.ContentType,
                Content = i.OpenReadStream()
            }));
            return Ok(images);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            return Ok(await _fileService.GetAllImages());
        }

        [Route("Thumbnails/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetThumbnails(Guid id)
        {
            return File(await _fileService.GetThumbnails(id),"image/jpeg");
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetFullScreenImageById(Guid id)
        {
            return Ok(await _fileService.GetFullScreenImageById(id));
        }
    }
}