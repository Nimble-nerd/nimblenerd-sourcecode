using MediatR;
using Microsoft.AspNetCore.Mvc;
using net08apibasics.Features.NewProject.Commands;
using net08apibasics.Features.NewProject.Entities;

namespace net08apibasics.Features.NewProject;

public static class ProductModule
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPost("/Product", async ([FromBody] Project product, IMediator mediator) =>
            {
                var result = await mediator.Send(new ProjectCommand() { Name = product.Name });
                return Results.Ok(result);

            })
            .WithName("CreateProduct")
            .WithOpenApi();

        endpointRouteBuilder
            .MapGet("/Product/{id}",
                async (IMediator mediator, [AsParameters] GetProductRequst request)
                    => await mediator.Send(request))
            .WithName("GetProductById")
            .WithOpenApi();
    }
}