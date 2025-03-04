using HR_Management.Application.Contract.Persistence;
using HR_Management.Domain;

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
