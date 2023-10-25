using DataAccess.DTOs;
using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface ITodoDal
{
    List<TodoDetailsDto> GetAllTodos();
    TodoDetailsDto GetTodoById(int todoId);
    List<TodoDetailsDto> GetCompletedTodos();
    List<TodoDetailsDto> GetUncompletedTodos();
    List<Todo> GetOverdueTodos();
    void AddTodo(Todo todo);
    void UpdateTodo(Todo todo);
    void DeleteTodo(int todoId);
}