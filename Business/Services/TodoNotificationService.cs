using Business.Contracts;

namespace Business.Services;

public class TodoNotificationService : BaseNotificationService
{
    private ITodoService _service;

    public TodoNotificationService(ITodoService service)
    {
        _service = service;
    }

    public override void SendNotification()
    {
        var overdueTodos = _service.GetOverdueTodos();
        foreach (var todo in overdueTodos)
        {
            Console.WriteLine(nameof(todo) + "'s date is finished!");
        }
    }
}