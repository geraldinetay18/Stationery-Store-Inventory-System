/* Author: Zhao Pengkai */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADProject_Team10.Services;

namespace ADProject_Team10.Store
{
    using Models;
    using System.Globalization;

    public class RequisitionTrend
    {
        public string Month { get; set; }
        public int Quantity { get; set; }
    }

    public class CompareRequisitionTrend
    {
        public string Month { get; set; }
        public int CompareQuantity { get; set; }
    }

    public partial class RequisitionTrendOnDepartment : System.Web.UI.Page
    {
        SSAEntities context = new SSAEntities();
        IStationeryService iss = new StationeryService();
        ICategoryService ics = new CategoryService();
        IDepartmentService ide = new DepartmentService();
        IDisbursementService idm = new DisbursementService();
        IDisbursementDetailsService idd = new DisbursementDetailsService();
        static string deptId1;
        static string deptId2;
        static string category;
        static int categoryId;
        static DateTime dts;
        static DateTime dte;
        static int startYear;
        static int startMonth;
        static int endYear;
        static int endMonth;
        static List<RequisitionTrend> requsitiontrendlist = new List<RequisitionTrend>();
        static List<CompareRequisitionTrend> comparerequisitiontrendlist = new List<CompareRequisitionTrend>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Manager"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                lbldep.Text = "";
                lblDateError.Text = "";
                lblTableTitle.Text = "";
                lblChartTitle.Text = "";
                Button1.Attributes.Add("onclick", "return confirm('Are you confirm to generate the graphs?');");
            }
        }

        protected void ddlDep1_SelectedIndexChanged(object sender, EventArgs e)
        {
            deptId1 = ddlDep1.SelectedItem.Text;
        }

        protected void ddlDep2_SelectedIndexChanged(object sender, EventArgs e)
        {
            deptId2 = ddlDep2.SelectedItem.Text;
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            category = ddlCategory.SelectedItem.Text;
            categoryId = ics.FindCategoryByFullName(category).CategoryId;
        }

        protected void ddlStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            startYear = Convert.ToInt32(ddlStartYear.SelectedItem.Text);
        }

        protected void ddlStartMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            startMonth = Convert.ToInt32(ddlStartMonth.SelectedItem.Text);
        }

        protected void ddlEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            endYear = Convert.ToInt32(ddlEndYear.SelectedItem.Text);
        }

        protected void ddlEndMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            endMonth = Convert.ToInt32(ddlEndMonth.SelectedItem.Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            deptId1 = ddlDep1.SelectedItem.Text;
            deptId2 = ddlDep2.SelectedItem.Text;
            category = ddlCategory.SelectedItem.Text;
            categoryId = ics.FindCategoryByFullName(category).CategoryId;
            startYear = Convert.ToInt32(ddlStartYear.SelectedItem.Text);
            startMonth = Convert.ToInt32(ddlStartMonth.SelectedItem.Text);
            endYear = Convert.ToInt32(ddlEndYear.SelectedItem.Text);
            endMonth = Convert.ToInt32(ddlEndMonth.SelectedItem.Text);

            if (deptId1 == deptId2)
            {
                lbldep.Text = "This will only display " + deptId1 + "'s data.";
                deptId2 = "None";
            }
            else
            {
                lbldep.Text = "";
            }
            string st = startYear.ToString() + "/" + startMonth.ToString() + "/01";
            string ed = endYear.ToString() + "/" + endMonth.ToString() + "/01";
            dts = Convert.ToDateTime(st);
            dte = Convert.ToDateTime(ed);
            if (DateTime.Compare(dts, dte) > 0)
            {
                lblDateError.Text = "End date can not before start date, please choose again!";
                PanelRequisitionTrend.Visible = false;
            }
            else
            {
                PanelRequisitionTrend.Visible = true;
                requsitiontrendlist.Clear();
                comparerequisitiontrendlist.Clear();

                //identify month amount
                lblDateError.Text = "";
                TimeSpan ts = dte - dts;
                double days = ts.Days;
                int monthamount = (int)Math.Floor((days + 2) / 30);

                //1. department1 data
                for (int k = 0; k < monthamount + 1; k++)
                {
                    DateTime dts11 = dts.AddDays(30 * k);
                    DateTime dts12 = dts.AddDays(30 * (k + 1));
                    List<Disbursement> d1 = context.Disbursements.Where(x => x.DateCreated.CompareTo(dts11) >= 0 && x.DateCreated.CompareTo(dts12) <= 0 && x.DeptId == deptId1).ToList<Disbursement>();
                    int sum1 = 0;
                    for (int i = 0; i < d1.Count(); i++)
                    {
                        List<DisbursementDetail> dd1 = idd.GetDisbursementDetailsByDisbursementId(d1.ElementAt(i).DisbursementId);
                        for (int j = 0; j < dd1.Count(); j++)
                        {
                            if (iss.FindStationeryById(dd1.ElementAt(j).ItemCode).CategoryId == categoryId)
                            {
                                sum1 = sum1 + dd1.ElementAt(j).QuantityRequested;
                            }
                        }
                    }
                    RequisitionTrend rt1 = new RequisitionTrend();
                    if (startMonth + k > 12)
                    {
                        DateTime d11 = new DateTime(startYear + 1, startMonth + k - 12, 1);
                        rt1.Month = string.Format("{0:MMM yyyy}", d11);
                    }
                    else
                    {
                        DateTime d10 = new DateTime(startYear, startMonth + k, 1);
                        rt1.Month = string.Format("{0:MMM yyyy}", d10);
                    }
                    rt1.Quantity = sum1;
                    requsitiontrendlist.Add(rt1);
                }

                //2. department2 data
                for (int k = 0; k < monthamount + 1; k++)
                {
                    DateTime dts21 = dts.AddDays(30 * k);
                    DateTime dts22 = dts.AddDays(30 * (k + 1));
                    List<Disbursement> d2 = context.Disbursements.Where(x => x.DateCreated.CompareTo(dts21) >= 0 && x.DateCreated.CompareTo(dts22) <= 0 && x.DeptId == deptId2).ToList<Disbursement>();
                    int sum2 = 0;
                    for (int i = 0; i < d2.Count(); i++)
                    {
                        List<DisbursementDetail> dd2 = idd.GetDisbursementDetailsByDisbursementId(d2.ElementAt(i).DisbursementId);
                        for (int j = 0; j < dd2.Count(); j++)
                        {
                            if (iss.FindStationeryById(dd2.ElementAt(j).ItemCode).CategoryId == categoryId)
                            {
                                sum2 = sum2 + dd2.ElementAt(j).QuantityRequested;
                            }
                        }
                    }
                    CompareRequisitionTrend rt2 = new CompareRequisitionTrend();
                    if (startMonth + k > 12)
                    {
                        DateTime d21 = new DateTime(startYear + 1, startMonth + k - 12, 1);
                        rt2.Month = string.Format("{0:MMM yyyy}", d21);
                    }
                    else
                    {
                        DateTime d20 = new DateTime(startYear, startMonth + k, 1);
                        rt2.Month = string.Format("{0:MMM yyyy}", d20);
                    }
                    rt2.CompareQuantity = sum2;
                    comparerequisitiontrendlist.Add(rt2);
                }


                //get graphs
                //1. table
                lblTableTitle.Text = "Requisition Trend Table";
                var query = from otl1 in requsitiontrendlist
                            join otl2 in comparerequisitiontrendlist on otl1.Month equals otl2.Month
                            select new { otl1.Month, otl1.Quantity, otl2.CompareQuantity };
                gvTable.Columns[1].HeaderText = deptId1 + "'s Quantity";
                gvTable.Columns[2].HeaderText = deptId2 + "'s Quantity";
                gvTable.DataSource = query;
                gvTable.DataBind();

                //2. bar chart
                lblChartTitle.Text = "Requisition Trend Chart";
                Chart1.Series["Series1"].XValueMember = "Month";
                Chart1.Series["Series1"].YValueMembers = "Quantity";
                Chart1.Series["Series2"].XValueMember = "Month";
                Chart1.Series["Series2"].YValueMembers = "CompareQuantity";
                Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Month Year";
                Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Total Item Amount";
                Chart1.Series["Series1"].Name = deptId1;
                Chart1.Series["Series2"].Name = deptId2;
                Chart1.DataSource = query;
                Chart1.DataBind();
            }
        }
    }
}
