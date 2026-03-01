using Business.Services;
using DataAccess.DTOs;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace LearnBoxApi.NUnitTests;

[TestFixture]
public class TodoServiceTests
{
    private Mock<ITodoDal> _mockTodoDal = null!;
    private TodoService _todoService = null!;

    [SetUp]
    public void SetUp()
    {
        _mockTodoDal = new Mock<ITodoDal>();
        _todoService = new TodoService(_mockTodoDal.Object);
    }

    [Test]
    public void GetAllTodos_ReturnsAllTodos()
    {
        var expected = new List<TodoDetailsDto>
        {
            new TodoDetailsDto { TodoId = 1, TodoTopic = "Topic 1", TodoText = "Text 1" },
            new TodoDetailsDto { TodoId = 2, TodoTopic = "Topic 2", TodoText = "Text 2" }
        };
        _mockTodoDal.Setup(d => d.GetAllTodos()).Returns(expected);

        var result = _todoService.GetAllTodos();

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result, Is.EqualTo(expected));
        _mockTodoDal.Verify(d => d.GetAllTodos(), Times.Once);
    }

    [Test]
    public void GetTodoById_ValidId_ReturnsTodo()
    {
        var expected = new TodoDetailsDto { TodoId = 3, TodoTopic = "Test", TodoText = "Test Text" };
        _mockTodoDal.Setup(d => d.GetTodoById(3)).Returns(expected);

        var result = _todoService.GetTodoById(3);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.TodoId, Is.EqualTo(3));
        Assert.That(result.TodoTopic, Is.EqualTo("Test"));
        _mockTodoDal.Verify(d => d.GetTodoById(3), Times.Once);
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-50)]
    public void GetTodoById_InvalidId_ThrowsArgumentException(int invalidId)
    {
        Assert.Throws<ArgumentException>(() => _todoService.GetTodoById(invalidId));
        _mockTodoDal.Verify(d => d.GetTodoById(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void GetCompletedTodos_ReturnsList()
    {
        var expected = new List<TodoDetailsDto>
        {
            new TodoDetailsDto { TodoId = 1, IsCompleted = true }
        };
        _mockTodoDal.Setup(d => d.GetCompletedTodos()).Returns(expected);

        var result = _todoService.GetCompletedTodos();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].IsCompleted, Is.True);
        _mockTodoDal.Verify(d => d.GetCompletedTodos(), Times.Once);
    }

    [Test]
    public void GetUncompletedTodos_ReturnsList()
    {
        var expected = new List<TodoDetailsDto>
        {
            new TodoDetailsDto { TodoId = 1, IsCompleted = false }
        };
        _mockTodoDal.Setup(d => d.GetUncompletedTodos()).Returns(expected);

        var result = _todoService.GetUncompletedTodos();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].IsCompleted, Is.False);
        _mockTodoDal.Verify(d => d.GetUncompletedTodos(), Times.Once);
    }

    [Test]
    public void GetOverdueTodos_ReturnsList()
    {
        var expected = new List<Todo>
        {
            new Todo { TodoId = 1, DueDate = DateTime.Today }
        };
        _mockTodoDal.Setup(d => d.GetOverdueTodos()).Returns(expected);

        var result = _todoService.GetOverdueTodos();

        Assert.That(result, Has.Count.EqualTo(1));
        _mockTodoDal.Verify(d => d.GetOverdueTodos(), Times.Once);
    }

    [Test]
    public void AddTodo_ValidTodo_CallsDal()
    {
        var todo = new Todo { TodoTopic = "New", TodoText = "New Todo", CategoryId = 1 };

        _todoService.AddTodo(todo);

        _mockTodoDal.Verify(d => d.AddTodo(todo), Times.Once);
    }

    [Test]
    public void AddTodo_NullTodo_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _todoService.AddTodo(null!));
        _mockTodoDal.Verify(d => d.AddTodo(It.IsAny<Todo>()), Times.Never);
    }

    [Test]
    public void UpdateTodo_ValidTodo_CallsDal()
    {
        var todo = new Todo { TodoId = 1, TodoTopic = "Updated" };

        _todoService.UpdateTodo(todo);

        _mockTodoDal.Verify(d => d.UpdateTodo(todo), Times.Once);
    }

    [Test]
    public void UpdateTodo_NullTodo_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _todoService.UpdateTodo(null!));
        _mockTodoDal.Verify(d => d.UpdateTodo(It.IsAny<Todo>()), Times.Never);
    }

    [Test]
    public void DeleteTodo_ValidId_CallsDal()
    {
        _todoService.DeleteTodo(10);

        _mockTodoDal.Verify(d => d.DeleteTodo(10), Times.Once);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void DeleteTodo_InvalidId_ThrowsArgumentException(int invalidId)
    {
        Assert.Throws<ArgumentException>(() => _todoService.DeleteTodo(invalidId));
        _mockTodoDal.Verify(d => d.DeleteTodo(It.IsAny<int>()), Times.Never);
    }

    [Test]
    public void GetAllTodos_EmptyList_ReturnsEmptyList()
    {
        _mockTodoDal.Setup(d => d.GetAllTodos()).Returns(new List<TodoDetailsDto>());

        var result = _todoService.GetAllTodos();

        Assert.That(result, Is.Empty);
    }
}
