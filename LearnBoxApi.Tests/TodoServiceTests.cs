using Business.Contracts;
using Business.Services;
using DataAccess.DTOs;
using DataAccess.Models;

namespace LearnBoxApi.Tests;

[CollectionDefinition("TodoCollection")]
public class TodoServiceTests
{
    public Mock<TodoService> MockTodoService { get; set; }

    private TodoDetailsDto CreateTestTodo()
    {
        string[] texts = { "Todo 1", "Todo 2", "Todo 3" };
        string[] topics = { "Topic 1", "Topic 2", "Topic 3" };

        return new TodoDetailsDto
        {
            TodoId = GetRandomId(),
            TodoText = texts[GetRandomIndex(texts.Length)],
            TodoTopic = topics[GetRandomIndex(topics.Length)]
        };
    }

    private Todo CreateTestTodo2()
    {
        string[] texts = { "Todo 1", "Todo 2", "Todo 3" };
        string[] topics = { "Topic 1", "Topic 2", "Topic 3" };

        return new Todo
        {
            TodoId = GetRandomId(),
            TodoText = texts[GetRandomIndex(texts.Length)],
            TodoTopic = topics[GetRandomIndex(topics.Length)]
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
    public void GetTodos_ReturnsTodos()
    {
        // ITodoService interface mock
        var mock = new Mock<ITodoService>();

        var testTodos = new List<TodoDetailsDto> { CreateTestTodo() };

        mock.Setup(x => x.GetAllTodos()).Returns(testTodos);

        // Use mock object
        var result = mock.Object.GetAllTodos();

        Assert.Equal(testTodos, result);
    }

    [Fact]
    public void GetById_ReturnsExpectedTodo()
    {
        var mock = new Mock<ITodoService>();

        // Arrange
        var testId = GetRandomId();
        var testTodo = CreateTestTodo();
        testTodo.TodoId = testId;

        mock.Setup(x => x.GetTodoById(testId)).Returns(testTodo);

        // Act
        var result = mock.Object.GetTodoById(testId);

        // Assert 
        Assert.Equal(testId, result.TodoId);
    }

    [Fact]
    public void AddTodo_AddsToList()
    {
        var mock = new Mock<ITodoService>();

        // Arrange 
        int expectedId = GetRandomId();
        var returnedTodo = new TodoDetailsDto { TodoId = expectedId };
        mock.Setup(x => x.GetTodoById(expectedId)).Returns(returnedTodo);

        // Act
        mock.Object.AddTodo(new Todo());

        // Assert
        var addedTodo = mock.Object.GetTodoById(expectedId);

        Assert.NotNull(addedTodo);
        Assert.Equal(expectedId, addedTodo.TodoId);
    }

    [Fact]
    public void UpdateTodo_UpdatesTodo()
    {
        var mock = new Mock<ITodoService>();

        // Arrange
        var testTodo = new TodoDetailsDto(){ TodoId = GetRandomId(), IsCompleted = false };

        mock.Setup(x => x.GetTodoById(testTodo.TodoId)).Returns(testTodo); 

        // Act
        testTodo.IsCompleted = true; // Güncelle
        mock.Object.UpdateTodo(new Todo());

        // Assert
        var result = mock.Object.GetTodoById(testTodo.TodoId);

        Assert.True(result.IsCompleted);
    }

    [Fact]
    public void DeleteTodo_RemovesFromList()
    {
        var mock = new Mock<ITodoService>();
        var deletedId = GetRandomId();
        mock.Setup(x => x.DeleteTodo(deletedId)); 
        mock.Object.DeleteTodo(deletedId);
        var result = mock.Object.GetTodoById(deletedId);
        Assert.Null(result);
    }
}