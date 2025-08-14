using FluentValidation;
using TaskTrackerAPI.Domain.DTOs;

namespace TaskTrackerAPI.Domain.Validators;

public class CreateTaskValidation: AbstractValidator<CreateTaskDTO>
{
    public CreateTaskValidation()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long");
        
        RuleFor(x => x.Description).NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Description must be at least 5 characters long");
        
        RuleFor(t => t.DueDate) 
            .GreaterThan(DateTime.UtcNow).WithMessage("Due date must be in the future.");
        
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status must be either Pending, InProgress, or Completed.");
    }
}