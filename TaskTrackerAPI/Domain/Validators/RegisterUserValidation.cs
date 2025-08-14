using FluentValidation;
using TaskTrackerAPI.Domain.DTOs;

namespace TaskTrackerAPI.Domain.Validators;

public class RegisterUserValidation: AbstractValidator<RegisterUserDto>
{
    public RegisterUserValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(15).WithMessage("Username cannot exceed 15 characters.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).+$")
            .WithMessage("Password must contain at least one uppercase letter, one number, and one special character.");
        
        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Role must be either User or Manager.");
    }
}