using Domain.ValueObjects; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Project
{ 
    private readonly List<TaskItem> _tasks = new();
 
    private Project() { }

    public Project(string name, string description, Priority priority, DateTime? dueDate = null)
    {
        Id = Guid.NewGuid();
        SetName(name);
        SetDescription(description);
        SetPriority(priority);
        SetDueDate(dueDate);
        CreatedDate = DateTime.UtcNow;
        IsActive = true;
    }
     
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? DueDate { get; private set; }
    public bool IsActive { get; private set; }
    public Priority Priority { get; private set; }
     
    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
     
    public void UpdateDetails(string name, string description, Priority priority, DateTime? dueDate)
    {
        SetName(name);
        SetDescription(description);
        SetPriority(priority);
        SetDueDate(dueDate);
    }

    public void AddTask(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        if (_tasks.Any(t => t.Id == task.Id))
            throw new InvalidOperationException("This task has already been added to the project.");

        _tasks.Add(task);
    }

    public void RemoveTask(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        if (!_tasks.Remove(task))
            throw new InvalidOperationException("The requested task was not found in this project.");
    }

    public void MarkAsActive() => IsActive = true;
    public void MarkAsCompleted() => IsActive = false;
     
    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty.", nameof(name));
        Name = name.Trim();
    }

    private void SetDescription(string description)
    {
        Description = description?.Trim() ?? string.Empty;
    }

    private void SetPriority(Priority priority)
    {
        ArgumentNullException.ThrowIfNull(priority);
        Priority = priority;
    }

    private void SetDueDate(DateTime? dueDate)
    {
        if (dueDate.HasValue && dueDate.Value < DateTime.UtcNow.Date)
            throw new ArgumentException("The due date cannot be in the past.", nameof(dueDate));
        DueDate = dueDate;
    }
}