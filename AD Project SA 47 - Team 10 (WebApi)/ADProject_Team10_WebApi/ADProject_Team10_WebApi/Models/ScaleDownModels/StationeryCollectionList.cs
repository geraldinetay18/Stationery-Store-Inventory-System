/* Author: Nguyen Ngoc Doan Trang */

using System;

namespace ADProject_Team10_WebApi.Models.ScaleDownModels
{
    public class StationeryCollectionList
    {
        private int no;

        private int disbursementId;

        private String dateOfDisbursement;

        private String status;

        private String remark;

        public int No { get => no; set => no = value; }
        public string DateOfDisbursement { get => dateOfDisbursement; set => dateOfDisbursement = value; }
        public string Status { get => status; set => status = value; }
        public string Remark { get => remark; set => remark = value; }
        public int DisbursementId { get => disbursementId; set => disbursementId = value; }
    }
}