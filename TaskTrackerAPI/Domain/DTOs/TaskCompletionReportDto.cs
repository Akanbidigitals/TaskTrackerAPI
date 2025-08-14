namespace TaskTrackerAPI.Domain.DTOs;

public class TaskCompletionReportDto
{
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public double CompletionRate { get; set; }
}