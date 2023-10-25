using Business.Contracts;

namespace Business.Services;

public class BookNotificationService : BaseNotificationService
{
    private IBookService _service;

    public BookNotificationService(IBookService service)
    {
        _service = service;
    }

    public override void SendNotification()
    {
        var overdueBooks = _service.GetOverdueBooks();
        foreach (var book in overdueBooks)
        {
            Console.WriteLine(nameof(book) + "'s finishing date is passed!");
        }
    }
}