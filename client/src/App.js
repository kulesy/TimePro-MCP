import React, { useState, useEffect } from 'react';
import { Container, AppBar, Toolbar, Typography, Box, CircularProgress, Alert } from '@mui/material';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import TimesheetGrid from './components/TimesheetGrid';
import TimesheetDetail from './components/TimesheetDetail';
import timesheetService from './services/timesheetService';

function App() {
  const [selectedTimesheet, setSelectedTimesheet] = useState(null);
  const [timesheets, setTimesheets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchTimesheets = async () => {
      try {
        setLoading(true);
        setError(null);
        const data = await timesheetService.getAllTimesheets();
        setTimesheets(data);
      } catch (err) {
        setError('Failed to load timesheets. Please make sure the server is running on http://localhost:5000');
        console.error('Failed to fetch timesheets:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchTimesheets();
  }, []);

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
          {loading ? (
            <Box sx={{ 
              display: 'flex', 
              justifyContent: 'center', 
              alignItems: 'center', 
              minHeight: '50vh',
              flexDirection: 'column',
              gap: 2
            }}>
              <CircularProgress size={60} />
              <Typography variant="h6" color="text.secondary">
                Loading timesheets...
              </Typography>
            </Box>
          ) : error ? (
            <Box sx={{ mt: 4 }}>
              <Alert severity="error" sx={{ mb: 2 }}>
                {error}
              </Alert>
              <Typography variant="body2" color="text.secondary">
                Make sure the .NET API server is running on port 5000:
              </Typography>
              <Typography variant="body2" sx={{ fontFamily: 'monospace', mt: 1 }}>
                cd server && dotnet run
              </Typography>
            </Box>
          ) : selectedTimesheet ? (
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
