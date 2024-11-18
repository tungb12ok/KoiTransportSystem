using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSContentController : ControllerBase
    {
        private readonly ICMSContentService _contentService;

        public CMSContentController(ICMSContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("get-articles")]
        public async Task<IActionResult> GetArticles(
            [FromQuery] string? contentType = null,  
            [FromQuery] string? searchTerm = null,    
            [FromQuery] string sortBy = "CreatedAt", 
            [FromQuery] bool isDescending = true,     
            [FromQuery] int pageNumber = 1,           
            [FromQuery] int pageSize = 10)            
        {
            var response = await _contentService.GetArticlesByFiltersAsync(contentType, searchTerm, sortBy, isDescending, pageNumber, pageSize);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        // Thêm nội dung
        [HttpPost("add-content")]
        public async Task<IActionResult> AddContent([FromBody] CreateCMSContentDto contentDto)
        {
            var response = await _contentService.AddContentAsync(contentDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Sửa nội dung
        [HttpPut("update-content/{contentId}")]
        public async Task<IActionResult> UpdateContent(int contentId, [FromBody] UpdateCMSContentDto updateContentDto)
        {
            var response = await _contentService.UpdateContentAsync(contentId, updateContentDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        // Xóa nội dung
        [HttpDelete("delete-content/{contentId}")]
        public async Task<IActionResult> DeleteContent(int contentId)
        {
            var response = await _contentService.DeleteContentAsync(contentId);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
