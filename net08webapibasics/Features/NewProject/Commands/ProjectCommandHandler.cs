using MediatR;
using net08apibasics.Features.NewProject.Entities;

namespace net08apibasics.Features.NewProject.Commands;

public class ProjectCommand : IRequest<Project>
{
    public string Id { get; set; }
    public string Name{ get; set; }
}
public class ProjectCommandHandler : IRequestHandler<ProjectCommand, Project>
{
    public async Task<Project> Handle(ProjectCommand request, CancellationToken cancellationToken)
    {
        // var validator = new ProjectValidator();
        // await validator.ValidateAndThrowAsync(request);
        
        request.Id = Guid.NewGuid().ToString();
        return await Task.FromResult(new Project(){ Name = request.Name, Id = request.Id});
    }
}