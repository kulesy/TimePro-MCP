import React from 'react';
import {
  Paper,
  Typography,
  Box,
  Button,
  Grid,
  Chip,
  Divider,
  Card,
  CardContent
} from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import BusinessIcon from '@mui/icons-material/Business';
import FolderIcon from '@mui/icons-material/Folder';

const TimesheetDetail = ({ timesheet, onBack }) => {
  const getStatusColor = (status) => {
    switch (status) {
      case 'Approved':
        return 'success';
      case 'Pending':
        return 'warning';
      case 'Rejected':
        return 'error';
      default:
        return 'default';
    }
  };

  const formatDate = (dateString) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  return (
    <Box>
      <Box sx={{ mb: 3, display: 'flex', alignItems: 'center', gap: 2 }}>
        <Button
          startIcon={<ArrowBackIcon />}
          onClick={onBack}
          variant="outlined"
        >
          Back to Timesheets
        </Button>
        <Typography variant="h4" component="h1">
          Timesheet Details
        </Typography>
      </Box>

      <Paper elevation={3} sx={{ p: 4 }}>
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
              <Typography variant="h5" component="h2">
                {timesheet.project}
              </Typography>
              <Chip
                label={timesheet.status}
                color={getStatusColor(timesheet.status)}
                size="large"
                variant="filled"
              />
            </Box>
          </Grid>

          <Grid item xs={12} md={6}>
            <Card elevation={1}>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <CalendarTodayIcon sx={{ mr: 2, color: 'primary.main' }} />
                  <Typography variant="h6">Date</Typography>
                </Box>
                <Typography variant="body1" color="text.secondary">
                  {formatDate(timesheet.date)}
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid item xs={12} md={6}>
            <Card elevation={1}>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <AccessTimeIcon sx={{ mr: 2, color: 'primary.main' }} />
                  <Typography variant="h6">Hours Worked</Typography>
                </Box>
                <Typography variant="body1" color="text.secondary">
                  {timesheet.hours} hours
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid item xs={12} md={6}>
            <Card elevation={1}>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <BusinessIcon sx={{ mr: 2, color: 'primary.main' }} />
                  <Typography variant="h6">Client</Typography>
                </Box>
                <Typography variant="body1" color="text.secondary">
                  {timesheet.client}
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid item xs={12} md={6}>
            <Card elevation={1}>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
                  <FolderIcon sx={{ mr: 2, color: 'primary.main' }} />
                  <Typography variant="h6">Project</Typography>
                </Box>
                <Typography variant="body1" color="text.secondary">
                  {timesheet.project}
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid item xs={12}>
            <Divider sx={{ my: 3 }} />
            <Typography variant="h6" gutterBottom>
              Work Details
            </Typography>
            <Paper 
              elevation={0} 
              sx={{ 
                p: 3, 
                backgroundColor: 'grey.50',
                border: '1px solid',
                borderColor: 'grey.200'
              }}
            >
              <Typography variant="body1" sx={{ lineHeight: 1.8, whiteSpace: 'pre-wrap' }}>
                {timesheet.details}
              </Typography>
            </Paper>
          </Grid>
        </Grid>
      </Paper>
    </Box>
  );
};

export default TimesheetDetail;
