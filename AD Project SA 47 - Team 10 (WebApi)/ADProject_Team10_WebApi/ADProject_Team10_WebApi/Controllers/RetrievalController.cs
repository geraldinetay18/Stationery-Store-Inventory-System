/* Author: Tran Thi Ngoc Thuy (Saphira) */

using ADProject_Team10_WebApi.BizLogic;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ADProject_Team10_WebApi.Controllers
{
    public class RetrievalController : ApiController
    {
        [Authorize]
        public IEnumerable<RetrievalItem> GetRetrievalItems(string id)
        {
            return new ReqRetrDisbBizLogic().GetRetrievalItems(Convert.ToInt32(id));
        }

        [Authorize]
        [HttpGet]
        [Route("api/Retrieval/update/{retrId}/{itemCode}/{qtyRetrieved}/{raiseSAVQty}/{reason}/{storeClerkId}")]
        public int UpdateRetDetRaiseSAV(string retrId, string itemCode, string qtyRetrieved, string raiseSAVQty, string reason, string storeClerkId)
        {
            return new ReqRetrDisbBizLogic().UpdateRetDetRaiseSAV(Convert.ToInt32(retrId), itemCode, Convert.ToInt32(qtyRetrieved), Convert.ToInt32(raiseSAVQty), reason, Convert.ToInt32(storeClerkId));
        }

        [Authorize]
        [HttpGet]
        [Route("api/Retrieval/confirm/{storeClerkId}/{retrId}")]
        public int ConfirmRetrieval(string storeClerkId, string retrId)
        {
            return new ReqRetrDisbBizLogic().ConfirmRetrieval(Convert.ToInt32(storeClerkId), Convert.ToInt32(retrId));
        }
    }
}
