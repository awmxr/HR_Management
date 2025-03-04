using HR_Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Application.Persistence.Contract;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetails();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(int id);
}
