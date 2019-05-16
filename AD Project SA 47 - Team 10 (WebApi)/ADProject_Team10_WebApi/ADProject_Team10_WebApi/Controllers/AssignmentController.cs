/* Author: Lee Kai Seng (Kyler) */

using ADProject_Team10_WebApi.BizLogic;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using System;
using System.Web.Http;

namespace ADProject_Team10_WebApi.Controllers
{
    public class AssignmentController : ApiController
    {

        // to get a single Assignment by deptId
        [Authorize]
        public Assignment GetAssignment(string id)
        {
            return new AuthMgmtBizLogic().SearchAssignment(id);
        }

        // to delete Assignment
        [Authorize]
        [HttpGet]
        [Route("api/Assignment/delete/{empId}")]
        public string DeleteAssignment(string empId)
        {
            return new AuthMgmtBizLogic().DeleteAssignment(Convert.ToInt32(empId));
        }

        // POST update Assignment
        [Authorize]
        [HttpPost]
        [Route("api/Assignment/update")]
        public string UpdateAssignment([FromBody]Assignment a)
        {
            return new AuthMgmtBizLogic().UpdateAssignment(a);
        }

        // POST add Assignment
        [Authorize]
        [HttpPost]
        [Route("api/Assignment/add")]
        public string AddAssignment([FromBody]Assignment a)
        {
            return new AuthMgmtBizLogic().AddAssignment(a);
        }
    }
}
