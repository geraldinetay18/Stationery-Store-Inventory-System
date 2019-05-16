/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADProject_Team10.Services
{
    public class TemporaryRoleService : ITemporaryRoleService
    {
        SSAEntities ssae = new SSAEntities();
        public int CreateAssignment(TemporaryRole temporaryRole)
        {
            try
            {
                ssae.TemporaryRoles.Add(temporaryRole);
                return ssae.SaveChanges(); // returns 1
            }
            catch (Exception)
            {
                return 0; // 0 rows added
            }
        }

        public int DeleteAssignment(int empId)
        {
            try
            {
                TemporaryRole tr = ssae.TemporaryRoles.Where(x => x.EmployeeId == empId).FirstOrDefault();
                ssae.TemporaryRoles.Remove(tr);
                return ssae.SaveChanges(); // returns 1
            }
            catch (Exception)
            {
                return 0; // 0 rows added
            }
        }

        public List<TemporaryRole> ListAllAssignment()
        {
            return ssae.TemporaryRoles.Include("Employee").ToList();
        }

        public TemporaryRole SearchAssignmentByEmpId(int empId)
        {
            return ssae.TemporaryRoles.Where(x => x.EmployeeId == empId).FirstOrDefault();
        }

        public int UpdateAssignment(TemporaryRole temporaryRole)
        {
            try
            {
                TemporaryRole tr = ssae.TemporaryRoles.Where(x => x.EmployeeId == temporaryRole.EmployeeId).FirstOrDefault();
                tr.StartDate = temporaryRole.StartDate;
                tr.EndDate = temporaryRole.EndDate;
                return ssae.SaveChanges(); // returns 1
            }
            catch (Exception)
            {
                return 0; // 0 rows updated
            }
        }
    }
}