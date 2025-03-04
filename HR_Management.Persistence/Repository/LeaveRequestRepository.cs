using HR_Management.Application.Contract.Persistence;
using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repository;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    private readonly LeaveManagementDbContext _dbContext;
    public LeaveRequestRepository(LeaveManagementDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
    {
        leaveRequest.Approval = approvalStatus;
        _dbContext.Entry(leaveRequest).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        return await _dbContext.LeaveRequests.Include(c=> c.LeaveType).ToListAsync();
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        return await _dbContext.LeaveRequests.Include(c => c.LeaveType).FirstOrDefaultAsync(c=> c.Id == id);
    }
}
