/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10.Models;
using System.Collections.Generic;

namespace ADProject_Team10.Services
{
    interface ITemporaryRoleService
    {
        List<TemporaryRole> ListAllAssignment();
        TemporaryRole SearchAssignmentByEmpId(int empId);
        int CreateAssignment(TemporaryRole temporaryRole);
        int UpdateAssignment(TemporaryRole temporaryRole);
        int DeleteAssignment(int empId);
    }
}
