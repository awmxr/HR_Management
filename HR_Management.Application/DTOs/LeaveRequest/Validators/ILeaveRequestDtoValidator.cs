using FluentValidation;
using HR_Management.Application.Persistence.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        RuleFor(p => p.StartDate)
           .LessThan(c => c.EndDate).WithMessage("{PropertyName} must be before {ComparsionName}");
        RuleFor(p => p.EndDate)
            .LessThan(c => c.StartDate).WithMessage("{PropertyName} must be after {ComparsionName}");
        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(async (id, token) =>
            {
                var leaveRequestExist = await _leaveTypeRepository.Exist(id);
                return !leaveRequestExist;
            }).WithMessage("{PropertyName} does not exist.");
    }
}
