using TaskTrackerAPI.Domain.Models;

namespace TaskTrackerAPI.Domain.DTOs;

public class UpdateTaskDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; } 
    public DateTime? DueDate { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public StatusOfTask? Status { get; set; } = StatusOfTask.Pending;
}