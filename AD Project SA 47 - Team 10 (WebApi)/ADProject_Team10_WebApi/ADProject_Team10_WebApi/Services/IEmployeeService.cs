/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10_WebApi.Models;
using System.Collections.Generic;

namespace ADProject_Team10_WebApi.Services
{
    interface IEmployeeService
    {
        List<Employee> ListAllEmployees();
        List<Employee> SearchEmployeeByDeptId(string deptId);
        List<Employee> SearchEmployeeByEmpName(string name);
        List<Employee> SearchEmployeeByDeptIdAndEmpName(string deptId, string name);
        Employee SearchEmployeeByEmpId(int id);
        Employee SearchEmployeeByUserName(string username);
        Employee SearchEmployeeByReqId(int repId);
        Employee SearchStoreSupervisor();
        List<Employee> FindEmployeesByRole(string role);
    }
}
