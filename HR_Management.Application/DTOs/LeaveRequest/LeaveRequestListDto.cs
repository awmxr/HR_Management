using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR_Management.Application.DTOs.LeaveType;

namespace HR_Management.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto
    {
        public LeaveTypeDto LeaveType { get; set; }
        public DateTime DateRequest { get; set; }
        public bool? Aproved { get; set; }

    }
}
