# MCP (Model Context Protocol) Server

This server includes MCP support for timesheet management operations, allowing you to create, read, update, and delete timesheets directly from MCP clients.

## Available MCP Methods

### 1. List Timesheets
**Method:** `timesheets/list`
**Description:** Get all timesheets
**Parameters:** None
**Example:**
```json
{
  "method": "timesheets/list",
  "params": {}
}
```

### 2. Get Timesheet
**Method:** `timesheets/get`
**Description:** Get a specific timesheet by ID
**Parameters:** `id` (int)
**Example:**
```json
{
  "method": "timesheets/get",
  "params": {
    "id": 1
  }
}
```

### 3. Create Timesheet
**Method:** `timesheets/create`
**Description:** Create a new timesheet
**Parameters:**
- `date` (string) - Date in YYYY-MM-DD format
- `project` (string) - Project name
- `hours` (double) - Hours worked
- `details` (string) - Work details
- `status` (string) - Status (Approved, Pending, Rejected)
- `client` (string) - Client name

**Example:**
```json
{
  "method": "timesheets/create",
  "params": {
    "date": "2024-01-20",
    "project": "New Project",
    "hours": 8.5,
    "details": "Worked on new feature implementation",
    "status": "Pending",
    "client": "Client E"
  }
}
```

### 4. Update Timesheet
**Method:** `timesheets/update`
**Description:** Update an existing timesheet
**Parameters:** Same as create + `id` (int)
**Example:**
```json
{
  "method": "timesheets/update",
  "params": {
    "id": 1,
    "date": "2024-01-20",
    "project": "Updated Project",
    "hours": 9.0,
    "details": "Updated work details",
    "status": "Approved",
    "client": "Client A"
  }
}
```

### 5. Delete Timesheet
**Method:** `timesheets/delete`
**Description:** Delete a timesheet
**Parameters:** `id` (int)
**Example:**
```json
{
  "method": "timesheets/delete",
  "params": {
    "id": 1
  }
}
```

## API Endpoints

### POST /api/mcp/request
Handle MCP requests by sending a JSON payload with the method and parameters.

### GET /api/mcp/methods
Get a list of all available MCP methods and their parameters.

## Response Format

All MCP responses follow this format:

**Success Response:**
```json
{
  "success": true,
  "data": { ... }
}
```

**Error Response:**
```json
{
  "success": false,
  "error": {
    "message": "Error description",
    "code": 400
  }
}
```

## Usage with MCP Clients

You can now use any MCP client to interact with your timesheet system. The server exposes the MCP functionality through HTTP endpoints, making it compatible with various MCP client implementations.

## Testing

You can test the MCP functionality using curl or any HTTP client:

```bash
# List all timesheets
curl -X POST http://localhost:5000/api/mcp/request \
  -H "Content-Type: application/json" \
  -d '{"method": "timesheets/list", "params": {}}'

# Create a new timesheet
curl -X POST http://localhost:5000/api/mcp/request \
  -H "Content-Type: application/json" \
  -d '{
    "method": "timesheets/create",
    "params": {
      "date": "2024-01-20",
      "project": "Test Project",
      "hours": 7.5,
      "details": "Testing MCP integration",
      "status": "Pending",
      "client": "Test Client"
    }
  }'
```
