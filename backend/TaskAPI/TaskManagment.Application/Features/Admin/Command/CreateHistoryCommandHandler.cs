using MediatR;
using TaskManagment.Application.Interfaces.Persistence;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Features.Admin;

public class CreateHistoryCommandHandler : IRequestHandler<CreateHistoryCommand, CustomResponse<History>>
{
    private readonly IGenericRepository<History> _historyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHistoryCommandHandler(IGenericRepository<History> historyRepository, IUnitOfWork unitOfWork)
    {
        _historyRepository = historyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse<History>> Handle(CreateHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var history = new History
            {
                UserId = request.UserId,
                MissionId = request.MissionId,
                Action = request.Action,
                ProjectId = request.ProjectId
            };

            _historyRepository.AddAsync(history);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CustomResponse<History>.SuccessResponse(history,"Success"); 
        }
        catch (Exception e)
        {
            return CustomResponse<History>.FailResponse("Hataaa");
        }
    }
}