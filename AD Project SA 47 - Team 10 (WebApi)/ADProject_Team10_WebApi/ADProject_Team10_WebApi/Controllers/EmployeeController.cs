/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10_WebApi.BizLogic;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ADProject_Team10_WebApi.Controllers
{
    public class EmployeeController : ApiController
    {

        // to get a single employee
        [Authorize]
        public BriefEmployee GetBriefEmployee(string id)
        {
            return new AuthMgmtBizLogic().SearchBriefEmployee(Convert.ToInt32(id));
        }

        // to get a list of employee under the department excl. act head
        [Authorize]
        [HttpGet]
        [Route("api/Employee/{email}/list")]
        public IEnumerable<BriefEmployee> GetBriefEmployeesUnderSameDeptExclActHead(string email)
        {
            return new AuthMgmtBizLogic().ListBriefEmpUnderSameDeptExclActHead(email);
        }

        [Authorize]
        [HttpGet]
        [Route("api/Employee/{email}/userdetail")]
        public BriefEmployee GetBriefEmployeeByEmail(string email)
        {
            return new AuthMgmtBizLogic().SearchBriefEmployeeByEmail(email);
        }

    }
}
