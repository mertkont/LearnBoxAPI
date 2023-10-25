using DataAccess.Models;

namespace Business.Contracts;

public interface ICategoryService
{
    List<Category> GetAllCategories();
    Category GetCategoryById(int categoryId);
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int categoryId);
}