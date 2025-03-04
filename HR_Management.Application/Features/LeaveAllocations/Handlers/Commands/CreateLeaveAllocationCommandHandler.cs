using AutoMapper;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Application.Persistence.Contract;
using HR_Management.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository , IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;   
        
    }
    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = _mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDto);
        leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
        return leaveAllocation.Id;
    }
}
