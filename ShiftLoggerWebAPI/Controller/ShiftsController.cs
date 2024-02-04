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
    // PUT: api/shifts/1
    [HttpPut("{id}")]
    public ActionResult UpdateShift(int id, [FromBody] Shift shift)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return 400 if the provided shift data is not valid
        }

        if (id != shift.ShiftId)
        {
            return BadRequest("Mismatched Ids"); // Return 400 if the provided id in the URL doesn't match the shift id in the body
        }

        try
        {
            _shiftService.UpdateShift(shift);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_shiftService.ShiftExists(id))
            {
                return NotFound(); // Return 404 if the shift with the specified id is not found
            }
            else
            {
                throw;
            }
        }

        return NoContent(); // Return 204 for successful update
    }
    
    /// <summary>
    /// Delete a shift of a existing worker.
    /// </summary>
    // DELETE: api/shifts/1
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