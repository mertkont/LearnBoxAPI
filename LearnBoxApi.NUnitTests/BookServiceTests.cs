using Business.Services;
using DataAccess.DTOs;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace LearnBoxApi.NUnitTests;

[TestFixture]
public class BookServiceTests
{
    private Mock<IBookDal> _mockBookDal = null!;
    private BookService _bookService = null!;

    [SetUp]
    public void SetUp()
    {
        _mockBookDal = new Mock<IBookDal>();
        _bookService = new BookService(_mockBookDal.Object);
    }

    [Test]
    public void GetAllBooks_ReturnsAllBooks()
    {
        var expected = new List<BookDetailsDto>
        {
            new BookDetailsDto { BookId = 1, BookName = "Book 1" },
            new BookDetailsDto { BookId = 2, BookName = "Book 2" }
        };
        _mockBookDal.Setup(d => d.GetAllBooks()).Returns(expected);

        var result = _bookService.GetAllBooks();

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result, Is.EqualTo(expected));
        _mockBookDal.Verify(d => d.GetAllBooks(), Times.Once);
    }

    [Test]
    public void GetBookById_ValidId_ReturnsBook()
    {
        var expected = new BookDetailsDto { BookId = 5, BookName = "Test Book" };
        _mockBookDal.Setup(d => d.GetBookById(5)).Returns(expected);

        var result = _bookService.GetBookById(5);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.BookId, Is.EqualTo(5));
        Assert.That(result.BookName, Is.EqualTo("Test Book"));
        _mockBookDal.Verify(d => d.GetBookById(5), Times.Once);
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-100)]
    public void GetBookById_InvalidId_ThrowsArgumentException(int invalidId)
    {
        Assert.Throws<ArgumentException>(() => _bookService.GetBookById(invalidId));
        _mockBookDal.Verify(d => d.GetBookById(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void GetCompletedBooks_ReturnsList()
    {
        var expected = new List<BookDetailsDto>
        {
            new BookDetailsDto { BookId = 1, IsFinished = true }
        };
        _mockBookDal.Setup(d => d.GetCompletedBooks()).Returns(expected);

        var result = _bookService.GetCompletedBooks();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].IsFinished, Is.True);
        _mockBookDal.Verify(d => d.GetCompletedBooks(), Times.Once);
    }

    [Test]
    public void GetUncompletedBooks_ReturnsList()
    {
        var expected = new List<BookDetailsDto>
        {
            new BookDetailsDto { BookId = 1, IsFinished = false }
        };
        _mockBookDal.Setup(d => d.GetUncompletedBooks()).Returns(expected);

        var result = _bookService.GetUncompletedBooks();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].IsFinished, Is.False);
        _mockBookDal.Verify(d => d.GetUncompletedBooks(), Times.Once);
    }

    [Test]
    public void GetOverdueBooks_ReturnsList()
    {
        var expected = new List<Book>
        {
            new Book { BookId = 1, FinishDate = DateTime.Today }
        };
        _mockBookDal.Setup(d => d.GetOverdueBooks()).Returns(expected);

        var result = _bookService.GetOverdueBooks();

        Assert.That(result, Has.Count.EqualTo(1));
        _mockBookDal.Verify(d => d.GetOverdueBooks(), Times.Once);
    }

    [Test]
    public void AddBook_ValidBook_CallsDal()
    {
        var book = new Book { BookName = "New Book", CategoryId = 1 };

        _bookService.AddBook(book);

        _mockBookDal.Verify(d => d.AddBook(book), Times.Once);
    }

    [Test]
    public void AddBook_NullBook_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _bookService.AddBook(null!));
        _mockBookDal.Verify(d => d.AddBook(It.IsAny<Book>()), Times.Never);
    }

    [Test]
    public void UpdateBook_ValidBook_CallsDal()
    {
        var book = new Book { BookId = 1, BookName = "Updated Book" };

        _bookService.UpdateBook(book);

        _mockBookDal.Verify(d => d.UpdateBook(book), Times.Once);
    }

    [Test]
    public void UpdateBook_NullBook_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _bookService.UpdateBook(null!));
        _mockBookDal.Verify(d => d.UpdateBook(It.IsAny<Book>()), Times.Never);
    }

    [Test]
    public void DeleteBook_ValidId_CallsDal()
    {
        _bookService.DeleteBook(5);

        _mockBookDal.Verify(d => d.DeleteBook(5), Times.Once);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void DeleteBook_InvalidId_ThrowsArgumentException(int invalidId)
    {
        Assert.Throws<ArgumentException>(() => _bookService.DeleteBook(invalidId));
        _mockBookDal.Verify(d => d.DeleteBook(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void GetAllBooks_EmptyList_ReturnsEmptyList()
    {
        _mockBookDal.Setup(d => d.GetAllBooks()).Returns(new List<BookDetailsDto>());

        var result = _bookService.GetAllBooks();

        Assert.That(result, Is.Empty);
    }
}
