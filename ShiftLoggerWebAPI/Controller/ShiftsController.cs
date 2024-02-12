using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftLoggerWebAPI.Models;
using ShiftLoggerWebAPI.Services;

namespace ShiftLoggerWebAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController:ControllerBase
{
    
    private readonly ShiftService _shiftService;
    public ShiftsController(ShiftService shiftService)
    {
        _shiftService = shiftService ?? throw new ArgumentNullException(nameof(shiftService));
    }
    /// <summary>
    /// Gets a list of shifts.
    /// </summary>
    // GET: api/shifts
    [HttpGet]
    public ActionResult<IEnumerable<Shift>> GetShifts()
    {
        var shifts = _shiftService.GetAllShifts();
        return Ok(shifts);
    }
    
    /// <summary>
    /// Gets shift by Id.
    /// </summary>

    [HttpGet("{id}")]
    public ActionResult<Shift> GetShiftById(int id)
    {
        var shift = _shiftService.GetShiftById(id);

        if (shift == null)
        {
            return NotFound(); // Return 404 if shift with the specified id is not found
        }

        return Ok(shift);
    }
    
    /// <summary>
    /// Add a shift to a existing worker.
    /// </summary>

    // POST: api/shifts
    [HttpPost]
    public ActionResult<ShiftDto> CreateShift([FromBody] ShiftDto shiftDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return 400 if the provided shift data is not valid
        }
        
        var shiftEntity = _shiftService.MapShiftDtoToEntity(shiftDto);

        _shiftService.AddShift(shiftEntity);
        
        var createdShiftDto = _shiftService.MapShiftEntityToDto(shiftEntity);

        return CreatedAtAction(nameof(GetShiftById), new { id = createdShiftDto.ShiftId }, createdShiftDto);
    }
    
    /// <summary>
    /// Update a shift of an existing worker
    /// </summary>
    // PUT: api/shifts/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateShift(int id, [FromBody] ShiftDto updatedShiftDto)
    {
        try
        {
            _shiftService.UpdateShift(id, updatedShiftDto);
            return NoContent(); // Return 204 for successful update
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message); // Return 500 for unexpected errors
        }
    }
    
    /// <summary>
    /// Delete a shift of a existing worker.
    /// </summary>
    // DELETE: api/shifts/{id}
    [HttpDelete("{id}")]
    public ActionResult<Shift> DeleteShift(int id)
    {
        var shift = _shiftService.GetShiftById(id);

        if (shift == null)
        {
            return NotFound(); // Return 404 if shift with the specified id is not found
        }

        _shiftService.DeleteShift(id);

        return Ok(shift);
    }
}