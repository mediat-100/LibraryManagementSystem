using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.Contracts.IRepository
{
    public interface IBookRepository
    {
        Task<Book?> GetBook(long bookId);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<bool> DeleteBook(long bookId);
        IEnumerable<Book> SearchBooks(string? title, string? author, string? isbn, DateTime? publishedDate);
    }
}
