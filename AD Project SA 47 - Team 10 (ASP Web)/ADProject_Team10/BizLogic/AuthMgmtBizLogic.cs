/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10.Models;
using ADProject_Team10.Services;
using System;
using System.Collections.Generic;


namespace ADProject_Team10.BizLogic
{
    public class AuthMgmtBizLogic
    {
        IEmployeeService empSer = new EmployeeService();
        ITemporaryRoleService tempRoleSer = new TemporaryRoleService();
        IDepartmentService deptSer = new DepartmentService();

        public Employee SearchEmployeeByUserName(string userName)
        {
            return empSer.SearchEmployeeByUserName(userName);
        }

        public List<Employee> SearchEmployeeByEmpName(string name)
        {
            return empSer.SearchEmployeeByEmpName(name);
        }

        public Employee SearchEmployeeByEmpId(int empId)
        {
            return empSer.SearchEmployeeByEmpId(empId);
        }

        public TemporaryRole SearchAssignmentByEmpId(int empId)
        {
            return tempRoleSer.SearchAssignmentByEmpId(empId);
        }

        public List<Employee> SearchEmployeeUnderSameDeptByEmpNameExclDeptRep(string userName, string empName)
        {
            return ListEmplyUnderSameDeptExclDeptRep(userName).FindAll(e => e.EmployeeName.CaseInsensitiveContains(empName));
        }

        public List<Employee> SearchEmployeeUnderSameDeptByEmpNameExclActHead(string userName, string empName)
        {
            return ListEmplyUnderSameDeptExclActHead(userName).FindAll(e => e.EmployeeName.CaseInsensitiveContains(empName));
        }

        public bool IsActingHead(string userName)
        {
            if (SearchEmployeeByUserName(userName) != null)
            {
                int empId = SearchEmployeeByUserName(userName).EmployeeId;
                TemporaryRole temporaryRole = tempRoleSer.SearchAssignmentByEmpId(empId);
                if (temporaryRole == null || DateTime.Compare(DateTime.Now.Date, temporaryRole.StartDate.Date) < 0 || DateTime.Compare(DateTime.Now.Date, temporaryRole.EndDate.Date) > 0)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public bool IsDeptRep(string userName)
        {
            if (SearchEmployeeByUserName(userName) != null)
            {
                int empId = SearchEmployeeByUserName(userName).EmployeeId;
                Department dept = deptSer.SearchDepartmentByDeptRepEmpId(empId);
                if (dept == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public List<Employee> ListEmplyUnderSameDeptExclDeptRep(string userName)
        {
            Employee head = SearchEmployeeByUserName(userName);
            Employee deptRep = SearchCurrentDeptRep(head.DeptId);
            List<Employee> empList = empSer.SearchEmployeeByDeptId(head.DeptId);
            List<Employee> unassignedEmpList = new List<Employee>();
            foreach (Employee e in empList)
            {
                if (e.EmployeeId != head.EmployeeId && e.EmployeeId != deptRep.EmployeeId && e.Role != "Department Head")
                {
                    unassignedEmpList.Add(e);
                }
            }
            return unassignedEmpList;
        }

        public Employee SearchCurrentDeptRep(string deptId)
        {
            Department dept = deptSer.SearchDepartmentByDeptId(deptId);
            return empSer.SearchEmployeeByEmpId(dept.RepresentativeId);
        }

        public int UpdateCurrentDeptRep(string deptId, int empId)
        {
            return deptSer.UpdateDeptRep(deptId, empId);
        }

        public List<Employee> ListEmplyUnderSameDeptExclActHead(string userName)
        {
            Employee head = SearchEmployeeByUserName(userName);
            List<Employee> empList = empSer.SearchEmployeeByDeptId(head.DeptId);
            List<Employee> unassignedEmpList = new List<Employee>();
            foreach (Employee emp in empList)
            {
                if (tempRoleSer.SearchAssignmentByEmpId(emp.EmployeeId) == null && emp.EmployeeId != head.EmployeeId)
                {
                    unassignedEmpList.Add(emp);
                }
            }
            return unassignedEmpList;
        }

        public Employee SearchCurrentActingHead(string deptId)
        {
            List<TemporaryRole> temporaryRoles = tempRoleSer.ListAllAssignment();
            Employee actingHead = null;
            foreach (TemporaryRole temporaryRole in temporaryRoles)
            {
                if (temporaryRole.Employee.DeptId == deptId)
                {
                    if (DateTime.Compare(DateTime.Now.Date, temporaryRole.EndDate.Date) > 0)
                    {
                        tempRoleSer.DeleteAssignment(temporaryRole.EmployeeId);
                        break;
                    }
                    else
                    {
                        actingHead = temporaryRole.Employee;
                        break;
                    }
                }
            }
            return actingHead;
        }

        public int DeleteTemporaryAssignment(int empId)
        {
            return tempRoleSer.DeleteAssignment(empId);
        }

        public int UpdateTemporaryAssignment(TemporaryRole temporaryRole)
        {
            return tempRoleSer.UpdateAssignment(temporaryRole);
        }

        public int CreateTemporaryAssignment(TemporaryRole temporaryRole)
        {
            return tempRoleSer.CreateAssignment(temporaryRole);
        }
    }
}