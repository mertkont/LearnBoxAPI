using DataAccess.Models;

namespace Business.Contracts;

public interface IStatService
{
    List<CategoryStats> GetCategoryStats();
}