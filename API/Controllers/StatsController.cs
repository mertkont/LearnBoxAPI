using Business.Contracts;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StatsController : Controller
{
    private IStatService _statService;

    public StatsController(IStatService statService)
    {
        _statService = statService;
    }
    
    [HttpGet("stats/category")]
    public ActionResult<List<CategoryStats>> GetCategoryStats()
    { 
        var categoryStats = _statService.GetCategoryStats();
        return Ok(categoryStats);
    }
}