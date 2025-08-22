# TimePro - Timesheet Management Application

A modern React application for managing timesheets with a beautiful Material-UI interface.

## Features

- 📊 Interactive timesheet grid with sorting and filtering
- 📝 Detailed timesheet view with comprehensive information
- 🎨 Modern Material-UI design
- 📱 Responsive layout for all devices
- 🔄 Ready for future API integration

## Getting Started

### Prerequisites

- Node.js (version 14 or higher)
- npm or yarn

### Installation

1. Install dependencies:
```bash
npm install
```

2. Start the development server:
```bash
npm start
```

3. Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

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
├── data/
│   └── mockData.js         # Mock timesheet data
├── App.js                  # Main application component
└── index.js               # Application entry point
```

## Future Enhancements

- API integration for real data
- Add/Edit/Delete timesheet functionality
- Export to PDF/Excel
- Advanced filtering and search
- Time tracking features

## Available Scripts

- `npm start` - Runs the app in development mode
- `npm build` - Builds the app for production
- `npm test` - Launches the test runner
