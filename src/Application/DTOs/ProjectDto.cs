using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;
    public class ProjectDto
    {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsActive { get; set; }
    public string Priority { get; set; }
    public List<TaskDto> Tasks { get; set; } = new();
    } 
