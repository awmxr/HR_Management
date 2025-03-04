using FluentValidation;
using HR_Management.Application.Contract.Persistence;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));
        RuleFor(c => c.Id)
            .NotNull().WithMessage("{PropertyName} is required.");
    }
}
