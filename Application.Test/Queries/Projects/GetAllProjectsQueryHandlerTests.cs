using Application.Common.Interfaces;
using Application.DTOs;
using Application.Queries.Projects.GetAllProjects;
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

namespace Application.Test.Queries.Projects;
public class GetAllProjectsQueryHandlerTests
{
    private readonly Mock<IProjectRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetAllProjectsQueryHandler _handler;

    public GetAllProjectsQueryHandlerTests()
    {
        _repositoryMock = new Mock<IProjectRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetAllProjectsQueryHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllProjects()
    {
        // Arrange
        var projects = new List<Project>
        {
            new Project("Project 1", "Desc 1", Priority.Medium),
            new Project("Project 2", "Desc 2", Priority.High)
        };

        var projectDtos = new List<ProjectDto>
        {
            new ProjectDto { Id = projects[0].Id, Name = "Project 1", Priority = "Medium" },
            new ProjectDto { Id = projects[1].Id, Name = "Project 2", Priority = "High" }
        };

        _repositoryMock.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(projects);
        _mapperMock.Setup(x => x.Map<List<ProjectDto>>(It.IsAny<IEnumerable<Project>>()))
            .Returns(projectDtos);

        var query = new GetAllProjectsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result[0].Name.Should().Be("Project 1");
        result[1].Name.Should().Be("Project 2");
        _repositoryMock.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}