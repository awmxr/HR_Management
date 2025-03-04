using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, LeaveRequestDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepository , IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        
    }
    public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
    {
        var leaveRequestDetail = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        return _mapper.Map<LeaveRequestDto>(leaveRequestDetail);
    }
}
