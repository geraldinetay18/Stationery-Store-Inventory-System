/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10_WebApi.BizLogic;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using System.Web.Http;

namespace ADProject_Team10_WebApi.Controllers
{
    public class DepartmentController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/Department/{id}")]
        public DepartmentDetails GetDepartmentById(string id)
        {
            return new ModifyCollectionPointBizLogic().FindDepartmentById(id);
        }

        [Authorize]
        [HttpGet]
        [Route("api/Department/updatecollection/{locationId}/{deptId}/{oldLocationId}")]
        public int UpdateCollectionPoint(string locationId, string deptId, string oldLocationId)
        {
            return new ModifyCollectionPointBizLogic().UpdateCollectionPoint(locationId, deptId, oldLocationId);
        }
    }
}
