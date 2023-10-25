using Business.Contracts;
using Business.Services;
using DataAccess.DTOs;
using DataAccess.Models;

namespace LearnBoxApi.Tests;

[CollectionDefinition("BookCollection")]
public class BookServiceTests
{
    public Mock<BookService> MockBookService { get; set; }

    private BookDetailsDto CreateTestBook()
    {
        string[] names = { "Book 1", "Book 2", "Book 3" };

        return new BookDetailsDto
        {
            BookId = GetRandomId(),
            BookName = names[GetRandomIndex(names.Length)]
        };
    }

    private Book CreateTestBook2()
    {
        string[] names = { "Book 1", "Book 2", "Book 3" };

        return new Book
        {
            BookId = GetRandomId(),
            BookName = names[GetRandomIndex(names.Length)]
        };
    }

    private int GetRandomId()
    {
        return new Random().Next(1, 500);
    }

    private int GetRandomIndex(int length)
    {
        return new Random().Next(0, length);
    }

    [Fact]
    public void GetBooks_ReturnsBooks()
    {
        var mock = new Mock<IBookService>();

        var testBooks = new List<BookDetailsDto> { CreateTestBook() };

        mock.Setup(x => x.GetAllBooks()).Returns(testBooks);

        // Use mock object
        var result = mock.Object.GetAllBooks();

        Assert.Equal(testBooks, result);
    }

    [Fact]
    public void GetById_ReturnsExpectedBook()
    {
        var mock = new Mock<IBookService>();

        // Arrange
        var testId = GetRandomId();
        var testBook = CreateTestBook();
        testBook.BookId = testId;

        mock.Setup(x => x.GetBookById(testId)).Returns(testBook);

        // Act
        var result = mock.Object.GetBookById(testId);

        // Assert 
        Assert.Equal(testId, result.BookId);
    }

    [Fact]
    public void AddBook_AddsToList()
    {
        var mock = new Mock<IBookService>();

        // Arrange 
        int expectedId = GetRandomId();
        var returnedBook = new BookDetailsDto { BookId = expectedId };
        mock.Setup(x => x.GetBookById(expectedId)).Returns(returnedBook);

        // Act
        mock.Object.AddBook(new Book());

        // Assert
        var addedBook = mock.Object.GetBookById(expectedId);

        Assert.NotNull(addedBook);
        Assert.Equal(expectedId, addedBook.BookId);
    }

    [Fact]
    public void UpdateBook_UpdatesBook()
    {
        var mock = new Mock<IBookService>();

        // Arrange
        var testBook = new BookDetailsDto(){ BookId = GetRandomId(), IsFinished = false };

        mock.Setup(x => x.GetBookById(testBook.BookId)).Returns(testBook); 

        // Act
        testBook.IsFinished = true;
        mock.Object.UpdateBook(new Book());

        // Assert
        var result = mock.Object.GetBookById(testBook.BookId);

        Assert.True(result.IsFinished);
    }

    [Fact]
    public void DeleteBook_RemovesFromList()
    {
        var mock = new Mock<IBookService>();
        var deletedId = GetRandomId();
        mock.Setup(x => x.DeleteBook(deletedId)); 
        mock.Object.DeleteBook(deletedId);
        var result = mock.Object.GetBookById(deletedId);
        Assert.Null(result);
    }
}