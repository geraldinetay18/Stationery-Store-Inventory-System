/* Author: Lee Kai Seng (Kyler) */

namespace ADProject_Team10_WebApi.Models.ScaleDownModels
{
    public class BriefDisbursementDetails
    {
        public BriefDisbursementDetails() { }

        public BriefDisbursementDetails(DisbursementDetail dd, Stationery sn)
        {
            DisbursementDetailsId = dd.DisbursementDetailsId.ToString();
            ItemCode = dd.ItemCode;
            ItemDescription = sn.Description;
            QuantityRequested = dd.QuantityRequested.ToString();
            QuantityReceived = dd.QuantityReceived.ToString();
        }

        public string DisbursementDetailsId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string QuantityRequested { get; set; }
        public string QuantityReceived { get; set; }
    }
}