using Business.Contracts;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CategoriesController : ControllerBase
{
    private ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }
    
    [HttpGet("GetAllCategories")]
    public ActionResult<IEnumerable<Category>> GetAllCategories()
    {
        var categories = _service.GetAllCategories();
        return Ok(categories);
    }

    [HttpGet("GetCategoryById")]
    public ActionResult<Category> GetCategoryById(int categoryId)
    {
        var category = _service.GetCategoryById(categoryId);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost("AddCategory")]
    public ActionResult<Category> CreateCategory([FromBody] Category category)
    {
        if (category == null)
        {
            return BadRequest("Unacceptable category data");
        }

        _service.AddCategory(category);
        return CreatedAtAction(nameof(GetCategoryById), new { categoryId = category.CategoryId }, category);
    }

    [HttpPut("UpdateCategory")]
    public IActionResult UpdateCategory(int categoryId, [FromBody] Category category)
    {
        if (category == null || category.CategoryId != categoryId)
        {
            return BadRequest("Unacceptable category data");
        }

        _service.UpdateCategory(category);
        return NoContent();
    }

    [HttpDelete("DeleteCategory")]
    public IActionResult DeleteCategory(int categoryId)
    {
        _service.DeleteCategory(categoryId);
        return NoContent();
    }
}