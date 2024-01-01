using MediatR;
using net08apibasics.Features.NewProject.Entities;
using net08apibasics.Features.NewProject.Exceptions;

public class GetProductRequst : IRequest<Project>
{
    public string Id { get; set; }
}
public class GetProductRequstHandler
    : IRequestHandler<GetProductRequst, Project>
{
    public async Task<Project> Handle(GetProductRequst request, CancellationToken cancellationToken)
    {
        if (request.Id == "111")
        {
            throw new ProjectNotFoundException();
        }
        var p = await Task.FromResult(new Project() { Id = request.Id, Name = "Test product" });
        return p;
    }
}
