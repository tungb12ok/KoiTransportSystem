using ApplicationCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IPriceService
    {
        Task<ApiResponse<List<PriceDto>>> GetPricesByFiltersAsync(string transportMethod = null, decimal? weight = null, decimal? minPrice = null, decimal? maxPrice = null);
        Task<ApiResponse<string>> UpdatePriceAsync(int priceId, UpdatePriceDto updatePriceDto);
        Task<ApiResponse<List<string>>> GetAvailableTransportMethodsAsync();
    }
}
