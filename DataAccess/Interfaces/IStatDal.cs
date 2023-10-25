using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IStatDal
{
    List<CategoryStats> GetCategoryStats();
}