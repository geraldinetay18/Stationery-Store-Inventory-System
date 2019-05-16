/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10.Models;
using ADProject_Team10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    public partial class AnnualCatalogUpdate : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (GridView1.Rows.Count == 0)
                {
                    Response.Write("<script>alert('Enter valid search text for searching!!')</script>");
                }
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/AddNewStationeryItem.aspx");
        }
    }
}
