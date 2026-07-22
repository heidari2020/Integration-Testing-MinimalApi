using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Projects.CreateProject;
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Project name is required.")
            .MaximumLength(200).WithMessage("The project name cannot be longer than 200 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Project priority is required..")
            .Must(x => new[] { "Low", "Medium", "High" }.Contains(x))
            .WithMessage("The priority must be one of the values ​​Low, Medium, or High.");
    }
}