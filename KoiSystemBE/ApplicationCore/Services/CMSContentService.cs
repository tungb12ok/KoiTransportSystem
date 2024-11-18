using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CMSContentService : ICMSContentService
    {
        private readonly KoiFishTransportationDBContext _context;
        private readonly IMapper _mapper;

        public CMSContentService(KoiFishTransportationDBContext context
            , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Lấy danh sách bài viết với bộ lọc
        public async Task<ApiResponse<List<CMSContentDto>>> GetArticlesByFiltersAsync(
                string? contentType = null,
                string? searchTerm = null,
                string sortBy = "CreatedAt",
                bool isDescending = true,
                int pageNumber = 1,
                int pageSize = 10)
        {
            var query = _context.CmsContents
                .Include(x => x.CreateByNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(contentType))
            {
                query = query.Where(c => c.ContentType == contentType);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.Title.Contains(searchTerm) || c.Content.Contains(searchTerm));
            }

            switch (sortBy.ToLower())
            {
                case "title":
                    query = isDescending ? query.OrderByDescending(c => c.Title) : query.OrderBy(c => c.Title);
                    break;
                case "createdat":
                default:
                    query = isDescending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt);
                    break;
            }

            var totalRecords = await query.CountAsync();
            var articles = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            if (articles.Count == 0)
            {
                return ApiResponse<List<CMSContentDto>>.ErrorResponse("No articles found with the given filters.");
            }

            var articleDtos = _mapper.Map<List<CMSContentDto>>(articles);
            var response = ApiResponse<List<CMSContentDto>>.SuccessResponse(articleDtos, "Articles retrieved successfully.");

            response.Pagination = new PaginationDto
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
            };

            return response;
        }

        // Thêm nội dung
        public async Task<ApiResponse<CMSContentDto>> AddContentAsync(CreateCMSContentDto contentDto)
        {
            try
            {
                var newContent = _mapper.Map<CmsContent>(contentDto);
                await _context.CmsContents.AddAsync(newContent);
                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<CMSContentDto>(newContent);
                return ApiResponse<CMSContentDto>.SuccessResponse(resultDto, "Content added successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<CMSContentDto>.ErrorResponse("Failed to add content: " + ex.Message);
            }
        }

        // Sửa nội dung
        public async Task<ApiResponse<CMSContentDto>> UpdateContentAsync(int contentId, UpdateCMSContentDto updateContentDto)
        {
            try
            {
                var content = await _context.CmsContents.FirstOrDefaultAsync(c => c.ContentId == contentId);
                if (content == null)
                {
                    return ApiResponse<CMSContentDto>.ErrorResponse("Content not found.");
                }

                content.Title = updateContentDto.Title;
                content.Content = updateContentDto.Content;
                content.ContentType = updateContentDto.ContentType;
                content.Image = updateContentDto.Image;
                content.UpdatedAt = updateContentDto.UpdatedAt;

                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<CMSContentDto>(content);
                return ApiResponse<CMSContentDto>.SuccessResponse(resultDto, "Content updated successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<CMSContentDto>.ErrorResponse("Failed to update content: " + ex.Message);
            }
        }

        // Xóa nội dung
        public async Task<ApiResponse<CMSContentDto>> DeleteContentAsync(int contentId)
        {
            try
            {
                var content = await _context.CmsContents.FirstOrDefaultAsync(c => c.ContentId == contentId);
                if (content == null)
                {
                    return ApiResponse<CMSContentDto>.ErrorResponse("Content not found.");
                }

                _context.CmsContents.Remove(content);
                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<CMSContentDto>(content);
                return ApiResponse<CMSContentDto>.SuccessResponse(resultDto, "Content deleted successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<CMSContentDto>.ErrorResponse("Failed to delete content: " + ex.Message);
            }
        }
    }
}
