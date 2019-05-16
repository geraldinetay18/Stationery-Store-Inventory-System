/* Author: Geraldine Tay */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace ADProject_Team10.BizLogic
{
    using Models;
    using Services;
    public class EmailBizLogic
    {
        ITemporaryRoleService it = new TemporaryRoleService();
        IEmployeeService ie = new EmployeeService();
        IDepartmentService id = new DepartmentService();
        public void SendEmail(Employee empToEmail, string subject, string body)
        {
            // Preparing email
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential("adteam10superman@gmail.com", "!Passw0rd");
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            // Setting To, Subject, Body
            MailMessage mm = new MailMessage("adteam10superman@gmail.com", empToEmail.Email);
            mm.Subject = subject;     
            mm.Body = "Dear " + empToEmail.EmployeeTitle + ". " + empToEmail.EmployeeName + ",\n\n" 
                             + body + "\n\nRegards,\nLogic University\nStationery Store";
            client.Send(mm);
        }

    }
}