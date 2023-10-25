using Business.Contracts;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace Business.Services;

public class CategoryService : ICategoryService
{
    private ICategoryDal _categoryDal;

    public CategoryService(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }
    
    public List<Category> GetAllCategories()
    {
        return _categoryDal.GetAllCategories();
    }

    public Category GetCategoryById(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentException("Unacceptable categoryId");
        }
        
        return _categoryDal.GetCategoryById(categoryId);
    }

    public void AddCategory(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category), "Category cannot be null");
        }
        
        _categoryDal.AddCategory(category);
    }

    public void UpdateCategory(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category), "Category cannot be null");
        }
        
        _categoryDal.UpdateCategory(category);
    }

    public void DeleteCategory(int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new ArgumentException("Category cannot be null");
        }
        
        _categoryDal.DeleteCategory(categoryId);
    }
}