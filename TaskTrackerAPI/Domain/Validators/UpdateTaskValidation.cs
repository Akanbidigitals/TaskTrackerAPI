using FluentValidation;
using TaskTrackerAPI.Domain.DTOs;

namespace TaskTrackerAPI.Domain.Validators;

public class UpdateTaskValidation : AbstractValidator<UpdateTaskDTO>
{
    public UpdateTaskValidation()
    {
        
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long");
        
        RuleFor(x => x.Description).NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Description must be at least 5 characters long");
        
        RuleFor(t => t.DueDate) 
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Due date must Today's date or future date.");
        
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status must be either Pending, InProgress, or Completed.");
    }
}