using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Exeptions;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository , IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeDtoValidator();

        var validatorResult = await validator.ValidateAsync(request.LeaveTypeDto);

        if (validatorResult.IsValid == false)
        {
            throw new ValidationExeption(validatorResult);
        }


        var leaveType = await _leaveTypeRepository.Get(request.LeaveTypeDto.Id);
        _mapper.Map(request.LeaveTypeDto,leaveType);
        await _leaveTypeRepository.Update(leaveType);

        return Unit.Value;
    }
}
