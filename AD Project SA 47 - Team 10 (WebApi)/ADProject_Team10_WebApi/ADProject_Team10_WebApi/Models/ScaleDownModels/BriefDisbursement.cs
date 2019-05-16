/* Author: Lee Kai Seng (Kyler) */

using System;

namespace ADProject_Team10_WebApi.Models.ScaleDownModels
{
    public class BriefDisbursement
    {
        public BriefDisbursement() { }
        public BriefDisbursement(Disbursement dis, Employee rep, CollectionPoint cp)
        {
            DisbursementId = dis.DisbursementId.ToString();
            DeptId = dis.DeptId;
            DeptName = dis.Department.DeptName;
            DateDisbursed = (dis.DateDisbursed != null) ? ((DateTime)dis.DateDisbursed).ToString("dd-MM-yyyy") : "";
            CollectionPointId = cp.LocationId;
            CollectionPointName = cp.LocationName;
            CollectionPointTime = cp.Time.ToString();
            Status = dis.Status;
            RepresentativeId = dis.RepresentativeId.ToString();
            RepresentativeName = rep.EmployeeName;
            RepresentativePhone = rep.Phone;
        }
        public string DisbursementId { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
        public string DateDisbursed { get; set; }
        public string CollectionPointId { get; set; }
        public string CollectionPointName { get; set; }
        public string CollectionPointTime { get; set; }
        public string Status { get; set; }
        public string RepresentativeId { get; set; }
        public string RepresentativeName { get; set; }
        public string RepresentativePhone { get; set; }
    }
}