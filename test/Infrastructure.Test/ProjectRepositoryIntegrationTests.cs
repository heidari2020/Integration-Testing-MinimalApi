using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure;

public class ProjectRepositoryIntegrationTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly IProjectRepository _repository;

    public ProjectRepositoryIntegrationTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ProjectRepository(_context);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProjectToDatabase()
    {
        var project = new Project("Test", "Desc", Priority.High);

      
        await _repository.AddAsync(project, CancellationToken.None);
        await _repository.SaveChangesAsync(CancellationToken.None);

        var savedProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);
        savedProject.Should().NotBeNull();
        savedProject!.Name.Should().Be("Test");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProjectWithTasks()
    {
        var project = new Project("Test", "Desc", Priority.Medium);
        var task = new TaskItem("Task 1", "Desc", project.Id);
        project.AddTask(task);
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(project.Id, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Test");
        result.Tasks.Should().Contain(t => t.Title == "Task 1");
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}