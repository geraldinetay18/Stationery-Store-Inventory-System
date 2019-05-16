/* Author: Tran Thi Ngoc Thuy (Saphira) */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADProject_Team10.BizLogic;
using ADProject_Team10.Models;

namespace ADProject_Team10.Store
{
    public partial class DisbursmentList : System.Web.UI.Page
    {
        ReqRetrDisbBizLogic myBz = new ReqRetrDisbBizLogic();

        int storeClerkId;
        List<Disbursement> disbursements;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                storeClerkId = (int)Session["employeeId"];
                disbursements = myBz.FindDisbursementsByCollecionStoreClerkId(storeClerkId);
                disbursements.Sort((x, y) => x.Status.CompareTo(y.Status));
                PanelMessage.Visible = false;
                lblResult.Text = "";

                if (!IsPostBack)
                {
                    BindDDLCollectionPoint();
                    BindGVDisbursement();
                }
            }
        }
        protected void BindDDLCollectionPoint()
        {
            List<CollectionPoint> cPoints = myBz.FindCollectionPointByEmpId(storeClerkId);
            if (cPoints.Count > 1)
            {
                CollectionPoint all = new CollectionPoint();
                all.LocationId = "ALL";
                all.LocationName = "ALL";

                cPoints.Add(all);
                cPoints.Sort((i, j) => i.LocationName.CompareTo(j.LocationName));
            }
            ddlCollectionPoint.DataSource = cPoints;
            ddlCollectionPoint.DataTextField = "LocationName";
            ddlCollectionPoint.DataValueField = "LocationId";
            ddlCollectionPoint.DataBind();
            BindDDLDept(cPoints[0].LocationId);
        }
        protected void BindDDLDept(string cp)
        {
            List<Department> depts = new List<Department>();
            if (cp == "ALL")
                depts = myBz.FindDepartmentByCollectionStoreClerkId(storeClerkId);
            else
                depts = myBz.FindDepartmentByCollectionPointId(cp);

            if (depts.Count > 1)
            {
                Department all = new Department();
                all.DeptId = "ALL";
                all.DeptName = "ALL";

                depts.Add(all);
                depts.Sort((i, j) => i.DeptName.CompareTo(j.DeptName));
            }

            ddlDept.DataSource = depts;
            ddlDept.DataTextField = "DeptName";
            ddlDept.DataValueField = "DeptId";
            ddlDept.DataBind();
        }

        protected void BindGVDisbursement()
        {
            if (disbursements.Count > 0)
            {
                gvDisbursementList.DataSource = disbursements;
                gvDisbursementList.DataBind();
            }
            else
                PanelMessage.Visible = true;
        }

        //for gv
        protected Department GetDepartmentyDisbId(int disbId)
        {
            return myBz.FindDeptByDisbId(disbId);
        }
        protected Employee GetEmployeeById(int empId)
        {
            return myBz.FindEmpById(empId);
        }

        protected void gvDisbursementList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
            GridViewRow row = gvDisbursementList.Rows[rowIndex];
            HiddenField hDisbId = (HiddenField)row.FindControl("hDisId");

            if (e.CommandName == "seeDetails")
            {
                Response.Redirect($"~/Store/DisbursementDetails.aspx?id={hDisbId.Value}");
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            List<Disbursement> disbListGenerate = new List<Disbursement>();
            if (ddlDept.SelectedValue == "ALL")
            {
                if (ddlCollectionPoint.SelectedValue == "ALL")
                    disbListGenerate = checkDate(disbursements);
                else
                {
                    List<Disbursement> cpDisbList = myBz.FilterDisbsByCollectionPointId(disbursements, ddlCollectionPoint.SelectedValue);
                    disbListGenerate = checkDate(cpDisbList);
                }
            }
            else
            {
                List<Disbursement> disbForCp = myBz.FilterDisbsByDeptId(disbursements, ddlDept.SelectedValue);
                disbListGenerate = checkDate(disbForCp);
            }

            if (disbListGenerate.Count > 0)
            {
                gvDisbursementList.DataSource = disbListGenerate;
                gvDisbursementList.DataBind();
            }
            else
            {
                lblResult.Text = "No Disbursements with the matching criteria found.";
            }
        }

        protected List<Disbursement> checkDate(List<Disbursement> d)
        {
            List<Disbursement> disbListGenerate = new List<Disbursement>();
            if (tbFrom.Text != "" && tbFrom.Text != null)
            {
                if (tbTo.Text != "" && tbTo.Text != null)
                    disbListGenerate = myBz.FilterDisbsByTo(myBz.FilterDisbsByFrom(d, DateTime.Parse(tbFrom.Text)), DateTime.Parse(tbTo.Text));
                else
                    disbListGenerate = myBz.FilterDisbsByFrom(d, DateTime.Parse(tbFrom.Text));
            }
            else
            {
                if (tbTo.Text != "" && tbTo.Text != null)
                    disbListGenerate = myBz.FilterDisbsByTo(d, DateTime.Parse(tbFrom.Text));
                else
                    disbListGenerate = d;
            }
            return disbListGenerate;
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            ddlDept.SelectedValue = "ALL";
            tbFrom.Text = null;
            tbTo.Text = null;
            BindGVDisbursement();
        }

        protected void ddlCollectionPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDDLDept(ddlCollectionPoint.SelectedValue);
            btnGenerate_Click(sender, e);
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGenerate_Click(sender, e);
        }

        protected void gvDisbursementList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "DateDisbursed") == null) e.Row.Cells[0].Text = "None yet";
                if (DataBinder.Eval(e.Row.DataItem, "Remark") == null) e.Row.Cells[4].Text = "NIL";
                string s;
                switch (s = (string)DataBinder.Eval(e.Row.DataItem, "Status"))
                {
                    case "Allocated": e.Row.Cells[3].ForeColor = System.Drawing.Color.Red; break;
                    case "Waiting for Acknowledgement": e.Row.Cells[3].ForeColor = System.Drawing.Color.Orange; break;
                    case "Acknowledged": e.Row.Cells[3].ForeColor = System.Drawing.Color.Green; break;
                    default: e.Row.Cells[3].ForeColor = System.Drawing.Color.Gray; break;
                }
            }
        }
    }
}