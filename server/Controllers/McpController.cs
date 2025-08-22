using Microsoft.AspNetCore.Mvc;
using TimePro.Server.MCP;

namespace TimePro.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class McpController : ControllerBase
    {
        private readonly McpServer _mcpServer;
        private readonly ILogger<McpController> _logger;

        public McpController(McpServer mcpServer, ILogger<McpController> logger)
        {
            _mcpServer = mcpServer;
            _logger = logger;
        }

        /// <summary>
        /// Handle MCP requests
        /// </summary>
        /// <param name="request">MCP request object</param>
        /// <returns>MCP response</returns>
        [HttpPost("request")]
        public async Task<IActionResult> HandleRequest([FromBody] McpRequest request)
        {
            try
            {
                var requestJson = System.Text.Json.JsonSerializer.Serialize(request);
                var responseJson = await _mcpServer.HandleRequestAsync(requestJson);
                var response = System.Text.Json.JsonSerializer.Deserialize<McpResponse>(responseJson);
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling MCP request");
                return StatusCode(500, new McpResponse
                {
                    Success = false,
                    Error = new McpError
                    {
                        Message = "Internal server error",
                        Code = 500
                    }
                });
            }
        }

        /// <summary>
        /// Get available MCP methods
        /// </summary>
        /// <returns>List of available methods</returns>
        [HttpGet("methods")]
        public IActionResult GetMethods()
        {
            var methods = new List<McpMethodInfo>
            {
                new McpMethodInfo 
                { 
                    Method = "timesheets/list", 
                    Description = "Get all timesheets",
                    Parameters = new Dictionary<string, string>()
                },
                new McpMethodInfo 
                { 
                    Method = "timesheets/get", 
                    Description = "Get a specific timesheet by ID",
                    Parameters = new Dictionary<string, string> { { "id", "int" } }
                },
                new McpMethodInfo 
                { 
                    Method = "timesheets/create", 
                    Description = "Create a new timesheet",
                    Parameters = new Dictionary<string, string> 
                    { 
                        { "date", "string" },
                        { "project", "string" },
                        { "hours", "double" },
                        { "details", "string" },
                        { "status", "string" },
                        { "client", "string" }
                    }
                },
                new McpMethodInfo 
                { 
                    Method = "timesheets/update", 
                    Description = "Update an existing timesheet",
                    Parameters = new Dictionary<string, string> 
                    { 
                        { "id", "int" },
                        { "date", "string" },
                        { "project", "string" },
                        { "hours", "double" },
                        { "details", "string" },
                        { "status", "string" },
                        { "client", "string" }
                    }
                },
                new McpMethodInfo 
                { 
                    Method = "timesheets/delete", 
                    Description = "Delete a timesheet",
                    Parameters = new Dictionary<string, string> { { "id", "int" } }
                }
            };

            return Ok(new { methods });
        }
    }
}
