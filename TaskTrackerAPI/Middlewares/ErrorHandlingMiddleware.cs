using TaskTrackerAPI.Exceptions;

namespace TaskTrackerAPI.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (AlreadyExistException alreadyExistException)
        {
            _logger.LogError(alreadyExistException.Message);
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync(alreadyExistException.Message);
            
        }
        catch (NotFoundException notFoundException)
        {
            _logger.LogError(notFoundException.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(notFoundException.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Something went wrong");
        }  
    }
}