using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TaskManagment.Application.Interfaces.Persistence;

namespace TaskManagment.Application.Features.User;

public class ProjectQueryHandler : IRequestHandler<ProjectQuery, List<ProjectResponse>>
{
    private readonly IGenericRepository<Domain.Entities.Project> _projectRepository;
    private readonly IMapper _mapper;

    public ProjectQueryHandler(
        IGenericRepository<Domain.Entities.Project> projectRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<List<ProjectResponse>> Handle(ProjectQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId > 0 && request.ProjectId == 0)
        {
            var projects = await _projectRepository.ListWithIncludeAsync(
                predicate: p => p.ProjectUsers.Any(x=>x.UserId == request.UserId),
                includes: new Expression<Func<Domain.Entities.Project, object>>[]
                {
                    x => x.Customer,
                    x => x.ProjectUsers,
                    x => x.Missions.Where(ms=>ms.IsDeleted ==false)
                },
                includeStrings: new[] { "ProjectUsers.User" },
                orderBy: q => q.OrderByDescending(p => p.CreatedAt),
                skip: 0,
                take: 10,
                asNoTracking: true
            );

            if (projects == null)
                throw new Exception("Project not found");
 
            return _mapper.Map<List<ProjectResponse>>(projects);
        }
        else
        {
            var projects = await _projectRepository.ListWithIncludeAsync(
                predicate: p => p.ProjectUsers.Any(x=>x.ProjectId == request.ProjectId && x.UserId == request.UserId),
                includes: new Expression<Func<Domain.Entities.Project, object>>[]
                {
                    x => x.Customer,
                    x => x.ProjectUsers,
                    x => x.Missions.Where(ms=>ms.IsDeleted ==false),
                },
                includeStrings: new[] { "ProjectUsers.User","Missions.AssignedUser" },
                orderBy: q => q.OrderByDescending(p => p.CreatedAt),
                skip: 0,
                take: 10,
                asNoTracking: true
            );

            if (projects == null)
                throw new Exception("Project not found");
 
            return _mapper.Map<List<ProjectResponse>>(projects);
            
        }
    }
}
