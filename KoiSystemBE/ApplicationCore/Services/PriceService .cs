using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PriceService : IPriceService
    {
        private readonly KoiFishTransportationDBContext _context;
        private readonly IMapper _mapper;

        public PriceService(KoiFishTransportationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<PriceDto>>> GetPricesByFiltersAsync(string? transportMethod = null, decimal? weight = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var query = _context.Pricings.AsQueryable();

            if (!string.IsNullOrEmpty(transportMethod))
            {
                query = query.Where(p => p.TransportMethod == transportMethod);
            }

            if (weight.HasValue)
            {
                query = query.Where(p => p.WeightRange.Contains(weight.Value.ToString()));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.PricePerKg >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.PricePerKg <= maxPrice.Value);
            }

            var prices = await query.ToListAsync();

            if (!prices.Any())
            {
                return ApiResponse<List<PriceDto>>.ErrorResponse("No prices found with the given filters.");
            }

            var priceDtos = _mapper.Map<List<PriceDto>>(prices);
            return ApiResponse<List<PriceDto>>.SuccessResponse(priceDtos, "Prices retrieved successfully.");
        }

        public async Task<ApiResponse<string>> UpdatePriceAsync(int priceId, UpdatePriceDto updatePriceDto)
        {
            var price = await _context.Pricings.FirstOrDefaultAsync(p => p.PriceId == priceId);

            if (price == null)
            {
                return ApiResponse<string>.ErrorResponse("Price not found.");
            }

            price.TransportMethod = updatePriceDto.TransportMethod;
            price.WeightRange = updatePriceDto.WeightRange;
            price.PricePerKg = updatePriceDto.PricePerKg;
            price.AdditionalServicePrice = updatePriceDto.AdditionalServicePrice;

            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Price updated successfully.");
        }

        public async Task<ApiResponse<List<string>>> GetAvailableTransportMethodsAsync()
        {
            var transportMethods = await _context.Pricings
                .Select(p => p.TransportMethod)
                .Distinct()
                .ToListAsync();

            if (!transportMethods.Any())
            {
                return ApiResponse<List<string>>.ErrorResponse("No transport methods found.");
            }

            return ApiResponse<List<string>>.SuccessResponse(transportMethods, "Transport methods retrieved successfully.");
        }
    }
}
