using DataAccess.DTOs;
using DataAccess.Models;

namespace Business.Contracts;

public interface IBookService
{
    List<BookDetailsDto> GetAllBooks();
    BookDetailsDto GetBookById(int bookId);
    List<BookDetailsDto> GetCompletedBooks();
    List<BookDetailsDto> GetUncompletedBooks();
    List<Book> GetOverdueBooks();
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(int bookId);
}