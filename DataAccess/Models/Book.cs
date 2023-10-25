namespace DataAccess.Models;

public class Book
{
    public int BookId { get; set; }
    public string BookName { get; set; }
    public DateTime BuyDate { get; set; }
    public DateTime FinishDate { get; set; }
    public Priority Priority { get; set; }
    public int CategoryId { get; set; }
    public bool IsFinished { get; set; }
}