using DataAccess.DTOs;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class EfBookDal : IBookDal
{
    private TodoContext _context;

    public EfBookDal(TodoContext context)
    {
        _context = context;
    }
    
    public List<BookDetailsDto> GetAllBooks()
    {
        using (TodoContext context = new TodoContext())
        {
            var result = from b in context.Books
                join c in context.Categories on b.CategoryId equals c.CategoryId
                select new BookDetailsDto
                {
                    BookId = b.BookId,
                    BookName = b.BookName,
                    BuyDate = b.BuyDate,
                    FinishDate = b.FinishDate,
                    Priority = b.Priority,
                    CategoryName = c.CategoryName,
                    IsFinished = b.IsFinished
                };
            return result.OrderBy(b => b.FinishDate).ToList();
        }
    }

    public BookDetailsDto GetBookById(int bookId)
    {
        using (TodoContext context = new TodoContext())
        {
            var result = from b in context.Books
                join c in context.Categories on b.CategoryId equals c.CategoryId
                where b.BookId == bookId
                select new BookDetailsDto
                {
                    BookId = b.BookId,
                    BookName = b.BookName,
                    BuyDate = b.BuyDate,
                    FinishDate = b.FinishDate,
                    Priority = b.Priority,
                    CategoryName = c.CategoryName,
                    IsFinished = b.IsFinished
                };
            return result.FirstOrDefault();
        }
    }
    
    public List<BookDetailsDto> GetCompletedBooks()
    {
        using (TodoContext context = new TodoContext())
        {
            var result = from b in context.Books
                join c in context.Categories on b.CategoryId equals c.CategoryId
                where b.IsFinished == true
                select new BookDetailsDto
                {
                    BookId = b.BookId,
                    BookName = b.BookName,
                    BuyDate = b.BuyDate,
                    FinishDate = b.FinishDate,
                    Priority = b.Priority,
                    CategoryName = c.CategoryName,
                    IsFinished = b.IsFinished
                };
            return result.ToList();
        }
    }

    public List<BookDetailsDto> GetUncompletedBooks()
    {
        using (TodoContext context = new TodoContext())
        {
            var result = from b in context.Books
                join c in context.Categories on b.CategoryId equals c.CategoryId
                where b.IsFinished == false
                select new BookDetailsDto
                {
                    BookId = b.BookId,
                    BookName = b.BookName,
                    BuyDate = b.BuyDate,
                    FinishDate = b.FinishDate,
                    Priority = b.Priority,
                    CategoryName = c.CategoryName,
                    IsFinished = b.IsFinished
                };
            return result.ToList();
        } 
    }

    public List<Book> GetOverdueBooks()
    {
        using (TodoContext context = new TodoContext())
        {
            return context.Books.Where(b => b.FinishDate == DateTime.Today).ToList();
        }
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public void UpdateBook(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteBook(int bookId)
    {
        var book = _context.Books.Find(bookId);
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}