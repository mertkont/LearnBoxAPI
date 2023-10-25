using Business.Contracts;
using DataAccess.DTOs;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TodosController : ControllerBase
{
    private ITodoService _service;

    public TodosController(ITodoService service)
    {
        _service = service;
    }
    
    [HttpGet("GetAllTodos")]
    public ActionResult<IEnumerable<TodoDetailsDto>> GetAllTodos()
    {
        var todos = _service.GetAllTodos();
        return Ok(todos);
    }

    [HttpGet("GetTodoById")]
    public ActionResult<TodoDetailsDto> GetTodoById(int todoId)
    {
        var todo = _service.GetTodoById(todoId);
        if (todo == null)
        {
            return NotFound();
        }
        return Ok(todo);
    }

    [HttpGet("GetCompletedTodos")]
    public ActionResult<List<TodoDetailsDto>> GetCompletedTodos()
    {
        var todos = _service.GetCompletedTodos();
        return Ok(todos);
    }
    
    [HttpGet("GetUncompletedTodos")]
    public ActionResult<List<TodoDetailsDto>> GetUnCompletedTodos()
    {
        var todos = _service.GetUncompletedTodos();
        return Ok(todos);
    }
    
    [HttpPost("AddTodo")]
    public ActionResult<Todo> CreateTodo([FromBody] Todo todo)
    {
        if (todo == null)
        {
            return BadRequest("Unacceptable todo data");
        }

        _service.AddTodo(todo);
        return CreatedAtAction(nameof(GetTodoById), new { todoId = todo.TodoId }, todo);
    }

    [HttpPut("UpdateTodo")]
    public IActionResult UpdateTodo(int todoId, [FromBody] Todo todo)
    {
        if (todo == null || todo.TodoId != todoId)
        {
            return BadRequest("Unacceptable todo data");
        }

        _service.UpdateTodo(todo);
        return NoContent();
    }

    [HttpDelete("DeleteTodo")]
    public IActionResult DeleteTodo(int todoId)
    {
        _service.DeleteTodo(todoId);
        return NoContent();
    }
}