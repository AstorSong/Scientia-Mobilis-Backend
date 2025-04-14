using Microsoft.AspNetCore.Mvc;
using ScientiaMobilis.Models;
using ScientiaMobilis.Services;

namespace ScientiaMobilis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromBody] EBook ebook)
        {
            if (string.IsNullOrWhiteSpace(ebook.Title) || string.IsNullOrWhiteSpace(ebook.FileUrl))
            {
                return BadRequest("Title and FileUrl are required.");
            }

            var result = await _uploadService.UploadEBookAsync(ebook);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ebooks = await _uploadService.GetAllEBooksAsync();
            return Ok(ebooks);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _uploadService.DeleteEBookAsync(id);
            if (!success)
                return NotFound($"EBook with ID {id} not found.");

            return Ok($"EBook with ID {id} deleted successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EBook ebook)
        {
            var updated = await _uploadService.UpdateEBookAsync(id, ebook);
            if (updated == null)
                return NotFound($"EBook with ID {id} not found.");

            return Ok(updated);
        }

        // test interface
        [HttpGet("test")]
        public IActionResult Test() => Ok("UploadController is working!");
    }
}
