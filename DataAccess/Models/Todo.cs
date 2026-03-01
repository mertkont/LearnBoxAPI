namespace DataAccess.Models;

public class Todo
{
    public int TodoId { get; set; }
    public string TodoTopic { get; set; } = string.Empty;
    public string TodoText { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public bool IsCompleted { get; set; }
}