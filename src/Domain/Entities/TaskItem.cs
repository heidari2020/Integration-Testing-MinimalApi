using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Domain.Entities;
public class TaskItem
{ 
    private TaskItem() { }

    public TaskItem(string title, string description, Guid projectId, DateTime? dueDate = null)
    {
        Id = Guid.NewGuid();
        SetTitle(title);
        SetDescription(description);
        SetProjectId(projectId);
        SetDueDate(dueDate);
        CreatedDate = DateTime.UtcNow;
        Status = TaskStatus.NotStarted;
    }
     
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public TaskStatus Status { get; private set; }
    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; } // Navigation Property
     
    public void UpdateDetails(string title, string description, DateTime? dueDate)
    {
        SetTitle(title);
        SetDescription(description);
        SetDueDate(dueDate);
    }

    public void ChangeStatus(TaskStatus newStatus)
    { 
        if (Status == TaskStatus.Completed && newStatus != TaskStatus.Completed)
            throw new InvalidOperationException("The task has already been completed and cannot be changed.");

        Status = newStatus;
    }

     
    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty..", nameof(title));
        Title = title.Trim();
    }

    private void SetDescription(string description)
    {
        Description = description?.Trim() ?? string.Empty;
    }

    private void SetProjectId(Guid projectId)
    {
        if (projectId == Guid.Empty)
            throw new ArgumentException("The project Id is not valid..", nameof(projectId));
        ProjectId = projectId;
    }

    private void SetDueDate(DateTime? dueDate)
    {
        if (dueDate.HasValue && dueDate.Value < DateTime.UtcNow.Date)
            throw new ArgumentException("The due date cannot be in the past..", nameof(dueDate));
        DueDate = dueDate;
    }
}