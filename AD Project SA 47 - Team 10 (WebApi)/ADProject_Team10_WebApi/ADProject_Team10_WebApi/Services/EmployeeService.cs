/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10_WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ADProject_Team10_WebApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        SSAEntities ssae = new SSAEntities();

        public List<Employee> ListAllEmployees()
        {
            return ssae.Employees.ToList();
        }

        public List<Employee> SearchEmployeeByDeptId(string deptId)
        {
            return ssae.Employees.Where(x => x.DeptId == deptId).ToList();
        }

        public Employee SearchEmployeeByEmpId(int id)
        {
            return ssae.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
        }


        public List<Employee> SearchEmployeeByEmpName(string name)
        {
            return ssae.Employees.Where(x => x.EmployeeName.Contains(name)).ToList();
        }


        public Employee SearchEmployeeByUserName(string username)
        {
            return ssae.Employees.Include("Department").Where(x => x.Email == username).FirstOrDefault();
        }

        public Employee SearchEmployeeByReqId(int repId)
        {
            Requisition req = ssae.Requisitions.Where(x => x.RequisitionId == repId).FirstOrDefault();
            return ssae.Employees.Where(x => x.EmployeeId == req.EmployeeId).FirstOrDefault();
        }

        public Employee SearchStoreSupervisor()
        {
            return ssae.Employees.Where(x => x.Role == "Store Supervisor").FirstOrDefault();
        }

        public List<Employee> SearchEmployeeByDeptIdAndEmpName(string deptId, string name)
        {
            return ssae.Employees.Where(x => x.DeptId == deptId && x.EmployeeName.Contains(name)).ToList();
        }

        public List<Employee> FindEmployeesByRole(string role)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Employees.Where(x => x.Role == role).ToList();
            }
        }
    }
}
