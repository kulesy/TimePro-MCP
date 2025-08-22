using Microsoft.AspNetCore.Mvc;
using TimePro.Server.Models;
using TimePro.Server.Services;

namespace TimePro.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimesheetsController : ControllerBase
    {
        private readonly ITimesheetService _timesheetService;
        private readonly ILogger<TimesheetsController> _logger;

        public TimesheetsController(ITimesheetService timesheetService, ILogger<TimesheetsController> logger)
        {
            _timesheetService = timesheetService;
            _logger = logger;
        }

        /// <summary>
        /// Get all timesheets
        /// </summary>
        /// <returns>List of all timesheets</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimesheetDto>>> GetTimesheets()
        {
            try
            {
                var timesheets = await _timesheetService.GetAllTimesheetsAsync();
                return Ok(timesheets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving timesheets");
                return StatusCode(500, "An error occurred while retrieving timesheets");
            }
        }

        /// <summary>
        /// Get a specific timesheet by ID
        /// </summary>
        /// <param name="id">Timesheet ID</param>
        /// <returns>Timesheet details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TimesheetDto>> GetTimesheet(int id)
        {
            try
            {
                var timesheet = await _timesheetService.GetTimesheetByIdAsync(id);
                
                if (timesheet == null)
                {
                    return NotFound($"Timesheet with ID {id} not found");
                }

                return Ok(timesheet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving timesheet {Id}", id);
                return StatusCode(500, "An error occurred while retrieving the timesheet");
            }
        }

        /// <summary>
        /// Create a new timesheet
        /// </summary>
        /// <param name="timesheetDto">Timesheet data</param>
        /// <returns>Created timesheet</returns>
        [HttpPost]
        public async Task<ActionResult<TimesheetDto>> CreateTimesheet([FromBody] TimesheetDto timesheetDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdTimesheet = await _timesheetService.CreateTimesheetAsync(timesheetDto);
                
                return CreatedAtAction(
                    nameof(GetTimesheet),
                    new { id = createdTimesheet.Id },
                    createdTimesheet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating timesheet");
                return StatusCode(500, "An error occurred while creating the timesheet");
            }
        }

        /// <summary>
        /// Update an existing timesheet
        /// </summary>
        /// <param name="id">Timesheet ID</param>
        /// <param name="timesheetDto">Updated timesheet data</param>
        /// <returns>Updated timesheet</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TimesheetDto>> UpdateTimesheet(int id, [FromBody] TimesheetDto timesheetDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedTimesheet = await _timesheetService.UpdateTimesheetAsync(id, timesheetDto);
                
                if (updatedTimesheet == null)
                {
                    return NotFound($"Timesheet with ID {id} not found");
                }

                return Ok(updatedTimesheet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating timesheet {Id}", id);
                return StatusCode(500, "An error occurred while updating the timesheet");
            }
        }

        /// <summary>
        /// Delete a timesheet
        /// </summary>
        /// <param name="id">Timesheet ID</param>
        /// <returns>Success status</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTimesheet(int id)
        {
            try
            {
                var success = await _timesheetService.DeleteTimesheetAsync(id);
                
                if (!success)
                {
                    return NotFound($"Timesheet with ID {id} not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting timesheet {Id}", id);
                return StatusCode(500, "An error occurred while deleting the timesheet");
            }
        }
    }
}


