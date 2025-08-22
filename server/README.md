# TimePro Server API

A .NET 9 Web API that serves timesheet data stored in JSON format.

## Features

- 🚀 RESTful API endpoints for timesheet management
- 📄 JSON-based data storage
- 🔄 Full CRUD operations (Create, Read, Update, Delete)
- 📚 Swagger/OpenAPI documentation
- 🌐 CORS enabled for React client
- 📝 Comprehensive logging
- 🤖 MCP (Model Context Protocol) support for AI client integration

## API Endpoints

### GET /api/timesheets
Get all timesheets

### GET /api/timesheets/{id}
Get a specific timesheet by ID

### POST /api/timesheets
Create a new timesheet

### PUT /api/timesheets/{id}
Update an existing timesheet

### DELETE /api/timesheets/{id}
Delete a timesheet

## MCP Endpoints

### POST /api/mcp/request
Handle MCP requests for timesheet operations

### GET /api/mcp/methods
Get available MCP methods and parameters

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- IDE (Visual Studio, VS Code, Rider)

### Running the API

1. Navigate to the server directory:
```bash
cd server
```

2. Restore packages:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

4. Open your browser to:
   - API: `https://localhost:5001` or `http://localhost:5000`
   - Swagger UI: `https://localhost:5001/swagger`

## Data Storage

Timesheet data is stored in `Data/timesheets.json`. The service automatically creates this directory and file if they don't exist.

## Project Structure

```
server/
├── Controllers/
│   ├── TimesheetsController.cs    # REST API endpoints
│   └── McpController.cs           # MCP API endpoints
├── Models/
│   └── Timesheet.cs               # Data models and DTOs
├── Services/
│   ├── ITimesheetService.cs       # Service interface
│   └── TimesheetService.cs        # JSON data service implementation
├── MCP/
│   ├── McpServer.cs               # MCP server implementation
│   └── README.md                  # MCP documentation
├── Data/
│   └── timesheets.json            # JSON data storage
├── Properties/
│   └── launchSettings.json        # Development settings
├── appsettings.json               # Application configuration
├── appsettings.Development.json   # Development configuration
├── Program.cs                     # Application entry point
└── TimePro.Server.csproj         # Project file
```

## CORS Configuration

The API is configured to allow requests from `http://localhost:3000` (React development server). Update the CORS policy in `Program.cs` if needed.

## Development

The API uses:
- **ASP.NET Core 9.0** - Web framework
- **Swagger/OpenAPI** - API documentation
- **Newtonsoft.Json** - JSON serialization
- **System.Text.Json** - MCP JSON handling
- **Dependency Injection** - Service management
- **Structured Logging** - Error tracking and debugging
- **MCP Protocol** - AI client integration support


