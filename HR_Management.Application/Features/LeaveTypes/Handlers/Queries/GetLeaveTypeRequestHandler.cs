using AutoMapper;
using HR_Management.Application.DTOs;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.Application.Persistence.Contract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeRequestHandler : IRequestHandler<GetLeaveTypeRequest, LeaveTypeDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypeRequestHandler(ILeaveTypeRepository leaveTypeRepository , IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    public async Task<LeaveTypeDto> Handle(GetLeaveTypeRequest request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepository.Get(request.Id);
        return _mapper.Map<LeaveTypeDto>(leaveType);
    }
}
