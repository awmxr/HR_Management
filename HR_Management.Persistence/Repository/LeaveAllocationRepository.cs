using HR_Management.Application.Contract.Persistence;
using HR_Management.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Persistence.Repository;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    private readonly LeaveManagementDbContext _dbContext;
    public LeaveAllocationRepository(LeaveManagementDbContext dbContext)
        :base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await _dbContext.LeaveAllocations.Include(c=> c.LeaveType).ToListAsync();
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await _dbContext.LeaveAllocations.Include(c => c.LeaveType).FirstOrDefaultAsync(c => c.Id == id);
    }
}
