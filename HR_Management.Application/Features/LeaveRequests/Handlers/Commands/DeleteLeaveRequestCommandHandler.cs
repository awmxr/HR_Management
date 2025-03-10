﻿using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.Exeptions;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository , IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.Get(request.Id);
        if (leaveRequest == null)
        {
            throw new NotFoundExeption(nameof(LeaveRequest), request.Id);
        }
        await _leaveRequestRepository.Delete(leaveRequest);
        return Unit.Value;
    }
}
