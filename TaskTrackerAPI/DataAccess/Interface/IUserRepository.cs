using TaskTrackerAPI.Domain.DTOs;
using TaskTrackerAPI.Domain.Models;

namespace TaskTrackerAPI.DataAccess.Interface;

public interface IUserRepository
{
    Task<string> RegisterAsync(RegisterUserDto registerUserDto);
    Task<string> LoginAsync(LoginDTO loginDto);
}