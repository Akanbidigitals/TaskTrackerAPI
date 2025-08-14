using Microsoft.EntityFrameworkCore;
using TaskTrackerAPI.DataAccess.DataContext;
using TaskTrackerAPI.DataAccess.Interface;
using TaskTrackerAPI.Domain.DTOs;
using TaskTrackerAPI.Domain.Models;
using TaskTrackerAPI.Exceptions;
using TaskTrackerAPI.Utility;

namespace TaskTrackerAPI.DataAccess.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor  _httpContextAccessor;

    public UserRepository(ApplicationDbContext dbContext ,  ILogger<UserRepository> logger, ITokenService tokenService ,  
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext; 
        _logger = logger;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<string> RegisterAsync(RegisterUserDto registerUserDto)
    {
        var user = await _dbContext.Users.AnyAsync(x => x.UserName == registerUserDto.Username);
        if (user)
        {
            throw new AlreadyExistException("Username already exists");
        }
        _logger.LogInformation("Registering new user");
        var newUser = new User
        {
            UserName = registerUserDto.Username,
            PasswordHash = PasswordUtility.HashPassword(registerUserDto.Password),
            Role = registerUserDto.Role
        };
        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
        return "User created";
    }

    public async Task<string> LoginAsync(LoginDTO loginDto)
    {
       
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);

      
        var passwordIsValid = user != null &&
                              PasswordUtility.VerifyHashPassword(loginDto.Password, user.PasswordHash);
        
        if (!passwordIsValid)
        {
            throw new NotFoundException("Username or password is incorrect");
        }
        
        
        var generateAccessToken = _tokenService.GenerateAccessToken(
            user.Id,
            user.UserName,
            user.Role
        );
        
        if (_httpContextAccessor.HttpContext != null)
        {
            _httpContextAccessor.HttpContext.Response.Headers["Authorization"] =
                $"Bearer {generateAccessToken}";
        }

        return "Login successfully";


    }
}