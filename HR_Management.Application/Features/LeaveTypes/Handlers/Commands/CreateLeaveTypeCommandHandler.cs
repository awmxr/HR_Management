using AutoMapper;
using HR_Management.Application.Contract.Persistence;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Exeptions;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Responses;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IMapper _mapper;
    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository , IMapper mapper)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _mapper = mapper;
    }
    public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateLeaveTypeDtoValidator();

        var validationResult = await validator.ValidateAsync(request.CreateLeaveTypeDto);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Errors = validationResult.Errors.Select(c=> c.ErrorMessage).ToList();
            response.Message = "Creataion Faild!";

        }
        else
        {
            var leaveType = _mapper.Map<LeaveType>(request.CreateLeaveTypeDto);
            leaveType = await _leaveTypeRepository.Add(leaveType);
            response.Success = true;
            response.Message = "Creataion Successfull";
            response.Id = leaveType.Id;
            //leaveType.Id;
        }

        return response;
        
        
    }
}
