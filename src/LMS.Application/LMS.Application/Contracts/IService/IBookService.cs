using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Dtos.Requests;
using LMS.Application.Dtos.Responses;

namespace LMS.Application.Contracts.IService
{
    public interface IBookService
    {
        ApiResponse<PagedResponse<BookResponseDto>> SearchBooks(string? title, string? author, string? isbn, DateTime? publishedDate, int page = 1, int pageSize = 10);
        Task<ApiResponse<BookResponseDto>> AddBook(AddBookRequestDto request);
        Task<ApiResponse<BookResponseDto>> GetBook(long id);
        Task<ApiResponse<string>> DeleteBook(long id);
        Task<ApiResponse<BookResponseDto>> UpdateBook(UpdateBookRequestDto request);
    }
}
