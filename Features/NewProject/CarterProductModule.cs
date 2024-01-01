using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using net08apibasics.Features.NewProject.Commands;
using net08apibasics.Features.NewProject.Entities;

namespace net08apibasics.Features.NewProject;

public class CarterProductModule : CarterModule
{
    public CarterProductModule()
        : base("/product") { }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] ProjectCommand product, IMediator mediator) =>
            {
                var result = await mediator.Send(product);
                return Results.Ok(result);

            })
            .Accepts<Project>("application/json")
            .Produces(201)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("CreateProduct")
            .WithTags("CreateProduct")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary",
                Description = "This is a description"
            })
            .AddFluentValidationAutoValidation();

        app.MapGet("/{id}",
                async (IMediator mediator, [AsParameters] GetProductRequst request)
                    => await mediator.Send(request)
            )
            .WithName("GetProductById")
            .WithTags("GetProductById")
            .WithOpenApi(operation => new(operation)
            {
                Summary = "This is a summary-1",
                Description = "This is a description-1"
            })
            .AddFluentValidationAutoValidation();
    }
}