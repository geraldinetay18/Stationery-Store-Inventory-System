/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ADProject_Team10_WebApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        public List<Department> ListAllDepartments()
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Departments.ToList();
            }
        }

        public Department SearchDepartmentByDeptId(string deptId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Departments.Where(x => x.DeptId == deptId).FirstOrDefault();
            }
        }

        public List<Department> SearchDepartmentByDeptName(string deptName)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Departments.Where(x => x.DeptName.Contains(deptName)).ToList();
            }
        }

        public Department SearchDepartmentByDeptRepEmpId(int depRepEmpId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Departments.Where(x => x.RepresentativeId == depRepEmpId).FirstOrDefault();
            }
        }

        public List<Department> SearchDepartmentByLocationId(string locationId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Departments.Where(x => x.LocationId == locationId).ToList();
            }
        }

        public List<Department> SearchDepartmentByStoreClerkEmpId(int empId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Departments.Where(x => x.StoreClerkId == empId).ToList();
            }
        }

        public Department SearchDeptByReqId(int reqId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                Requisition req = ssae.Requisitions.Where(x => x.RequisitionId == reqId).First();
                Employee em = ssae.Employees.Where(emp => emp.EmployeeId == req.EmployeeId).First();
                Department dp = ssae.Departments.Where(dept => dept.DeptId == em.DeptId).First();
                return dp;
            }
        }

        public int UpdateCollectionPoint(string locationId, string deptId)
        {
            SSAEntities ssae = new SSAEntities();
            try
            {
                Department d = ssae.Departments.Where(x => x.DeptId == deptId).FirstOrDefault();
                d.LocationId = locationId;
                return ssae.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateDeptRep(string deptId, int deptRepEmpId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                try
                {
                    Department dept = ssae.Departments.Where(x => x.DeptId == deptId).FirstOrDefault();
                    dept.RepresentativeId = deptRepEmpId;
                    return ssae.SaveChanges();
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public Department SearchDepartmentByName(string deptName)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Departments.Where(x => x.DeptName.Equals(deptName)).FirstOrDefault();
            }
        }
        public Department SearchDepartmentByHeadId(int headId)
        {
            using (SSAEntities ssae = new SSAEntities())
            {
                return ssae.Departments.Where(x => x.HeadId == headId).First();
            }
        }
    }
}