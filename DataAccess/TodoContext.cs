using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class TodoContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source=/home/mert/Desktop/Project/LearnBoxAPI/DataAccess/identifier.sqlite";
        //string connectionString = "Data Source=/src/DataAccess/identifier.sqlite";
        optionsBuilder.UseSqlite(connectionString);
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Category> Categories { get; set; }
}