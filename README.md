# TimePro - Full Stack Timesheet Application

A modern full-stack timesheet management application with React frontend and .NET API backend.

## 🏗️ Project Structure

```
TimePro-MCP/
├── client/                 # React frontend application
│   ├── src/
│   │   ├── components/     # React components
│   │   ├── data/           # Mock data (to be replaced with API calls)
│   │   ├── App.js          # Main application
│   │   └── index.js        # Entry point
│   ├── public/
│   └── package.json
├── server/                 # .NET 9 Web API
│   ├── Controllers/        # API controllers
│   ├── Models/             # Data models and DTOs
│   ├── Services/           # Business logic services
│   ├── Data/               # JSON data storage
│   ├── Properties/         # Launch settings
│   ├── Program.cs          # API entry point
│   └── TimePro.Server.csproj
└── README.md
```

## 🚀 Getting Started

### Prerequisites
- **Frontend**: Node.js 18+ and npm
- **Backend**: .NET 9.0 SDK

### Running the Application

#### 1. Start the API Server
```bash
cd server
dotnet restore
dotnet run
```
API will be available at: `https://localhost:5001` and `http://localhost:5000`
Swagger documentation: `https://localhost:5001/swagger`

#### 2. Start the React Client
```bash
cd client
npm install
npm start
```
Client will be available at: `http://localhost:3000`

## 🔄 API Integration

The server provides a REST API with the following endpoints:

- `GET /api/timesheets` - Get all timesheets
- `GET /api/timesheets/{id}` - Get specific timesheet
- `POST /api/timesheets` - Create new timesheet
- `PUT /api/timesheets/{id}` - Update timesheet
- `DELETE /api/timesheets/{id}` - Delete timesheet

### Next Steps for Client Integration
To connect the React client to the API, replace the mock data in the client with actual API calls to `http://localhost:5000/api/timesheets`.

## 📋 Features

### Frontend (React + Material-UI)
- ✅ Interactive timesheet grid with sorting and filtering
- ✅ Detailed timesheet view
- ✅ Modern Material-UI design
- ✅ Responsive layout
- 🔄 Ready for API integration

### Backend (.NET 9 Web API)
- ✅ RESTful API endpoints
- ✅ JSON-based data storage
- ✅ Full CRUD operations
- ✅ CORS enabled for React client
- ✅ Swagger/OpenAPI documentation
- ✅ Comprehensive error handling and logging

## 🛠️ Development

### Backend Development
- API runs on ports 5000 (HTTP) and 5001 (HTTPS)
- Data stored in `server/Data/timesheets.json`
- CORS configured for `http://localhost:3000`
- Uses dependency injection and structured logging

### Frontend Development
- React 18 with Material-UI components
- Currently uses mock data in `client/src/data/mockData.js`
- Material-UI DataGrid for timesheet display
- Responsive design with modern UX

## 📄 API Documentation

When the server is running, visit `https://localhost:5001/swagger` for interactive API documentation and testing capabilities.

## 🔧 Tech Stack

**Frontend:**
- React 18
- Material-UI (MUI)
- Material-UI DataGrid
- Responsive design

**Backend:**
- .NET 9 Web API
- ASP.NET Core
- Newtonsoft.Json
- Swagger/OpenAPI
- File-based JSON storage