using AutoMapper;
using HR_Management.Application.DTOs;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Application.Persistence.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Queries;

public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, LeaveAllocationDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    public GetLeaveAllocationDetailRequestHandler(ILeaveAllocationRepository leaveAllocationRepository , IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        
    }
    public async Task<LeaveAllocationDto> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
    {
        var leaveAllocationDetail = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
        return _mapper.Map<LeaveAllocationDto>(leaveAllocationDetail);
    }
}
