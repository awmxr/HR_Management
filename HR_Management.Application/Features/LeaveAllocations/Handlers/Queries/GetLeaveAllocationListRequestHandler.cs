using AutoMapper;
using HR_Management.Application.DTOs;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Application.Persistence.Contract;
using HR_Management.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Queries;

public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository , IMapper mapper)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
    }
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
    {
        var leaveAllocationList = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        return _mapper.Map<List<LeaveAllocationDto>>(leaveAllocationList);
    }
}
