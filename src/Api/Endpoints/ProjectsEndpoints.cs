using Application.Commands.Projects.CreateProject;
using Application.Commands.Projects.DeleteProject;
using Application.Commands.Projects.UpdateProject;
using Application.DTOs;
using Application.Queries.Projects.GetAllProjects;
using Application.Queries.Projects.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Presentation;

public static class ProjectsEndpoints
{
    public static WebApplication MapProjectsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/projects")
            .WithTags("Projects")
            .RequireAuthorization();

        // GET: /api/projects
        group.MapGet("/", async (IMediator mediator) =>
        {
            var query = new GetAllProjectsQuery();
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetAllProjects")
        .WithSummary("Get a list of all projects")
        .Produces<List<ProjectDto>>(StatusCodes.Status200OK);

        // GET: /api/projects/{id}
        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var query = new GetProjectByIdQuery { Id = id };
            var result = await mediator.Send(query);
            return Results.Ok(result);
        })
        .WithName("GetProjectById")
        .WithSummary("Get a project with an Id")
        .Produces<ProjectDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);

        // POST: /api/projects
        group.MapPost("/", async (CreateProjectCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/api/projects/{result.Id}", result);
        })
        .WithName("CreateProject")
        .WithSummary("Create a new project")
        .Produces<ProjectDto>(StatusCodes.Status201Created)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        // PUT: /api/projects/{id}
        group.MapPut("/{id:guid}", async (Guid id, UpdateProjectCommand command, IMediator mediator) =>
        {
            if (id != command.Id)
                return Results.BadRequest(new { error = "The id in the path and request body do not match.." });

            var result = await mediator.Send(command);
            return Results.Ok(result);
        })
        .WithName("UpdateProject")
        .WithSummary("Update Project")
        .Produces<ProjectDto>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);

        // DELETE: /api/projects/{id}
        group.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var command = new DeleteProjectCommand { Id = id };
            await mediator.Send(command);
            return Results.NoContent();
        })
        .WithName("DeleteProject")
        .WithSummary("Delete Project")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }
}