using LMS.Application.Contracts.IRepository;
using LMS.Application.Contracts.IService;
using LMS.Application.Dtos.Requests;
using LMS.Application.Dtos.Responses;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using static LMS.Application.Helpers.CustomExceptions;

namespace LMS.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<ApiResponse<BookResponseDto>> AddBook(AddBookRequestDto request)
        {
            // validate input
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ValidationException("Please input book title");

            if (string.IsNullOrWhiteSpace(request.Author))
                throw new ValidationException("Please input book author");

            if (string.IsNullOrWhiteSpace(request.Isbn))
                throw new ValidationException("Please input book isbn");

            var existingBook = SearchBooks(title: request.Title, null, null, null);
            if (existingBook.Data.Data.Count() > 0)
                throw new ValidationException($"Book with title {request.Title.ToUpper()} already exist!");

            var book = new Book()
            {
                Title = request.Title,
                Author = request.Author,
                ISBN = request.Isbn,
                PublishedDate = request.PublishedDate,
                RecordStatus = RecordStatus.Active,
            };

            book = await _bookRepository.AddBook(book);

            var response = MapToBookResponseDto(book);

            return ApiResponse<BookResponseDto>.Ok(response, "Book Added Successfully");
        }

        public async Task<ApiResponse<BookResponseDto>> GetBook(long id)
        {
            var book = await _bookRepository.GetBook(id) ??
                throw new NotFoundException("BookId Not Found!");

            var response = MapToBookResponseDto(book);

            return ApiResponse<BookResponseDto>.Ok(response, "Book Fetched Successfully");
        }

        public async Task<ApiResponse<string>> DeleteBook(long id)
        {
            var book = await _bookRepository.GetBook(id) ??
               throw new NotFoundException("BookId Not Found!");

            var isDeleted = await _bookRepository.DeleteBook(book.Id);
            if (!isDeleted)
                throw new ValidationException("Failed to delete book");

            return ApiResponse<string>.Ok(null, "Book Deleted Successfully");

        }



        public ApiResponse<PagedResponse<BookResponseDto>> SearchBooks(string? title, string? author, string? isbn, DateTime? publishedDate, int page = 1, int pageSize = 10)
        {
            var books = _bookRepository.SearchBooks(title, author, isbn, publishedDate);
            var bookResponseList = books.Select(x => MapToBookResponseDto(x));
            var response = PagedResponse<BookResponseDto>.Create(bookResponseList, page, pageSize);
            return ApiResponse<PagedResponse<BookResponseDto>>.Ok(response, "Books Fetched Successfully");
        }

        public async Task<ApiResponse<BookResponseDto>> UpdateBook(UpdateBookRequestDto request)
        {
            var existingBook = await _bookRepository.GetBook(request.Id) 
                ?? throw new NotFoundException("BookId Not Found!");

            existingBook.Title = request.Title;
            existingBook.Author = request.Author;
            existingBook.ISBN = request.Isbn;
            existingBook.PublishedDate = request.PublishedDate;
            existingBook.RecordStatus = request.RecordStatus;
            existingBook.UpdatedAt = DateTime.UtcNow;

            existingBook = await _bookRepository.UpdateBook(existingBook);

            var response = MapToBookResponseDto(existingBook);

            return ApiResponse<BookResponseDto>.Ok(response, "Book Updated Successfully");
        }

        private BookResponseDto MapToBookResponseDto(Book book)
        {
            return new BookResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.ISBN,
                PublishedDate = book.PublishedDate,
                RecordStatus = book.RecordStatus.ToString(),
                CreatedAt = book.CreatedAt,
                UpdatedAt = book.UpdatedAt,
            };
        }
    }
}
