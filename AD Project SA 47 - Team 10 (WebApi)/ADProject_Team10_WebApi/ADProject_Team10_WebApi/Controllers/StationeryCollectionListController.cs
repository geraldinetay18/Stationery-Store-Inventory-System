/* Author: Nguyen Ngoc Doan Trang */

using ADProject_Team10_WebApi.BizLogic;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ADProject_Team10_WebApi.Controllers
{
    public class StationeryCollectionListController : ApiController
    {
        //to get a StationeryCollectionList of the Department of the Dept Rep Login
        [Authorize]
        [HttpGet]
        [Route("api/StationeryCollection/{year}/{deptId}")]
        public IEnumerable<StationeryCollectionList> getStationeryCollectionListByYear(String year, String deptId)
        {
            return AuthStationeryCollectionBizLogic.getStationeryCollectionListByYear(year, deptId);
        }
    }
}
