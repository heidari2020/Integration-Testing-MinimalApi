using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces;
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Project project, CancellationToken cancellationToken);
        void Update(Project project);
        void Delete(Project project);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }