namespace DataAccess.Models;

public class Todo
{
    public int TodoId { get; set; }
    public string TodoTopic { get; set; }
    public string TodoText { get; set; }
    public int CategoryId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public bool IsCompleted { get; set; }
}