using TimePro.Server.Models;

namespace TimePro.Server.Services
{
    public interface ITimesheetService
    {
        Task<IEnumerable<TimesheetDto>> GetAllTimesheetsAsync();
        Task<TimesheetDto?> GetTimesheetByIdAsync(int id);
        Task<TimesheetDto> CreateTimesheetAsync(TimesheetDto timesheetDto);
        Task<TimesheetDto?> UpdateTimesheetAsync(int id, TimesheetDto timesheetDto);
        Task<bool> DeleteTimesheetAsync(int id);
    }
}


