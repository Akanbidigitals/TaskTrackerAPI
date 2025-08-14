namespace TaskTrackerAPI.Domain.Models;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public StatusOfTask Status { get; set; } = StatusOfTask.Pending;
    public Guid AssignToId { get; set; }
    public User AssignTo { get; set; }

}

