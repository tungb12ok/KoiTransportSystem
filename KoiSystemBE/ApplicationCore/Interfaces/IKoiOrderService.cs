using ApplicationCore.Dto;
using Infrastructure.Models;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IKoiOrderService
    {
        Task<ApiResponse<KoiOrderDto>> CreateKoiOrderAsync(CreateKoiOrderDto orderDto);
        Task<ApiResponse<KoiOrderDto>> UpdateKoiOrderAsync(int orderId, UpdateKoiOrderDto updateOrderDto);
        Task<ApiResponse<KoiOrderDto>> GetOrderDetailsAsync(int orderId);
        Task<ApiResponse<List<KoiOrderDto>>> GetOrdersByFiltersAsync(string? status, string? transportMethod, string? searchTerm, string sortBy, bool isDescending, int pageNumber, int pageSize);
        Task<ApiResponse<string>> CancelOrderAsync(int orderId);
        Task<ApiResponse<List<KoiOrderDto>>> GetOrdersByCustomerIdAsync(
                int customerId,
                int pageNumber = 1,
                int pageSize = 10);
        Task<ApiResponse<List<KoiOrderDto>>> GetCompletedOrdersByCustomerIdAsync(int customerId);
        Task<ApiResponse<List<KoiOrderDto>>> GetOrdersPendingHealthCheckAsync();
        Task<ApiResponse<string>> UpdateHealthStatusAsync(int orderId, string healthStatus, List<HealthCertificateDto> certificates);
        Task<ApiResponse<string>> UpdateTransportProcessAsync(int orderId, string step, string status);
        Task<ApiResponse<string>> DeleteKoiOrderAsync(int orderId);
        Task<ApiResponse<List<EnumDto>>> GetOrderStatusListAsync();
        Task<ApiResponse<KoiOrderDto>> UpdateOrderStatus(int orderId, string orderStatus);
    }
}