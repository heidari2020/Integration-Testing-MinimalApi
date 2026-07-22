using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Projects.DeleteProject;
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IProjectRepository _projectRepository;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);
            if (project == null)
                throw new NotFoundException("Project",request.Id);

               _projectRepository.Delete(project);
            await _projectRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
    }