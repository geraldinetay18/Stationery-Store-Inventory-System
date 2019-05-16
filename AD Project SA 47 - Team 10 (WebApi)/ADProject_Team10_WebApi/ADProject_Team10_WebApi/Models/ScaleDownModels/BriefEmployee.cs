/* Author: Lee Kai Seng (Kyler) */

namespace ADProject_Team10_WebApi.Models.ScaleDownModels
{
    public class BriefEmployee
    {
        public BriefEmployee() { }
        public BriefEmployee(Employee emp)
        {
            EmployeeId = emp.EmployeeId.ToString();
            DeptId = emp.DeptId;
            EmployeeName = emp.EmployeeName;
            Role = emp.Role;
            Email = emp.Email;
            Phone = emp.Phone;
        }
        public string EmployeeId { get; set; }
        public string DeptId { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}