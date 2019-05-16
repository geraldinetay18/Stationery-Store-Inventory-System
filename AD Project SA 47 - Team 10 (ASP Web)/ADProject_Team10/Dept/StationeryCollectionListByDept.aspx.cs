/* Author: Nguyen Ngoc Doan Trang */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADProject_Team10.Models;
using ADProject_Team10.BizLogic;
using ADProject_Team10.Services;
using System.Globalization;

namespace ADProject_Team10.Dept
{
    public partial class StationeryCollectionListByDept : System.Web.UI.Page
    {
        SSAEntities ssae = new SSAEntities();
        IDisbursementService dService = new DisbursementService();
        IEmployeeService eService = new EmployeeService();
        AuthMgmtBizLogic authMgmt = new AuthMgmtBizLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(authMgmt.IsDeptRep(Context.User.Identity.Name)))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (!IsPostBack)
                {
                    // Only display disbursements associated with user's department
                    string deptId = eService.SearchEmployeeByEmpId((int)Session["employeeId"]).DeptId;
                    ViewState["deptId"] = deptId;

                    DataBind();
                    gvStationeryCollectionList.DataSource = StationeryCollectionLogic.getStationeryCollectionList(deptId).OrderByDescending(x => x.DisbursementId);
                    gvStationeryCollectionList.DataBind();

                    PopulateDropdownYears();
                }
            }
        }

        private void PopulateDropdownYears()
        {
            Disbursement first = dService.FindEarliestDisbursement();
            Disbursement last = dService.FindLatestDisbursement();

            // Default values if 0 records
            int lastYear = DateTime.Today.Year;
            int firstYear = lastYear - 3;

            if (first != null)
            {
                // 1 record
                if (first.DisbursementId == last.DisbursementId)
                {
                    if (first.DateDisbursed != null)
                        lastYear = ((DateTime)first.DateDisbursed).Year;
                    firstYear = lastYear - 3;
                }
                // multiple records
                else
                {
                    if (last.DateDisbursed != null)
                        lastYear = ((DateTime)last.DateDisbursed).Year;

                    if (first.DateDisbursed != null)
                        firstYear = ((DateTime)first.DateDisbursed).Year;

                    if (lastYear - firstYear < 3) // minimally 3 options
                        firstYear = lastYear - 3;
                }
            }
            // Creating the list
            List<int> yearsDisplay = new List<int>();
            for (int i = lastYear; i >= firstYear; i--) { yearsDisplay.Add(i); }

            //Binding to dropdownlist
            ddlSearchYear.DataSource = yearsDisplay;
            ddlSearchYear.DataBind();
        }

        protected void Disbursement_SelectedIndexChanged(object sender, EventArgs e)
        {
            String disbursementId = Convert.ToString(gvStationeryCollectionList.SelectedDataKey.Value);
            Response.Redirect("~/Dept/StationeryCollectionDetails.aspx?id=" + disbursementId);
        }

        protected void ddlSearchYear_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string deptId = (string)ViewState["deptId"];

            if (ddlSearchYear.Text == "" && tbSearchDate.Text == "")
            {
                gvStationeryCollectionList.DataSource = StationeryCollectionLogic.getStationeryCollectionList(deptId);
                gvStationeryCollectionList.DataBind();
            }

            if (ddlSearchYear.Text != "" && tbSearchDate.Text == "")
            {
                gvStationeryCollectionList.DataSource = StationeryCollectionLogic.getStationeryCollectionListByYear(ddlSearchYear.Text, deptId);
                gvStationeryCollectionList.DataBind();
            }

            if (ddlSearchYear.Text == "" && tbSearchDate.Text != "")
            {
                String dateString = tbSearchDate.Text;
                DateTime date = DateTime.Parse(dateString);
                gvStationeryCollectionList.DataSource = StationeryCollectionLogic.getStationeryCollectionListByDate(date, deptId);
                gvStationeryCollectionList.DataBind();
            }

            if (ddlSearchYear.Text != "" && tbSearchDate.Text != "")
            {
                String dateString = tbSearchDate.Text;
                DateTime date = DateTime.Parse(dateString);
                gvStationeryCollectionList.DataSource = StationeryCollectionLogic.getStationeryCollectionListByDate(date, deptId);
                gvStationeryCollectionList.DataBind();
            }
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            ddlSearchYear.SelectedIndex = 0;
            tbSearchDate.Text = "";
            gvStationeryCollectionList.DataSource = StationeryCollectionLogic.getStationeryCollectionList((string)ViewState["deptId"]).OrderByDescending(x => x.DisbursementId);
            gvStationeryCollectionList.DataBind();
        }
    }
}