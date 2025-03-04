using HR_Management.Application.Persistence.Contract;
using HR_Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Persistence.Repository;

public class LeaveTypeRepository : GenericRepository<LeaveType> , ILeaveTypeRepository
{
    private readonly LeaveManagementDbContext _dbContext;
    public LeaveTypeRepository(LeaveManagementDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
