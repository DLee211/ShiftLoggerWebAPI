using System.ComponentModel.DataAnnotations;

namespace ShiftLoggerWebAPI.Models;

public class Shift
{
    public int ShiftId { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }

    // Other shift-related properties can be added, like location, description, etc.
    
    public int WorkerId { get; set; }
    public Worker Worker { get; set; }
}

public class ShiftDto
{
    public int ShiftId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int WorkerId { get; set; }
    public WorkerDto Worker { get; set; }
}