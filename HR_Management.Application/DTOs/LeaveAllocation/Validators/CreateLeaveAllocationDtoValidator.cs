using FluentValidation;
using HR_Management.Application.Persistence.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.DTOs.LeaveAllocation.Validators;

public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        Include(new ILeaveAllocationDtoValidator(leaveTypeRepository));
    }
}
