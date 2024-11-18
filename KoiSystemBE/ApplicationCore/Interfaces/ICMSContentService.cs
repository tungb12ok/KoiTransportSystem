using ApplicationCore.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICMSContentService
    {
        Task<ApiResponse<List<CMSContentDto>>> GetArticlesByFiltersAsync(
            string contentType = null,
            string searchTerm = null,
            string sortBy = "CreatedAt",
            bool isDescending = true,
            int pageNumber = 1,
            int pageSize = 10);

        Task<ApiResponse<CMSContentDto>> AddContentAsync(CreateCMSContentDto contentDto);
        Task<ApiResponse<CMSContentDto>> UpdateContentAsync(int contentId, UpdateCMSContentDto updateContentDto);
        Task<ApiResponse<CMSContentDto>> DeleteContentAsync(int contentId);
    }
}
