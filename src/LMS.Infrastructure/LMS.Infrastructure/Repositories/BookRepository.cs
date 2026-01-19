using LMS.Application.Contracts.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using LMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Book> AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            return book;
        }

        public async Task<bool> DeleteBook(long bookId)
        {
            var existingBook = await GetBook(bookId);
            if (existingBook == null)
                return false;

            existingBook.RecordStatus = Domain.Enums.RecordStatus.Deleted;
            _dbContext.Update(existingBook);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Book?> GetBook(long bookId)
        {
            var existingBook = await _dbContext.Books.Where(x => x.Id == bookId && x.RecordStatus == RecordStatus.Active).FirstOrDefaultAsync();
            if (existingBook == null)
                return null;

            return existingBook;
        }

        public IEnumerable<Book> SearchBooks(string? title, string? author, string? isbn, DateTime? publishedDate)
        {
            IQueryable<Book> query = _dbContext.Books;

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(x => x.Title == title);

            if (!string.IsNullOrWhiteSpace(author))
                query = query.Where(x => x.Author == author);

            if (!string.IsNullOrWhiteSpace(isbn))
                query = query.Where(x => x.ISBN == isbn);

            if (publishedDate != null)
                query = query.Where( x => x.PublishedDate == publishedDate);

            query = query.Where(x => x.RecordStatus == Domain.Enums.RecordStatus.Active);

            return query.AsEnumerable();
        }

        public async Task<Book> UpdateBook(Book book)
        {
            var existingBook = await GetBook(book.Id);
            if (existingBook == null)
                return book;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;
            existingBook.PublishedDate = book.PublishedDate;

            _dbContext.Update(existingBook);
            await _dbContext.SaveChangesAsync();

            return existingBook;
        }
    }
}
