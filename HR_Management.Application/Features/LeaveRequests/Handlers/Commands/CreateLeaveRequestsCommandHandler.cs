using AutoMapper;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Persistence.Contract;
using HR_Management.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestsCommandHandler : IRequestHandler<CreateLeaveRequestsCommand, int>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    public CreateLeaveRequestsCommandHandler(ILeaveRequestRepository leaveRequestRepository , IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateLeaveRequestsCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
        leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
        return leaveRequest.Id;
    }
}
