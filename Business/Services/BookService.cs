using Business.Contracts;
using DataAccess.DTOs;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace Business.Services;

public class BookService : IBookService
{
    private IBookDal _bookDal;

    public BookService(IBookDal bookDal)
    {
        _bookDal = bookDal;
    }

    public List<BookDetailsDto> GetAllBooks()
    {
        return _bookDal.GetAllBooks();
    }

    public BookDetailsDto GetBookById(int bookId)
    {
        if (bookId <= 0)
        {
            throw new ArgumentException("Unacceptable bookId");
        }

        return _bookDal.GetBookById(bookId);
    }

    public List<BookDetailsDto> GetCompletedBooks()
    {
        return _bookDal.GetCompletedBooks();
    }

    public List<BookDetailsDto> GetUncompletedBooks()
    {
        return _bookDal.GetUncompletedBooks();
    }

    public List<Book> GetOverdueBooks()
    {
        return _bookDal.GetOverdueBooks();
    }

    public void AddBook(Book book)
    {
        if (book == null)
        {
            throw new ArgumentNullException(nameof(book), "Book cannot be null");
        }
        
        _bookDal.AddBook(book);
    }

    public void UpdateBook(Book book)
    {
        if (book == null)
        {
            throw new ArgumentNullException(nameof(book), "Book cannot be null");
        }
        
        _bookDal.UpdateBook(book);
    }

    public void DeleteBook(int bookId)
    {
        if (bookId <= 0)
        {
            throw new ArgumentException("Book cannot be null");
        }
        
        _bookDal.DeleteBook(bookId);
    }
}