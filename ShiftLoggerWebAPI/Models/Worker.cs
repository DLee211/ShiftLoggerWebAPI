using System.ComponentModel.DataAnnotations;

namespace ShiftLoggerWebAPI.Models;

public class Worker
{
    public int WorkerId { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    // Add other worker details as needed, such as contact information, etc.
    
    public ICollection<Shift> Shifts { get; set; }
}
