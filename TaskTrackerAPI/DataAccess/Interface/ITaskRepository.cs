using TaskTrackerAPI.Domain.DTOs;
using Task = TaskTrackerAPI.Domain.Models.Task;

namespace TaskTrackerAPI.DataAccess.Interface;

public interface ITaskRepository
{
    Task<string> AddTaskAsync (CreateTaskDTO  createTaskDto);
    Task<Task> GetTaskByTitleAsync(string title); // we can also have Guid Id in the paramenter
    Task<List<Task>> GetAllTasksAsync();
    Task<string> DeleteTaskAsync(string title); // we can also have Guid Id in the paramenter
    Task<string> UpdateTaskByTitle(UpdateTaskDTO updateTaskDto); // we can also have Guid Id in the paramenter
    Task<TaskCompletionReportDto>TaskReportAsync();
}