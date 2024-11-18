using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Enums;
using Infrastructure.Models;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace ApplicationCore.Services
{
    public class KoiOrderService : IKoiOrderService
    {
        private readonly KoiFishTransportationDBContext _context;
        private readonly IMapper _mapper;

        public KoiOrderService(KoiFishTransportationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<KoiOrderDto>> CreateKoiOrderAsync(CreateKoiOrderDto orderDto)
        {
            var newOrder = _mapper.Map<KoiOrder>(orderDto);
            newOrder.Status = OrderStatus.WaitingForPayment.ToString();
            var pricing = _context.Pricings.FirstOrDefault(x => x.PriceId == newOrder.PricingId);
            newOrder.Total = pricing.PricePerKg * newOrder.Weight;
            await _context.KoiOrders.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<KoiOrderDto>(newOrder);
            return ApiResponse<KoiOrderDto>.SuccessResponse(responseDto, "Order created successfully.");
        }

        public async Task<ApiResponse<KoiOrderDto>> UpdateKoiOrderAsync(int orderId, UpdateKoiOrderDto updateOrderDto)
        {
            var order = await _context.KoiOrders.FindAsync(orderId);
            if (order == null)
            {
                return ApiResponse<KoiOrderDto>.ErrorResponse("Order not found.");
            }

            _mapper.Map(updateOrderDto, order);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<KoiOrderDto>(order);
            return ApiResponse<KoiOrderDto>.SuccessResponse(responseDto, "Order updated successfully.");
        }


        public async Task<ApiResponse<KoiOrderDto>> GetOrderDetailsAsync(int orderId)
        {
            var order = await _context.KoiOrders.Include(o => o.Customer)
                                                .Include(o => o.Pricing)
                                                .FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                return ApiResponse<KoiOrderDto>.ErrorResponse("Order not found.");
            }

            var responseDto = _mapper.Map<KoiOrderDto>(order);
            return ApiResponse<KoiOrderDto>.SuccessResponse(responseDto, "Order details retrieved successfully.");
        }

        public async Task<ApiResponse<List<KoiOrderDto>>> GetOrdersByFiltersAsync(
                    string? status = null,
                    string? transportMethod = null,
                    string? searchTerm = null,
                    string sortBy = "PlacedDate",
                    bool isDescending = true,
                    int pageNumber = 1,
                    int pageSize = 10)
        {
            var query = _context.KoiOrders
                .Include(x => x.Customer)
                .OrderByDescending(o => o.OrderId)
                .Include(x => x.Pricing)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }

            if (!string.IsNullOrEmpty(transportMethod))
            {
                query = query.Where(o => o.TransportMethod == transportMethod);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o => o.PickupLocation.Contains(searchTerm) || o.Destination.Contains(searchTerm));
            }

            query = isDescending
                ? query.OrderByDescending(o => EF.Property<object>(o, sortBy))
                : query.OrderBy(o => EF.Property<object>(o, sortBy));

            var totalRecords = await query.CountAsync();
            var orders = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var orderDtos = _mapper.Map<List<KoiOrderDto>>(orders);
            var response = ApiResponse<List<KoiOrderDto>>.SuccessResponse(orderDtos, "Orders retrieved successfully.");

            response.Pagination = new PaginationDto
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            if (orders.Count == 0)
            {
                return ApiResponse<List<KoiOrderDto>>.ErrorResponse("No orders found with the given filters.");
            }

            return response;
        }


        public async Task<ApiResponse<string>> CancelOrderAsync(int orderId)
        {
            var order = await _context.KoiOrders.FindAsync(orderId);
            if (order == null)
            {
                return ApiResponse<string>.ErrorResponse("Order not found.");
            }

            order.Status = "Canceled";
            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Order canceled successfully.");
        }

        public async Task<ApiResponse<List<KoiOrderDto>>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _context.KoiOrders.Where(o => o.CustomerId == customerId).ToListAsync();

            if (orders.Count == 0)
            {
                return ApiResponse<List<KoiOrderDto>>.ErrorResponse("No orders found for the customer.");
            }

            var orderDtos = _mapper.Map<List<KoiOrderDto>>(orders);
            return ApiResponse<List<KoiOrderDto>>.SuccessResponse(orderDtos, "Orders retrieved successfully.");
        }
        public async Task<ApiResponse<List<KoiOrderDto>>> GetCompletedOrdersByCustomerIdAsync(int customerId)
        {
            var completedOrders = await _context.KoiOrders
                .Where(o => o.CustomerId == customerId && o.Status == "Completed")
                .ToListAsync();

            if (completedOrders.Count == 0)
            {
                return ApiResponse<List<KoiOrderDto>>.ErrorResponse("No completed orders found for the customer.");
            }

            var orderDtos = _mapper.Map<List<KoiOrderDto>>(completedOrders);
            return ApiResponse<List<KoiOrderDto>>.SuccessResponse(orderDtos, "Completed orders retrieved successfully.");
        }
        public async Task<ApiResponse<List<KoiOrderDto>>> GetOrdersPendingHealthCheckAsync()
        {
            var pendingOrders = await _context.KoiOrders
                .Where(o => o.Status == "Pending Health Check")
                .ToListAsync();

            if (pendingOrders.Count == 0)
            {
                return ApiResponse<List<KoiOrderDto>>.ErrorResponse("No orders pending health check.");
            }

            var orderDtos = _mapper.Map<List<KoiOrderDto>>(pendingOrders);
            return ApiResponse<List<KoiOrderDto>>.SuccessResponse(orderDtos, "Orders pending health check retrieved successfully.");
        }
        public async Task<ApiResponse<string>> UpdateHealthStatusAsync(int orderId, string healthStatus, List<HealthCertificateDto> certificates)
        {
            var order = await _context.KoiOrders.Include(o => o.OrderDocuments)
                                                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return ApiResponse<string>.ErrorResponse("Order not found.");
            }

            order.Status = healthStatus;

            foreach (var certificate in certificates)
            {
                var document = new OrderDocument
                {
                    OrderId = orderId,
                    DocumentType = certificate.CertificateType,
                    DocumentPath = certificate.CertificatePath,
                    CreatedAt = DateTime.UtcNow
                };
                await _context.OrderDocuments.AddAsync(document);
            }

            await _context.SaveChangesAsync();
            return ApiResponse<string>.SuccessResponse("Health status and certificates updated successfully.");
        }
        public async Task<ApiResponse<string>> UpdateTransportProcessAsync(int orderId, string step, string status)
        {
            var order = await _context.KoiOrders.Include(o => o.TransportProcesses)
                                                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return ApiResponse<string>.ErrorResponse("Order not found.");
            }

            var transportProcess = new TransportProcess
            {
                OrderId = orderId,
                Step = step,
                Status = status,
                ProcessDate = DateTime.UtcNow
            };

            await _context.TransportProcesses.AddAsync(transportProcess);
            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Transport process updated successfully.");
        }
        public async Task<ApiResponse<string>> DeleteKoiOrderAsync(int orderId)
        {
            var order = await _context.KoiOrders.FindAsync(orderId);

            if (order == null)
            {
                return ApiResponse<string>.ErrorResponse("Order not found.");
            }

            _context.KoiOrders.Remove(order);
            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Order deleted successfully.");
        }
        public async Task<ApiResponse<List<EnumDto>>> GetOrderStatusListAsync()
        {
            var orderStatuses = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(status => new EnumDto
                {
                    Value = (int)status,
                    Name = status.ToString()
                })
                .ToList();

            return ApiResponse<List<EnumDto>>.SuccessResponse(orderStatuses, "Order statuses retrieved successfully.");
        }

        public async Task<ApiResponse<KoiOrderDto>> UpdateOrderStatus(int orderId, string orderStatus)
        {
            try
            {
                using (var context = new KoiFishTransportationDBContext())
                {
                    var orderUpdate = await context.KoiOrders.FirstOrDefaultAsync(x => x.OrderId == orderId);
                    if (orderUpdate == null)
                    {
                        return ApiResponse<KoiOrderDto>.ErrorResponse("Order not found.");
                    }

                    orderUpdate.Status = orderStatus;
                    await context.SaveChangesAsync();

                    var responseDto = _mapper.Map<KoiOrderDto>(orderUpdate);
                    return ApiResponse<KoiOrderDto>.SuccessResponse(responseDto, "Order status updated successfully.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse<KoiOrderDto>.ErrorResponse($"An error occurred while updating order status: {ex.Message}");
            }
        }



        public async Task<ApiResponse<List<KoiOrderDto>>> GetOrdersByCustomerIdAsync(
            int customerId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.KoiOrders
                .Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderId)
                .AsQueryable();

            var totalRecords = await query.CountAsync();

            var orders = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (!orders.Any())
            {
                return ApiResponse<List<KoiOrderDto>>.ErrorResponse("No orders found for the customer.");
            }

            var orderDtos = _mapper.Map<List<KoiOrderDto>>(orders);

            var response = ApiResponse<List<KoiOrderDto>>.SuccessResponse(orderDtos, "Orders retrieved successfully.");
            response.Pagination = new PaginationDto
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            return response;
        }

    }
}
