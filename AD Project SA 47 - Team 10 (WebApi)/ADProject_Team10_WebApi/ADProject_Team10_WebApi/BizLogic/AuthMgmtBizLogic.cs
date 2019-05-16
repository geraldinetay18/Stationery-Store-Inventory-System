/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10_WebApi.Models;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using ADProject_Team10_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ADProject_Team10_WebApi.BizLogic
{
    public class AuthMgmtBizLogic
    {
        IEmployeeService empSer = new EmployeeService();
        ITemporaryRoleService tempRoleSer = new TemporaryRoleService();
        IDepartmentService deptSer = new DepartmentService();

        public List<Employee> ListAllEmployee()
        {
            return empSer.ListAllEmployees();
        }
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

        public TemporaryRole SearchTemporaryRoleByEmpId(int empId)
        {
            return tempRoleSer.SearchTemporaryRoleByEmpId(empId);
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
            int empId = SearchEmployeeByUserName(userName).EmployeeId;
            TemporaryRole temporaryRole = tempRoleSer.SearchTemporaryRoleByEmpId(empId);
            if (temporaryRole == null || DateTime.Compare(DateTime.Now.Date, temporaryRole.StartDate.Date) < 0 || DateTime.Compare(DateTime.Now.Date, temporaryRole.EndDate.Date) > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsDeptRep(string userName)
        {
            int empId = SearchEmployeeByUserName(userName).EmployeeId;
            Department dept = deptSer.SearchDepartmentByDeptRepEmpId(empId);
            if (dept == null)
                return false;
            else
                return true;
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
                if (tempRoleSer.SearchTemporaryRoleByEmpId(emp.EmployeeId) == null && emp.EmployeeId != head.EmployeeId)
                {
                    unassignedEmpList.Add(emp);
                }
            }
            return unassignedEmpList;
        }

        public Employee SearchCurrentActingHead(string deptId)
        {
            List<TemporaryRole> temporaryRoles = tempRoleSer.ListAllTemporaryRole();
            Employee actingHead = null;
            foreach (TemporaryRole temporaryRole in temporaryRoles)
            {
                if (temporaryRole.Employee.DeptId == deptId)
                {
                    if (DateTime.Compare(DateTime.Now.Date, temporaryRole.EndDate.Date) > 0)
                    {
                        tempRoleSer.DeleteTemporaryRole(temporaryRole.EmployeeId);
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

        public int DeleteTemporaryRole(int empId)
        {
            return tempRoleSer.DeleteTemporaryRole(empId);
        }

        public int UpdateTemporaryRole(TemporaryRole temporaryRole)
        {
            return tempRoleSer.UpdateTemporaryRole(temporaryRole);
        }

        public int CreateTemporaryRole(TemporaryRole temporaryRole)
        {
            return tempRoleSer.CreateTemporaryRole(temporaryRole);
        }

        public Assignment SearchAssignment(string deptId)
        {
            Assignment assignment = null;
            Employee emp = SearchCurrentActingHead(deptId);
            if (emp != null)
            {
                TemporaryRole temporaryRole = tempRoleSer.SearchTemporaryRoleByEmpId(emp.EmployeeId);
                if (temporaryRole != null)
                {
                    assignment = new Assignment()
                    {
                        EmployeeName = emp.EmployeeName,
                        TemporaryRoleId = temporaryRole.TemporaryRoleId,
                        EmployeeId = temporaryRole.EmployeeId.ToString(),
                        StartDate = temporaryRole.StartDate.ToString("dd-MM-yyyy"),
                        EndDate = temporaryRole.EndDate.ToString("dd-MM-yyyy")
                    };
                }
            }
            return assignment;
        }

        public string UpdateAssignment(Assignment a)
        {
            string result = "false";
            DateTime StartDate, EndDate;
            string format = "dd-MM-yyyy";
            TemporaryRole temporaryRole = new TemporaryRole()
            {
                TemporaryRoleId = a.TemporaryRoleId,
                EmployeeId = Convert.ToInt32(a.EmployeeId)
            };

            if (DateTime.TryParseExact(a.StartDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate) && DateTime.TryParseExact(a.EndDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
            {
                temporaryRole.StartDate = StartDate;
                temporaryRole.EndDate = EndDate;
            }

            int outcome = tempRoleSer.UpdateTemporaryRole(temporaryRole);
            if (outcome == 1)
            {
                result = "true";
            }
            return result;
        }

        public string AddAssignment(Assignment a)
        {
            string result = "false";
            DateTime StartDate, EndDate;
            string format = "dd-MM-yyyy";
            TemporaryRole temporaryRole = new TemporaryRole()
            {
                TemporaryRoleId = a.TemporaryRoleId,
                EmployeeId = Convert.ToInt32(a.EmployeeId)
            };

            if (DateTime.TryParseExact(a.StartDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out StartDate) && DateTime.TryParseExact(a.EndDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out EndDate))
            {
                temporaryRole.StartDate = StartDate;
                temporaryRole.EndDate = EndDate;
            }

            int outcome = tempRoleSer.CreateTemporaryRole(temporaryRole);
            if (outcome == 1)
            {
                result = "true";
            }
            return result;
        }

        public string DeleteAssignment(int empId)
        {
            string result = "false";
            int outcome = tempRoleSer.DeleteTemporaryRole(empId);
            if (outcome == 1)
            {
                result = "true";
            }
            return result;
        }

        public BriefEmployee SearchBriefEmployee(int empId)
        {
            BriefEmployee briefEmployee = null;
            Employee emp = empSer.SearchEmployeeByEmpId(empId);
            if (emp != null)
            {
                briefEmployee = new BriefEmployee(emp);
            }
            return briefEmployee;
        }

        public List<BriefEmployee> ListBriefEmpUnderSameDeptExclActHead(string userName)
        {
            List<BriefEmployee> briefEmployees = new List<BriefEmployee>();
            List<Employee> empList = ListEmplyUnderSameDeptExclActHead(userName);
            foreach (Employee emp in empList)
            {
                BriefEmployee briefEmployee = SearchBriefEmployee(emp.EmployeeId);
                briefEmployees.Add(briefEmployee);
            }
            return briefEmployees;
        }

        public BriefEmployee SearchBriefEmployeeByEmail(string email)
        {
            BriefEmployee briefEmployee = null;
            Employee emp = empSer.SearchEmployeeByUserName(email);
            if (emp != null)
            {
                briefEmployee = new BriefEmployee(emp);
            }

            if (IsActingHead(email) && !IsDeptRep(email))
                briefEmployee.Role = "Department Head";
            else if (IsDeptRep(email))
                briefEmployee.Role = "DeptRep";
            return briefEmployee;
        }
    }
}