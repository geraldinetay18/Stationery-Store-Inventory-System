/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10_WebApi.Models;
using System.Collections.Generic;

namespace ADProject_Team10_WebApi.Services
{
    interface IDepartmentService
    {
        List<Department> ListAllDepartments();
        List<Department> SearchDepartmentByDeptName(string deptName);
        List<Department> SearchDepartmentByStoreClerkEmpId(int empId);
        List<Department> SearchDepartmentByLocationId(string locationId);
        Department SearchDepartmentByDeptId(string deptId);
        Department SearchDepartmentByDeptRepEmpId(int deptRepEmpId);
        Department SearchDeptByReqId(int reqId);
        int UpdateDeptRep(string deptId, int deptRepEmpId);
        int UpdateCollectionPoint(string locationId, string deptId);
        Department SearchDepartmentByName(string deptName);
        Department SearchDepartmentByHeadId(int headId);
    }
}
