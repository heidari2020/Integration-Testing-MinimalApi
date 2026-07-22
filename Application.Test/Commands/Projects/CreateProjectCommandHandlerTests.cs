using Application.Commands.Projects.CreateProject;
using Application.Common.Interfaces;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.Commands.Projects;
public class CreateProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateProjectCommandHandler _handler;

    public CreateProjectCommandHandlerTests()
    {
        _repositoryMock = new Mock<IProjectRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new CreateProjectCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateProject()
    {
        // Arrange
        var command = new CreateProjectCommand
        {
            Name = "Test Project",
            Description = "Test Description",
            Priority = "High",
            DueDate = DateTime.UtcNow.AddDays(7)
        };

        var project = new Project(command.Name, command.Description, Priority.High, command.DueDate);
        var projectDto = new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Priority = "High",
            DueDate = project.DueDate,
            IsActive = true
        };

        _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _repositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _mapperMock.Setup(x => x.Map<ProjectDto>(It.IsAny<Project>()))
            .Returns(projectDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(project.Id);
        result.Name.Should().Be(command.Name);
        result.Priority.Should().Be("High");
        _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
