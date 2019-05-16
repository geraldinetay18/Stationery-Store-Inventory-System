/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10_WebApi.Models;
using ADProject_Team10_WebApi.Models.ScaleDownModels;
using ADProject_Team10_WebApi.Services;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace ADProject_Team10_WebApi.BizLogic
{
    public class ModifyCollectionPointBizLogic
    {
        ICollectionPointService icps = new CollectionPointService();
        IEmployeeService ies = new EmployeeService();
        IDepartmentService ids = new DepartmentService();

        public CollectionPointList FindCollectionById(string id)
        {
            CollectionPointList cpl = null;
            CollectionPoint cp = icps.GetCollectionPointById(id);
            if (cp != null)
            {
                cpl = new CollectionPointList()
                {
                    LocationId = cp.LocationId,
                    LocationName = cp.LocationName,
                    EmployeeId = cp.EmployeeId.ToString(),
                    Time = cp.Time.ToString()
                };
            }
            return cpl;
        }
        public DepartmentDetails FindDepartmentById(string id)
        {
            DepartmentDetails dd = null;
            Department d = ids.SearchDepartmentByDeptId(id);
            if (d != null)
            {
                dd = new DepartmentDetails()
                {
                    DeptId = d.DeptId,
                    LocationId = d.LocationId,
                    DeptName = d.DeptName,
                    Contact_Name = d.Contact_Name,
                    RepresentativeId = d.RepresentativeId.ToString(),
                    HeadId = d.HeadId.ToString(),
                    StoreClerkId = d.StoreClerkId.ToString(),
                    TelephoneNo = d.TelephoneNo,
                    Fax = d.Fax
                };
            }
            return dd;
        }

        public int UpdateCollectionPoint(string locationId, string deptId, string oldLocationId)
        {
            int i, j;
            i = ids.UpdateCollectionPoint(locationId, deptId);
            j = Mail(locationId, deptId, oldLocationId);
            if (i == 1 && j == 1)
            {
                return 1;
            }
            else if (i == 1 && j == 0)
            {
                return 2;
            }
            else
                return 0;
        }
        public List<CollectionPointList> GetAllCollectionPoint()
        {
            List<CollectionPointList> cpl = new List<CollectionPointList>();
            List<CollectionPoint> cp = icps.GetAllCollectionPoint();
            foreach (CollectionPoint c in cp)
            {
                CollectionPointList collection = new CollectionPointList();
                collection.EmployeeId = c.EmployeeId.ToString();
                collection.LocationId = c.LocationId;
                collection.LocationName = c.LocationName;
                collection.Time = c.Time.ToString();
                cpl.Add(collection);
            }
            return cpl;
        }
        public int Mail(string locationId, string deptId, string oldLocationId)
        {
            try
            {
                CollectionPoint currentloc = icps.GetCollectionPointById(oldLocationId);
                String clerkemail = ies.SearchEmployeeByEmpId(currentloc.EmployeeId).Email;
                string headname = ies.SearchEmployeeByEmpId(ids.SearchDepartmentByDeptId(deptId).HeadId).EmployeeName;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("adteam10superman@gmail.com", "!Passw0rd");
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage mm = new MailMessage("adteam10superman@gmail.com", clerkemail);
                mm.Subject = "Change of Collection Point";
                CollectionPoint newLocation = icps.GetCollectionPointById(locationId);
                string locname = newLocation.LocationName;
                mm.Body = "Department:\t" + ids.SearchDepartmentByDeptId(deptId).DeptName + "\nDepartment Head:\t" + headname
                        + "\nHas changed the location to\t" + locname;
                client.Send(mm);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}