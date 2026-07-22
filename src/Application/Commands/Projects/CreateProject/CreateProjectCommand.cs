using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Projects.CreateProject;
public class CreateProjectCommand : IRequest<ProjectDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; }
    public DateTime? DueDate { get; set; }
}
