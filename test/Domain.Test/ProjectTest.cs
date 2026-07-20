using Domain.Entities;
using Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Test;
public class ProjectTest
{
    [Fact]
    public void AddTask_ShouldAddTaskToProject()
    {
        // Arrange
        var project = new Project("Test", "Desc", Priority.Medium);
        var task = new TaskItem("Task 1", "Task Desc", project.Id);

        // Act
        project.AddTask(task);

        // Assert
        project.Tasks.Should().Contain(task);
        project.Tasks.Should().HaveCount(1);

    }
    [Fact]
    public void AddDuplicateTask_ShouldThrowInvalidOperationException()
    { 
        var project = new Project("Test", "Desc", Priority.Medium);
        var task = new TaskItem("Task", "Desc", project.Id);
        project.AddTask(task);
 
        var act = () => project.AddTask(task);
         
        act.Should().Throw<InvalidOperationException>().WithMessage("*This task has already been added to the project*");
    }

    [Fact]
    public void MarkAsCompleted_ShouldDeactivateProject()
    { 
        var project = new Project("Test", "Desc", Priority.Medium);

         
        project.MarkAsCompleted();

         
        project.IsActive.Should().BeFalse();
    }
}