using ADProject_Team10.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;

namespace ADProject_Team10.Account
{
    public partial class AssignPmtRole : System.Web.UI.Page
    {
        void GenerateRoles() // To generate a list of roles
        {
            ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            if (DbContext.Roles.ToList<IdentityRole>().Count == 0)
            {
                string[] roleName = { "Acting Department Head", "Department Head", "Department Representative", "Department Staff", "Store Clerk", "Store Manager", "Store Supervisor" };
                string[] roleId = { "ActHead", "DHead", "DRep", "DStaff", "SClerk", "SMan", "SSup" };
                for (int i = 0; i < roleName.Length; i++)
                {
                    IdentityRole iR = new IdentityRole(roleName[i]);
                    iR.Id = roleId[i];
                    DbContext.Roles.Add(iR);
                }
                DbContext.SaveChanges();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GenerateRoles();
                ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
                rblRoleList.DataSource = DbContext.Roles.ToList<IdentityRole>();
                rblRoleList.DataTextField = "Name";
                rblRoleList.DataValueField = "Name";
                rblRoleList.DataBind();
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            ApplicationDbContext DbContext = Context.GetOwinContext().Get<ApplicationDbContext>();
            string username = (string)gvStaffList.SelectedDataKey.Value;
            string roleId = rblRoleList.SelectedValue;
            IdentityUser user = DbContext.Users.FirstOrDefault
                                   (u => u.UserName.Equals(username,
                                    StringComparison.CurrentCultureIgnoreCase));
            ApplicationUserManager manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            manager.AddToRole(user.Id, roleId);
        }
    }
}