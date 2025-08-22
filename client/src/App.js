import React, { useState } from 'react';
import { Container, AppBar, Toolbar, Typography, Box } from '@mui/material';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import TimesheetGrid from './components/TimesheetGrid';
import TimesheetDetail from './components/TimesheetDetail';
import { mockTimesheets } from './data/mockData';

function App() {
  const [selectedTimesheet, setSelectedTimesheet] = useState(null);
  const [timesheets] = useState(mockTimesheets);

  const handleTimesheetSelect = (timesheet) => {
    setSelectedTimesheet(timesheet);
  };

  const handleBackToGrid = () => {
    setSelectedTimesheet(null);
  };

  return (
    <div className="App">
      <AppBar position="static" elevation={2}>
        <Toolbar>
          <AccessTimeIcon sx={{ mr: 2 }} />
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            TimePro - Timesheet Management
          </Typography>
        </Toolbar>
      </AppBar>
      
      <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
        <Box sx={{ minHeight: '80vh' }}>
          {selectedTimesheet ? (
            <TimesheetDetail
              timesheet={selectedTimesheet}
              onBack={handleBackToGrid}
            />
          ) : (
            <TimesheetGrid
              timesheets={timesheets}
              onTimesheetSelect={handleTimesheetSelect}
            />
          )}
        </Box>
      </Container>
    </div>
  );
}

export default App;
