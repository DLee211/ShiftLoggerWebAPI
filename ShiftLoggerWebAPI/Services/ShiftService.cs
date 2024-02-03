using Microsoft.EntityFrameworkCore;
using ShiftLoggerWebAPI.Models;

namespace ShiftLoggerWebAPI.Services;

public class ShiftService
{
    private readonly ShiftDbContext _dbContext;

    public ShiftService(ShiftDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    //CRUD

    public IEnumerable<ShiftDto> GetAllShifts()
    {
        return _dbContext.Shifts.Include(s => s.Worker)
            .Select(s => new ShiftDto
            {
                ShiftId = s.ShiftId,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                WorkerId = s.WorkerId,
                Worker = new WorkerDto
                {
                    WorkerId = s.WorkerId,
                    FirstName = s.Worker.FirstName,
                    LastName = s.Worker.LastName
                }
            })
            .ToList();
            
    }

    public ShiftDto  GetShiftById(int shiftId)
    {
        var shiftEntity = _dbContext.Shifts
            .Include(s => s.Worker)
            .FirstOrDefault(s => s.ShiftId == shiftId);
        
        if (shiftEntity == null)
        {
            return null; // Or handle the null case based on your requirements
        }

        // Map the Shift entity to ShiftDto
        var shiftDto = new ShiftDto
        {
            ShiftId = shiftEntity.ShiftId,
            StartTime = shiftEntity.StartTime,
            EndTime = shiftEntity.EndTime,
            WorkerId = shiftEntity.WorkerId,
            Worker = new WorkerDto
            {
                WorkerId = shiftEntity.Worker?.WorkerId ?? 0, // Ensure not null
                FirstName = shiftEntity.Worker?.FirstName,
                LastName = shiftEntity.Worker?.LastName
            }
        };

        return shiftDto;
        
    }

    public void AddShift(Shift shift)
    {
        _dbContext.Shifts.Add(shift);
        _dbContext.SaveChanges();
    }
    
    public void UpdateShift(Shift shift)
    {
        _dbContext.Entry(shift).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }
    
    public void DeleteShift(int shiftId)
    {
        var shiftToDelete = _dbContext.Shifts.Find(shiftId);
        if (shiftToDelete != null)
        {
            _dbContext.Shifts.Remove(shiftToDelete);
            _dbContext.SaveChanges();
        }
    }
    
    public TimeSpan CalculateShiftDuration(Shift shift)
    {
        return shift.EndTime - shift.StartTime;
    }
    
    public bool ShiftExists(int shiftId)
    {
        return _dbContext.Shifts.Any(s => s.ShiftId == shiftId);
    }
}