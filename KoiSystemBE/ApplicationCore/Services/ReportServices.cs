using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ReportServices : IReportService
    {
        private readonly KoiFishTransportationDBContext _context;

        public ReportServices(KoiFishTransportationDBContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ReportDto>> GetSummaryReportAsync()
        {
            var totalRevenue = await _context.KoiOrders
                .Where(o => o.Status == "Completed")
                .SumAsync(o => o.Weight * o.Quantity);

            var totalOrders = await _context.KoiOrders.CountAsync();
            var completedOrders = await _context.KoiOrders.CountAsync(o => o.Status == "Completed");
            var pendingOrders = await _context.KoiOrders.CountAsync(o => o.Status == "Pending");

            var feedbacks = await _context.Feedbacks.ToListAsync();
            var totalFeedbacks = feedbacks.Count;
            var averageRating = totalFeedbacks > 0 ? feedbacks.Average(f => f.Rating) : 0;

            var report = new ReportDto
            {
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                CompletedOrders = completedOrders,
                PendingOrders = pendingOrders,
                AverageFeedbackRating = (decimal)averageRating,
                TotalFeedbacks = totalFeedbacks
            };

            return ApiResponse<ReportDto>.SuccessResponse(report, "Summary report generated successfully.");
        }

    }
}
