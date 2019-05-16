/* Author: Nguyen Ngoc Doan Trang */

using ADProject_Team10_WebApi.BizLogic;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ADProject_Team10_WebApi.Controllers
{
    public class StationeryCollectionDetailsController : ApiController
    {
        //to get a Stationery Collection Details
        [Authorize]
        [HttpGet]
        [Route("api/StationeryCollection/{disbursementId}")]
        public StationeryCollectionDetails getStationeryCollectionDetails(String disbursementId)
        {
            return AuthStationeryCollectionBizLogic.getStationeryCollectionDetails(disbursementId);
        }

        [Authorize]
        [HttpGet]
        [Route("api/Stationery/{disbursementId}")]
        public IEnumerable<StationeryCollectionDetails> getStationeryCollectionDetailsLists(String disbursementId)
        {
            return AuthStationeryCollectionBizLogic.getStationeryCollectionDetailsLists(Convert.ToInt32(disbursementId));
        }

        //to update database when acknowledged
        [Authorize]
        [HttpGet]
        [Route("api/sc/acknowledge/{disbursementId}")]
        public void acknowledge(String disbursementId)
        {
            AuthStationeryCollectionBizLogic.acknowledge(disbursementId, null);
        }
    }
}
