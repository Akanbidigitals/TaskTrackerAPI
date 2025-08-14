using TaskTrackerAPI.Domain.Models;

namespace TaskTrackerAPI.Domain.DTOs;

public class RegisterUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}