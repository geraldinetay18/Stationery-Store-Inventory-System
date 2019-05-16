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
    public class OrderTrend
    {
        public string Month { get; set; }
        public int Quantity { get; set; }
    }

    public class CompareOrderTrend
    {
        public string Month { get; set; }
        public int CompareQuantity { get; set; }
    }

    public partial class OrderTrendOnCategory : System.Web.UI.Page
    {
        SSAEntities context = new SSAEntities();
        IStationeryService iss = new StationeryService();
        ICategoryService ics = new CategoryService();
        ISupplierService isu = new SupplierService();
        IOrderService ios = new OrderService();
        IOrderDetailsService iod = new OrderDetailService();
        static string supplierCode1;
        static string supplierCode2;
        static string category;
        static int categoryId;
        static DateTime dts;
        static DateTime dte;
        static int startYear;
        static int startMonth;
        static int endYear;
        static int endMonth;
        static List<OrderTrend> ordertrendlist1 = new List<OrderTrend>();
        static List<CompareOrderTrend> ordertrendlist2 = new List<CompareOrderTrend>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Manager"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                lblSupplierError.Text = "";
                lblDateError.Text = "";
                lblTableTiltle.Text = "";
                lblChartTitle.Text = "";
                btnGenerate.Attributes.Add("onclick", "return confirm('Are you confirm to generate the graphs?');");
            }
        }

        //to generete graph by click btn
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            supplierCode1 = ddlSupplier1.SelectedItem.Text;
            supplierCode2 = ddlSupplier2.SelectedItem.Text;
            category = ddlCategory.SelectedItem.Text;
            categoryId = ics.FindCategoryByFullName(category).CategoryId;
            startYear = Convert.ToInt32(ddlStartYear.SelectedItem.Text);
            startMonth = Convert.ToInt32(ddlStartMonth.SelectedItem.Text);
            endYear = Convert.ToInt32(ddlEndYear.SelectedItem.Text);
            endMonth = Convert.ToInt32(ddlEndMonth.SelectedItem.Text);


            if (supplierCode1 == supplierCode2)
            {
                lblSupplierError.Text = "This will only display " + supplierCode1 + "'s data.";
                supplierCode2 = "None";
            }
            else
            {
                lblSupplierError.Text = "";
            }
            string st = startYear.ToString() + "/" + startMonth.ToString() + "/01";
            string ed = endYear.ToString() + "/" + endMonth.ToString() + "/01";
            dts = Convert.ToDateTime(st);
            dte = Convert.ToDateTime(ed);
            if (DateTime.Compare(dts, dte) > 0)
            {
                lblDateError.Text = "End date can not before start date, please choose again!";
                PanelOrderTrend.Visible = false;
            }
            else
            {
                PanelOrderTrend.Visible = true;
                ordertrendlist1.Clear();
                ordertrendlist2.Clear();

                //identify month amount
                lblDateError.Text = "";
                TimeSpan ts = dte - dts;
                double days = ts.Days;
                int monthamount = (int)Math.Floor((days + 2) / 30);

                //1. supplier1 data
                for (int k = 0; k < monthamount + 1; k++)
                {
                    DateTime dts11 = dts.AddDays(30 * k);
                    DateTime dts12 = dts.AddDays(30 * (k + 1));
                    List<Order> o1 = context.Orders.Where(x => x.DateOrdered.CompareTo(dts11) >= 0 && x.DateOrdered.CompareTo(dts12) <= 0 && x.SupplierCode == supplierCode1).ToList<Order>();
                    int sum1 = 0;
                    for (int i = 0; i < o1.Count(); i++)
                    {
                        List<OrderDetail> od1 = iod.FindOrderDetailByPONumber(o1.ElementAt(i).PoNumber);
                        for (int j = 0; j < od1.Count(); j++)
                        {
                            if (iss.FindStationeryById(od1.ElementAt(j).ItemCode).CategoryId == categoryId)
                            {
                                sum1 = sum1 + od1.ElementAt(j).Quantity;
                            }
                        }
                    }
                    OrderTrend ot1 = new OrderTrend();
                    if (startMonth + k > 12)
                    {
                        DateTime d11 = new DateTime(startYear + 1, startMonth + k - 12, 1);
                        ot1.Month = string.Format("{0:MMM yyyy}", d11);
                    }
                    else
                    {
                        DateTime d10 = new DateTime(startYear, startMonth + k, 1);
                        ot1.Month = string.Format("{0:MMM yyyy}", d10);
                    }
                    ot1.Quantity = sum1;
                    ordertrendlist1.Add(ot1);
                }

                //2. supplier2 data
                for (int k = 0; k < monthamount + 1; k++)
                {
                    DateTime dts21 = dts.AddDays(30 * k);
                    DateTime dts22 = dts.AddDays(30 * (k + 1));
                    List<Order> o2 = context.Orders.Where(x => x.DateOrdered.CompareTo(dts21) >= 0 && x.DateOrdered.CompareTo(dts22) <= 0 && x.SupplierCode == supplierCode2).ToList<Order>();
                    int sum2 = 0;
                    for (int i = 0; i < o2.Count(); i++)
                    {
                        List<OrderDetail> od2 = iod.FindOrderDetailByPONumber(o2.ElementAt(i).PoNumber);
                        for (int j = 0; j < od2.Count(); j++)
                        {
                            if (iss.FindStationeryById(od2.ElementAt(j).ItemCode).CategoryId == categoryId)
                            {
                                sum2 = sum2 + od2.ElementAt(j).Quantity;
                            }
                        }
                    }
                    CompareOrderTrend ot2 = new CompareOrderTrend();
                    if (startMonth + k > 12)
                    {
                        DateTime d21 = new DateTime(startYear + 1, startMonth + k - 12, 1);
                        ot2.Month = string.Format("{0:MMM yyyy}", d21);
                    }
                    else
                    {
                        DateTime d20 = new DateTime(startYear, startMonth + k, 1);
                        ot2.Month = string.Format("{0:MMM yyyy}", d20);
                    }
                    ot2.CompareQuantity = sum2;
                    ordertrendlist2.Add(ot2);
                }


                //get graphs
                //1. table
                lblTableTiltle.Text = "Order Trend Table";
                var query = from otl1 in ordertrendlist1
                            join otl2 in ordertrendlist2 on otl1.Month equals otl2.Month
                            select new { otl1.Month, otl1.Quantity, otl2.CompareQuantity };
                gvTable.Columns[1].HeaderText = supplierCode1 + "'s Quantity";
                gvTable.Columns[2].HeaderText = supplierCode2 + "'s Quantity";
                gvTable.DataSource = query;
                gvTable.DataBind();

                //2. bar chart
                lblChartTitle.Text = "Order Trend Chart";
                Chart1.Series["Series1"].XValueMember = "Month";
                Chart1.Series["Series1"].YValueMembers = "Quantity";
                Chart1.Series["Series2"].XValueMember = "Month";
                Chart1.Series["Series2"].YValueMembers = "CompareQuantity";
                Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Year-Month";
                Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Total Item Amount";
                Chart1.Series["Series1"].Name = supplierCode1;
                Chart1.Series["Series2"].Name = supplierCode2;
                Chart1.DataSource = query;
                Chart1.DataBind();
            }
        }

        //identify dropdownlist value
        protected void ddlSupplier1_SelectedIndexChanged(object sender, EventArgs e)
        {
            supplierCode1 = ddlSupplier1.SelectedItem.Text;
        }

        protected void ddlSupplier2_SelectedIndexChanged(object sender, EventArgs e)
        {
            supplierCode2 = ddlSupplier2.SelectedItem.Text;
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

    }
}