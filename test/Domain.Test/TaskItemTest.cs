using Domain;
using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using RsjFramework.Entities;
using Domain.Enums;
using TaskStatus = Domain.Enums.TaskStatus;
namespace Test;
public class TaskItemTest
{
    [Fact]
    public void CreateTask_WithValidParameters_ShouldInitializeCorrectly()
    {
        // Arrange
        var project = new Project("Test", "Desc", Priority.Medium);
        var title = "Implement Authentication";
        var desc = "Add JWT authentication to the API";
        var dueDate = DateTime.UtcNow.AddDays(5);

        // Act
        var task = new TaskItem(title, desc, project.Id, dueDate);

        // Assert
        task.Id.Should().NotBeEmpty();
        task.Title.Should().Be(title);
        task.Description.Should().Be(desc);
        task.ProjectId.Should().Be(project.Id);
        task.DueDate.Should().Be(dueDate);
        task.Status.Should().Be(TaskStatus.NotStarted);
    }

    [Fact]
    public void ChangeStatus_ShouldUpdateTaskStatus()
    { 
        var project = new Project("Test", "Desc", Priority.Medium);
        var task = new TaskItem("Task", "Desc", project.Id);
        var newStatus = TaskStatus.InProgress;
         
        task.ChangeStatus(newStatus);
         
        task.Status.Should().Be(newStatus);
    }

    [Fact]
    public void ChangeStatus_WhenTaskAlreadyCompleted_ShouldThrowInvalidOperationException()
    { 
        var project = new Project("Test", "Desc", Priority.Medium);
        var task = new TaskItem("Task", "Desc", project.Id);
        task.ChangeStatus(TaskStatus.Completed);
         
        var act = () => task.ChangeStatus(TaskStatus.InProgress);
         
        act.Should().Throw<InvalidOperationException>().WithMessage("*already been completed*");
    }
}