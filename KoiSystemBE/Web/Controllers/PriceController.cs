using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }
        [HttpGet("transport-methods")]
        public async Task<IActionResult> GetAvailableTransportMethods()
        {
            var response = await _priceService.GetAvailableTransportMethodsAsync();

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }
        [HttpGet("get-prices")]
        public async Task<IActionResult> GetPrices([FromQuery] string? transportMethod, [FromQuery] decimal? weight, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var response = await _priceService.GetPricesByFiltersAsync(transportMethod, weight, minPrice, maxPrice);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut("update-price/{priceId}")]
        public async Task<IActionResult> UpdatePrice(int priceId, [FromBody] UpdatePriceDto updatePriceDto)
        {
            var response = await _priceService.UpdatePriceAsync(priceId, updatePriceDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
