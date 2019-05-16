/* Author: Nguyen Ngoc Doan Trang */

using System;

namespace ADProject_Team10_WebApi.Models.ScaleDownModels
{
    public class StationeryCollectionDetails
    {
        private String collectionPoint;
        private String status;
        private String date;
        private String stationeryDescription;
        private int quantityNeed;
        private int quantityDisbursed;
        private String itemStatus;
        private String requestDate;

        public string CollectionPoint { get => collectionPoint; set => collectionPoint = value; }
        public string Status { get => status; set => status = value; }
        public string Date { get => date; set => date = value; }
        public string StationeryDescription { get => stationeryDescription; set => stationeryDescription = value; }
        public int QuantityNeed { get => quantityNeed; set => quantityNeed = value; }
        public int QuantityDisbursed { get => quantityDisbursed; set => quantityDisbursed = value; }
        public string ItemStatus { get => itemStatus; set => itemStatus = value; }
        public string RequestDate { get => requestDate; set => requestDate = value; }
    }
}