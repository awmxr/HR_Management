using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.DTOs.LeaveType.Validators;

public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
{
    public UpdateLeaveTypeDtoValidator()
    {
        Include(new ILeaveTypeDtoValidator());
        RuleFor(c => c.Id)
            .NotNull().WithMessage("{PropertyName} is required.");

    }
}
