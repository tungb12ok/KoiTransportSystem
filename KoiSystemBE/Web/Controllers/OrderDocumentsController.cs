using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDocumentsController : ControllerBase
    {
        private readonly IOrderDocumentService _orderDocumentService;

        public OrderDocumentsController(IOrderDocumentService orderDocumentService)
        {
            _orderDocumentService = orderDocumentService;
        }

        // POST: api/OrderDocuments
        [HttpPost]
        public async Task<IActionResult> CreateOrderDocument([FromBody] CreateOrderDocumentDto documentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _orderDocumentService.CreateOrderDocumentAsync(documentDto);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        // GET: api/OrderDocuments/order/5
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetDocumentsByOrderId(int orderId)
        {
            var response = await _orderDocumentService.GetDocumentsByOrderIdAsync(orderId);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }

        // DELETE: api/OrderDocuments/5
        [HttpDelete("{documentId}")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            var response = await _orderDocumentService.DeleteDocumentAsync(documentId);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response);
        }
        // PUT: api/OrderDocuments
        [HttpPut]
        public async Task<IActionResult> UpdateOrderDocument([FromBody] UpdateOrderDocumentDto documentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _orderDocumentService.UpdateOrderDocumentAsync(documentDto);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}
