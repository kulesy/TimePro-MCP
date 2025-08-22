const API_BASE_URL = 'http://localhost:5000/api';

class TimesheetService {
  async getAllTimesheets() {
    try {
      const response = await fetch(`${API_BASE_URL}/timesheets`);
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      const data = await response.json();
      return data;
    } catch (error) {
      console.error('Error fetching timesheets:', error);
      throw error;
    }
  }

  async getTimesheetById(id) {
    try {
      const response = await fetch(`${API_BASE_URL}/timesheets/${id}`);
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      const data = await response.json();
      return data;
    } catch (error) {
      console.error(`Error fetching timesheet ${id}:`, error);
      throw error;
    }
  }

  async createTimesheet(timesheet) {
    try {
      const response = await fetch(`${API_BASE_URL}/timesheets`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(timesheet),
      });
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      const data = await response.json();
      return data;
    } catch (error) {
      console.error('Error creating timesheet:', error);
      throw error;
    }
  }

  async updateTimesheet(id, timesheet) {
    try {
      const response = await fetch(`${API_BASE_URL}/timesheets/${id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(timesheet),
      });
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      const data = await response.json();
      return data;
    } catch (error) {
      console.error(`Error updating timesheet ${id}:`, error);
      throw error;
    }
  }

  async deleteTimesheet(id) {
    try {
      const response = await fetch(`${API_BASE_URL}/timesheets/${id}`, {
        method: 'DELETE',
      });
      
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      return true;
    } catch (error) {
      console.error(`Error deleting timesheet ${id}:`, error);
      throw error;
    }
  }
}

export default new TimesheetService();
