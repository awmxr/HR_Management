using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation.Validators;
using HR_Management.Application.Exeptions;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository , IMapper mapper , ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        
    }
    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request.CreateLeaveAllocationDto);

        if(validationResult.IsValid == false)
        {
            throw new ValidationExeption(validationResult);
        }

        var leaveAllocation = _mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDto);
        leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
        return leaveAllocation.Id;
    }
}
