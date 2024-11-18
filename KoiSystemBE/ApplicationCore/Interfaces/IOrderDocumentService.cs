using ApplicationCore.Dto;
using Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IOrderDocumentService
    {
        Task<ApiResponse<OrderDocumentDto>> CreateOrderDocumentAsync(CreateOrderDocumentDto documentDto);
        Task<ApiResponse<List<OrderDocumentDto>>> GetDocumentsByOrderIdAsync(int orderId);
        Task<ApiResponse<string>> DeleteDocumentAsync(int documentId);
        Task<ApiResponse<OrderDocumentDto>> UpdateOrderDocumentAsync(UpdateOrderDocumentDto documentDto);
    }
}
