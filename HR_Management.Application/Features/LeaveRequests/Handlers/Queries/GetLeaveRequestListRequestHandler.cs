using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Queries;

public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository , IMapper mapper)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        
    }
    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
    {
        var leaveRequestList = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
        return _mapper.Map<List<LeaveRequestListDto>>(leaveRequestList);
    }
}
