using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.Exeptions;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;

    }
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepository.Get(request.Id);
        if (leaveAllocation == null)
        {
            throw new NotFoundExeption(nameof(LeaveAllocation), request.Id);
        }
        await _leaveAllocationRepository.Delete(leaveAllocation);

        return Unit.Value;
    }
}
