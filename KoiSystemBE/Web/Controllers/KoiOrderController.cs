using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoiOrderController : ControllerBase
    {
        private readonly IKoiOrderService _orderService;

        public KoiOrderController(IKoiOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getKoiOrderAndFilter")]
        public async Task<IActionResult> GetKoiOrderAndFilter(
            [FromQuery] string? status = null,
            [FromQuery] string? transportMethod = null,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string sortBy = "PlacedDate",
            [FromQuery] bool isDescending = true,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _orderService.GetOrdersByFiltersAsync(
                status,
                transportMethod,
                searchTerm,
                sortBy,
                isDescending,
                pageNumber,
                pageSize
            );
            return Ok(response);
        }

        [HttpGet("getEnumStatusKoiOrder")]
        public async Task<IActionResult> GetEnumKoiOrderStatus()
        {
            var response = await _orderService.GetOrderStatusListAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateKoiOrderDto orderDto)
        {
            var response = await _orderService.CreateKoiOrderAsync(orderDto);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("customer-orders")]
        public async Task<IActionResult> GetOrdersByCustomerId(
            [FromQuery] int customerId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _orderService.GetOrdersByCustomerIdAsync(customerId, pageNumber, pageSize);
            return Ok(response);
        }


        [HttpGet("order-details/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var response = await _orderService.GetOrderDetailsAsync(orderId);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost("cancel-order/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.CancelOrderAsync(orderId);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("completed-orders/{customerId}")]
        public async Task<IActionResult> GetCompletedOrders(int customerId)
        {
            var response = await _orderService.GetCompletedOrdersByCustomerIdAsync(customerId);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpGet("health-check-orders")]
        public async Task<IActionResult> GetOrdersPendingHealthCheck()
        {
            var response = await _orderService.GetOrdersPendingHealthCheckAsync();
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost("update-health-status/{orderId}")]
        public async Task<IActionResult> UpdateHealthStatus(int orderId, [FromBody] UpdateHealthStatusRequest request)
        {
            var response = await _orderService.UpdateHealthStatusAsync(orderId, request.HealthStatus, request.Certificates);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPut("update-order-status/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, string status)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, status);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
