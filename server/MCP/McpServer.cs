using System.Text.Json;
using TimePro.Server.Models;
using TimePro.Server.Services;

namespace TimePro.Server.MCP
{
    public class McpServer
    {
        private readonly ITimesheetService _timesheetService;
        private readonly ILogger<McpServer> _logger;

        public McpServer(ITimesheetService timesheetService, ILogger<McpServer> logger)
        {
            _timesheetService = timesheetService;
            _logger = logger;
        }

        public async Task<string> HandleRequestAsync(string requestJson)
        {
            try
            {
                var request = JsonSerializer.Deserialize<McpRequest>(requestJson);
                
                switch (request?.Method)
                {
                    case "timesheets/list":
                        return await HandleListTimesheetsAsync();
                    
                    case "timesheets/get":
                        return await HandleGetTimesheetAsync(request);
                    
                    case "timesheets/create":
                        return await HandleCreateTimesheetAsync(request);
                    
                    case "timesheets/update":
                        return await HandleUpdateTimesheetAsync(request);
                    
                    case "timesheets/delete":
                        return await HandleDeleteTimesheetAsync(request);
                    
                    default:
                        return CreateErrorResponse("Method not found", 404);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling MCP request");
                return CreateErrorResponse("Internal server error", 500);
            }
        }

        private async Task<string> HandleListTimesheetsAsync()
        {
            try
            {
                var timesheets = await _timesheetService.GetAllTimesheetsAsync();
                var response = new McpResponse
                {
                    Success = true,
                    Data = timesheets
                };
                
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error listing timesheets");
                return CreateErrorResponse("Failed to list timesheets", 500);
            }
        }

        private async Task<string> HandleGetTimesheetAsync(McpRequest request)
        {
            if (!int.TryParse(request.Params?.GetValueOrDefault("id")?.ToString(), out int id))
            {
                return CreateErrorResponse("Invalid timesheet ID", 400);
            }

            try
            {
                var timesheet = await _timesheetService.GetTimesheetByIdAsync(id);
                if (timesheet == null)
                {
                    return CreateErrorResponse("Timesheet not found", 404);
                }

                var response = new McpResponse
                {
                    Success = true,
                    Data = timesheet
                };
                
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting timesheet {Id}", id);
                return CreateErrorResponse("Failed to get timesheet", 500);
            }
        }

        private async Task<string> HandleCreateTimesheetAsync(McpRequest request)
        {
            try
            {
                if (request.Params == null)
                {
                    return CreateErrorResponse("No parameters provided", 400);
                }

                var timesheetData = new TimesheetDto
                {
                    Date = request.Params.GetValueOrDefault("date")?.ToString() ?? string.Empty,
                    Project = request.Params.GetValueOrDefault("project")?.ToString() ?? string.Empty,
                    Hours = ParseDouble(request.Params.GetValueOrDefault("hours")),
                    Details = request.Params.GetValueOrDefault("details")?.ToString() ?? string.Empty,
                    Status = request.Params.GetValueOrDefault("status")?.ToString() ?? string.Empty,
                    Client = request.Params.GetValueOrDefault("client")?.ToString() ?? string.Empty
                };

                if (string.IsNullOrEmpty(timesheetData.Date) || 
                    string.IsNullOrEmpty(timesheetData.Project) || 
                    timesheetData.Hours <= 0)
                {
                    return CreateErrorResponse("Missing required fields: date, project, and hours are required", 400);
                }

                var createdTimesheet = await _timesheetService.CreateTimesheetAsync(timesheetData);
                var response = new McpResponse
                {
                    Success = true,
                    Data = createdTimesheet
                };
                
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating timesheet");
                return CreateErrorResponse("Failed to create timesheet", 500);
            }
        }

        private async Task<string> HandleUpdateTimesheetAsync(McpRequest request)
        {
            if (!int.TryParse(request.Params?.GetValueOrDefault("id")?.ToString(), out int id))
            {
                return CreateErrorResponse("Invalid timesheet ID", 400);
            }

            try
            {
                if (request.Params == null)
                {
                    return CreateErrorResponse("No parameters provided", 400);
                }

                var timesheetData = new TimesheetDto
                {
                    Id = id,
                    Date = request.Params.GetValueOrDefault("date")?.ToString() ?? string.Empty,
                    Project = request.Params.GetValueOrDefault("project")?.ToString() ?? string.Empty,
                    Hours = ParseDouble(request.Params.GetValueOrDefault("hours")),
                    Details = request.Params.GetValueOrDefault("details")?.ToString() ?? string.Empty,
                    Status = request.Params.GetValueOrDefault("status")?.ToString() ?? string.Empty,
                    Client = request.Params.GetValueOrDefault("client")?.ToString() ?? string.Empty
                };

                if (string.IsNullOrEmpty(timesheetData.Date) || 
                    string.IsNullOrEmpty(timesheetData.Project) || 
                    timesheetData.Hours <= 0)
                {
                    return CreateErrorResponse("Missing required fields: date, project, and hours are required", 400);
                }

                var updatedTimesheet = await _timesheetService.UpdateTimesheetAsync(id, timesheetData);
                if (updatedTimesheet == null)
                {
                    return CreateErrorResponse("Timesheet not found", 404);
                }

                var response = new McpResponse
                {
                    Success = true,
                    Data = updatedTimesheet
                };
                
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating timesheet {Id}", id);
                return CreateErrorResponse("Failed to update timesheet", 500);
            }
        }

        private async Task<string> HandleDeleteTimesheetAsync(McpRequest request)
        {
            if (!int.TryParse(request.Params?.GetValueOrDefault("id")?.ToString(), out int id))
            {
                return CreateErrorResponse("Invalid timesheet ID", 400);
            }

            try
            {
                var success = await _timesheetService.DeleteTimesheetAsync(id);
                if (!success)
                {
                    return CreateErrorResponse("Timesheet not found", 404);
                }

                var response = new McpResponse
                {
                    Success = true,
                    Data = new { message = "Timesheet deleted successfully" }
                };
                
                return JsonSerializer.Serialize(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting timesheet {Id}", id);
                return CreateErrorResponse("Failed to delete timesheet", 500);
            }
        }

        private string CreateErrorResponse(string message, int code)
        {
            var response = new McpResponse
            {
                Success = false,
                Error = new McpError
                {
                    Message = message,
                    Code = code
                }
            };
            
            return JsonSerializer.Serialize(response);
        }

        private double ParseDouble(object? value)
        {
            if (value == null) return 0.0;
            
            if (value is JsonElement jsonElement)
            {
                return jsonElement.ValueKind switch
                {
                    JsonValueKind.Number => jsonElement.GetDouble(),
                    JsonValueKind.String => double.TryParse(jsonElement.GetString(), out var result) ? result : 0.0,
                    _ => 0.0
                };
            }
            
            if (value is IConvertible convertible)
            {
                try
                {
                    return Convert.ToDouble(convertible);
                }
                catch
                {
                    return 0.0;
                }
            }
            
            return 0.0;
        }
    }

    public class McpRequest
    {
        public string Method { get; set; } = string.Empty;
        public Dictionary<string, object>? Params { get; set; }
    }

    public class McpResponse
    {
        public bool Success { get; set; }
        public object? Data { get; set; }
        public McpError? Error { get; set; }
    }

    public class McpError
    {
        public string Message { get; set; } = string.Empty;
        public int Code { get; set; }
    }

    public class McpMethodInfo
    {
        public string Method { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    }
}
