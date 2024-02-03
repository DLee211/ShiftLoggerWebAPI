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

    public IEnumerable<Shift> GetAllShifts()
    {
        return _dbContext.Shifts.ToList();
    }

    public Shift GetShiftById(int shiftId)
    {
        return _dbContext.Shifts.Find(shiftId);
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
}