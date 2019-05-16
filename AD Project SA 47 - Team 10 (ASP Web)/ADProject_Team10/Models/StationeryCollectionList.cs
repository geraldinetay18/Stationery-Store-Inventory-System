/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADProject_Team10.Models
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