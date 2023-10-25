using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.Data;

public class EfStatDal : IStatDal
{
    private TodoContext _context;

    public EfStatDal(TodoContext context)
    {
        _context = context;
    }
    
    public List<CategoryStats> GetCategoryStats()
    {
        using(var context = new TodoContext())
        {
           var bookStats = context.Books.Join(context.Categories, b => b.CategoryId, c => c.CategoryId,
                   (b, c) => new { Book = b, Category = c }).GroupBy(x => x.Category.CategoryName)
               .Select(g => new CategoryStats
               {
                   Category = g.Key,
                   BookCount = g.Count()
               }).ToList();

            var todoStats = context.Todos.Join(context.Categories, t => t.CategoryId, c => c.CategoryId,
                    (t, c) => new { Todo = t, Category = c }).GroupBy(x => x.Category.CategoryName)
                .Select(g => new { Category = g.Key, TodoCount = g.Count()}).ToList();

            var result = bookStats.GroupJoin(todoStats,
                bs => bs.Category,  
                ts => ts.Category,
                (bs, todoGroup) => new CategoryStats {
                    Category = bs.Category,
                    BookCount = bs.BookCount,
                    TodoCount = todoStats.Count()
                }
            ).ToList();
  
            return result;
        }
    }
}