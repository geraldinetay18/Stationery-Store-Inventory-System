/* Author: Tran Thi Ngoc Thuy (Saphira) */

using ADProject_Team10_WebApi.BizLogic;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ADProject_Team10_WebApi.Controllers
{
    public class DisbursementController : ApiController
    {
        [Authorize]
        public IEnumerable<BriefDisbursement> GetBriefDisbursements(string id)
        {
            return new ReqRetrDisbBizLogic().GetBriefDisbursements(Convert.ToInt32(id));
        }

        [Authorize]
        [HttpGet]
        [Route("api/Disbursement/details/{disbursementId}")]
        public IEnumerable<BriefDisbursementDetails> GetBriefDisbursementDetailsList(string disbursementId)
        {
            return new ReqRetrDisbBizLogic().GetBriefDisbursementDetailsList(Convert.ToInt32(disbursementId));
        }

        [Authorize]
        [HttpGet]
        [Route("api/Disbursement/details/save/{storeClerkId}/{disbursementDetailId}/{receivedQty}/{SAVQty}/{reason}")]
        public int SaveEachDisbursingDetail(string storeClerkId, string disbursementDetailId, string receivedQty, string SAVQty, string reason)
        {
            return new ReqRetrDisbBizLogic().SaveEachDisbursingDetail(Convert.ToInt32(storeClerkId), Convert.ToInt32(disbursementDetailId), Convert.ToInt32(receivedQty), Convert.ToInt32(SAVQty), reason);
        }

        [Authorize]
        [HttpGet]
        [Route("api/Disbursement/notcollected/{storeClerkId}/{currentDisbId}")]
        public int MarkAsNotCollected(string storeClerkId, string currentDisbId)
        {
            return new ReqRetrDisbBizLogic().MarkAsNotCollected(Convert.ToInt32(storeClerkId), Convert.ToInt32(currentDisbId));
        }

        [Authorize]
        [HttpGet]
        [Route("api/Disbursement/acknowledge/{storeClerkId}/{currentDisbId}")]
        public int RequestForAcknowledgement(string storeClerkId, string currentDisbId)
        {
            return new ReqRetrDisbBizLogic().RequestForAcknowledgement(Convert.ToInt32(storeClerkId), Convert.ToInt32(currentDisbId));
        }
    }
}
