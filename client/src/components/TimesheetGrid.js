import React from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { Paper, Typography, Box, Chip } from '@mui/material';

const TimesheetGrid = ({ timesheets, onTimesheetSelect }) => {
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

  const columns = [
    {
      field: 'date',
      headerName: 'Date',
      width: 120,
      type: 'date',
      valueGetter: (params) => new Date(params.row.date),
    },
    {
      field: 'project',
      headerName: 'Project',
      width: 150,
      flex: 1,
    },
    {
      field: 'client',
      headerName: 'Client',
      width: 130,
    },
    {
      field: 'hours',
      headerName: 'Hours',
      width: 100,
      type: 'number',
      valueFormatter: (params) => `${params.value}h`,
    },
    {
      field: 'status',
      headerName: 'Status',
      width: 120,
      renderCell: (params) => (
        <Chip
          label={params.value}
          color={getStatusColor(params.value)}
          size="small"
          variant="outlined"
        />
      ),
    },
    {
      field: 'details',
      headerName: 'Details Preview',
      width: 200,
      flex: 1,
      renderCell: (params) => (
        <Box sx={{ 
          overflow: 'hidden', 
          textOverflow: 'ellipsis', 
          whiteSpace: 'nowrap',
          maxWidth: '100%'
        }}>
          {params.value.substring(0, 50)}...
        </Box>
      ),
    },
  ];

  const handleRowClick = (params) => {
    onTimesheetSelect(params.row);
  };

  return (
    <Box>
      <Typography variant="h4" component="h1" gutterBottom>
        Timesheet Overview
      </Typography>
      <Typography variant="body1" color="text.secondary" sx={{ mb: 3 }}>
        Click on any row to view detailed information
      </Typography>
      
      <Paper elevation={2} sx={{ height: 600, width: '100%' }}>
        <DataGrid
          rows={timesheets}
          columns={columns}
          pageSize={10}
          rowsPerPageOptions={[5, 10, 20]}
          onRowClick={handleRowClick}
          sx={{
            '& .MuiDataGrid-row:hover': {
              cursor: 'pointer',
              backgroundColor: 'action.hover',
            },
            '& .MuiDataGrid-cell:focus': {
              outline: 'none',
            },
            '& .MuiDataGrid-row:focus': {
              outline: 'none',
            },
          }}
          disableSelectionOnClick
          autoHeight={false}
        />
      </Paper>
    </Box>
  );
};

export default TimesheetGrid;
