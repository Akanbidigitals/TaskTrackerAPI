using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackerAPI.DataAccess.Interface;
using TaskTrackerAPI.Domain.DTOs;
using TaskTrackerAPI.Utility;

namespace TaskTrackerAPI.Controllers;
[Route("api/tasktracker")]
[ApiController]
public class TaskTrackerController : Controller
{
   private readonly ITaskRepository  _taskRepository;
   private readonly IUserRepository _userRepository;

   public TaskTrackerController(ITaskRepository taskRepository , IUserRepository userRepository)
   {
      _taskRepository = taskRepository;
      _userRepository = userRepository;
   }

   [HttpPost("addTask")]
   public async Task<IActionResult> AddTask(CreateTaskDTO createTaskDTO)
   {
      var response = await _taskRepository.AddTaskAsync(createTaskDTO);
      return Ok(response);
   }

   [HttpPost("updateTask")]
   public async Task<IActionResult> UpdateTask(UpdateTaskDTO updateTaskDTO)
   {
      var response = await _taskRepository.UpdateTaskByTitle(updateTaskDTO);
      return Ok(response);
   }

   [HttpGet("getTaskbyTitle/{title}")]
   public async Task<IActionResult> GetTaskbyTitle([FromRoute]string title)
   {
      var response = await _taskRepository.GetTaskByTitleAsync(title);
      return Ok(response);
   }

   [HttpGet("getAllTasks")]
   public async Task<ActionResult<List<Task>>> GetAllTasks()
   {
      var  response = await _taskRepository.GetAllTasksAsync();
      return Ok(response);
   }

   [HttpDelete("deleteTaskbyTitle/{title}")]
   public async Task<IActionResult> DeleteTask([FromRoute]string title)
   {
      var response = await _taskRepository.DeleteTaskAsync(title);
      return Ok(response);
   }
   [HttpGet("/reports/completion")]
   [Authorize(Roles = PoliciesConstant.Manager)] // Added Role for this endpoint
   public async Task<IActionResult> GetTaskCompletionReport()
   {
      var response = await _taskRepository.TaskReportAsync();
      return Ok(response);
   }
   [HttpPost("/register")]
   public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
   {
      var response = await _userRepository.RegisterAsync(registerUserDto);
      return Ok(response);
   }

   [HttpPost("/login")]
   public async Task<IActionResult> Login(LoginDTO loginDto)
   {
      var response = await _userRepository.LoginAsync(loginDto);
      return Ok(response);
   }
   
}