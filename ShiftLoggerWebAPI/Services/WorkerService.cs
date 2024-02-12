using Microsoft.EntityFrameworkCore;
using ShiftLoggerWebAPI.Models;

namespace ShiftLoggerWebAPI.Services;

public class WorkerService
{
    private readonly ShiftDbContext _dbContext;

    public WorkerService(ShiftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<WorkerDto> GetAllWorkers()
    {
        return _dbContext.Workers
            .Select(worker => new WorkerDto()
            {
                WorkerId = worker.WorkerId,
                FirstName = worker.FirstName,
                LastName = worker.LastName
            })
            .ToList();
    } 
}