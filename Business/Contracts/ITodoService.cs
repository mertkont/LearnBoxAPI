using DataAccess.DTOs;
using DataAccess.Models;

namespace Business.Contracts;

public interface ITodoService
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