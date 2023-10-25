using DataAccess.DTOs;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class EfTodoDal : ITodoDal
{
    private TodoContext _context;
    
    public EfTodoDal(TodoContext context)
    {
        _context = context;
    }

    public List<TodoDetailsDto> GetAllTodos()
    {
        using (TodoContext context = new TodoContext())
        {
            var result = from t in context.Todos
                join c in context.Categories on t.CategoryId equals c.CategoryId
                select new TodoDetailsDto
                {
                    TodoId = t.TodoId,
                    TodoTopic = t.TodoTopic,
                    TodoText = t.TodoText,
                    CategoryName = c.CategoryName,
                    CreatedDate = t.CreatedDate,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    IsCompleted = t.IsCompleted
                };
            return result.OrderBy(t => t.DueDate).ToList();
        }
    }

    public TodoDetailsDto GetTodoById(int todoId)
    {
        using (TodoContext context = new TodoContext())
        {
            var result = from t in context.Todos
                join c in context.Categories on t.CategoryId equals c.CategoryId
                where t.TodoId == todoId
                select new TodoDetailsDto
                {
                    TodoId = t.TodoId,
                    TodoTopic = t.TodoTopic,
                    TodoText = t.TodoText,
                    CategoryName = c.CategoryName,
                    CreatedDate = t.CreatedDate,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    IsCompleted = t.IsCompleted
                };
            return result.FirstOrDefault();
        }
    }

    public List<TodoDetailsDto> GetCompletedTodos()
    {
        using (TodoContext context = new TodoContext())
        {
            var result = from t in context.Todos
                join c in context.Categories on t.CategoryId equals c.CategoryId
                where t.IsCompleted == true
                select new TodoDetailsDto
                {
                    TodoId = t.TodoId,
                    TodoTopic = t.TodoTopic,
                    TodoText = t.TodoText,
                    CategoryName = c.CategoryName,
                    CreatedDate = t.CreatedDate,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    IsCompleted = t.IsCompleted
                };
            return result.ToList();
        }
    }

    public List<TodoDetailsDto> GetUncompletedTodos()
    {
        using (TodoContext context = new TodoContext())
        {
            var result = from t in context.Todos
                join c in context.Categories on t.CategoryId equals c.CategoryId
                where t.IsCompleted == false
                select new TodoDetailsDto
                {
                    TodoId = t.TodoId,
                    TodoTopic = t.TodoTopic,
                    TodoText = t.TodoText,
                    CategoryName = c.CategoryName,
                    CreatedDate = t.CreatedDate,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    IsCompleted = t.IsCompleted
                };
            return result.ToList();
        } 
    }

    public List<Todo> GetOverdueTodos()
    {
        using(var context = new TodoContext())
        {
            return context.Todos.Where(t => t.DueDate == DateTime.Today).ToList();  
        }
    }

    public void AddTodo(Todo todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
    }

    public void UpdateTodo(Todo todo)
    {
        _context.Entry(todo).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteTodo(int todoId)
    {
        var todo = _context.Todos.Find(todoId);
        if (todo != null)
        {
            _context.Todos.Remove(todo);
            _context.SaveChanges();
        }
    }
}