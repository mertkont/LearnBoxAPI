using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class EfCategoryDal : ICategoryDal
{
    private TodoContext _context;

    public EfCategoryDal(TodoContext context)
    {
        _context = context;
    }

    public List<Category> GetAllCategories()
    {
        return _context.Categories.ToList();
    }

    public Category GetCategoryById(int categoryId)
    {
        return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
    }

    public void AddCategory(Category category)
    {
        _context.Add(category);
        _context.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteCategory(int categoryId)
    {
        var category = _context.Categories.Find(categoryId);
        if (categoryId != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}