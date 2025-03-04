using FluentValidation;
using HR_Management.Application.Persistence.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    public CreateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        Include(new ILeaveRequestDtoValidator(leaveTypeRepository));
       
    }
}
