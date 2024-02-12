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

    /// <summary>
    ///  Delete worker by Id
    /// </summary>
    /// <returns></returns>
    // DELETE: api/workers/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteWorker(int id)
    {
        try
        {
            _workerService.DeleteWorker(id);
            return NoContent(); // Return 204 for successful deletion
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message); // Return 404 if worker with the specified id is not found
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 for other unexpected errors
        }
    }
    /// <summary>
    ///  Update worker
    /// </summary>
    /// <returns></returns>
    // PUT: api/workers/{id}/name
    [HttpPut("{id}/name")]
    public IActionResult UpdateWorkerName(int id, [FromBody] WorkerDto model)
    {
        try
        {
            _workerService.UpdateWorkerName(id, model.FirstName, model.LastName);
            return NoContent(); // Return 204 for successful update
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message); // Return 404 if worker with the specified id is not found
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 for other unexpected errors
        }
    }
}