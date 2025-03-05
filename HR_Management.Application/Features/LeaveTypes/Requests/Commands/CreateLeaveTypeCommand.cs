using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Commands;

public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
{
    public CreateLeaveTypeDto CreateLeaveTypeDto { get; set; }
}
