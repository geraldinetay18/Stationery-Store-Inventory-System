/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10_WebApi.BizLogic;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using System.Collections.Generic;
using System.Web.Http;

namespace ADProject_Team10_WebApi.Controllers
{
    public class CollectionPointController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/Collection/Collectionlist")]
        public IEnumerable<CollectionPointList> GetCollectionPoints()
        {
            return new ModifyCollectionPointBizLogic().GetAllCollectionPoint();
        }

        [Authorize]
        [HttpGet]
        [Route("api/Collection/{id}")]
        public CollectionPointList GetCollectionById(string id)
        {
            return new ModifyCollectionPointBizLogic().FindCollectionById(id);
        }
    }
}
