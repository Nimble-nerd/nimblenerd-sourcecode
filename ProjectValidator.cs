using FluentValidation;
using net08apibasics.Features.NewProject.Commands;

public class ProjectValidator : AbstractValidator<ProjectCommand> 
{
    public ProjectValidator() 
    {
        RuleFor(x => x.Name).Length(5);
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}