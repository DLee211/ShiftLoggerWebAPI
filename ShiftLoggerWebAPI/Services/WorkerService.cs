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

    public WorkerDto GetWorkerById(int workerId)
    {
        var workerEntity = _dbContext.Workers
            .FirstOrDefault(worker => worker.WorkerId == workerId);

        if (workerEntity == null)
        {
            return null;
        }
        
        var workerDto = new WorkerDto
        {
            WorkerId = workerEntity.WorkerId,
            FirstName = workerEntity.FirstName,
            LastName = workerEntity.LastName
            // Add any other properties you need to map
        };

        return workerDto;
    }

    public void DeleteWorker(int workerId)
    {
        var workerEntity = _dbContext.Workers
            .FirstOrDefault(worker => worker.WorkerId == workerId);
        
        if (workerEntity == null)
        {
            throw new ArgumentException($"Worker with ID {workerId} not found.");
        }

        _dbContext.Workers.Remove(workerEntity);
        _dbContext.SaveChanges();
    }
}