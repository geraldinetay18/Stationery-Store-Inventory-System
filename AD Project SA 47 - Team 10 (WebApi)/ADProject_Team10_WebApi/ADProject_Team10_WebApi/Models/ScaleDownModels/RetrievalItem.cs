/* Author: Lee Kai Seng (Kyler) */

namespace ADProject_Team10_WebApi.Models.ScaleDownModels
{
    public class RetrievalItem
    {
        public RetrievalItem() { }
        public RetrievalItem(RetrievalDetail rd, Stationery sn)
        {
            RetrievalDetailsId = rd.RetrievalDetailsId.ToString();
            ItemCode = rd.ItemCode;
            RetrievalId = rd.RetrievalId.ToString();
            QuantityRetrieved = rd.QuantityRetrieved.ToString();
            QuantityNeeded = rd.QuantityNeeded.ToString();
            Description = sn.Description;
            QuantityInStock = sn.QuantityInStock.ToString();
            Bin = sn.Bin.ToString();
        }

        public string RetrievalDetailsId { get; set; }

        public string ItemCode { get; set; }

        public string RetrievalId { get; set; }

        public string QuantityRetrieved { get; set; }

        public string QuantityNeeded { get; set; }

        public string Description { get; set; }

        public string QuantityInStock { get; set; }

        public string Bin { get; set; }
    }
}