using DataAccess.Interfaces;
using DataAccess.Models;

namespace DataAccess.DTOs;

public class BookDetailsDto : IDto
{
    public int BookId { get; set; }
    public string BookName { get; set; }
    public DateTime BuyDate { get; set; }
    public DateTime FinishDate { get; set; }
    public Priority Priority { get; set; }
    public string PriorityText
    { 
        get
        {
            return Priority.ToString();
        }
    }
    public string CategoryName { get; set; }
    public bool IsFinished { get; set; }
}

public class TodoDetailsDto : IDto
{
    public int TodoId { get; set; }
    public string TodoTopic { get; set; }
    public string TodoText { get; set; }
    public string CategoryName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    public string PriorityText
    { 
        get
        {
            return Priority.ToString();
        }
    }
    public bool IsCompleted { get; set; }
}