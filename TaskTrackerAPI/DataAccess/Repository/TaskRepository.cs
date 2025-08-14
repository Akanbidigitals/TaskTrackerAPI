using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.DataAccess.DataContext;
using TaskTrackerAPI.DataAccess.Interface;
using TaskTrackerAPI.Domain.DTOs;
using TaskTrackerAPI.Domain.Models;
using TaskTrackerAPI.Exceptions;
using Task = TaskTrackerAPI.Domain.Models.Task;

namespace TaskTrackerAPI.DataAccess.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<TaskRepository> _logger;

    public TaskRepository(ApplicationDbContext dbContext,  ILogger<TaskRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<string> AddTaskAsync(CreateTaskDTO createTaskDto)
    {
        _logger.LogInformation("Adding new task");
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == createTaskDto.AssignedToUserId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var addTask = new Task()
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            DueDate = createTaskDto.DueDate,
            AssignToId = user.Id,
            Status = createTaskDto.Status,
        };
        await _dbContext.AddAsync(addTask);
        await _dbContext.SaveChangesAsync();
        return "Task successfully added";
    }

    public async Task<Task> GetTaskByTitleAsync(string title)
    {
        _logger.LogInformation("Getting task by title");
        var getTask = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Title == title);
        if (getTask == null) throw new NotFoundException("Task not found");
        
        return getTask;
    }

    public async Task<List<Task>> GetAllTasksAsync()
    {
        _logger.LogInformation("Getting all tasks");
        var tasks = await _dbContext.Tasks.ToListAsync();
        return tasks;
    }

    public async Task<string> DeleteTaskAsync(string title)
    {
        _logger.LogInformation($"Deleting task {title}");
        var deleteTask = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Title == title);
        if (deleteTask == null) throw new NotFoundException("Task not found");
        _dbContext.Remove(deleteTask);
        await _dbContext.SaveChangesAsync();
        return "Task successfully deleted";
    }

    public async Task<string> UpdateTaskByTitle(UpdateTaskDTO updateTaskDto)
    {
        _logger.LogInformation("Updating task by title");
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Title == updateTaskDto.Title);
        if(task == null) throw new NotFoundException("Task not found");
       
        task.Id = task.Id;
        task.Title = !string.IsNullOrEmpty(updateTaskDto.Title) ? updateTaskDto.Title : task.Title; 
        task.Description = !string.IsNullOrEmpty(updateTaskDto.Description) ? updateTaskDto.Description : task.Description;
        task.DueDate = updateTaskDto.DueDate.HasValue ? updateTaskDto.DueDate.Value : task.DueDate;
        task.AssignToId = updateTaskDto.AssignedToUserId.HasValue ? updateTaskDto.AssignedToUserId.Value : task.AssignToId;
        task.Status = updateTaskDto.Status.HasValue ? updateTaskDto.Status.Value : task.Status;
        
        _dbContext.Update(task);
        await _dbContext.SaveChangesAsync();
        return "Task successfully updated";
    }

    public async Task<TaskCompletionReportDto> TaskReportAsync()
    {
        var taskTotal = await _dbContext.Tasks.CountAsync();
        var taskCompleted = await _dbContext.Tasks.CountAsync(x => x.Status == StatusOfTask.Completed);

        double completionRate = 0;
        if (taskTotal > 0)
        {
            completionRate = (taskCompleted * 100.0) / taskTotal; //percentage of completion
        }

        return new TaskCompletionReportDto
        {
            TotalTasks = taskTotal,
            CompletedTasks = taskCompleted,
            CompletionRate = Math.Round(completionRate , 2)// 2 d.P
        };
    }
}