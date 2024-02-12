using Microsoft.AspNetCore.Mvc;
using ShiftLoggerWebAPI.Models;
using ShiftLoggerWebAPI.Services;

namespace ShiftLoggerWebAPI.Controller;
[ApiController]
[Route("api/[controller]")]
public class WorkersController:ControllerBase
{
    private readonly WorkerService _workerService;
    
    public WorkersController(WorkerService workerService)
    {
        _workerService = workerService ?? throw new ArgumentNullException(nameof(workerService));
    }
    
    /// <summary>
    /// Gets a list of Workers.
    /// </summary>
    // GET: api/Worker
    [HttpGet]
    public ActionResult<IEnumerable<Worker>> GetWorkers()
    {
        var workers = _workerService.GetAllWorkers();
        return Ok(workers);
    }
    
    /// <summary>
    /// Gets worker by Id.
    /// </summary>

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftById(int id)
    {
        var shift = _workerService.GetWorkerById(id);
        
        return Ok(shift);
    }
}