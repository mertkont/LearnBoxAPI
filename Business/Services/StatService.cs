using Business.Contracts;
using DataAccess.Interfaces;
using DataAccess.Models;

namespace Business.Services;

public class StatService : IStatService
{
    private IStatDal _statDal;

    public StatService(IStatDal statDal)
    {
        _statDal = statDal;
    }
    
    public List<CategoryStats> GetCategoryStats()
    {
        return _statDal.GetCategoryStats();
    }
}