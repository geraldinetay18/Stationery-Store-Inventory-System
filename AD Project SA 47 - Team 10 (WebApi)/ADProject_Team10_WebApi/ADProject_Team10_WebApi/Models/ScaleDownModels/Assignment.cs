/* Author: Lee Kai Seng (Kyler) */

namespace ADProject_Team10_WebApi.Models.ScaleDownModels
{
    // To provide TemporaryRole object with Employee Name
    public class Assignment
    {
        public string EmployeeName { get; set; }
        public string TemporaryRoleId { get; set; }
        public string EmployeeId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

    }
}