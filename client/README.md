# TimePro - Timesheet Management Application

A modern React application for managing timesheets with a beautiful Material-UI interface, connected to a .NET API backend.

## Features

- 📊 Interactive timesheet grid with sorting and filtering
- 📝 Detailed timesheet view with comprehensive information
- 🎨 Modern Material-UI design
- 📱 Responsive layout for all devices
- 🔗 Connected to .NET API backend
- ⚡ Real-time data loading with error handling
- 🔄 Loading states and user feedback

## Getting Started

### Prerequisites

- Node.js (version 18 or higher)
- npm or yarn
- .NET 9.0 SDK (for the API server)

### Installation

1. **Start the API Server** (required for data):
```bash
cd ../server
dotnet restore
dotnet run
```
The API will be available at `http://localhost:5000`

2. **Start the React Client**:
```bash
npm install
npm start
```

3. Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

**Note**: The React client requires the .NET API server to be running on port 5000 to load timesheet data.

## Usage

- **Grid View**: Browse all timesheets in an interactive data grid
- **Detail View**: Click on any timesheet row to view comprehensive details
- **Navigation**: Use the back button to return to the grid view

## Project Structure

```
src/
├── components/
│   ├── TimesheetGrid.js    # Main grid component
│   └── TimesheetDetail.js  # Detail view component
├── services/
│   └── timesheetService.js # API service for server communication
├── data/
│   └── mockData.js         # Mock timesheet data (deprecated)
├── App.js                  # Main application component
└── index.js               # Application entry point
```

## API Integration

The client is connected to a .NET 9 Web API backend that provides:
- GET `/api/timesheets` - Fetch all timesheets
- GET `/api/timesheets/{id}` - Fetch specific timesheet
- POST `/api/timesheets` - Create new timesheet
- PUT `/api/timesheets/{id}` - Update timesheet
- DELETE `/api/timesheets/{id}` - Delete timesheet

## Future Enhancements

- Add/Edit/Delete timesheet functionality in the UI
- Export to PDF/Excel
- Advanced filtering and search
- Time tracking features
- Authentication and user management

## Available Scripts

- `npm start` - Runs the app in development mode
- `npm build` - Builds the app for production
- `npm test` - Launches the test runner
