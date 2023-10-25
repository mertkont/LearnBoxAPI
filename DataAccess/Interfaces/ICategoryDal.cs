using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface ICategoryDal
{
    List<Category> GetAllCategories();
    Category GetCategoryById(int categoryId);
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int categoryId);
}