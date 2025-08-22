using Newtonsoft.Json;
using TimePro.Server.Models;

namespace TimePro.Server.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly string _dataFilePath;
        private readonly ILogger<TimesheetService> _logger;

        public TimesheetService(ILogger<TimesheetService> logger)
        {
            _logger = logger;
            _dataFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "timesheets.json");
            
            // Ensure data directory exists
            var dataDirectory = Path.GetDirectoryName(_dataFilePath);
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory!);
            }
        }

        public async Task<IEnumerable<TimesheetDto>> GetAllTimesheetsAsync()
        {
            try
            {
                var timesheets = await LoadTimesheetsFromFileAsync();
                return timesheets.Select(ConvertToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading timesheets from file");
                return Enumerable.Empty<TimesheetDto>();
            }
        }

        public async Task<TimesheetDto?> GetTimesheetByIdAsync(int id)
        {
            try
            {
                var timesheets = await LoadTimesheetsFromFileAsync();
                var timesheet = timesheets.FirstOrDefault(t => t.Id == id);
                return timesheet != null ? ConvertToDto(timesheet) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading timesheet {Id} from file", id);
                return null;
            }
        }

        public async Task<TimesheetDto> CreateTimesheetAsync(TimesheetDto timesheetDto)
        {
            try
            {
                var timesheets = (await LoadTimesheetsFromFileAsync()).ToList();
                var newTimesheet = ConvertFromDto(timesheetDto);
                newTimesheet.Id = timesheets.Any() ? timesheets.Max(t => t.Id) + 1 : 1;
                
                timesheets.Add(newTimesheet);
                await SaveTimesheetsToFileAsync(timesheets);
                
                return ConvertToDto(newTimesheet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating timesheet");
                throw;
            }
        }

        public async Task<TimesheetDto?> UpdateTimesheetAsync(int id, TimesheetDto timesheetDto)
        {
            try
            {
                var timesheets = (await LoadTimesheetsFromFileAsync()).ToList();
                var existingTimesheet = timesheets.FirstOrDefault(t => t.Id == id);
                
                if (existingTimesheet == null)
                    return null;

                var updatedTimesheet = ConvertFromDto(timesheetDto);
                updatedTimesheet.Id = id;
                
                var index = timesheets.FindIndex(t => t.Id == id);
                timesheets[index] = updatedTimesheet;
                
                await SaveTimesheetsToFileAsync(timesheets);
                
                return ConvertToDto(updatedTimesheet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating timesheet {Id}", id);
                return null;
            }
        }

        public async Task<bool> DeleteTimesheetAsync(int id)
        {
            try
            {
                var timesheets = (await LoadTimesheetsFromFileAsync()).ToList();
                var timesheet = timesheets.FirstOrDefault(t => t.Id == id);
                
                if (timesheet == null)
                    return false;

                timesheets.Remove(timesheet);
                await SaveTimesheetsToFileAsync(timesheets);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting timesheet {Id}", id);
                return false;
            }
        }

        private async Task<IEnumerable<Timesheet>> LoadTimesheetsFromFileAsync()
        {
            if (!File.Exists(_dataFilePath))
            {
                return Enumerable.Empty<Timesheet>();
            }

            var json = await File.ReadAllTextAsync(_dataFilePath);
            return JsonConvert.DeserializeObject<IEnumerable<Timesheet>>(json) ?? Enumerable.Empty<Timesheet>();
        }

        private async Task SaveTimesheetsToFileAsync(IEnumerable<Timesheet> timesheets)
        {
            var json = JsonConvert.SerializeObject(timesheets, Formatting.Indented);
            await File.WriteAllTextAsync(_dataFilePath, json);
        }

        private static TimesheetDto ConvertToDto(Timesheet timesheet)
        {
            return new TimesheetDto
            {
                Id = timesheet.Id,
                Date = timesheet.Date.ToString("yyyy-MM-dd"),
                Project = timesheet.Project,
                Hours = timesheet.Hours,
                Details = timesheet.Details,
                Status = timesheet.Status,
                Client = timesheet.Client
            };
        }

        private static Timesheet ConvertFromDto(TimesheetDto dto)
        {
            return new Timesheet
            {
                Id = dto.Id,
                Date = DateTime.Parse(dto.Date),
                Project = dto.Project,
                Hours = dto.Hours,
                Details = dto.Details,
                Status = dto.Status,
                Client = dto.Client
            };
        }
    }
}


