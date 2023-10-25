using Business.Contracts;
using DataAccess.DTOs;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace Business.Services;

public class TodoService : ITodoService
{
    private readonly ITodoDal _todoDal;

    public TodoService(ITodoDal todoDal)
    {
        _todoDal = todoDal;
    }

    public List<TodoDetailsDto> GetAllTodos()
    {
        return _todoDal.GetAllTodos();
    }

    public TodoDetailsDto GetTodoById(int todoId)
    {
        if (todoId <= 0)
        {
            throw new ArgumentException("Unacceptable todoId");
        }

        return _todoDal.GetTodoById(todoId);
    }

    public List<TodoDetailsDto> GetCompletedTodos()
    {
        return _todoDal.GetCompletedTodos();
    }

    public List<TodoDetailsDto> GetUncompletedTodos()
    {
        return _todoDal.GetUncompletedTodos();
    }

    public List<Todo> GetOverdueTodos()
    {
        return _todoDal.GetOverdueTodos();
    }

    public void AddTodo(Todo todo)
    {
        if (todo == null)
        {
            throw new ArgumentNullException(nameof(todo), "Todo cannot be null");
        }
        
        _todoDal.AddTodo(todo);
    }

    public void UpdateTodo(Todo todo)
    {
        if (todo == null)
        {
            throw new ArgumentNullException(nameof(todo), "Todo cannot be null");
        }

        _todoDal.UpdateTodo(todo);
    }

    public void DeleteTodo(int todoId)
    {
        if (todoId <= 0)
        {
            throw new ArgumentException("Unacceptable todoId");
        }
        
        _todoDal.DeleteTodo(todoId);
    }
}