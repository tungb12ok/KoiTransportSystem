using ApplicationCore.Dto;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class OrderDocumentService : IOrderDocumentService
    {
        private readonly KoiFishTransportationDBContext _context;
        private readonly IMapper _mapper;

        public OrderDocumentService(KoiFishTransportationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<OrderDocumentDto>> CreateOrderDocumentAsync(CreateOrderDocumentDto documentDto)
        {
            var newDocument = _mapper.Map<OrderDocument>(documentDto);

            await _context.OrderDocuments.AddAsync(newDocument);
            await _context.SaveChangesAsync();

            var documentDtoResponse = _mapper.Map<OrderDocumentDto>(newDocument);
            return ApiResponse<OrderDocumentDto>.SuccessResponse(documentDtoResponse, "Order document created successfully.");
        }

        public async Task<ApiResponse<List<OrderDocumentDto>>> GetDocumentsByOrderIdAsync(int orderId)
        {
            var documents = await _context.OrderDocuments
                .Where(d => d.OrderId == orderId)
                .ToListAsync();

            if (!documents.Any())
            {
                return ApiResponse<List<OrderDocumentDto>>.ErrorResponse("No documents found for the given order.");
            }

            var documentsDto = _mapper.Map<List<OrderDocumentDto>>(documents);
            return ApiResponse<List<OrderDocumentDto>>.SuccessResponse(documentsDto, "Documents retrieved successfully.");
        }

        public async Task<ApiResponse<string>> DeleteDocumentAsync(int documentId)
        {
            var document = await _context.OrderDocuments.FindAsync(documentId);

            if (document == null)
            {
                return ApiResponse<string>.ErrorResponse("Document not found.");
            }

            _context.OrderDocuments.Remove(document);
            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("Document deleted successfully.");
        }

        public async Task<ApiResponse<OrderDocumentDto>> UpdateOrderDocumentAsync(UpdateOrderDocumentDto documentDto)
        {
            var existingDocument = await _context.OrderDocuments
                .FirstOrDefaultAsync(d => d.DocumentId == documentDto.DocumentId);

            if (existingDocument == null)
            {
                return ApiResponse<OrderDocumentDto>.ErrorResponse("Document not found.");
            }

            _mapper.Map(documentDto, existingDocument);

            await _context.SaveChangesAsync();

            var updatedDocumentDto = _mapper.Map<OrderDocumentDto>(existingDocument);
            return ApiResponse<OrderDocumentDto>.SuccessResponse(updatedDocumentDto, "Order document updated successfully.");
        }

    }
}
