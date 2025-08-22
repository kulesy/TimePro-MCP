namespace TimePro.Server.Models
{
    public class Timesheet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Project { get; set; } = string.Empty;
        public double Hours { get; set; }
        public string Details { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Client { get; set; } = string.Empty;
    }

    public class TimesheetDto
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
        public double Hours { get; set; }
        public string Details { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Client { get; set; } = string.Empty;
    }
}


