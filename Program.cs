using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Routing;
using net08apibasics.Features.NewProject.Commands;
using net08apibasics.GlobalExceptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddCarter();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// builder.Services.AddScoped<IValidator<ProjectCommand>, ProjectValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

/// 'Organic' approach provided by the .net core.
//app.MapProductEndpoints();

app.MapCarter();

app.Run();


//1. Global exception handling with problem-details
//2. Carter module for setting up routes
//3. Auto-mapper with auto-mapper profile to DTOs
//4. Fluent validation
//5. Result pattern for response generation