/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10_WebApi.Models;
using System.Collections.Generic;

namespace ADProject_Team10_WebApi.Services
{
    interface ITemporaryRoleService
    {
        List<TemporaryRole> ListAllTemporaryRole();
        TemporaryRole SearchTemporaryRoleByEmpId(int empId);
        int CreateTemporaryRole(TemporaryRole temporaryRole);
        int UpdateTemporaryRole(TemporaryRole temporaryRole);
        int DeleteTemporaryRole(int empId);
    }
}
