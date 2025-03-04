using FluentValidation;
using HR_Management.Application.Persistence.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.DTOs.LeaveAllocation.Validators;

public class UpdateLeaveAllocationDtoValidator : AbstractValidator<UpdateLeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public UpdateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        Include(new ILeaveAllocationDtoValidator(_leaveTypeRepository));
        RuleFor(c => c.Id)
            .NotNull().WithMessage("{PropertyName} is required.");
    }
}
