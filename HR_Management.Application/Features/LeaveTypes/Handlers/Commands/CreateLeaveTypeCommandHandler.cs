using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Exeptions;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository , IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveTypeDtoValidator();

        var validationResult = await validator.ValidateAsync(request.CreateLeaveTypeDto);

        if (validationResult.IsValid == false)
        {
            throw new ValidationExeption(validationResult);
        }

        var leaveType = _mapper.Map<LeaveType>(request.CreateLeaveTypeDto);
        leaveType = await _leaveTypeRepository.Add(leaveType);
        return leaveType.Id;
    }
}
