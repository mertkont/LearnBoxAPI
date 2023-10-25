using DataAccess.Interfaces;

namespace DataAccess.Models;

public class CategoryStats : IStat
{
    public string Category { get; set; }
    public int BookCount { get; set; }
    public int TodoCount { get; set; }
}