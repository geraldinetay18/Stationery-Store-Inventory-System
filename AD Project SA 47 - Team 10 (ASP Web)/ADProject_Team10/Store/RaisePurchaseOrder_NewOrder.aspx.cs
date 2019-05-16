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
    public partial class RaisePurchaseOrder_NewOrder : System.Web.UI.Page
    {
        IOrderDetailsService iod = new OrderDetailService();
        IStationeryService iss = new StationeryService();
        IOrderService io = new OrderService();
        ISupplierService isu = new SupplierService();
        ISupplierStationeryService isp = new SupplierStationeryService();
        static int poNumber;
        static int doNumber;
        static List<OrderDetail> orderdetailslist = new List<OrderDetail>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (!IsPostBack)
                {
                    SSAEntities context = new SSAEntities();
                    int count = context.Orders.Count();
                    count = count - 1;
                    List<Order> midlist = context.Orders.ToList<Order>();
                    poNumber = io.FindLastPoNumber() + 1;
                    doNumber = io.FindLastDoNumber() + 1;

                    lblPONumber.Text = poNumber.ToString();
                    gvSupplier.DataSource = isu.FindAllSuppliers();
                    gvSupplier.DataBind();
                    DateTime dt = DateTime.Today;
                    lblDateofOrder.Text = string.Format("{0:dd MMM yyyy}", dt);
                    lblSupplier.Text = "please choose a supplier...";
                    lblTotalAmount.Text = "0";
                    btnCancel.Attributes.Add("onclick", "return confirm('Are you sure that you want to cancel this order?');");
                    btnSubmit.Attributes.Add("onclick", "return confirm('Are you sure that you want to submit this order?');");
                    lblitemList.Text = "";
                    lblOrderDetails.Text = "";
                    lblMessage1.Text = "";
                }
            }
        }

        protected void gvItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblOrderDetails.Text = "Order Details";
            string itemcode = gvItemList.Rows[gvItemList.SelectedIndex].Cells[0].Text;
            Stationery newstationery = iss.FindStationeryById(itemcode);
            OrderDetail orderdetail = new OrderDetail();
            orderdetail.ItemCode = newstationery.ItemCode;
            orderdetail.PoNumber = poNumber;
            orderdetail.Quantity = newstationery.QuantityReorder;
            int count1 = 0;
            for (int i = 0; i < orderdetailslist.Count(); i++)
            {
                if (orderdetailslist.ElementAt(i).ItemCode == orderdetail.ItemCode)
                {
                    count1 = count1 + 1;
                }
            }
            if (count1 == 0)
            {
                orderdetailslist.Add(orderdetail);
            }
            List<Stationery> s = iss.FindAllStationery();
            List<SupplierStationery> ss3 = isp.FindStationeryDetailOfSupplier(lblSupplier.Text);
            if (orderdetailslist.Count() == 0)
            {
                PanelNewOrder.Visible = false;
            }
            else
            {
                PanelNewOrder.Visible = true;
            }
            var query = from odl in orderdetailslist
                        join sta in s on odl.ItemCode equals sta.ItemCode
                        join ss in ss3 on odl.ItemCode equals ss.ItemCode
                        select new { odl.ItemCode, sta.Description, odl.Quantity, ss.UnitPrice };
            gvNewOrder.DataSource = query;
            gvNewOrder.DataBind();
            double sum = 0;
            for (int i = 0; i < query.Count(); i++)
            {
                orderdetailslist.ElementAt(i).Quantity = query.ElementAt(i).Quantity;
                sum = sum + query.ElementAt(i).Quantity * query.ElementAt(i).UnitPrice;
            }
            lblTotalAmount.Text = sum.ToString();
        }

        protected void btnSubmit_Click2(object sender, EventArgs e)
        {
            if (orderdetailslist.Count() > 0)
            {
                Order orders = new Order();
                orders.PoNumber = poNumber;
                orders.DoNumber = doNumber;
                orders.SupplierCode = lblSupplier.Text;
                orders.DateOrdered = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                orders.OrderEmployeeId = (int)Session["employeeId"];
                orders.Status = "Ordered";
                orders.DateSupply = cldSupply.SelectedDate;

                if (DateTime.Compare(orders.DateOrdered, (DateTime)orders.DateSupply) < 0)
                {
                    io.AddOrder(orders);
                    poNumber = io.FindLastPoNumber();
                    for (int i = 0; i < orderdetailslist.Count(); i++)
                    {
                        orderdetailslist.ElementAt(i).PoNumber = poNumber;
                        iod.AddOrderDetail(orderdetailslist.ElementAt(i));
                    }
                    orderdetailslist.Clear();
                    Response.Redirect("~/Store/RaisePurchaseOrder_OrderList.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Please choose a right date when items should be supply!');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('No item in order!');</script>");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            orderdetailslist.Clear();
            Response.Redirect("~/Store/RaisePurchaseOrder_OrderList.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<Stationery> stationerylist = iss.FindStationeryByName(txbSearch.Text);

            for (int i = 0; i < stationerylist.Count(); i++)
            {
                if (stationerylist.ElementAt(i).QuantityInStock <= stationerylist.ElementAt(i).ReorderLevel)
                {
                    stationerylist.ElementAt(i).AdjustmentRemark = "Still Enough";
                }
            }

            List<SupplierStationery> ss2 = isp.FindStationeryDetailOfSupplier(lblSupplier.Text);
            var q2 = from ss in ss2
                     join st in stationerylist on ss.ItemCode equals st.ItemCode
                     select new { ss.ItemCode, st.Description, st.AdjustmentRemark, ss.SupplierRank, ss.UnitPrice };
            if (stationerylist.Count() == 0)
            {
                PanelItemList.Visible = false;
                lblMessage1.Text = "There are no matching items!";
                Panelcheck1.Visible = true;
            }
            else
            {
                Panelcheck1.Visible = false;
                PanelItemList.Visible = true;
                if (orderdetailslist.Count() == 0)
                {
                    PanelNewOrder.Visible = false;
                }
                else
                {
                    PanelNewOrder.Visible = true;
                }
            }
            gvItemList.DataSource = q2;
            gvItemList.DataBind();
        }

        protected void gvNewOrder_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNewOrder.EditIndex = e.NewEditIndex;
            List<Stationery> s = iss.FindAllStationery();
            List<SupplierStationery> ss3 = isp.FindStationeryDetailOfSupplier(lblSupplier.Text);
            var query = from odl in orderdetailslist
                        join sta in s on odl.ItemCode equals sta.ItemCode
                        join ss in ss3 on odl.ItemCode equals ss.ItemCode
                        select new { odl.ItemCode, sta.Description, odl.Quantity, ss.UnitPrice };
            gvNewOrder.DataSource = query;
            gvNewOrder.DataBind();
            double sum = 0;
            for (int i = 0; i < query.Count(); i++)
            {
                orderdetailslist.ElementAt(i).Quantity = query.ElementAt(i).Quantity;
                sum = sum + query.ElementAt(i).Quantity * query.ElementAt(i).UnitPrice;
            }
            lblTotalAmount.Text = sum.ToString();
            btnSubmit.Visible = false;
        }

        protected void gvNewOrder_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNewOrder.EditIndex = -1;
            btnSubmit.Visible = true;
            List<Stationery> s = iss.FindAllStationery();
            List<SupplierStationery> ss3 = isp.FindStationeryDetailOfSupplier(lblSupplier.Text);
            var query = from odl in orderdetailslist
                        join sta in s on odl.ItemCode equals sta.ItemCode
                        join ss in ss3 on odl.ItemCode equals ss.ItemCode
                        select new { odl.ItemCode, sta.Description, odl.Quantity, ss.UnitPrice };
            gvNewOrder.DataSource = query;
            gvNewOrder.DataBind();
            double sum = 0;
            for (int i = 0; i < query.Count(); i++)
            {
                orderdetailslist.ElementAt(i).Quantity = query.ElementAt(i).Quantity;
                sum = sum + query.ElementAt(i).Quantity * query.ElementAt(i).UnitPrice;
            }
            lblTotalAmount.Text = sum.ToString();
        }

        protected void gvNewOrder_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            gvNewOrder.EditIndex = -1;
            btnSubmit.Visible = true;
            TextBox txbQuantity = (TextBox)gvNewOrder.Rows[e.RowIndex].Cells[2].Controls[0];
            try
            {
                if (Convert.ToInt32(txbQuantity.Text) > 0)
                {
                    orderdetailslist.ElementAt(e.RowIndex).Quantity = Convert.ToInt32(txbQuantity.Text);
                    List<Stationery> s = iss.FindAllStationery();
                    List<SupplierStationery> ss3 = isp.FindStationeryDetailOfSupplier(lblSupplier.Text);
                    var query = from odl in orderdetailslist
                                join sta in s on odl.ItemCode equals sta.ItemCode
                                join ss in ss3 on odl.ItemCode equals ss.ItemCode
                                select new { odl.ItemCode, sta.Description, odl.Quantity, ss.UnitPrice };
                    gvNewOrder.DataSource = query;
                    gvNewOrder.DataBind();
                    double sum = 0;
                    for (int i = 0; i < query.Count(); i++)
                    {
                        orderdetailslist.ElementAt(i).Quantity = query.ElementAt(i).Quantity;
                        sum = sum + query.ElementAt(i).Quantity * query.ElementAt(i).UnitPrice;
                    }
                    lblTotalAmount.Text = sum.ToString();
                }
                else
                {
                    Response.Write("<script>alert('Your input is not an right integer, please check and modify!');</script>");
                    Response.Write("<script language=javascript>history.go(-1);</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Your input is not an right integer, please check and modify!');</script>");
                Response.Write("<script language=javascript>history.go(-1);</script>");

            }
        }

        protected void gvNewOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Index = e.RowIndex;
            orderdetailslist.RemoveAt(Index);
            if (orderdetailslist.Count() == 0)
            {
                PanelNewOrder.Visible = false;
            }
            else
            {
                PanelNewOrder.Visible = true;
            }
            List<Stationery> s = iss.FindAllStationery();
            List<SupplierStationery> ss3 = isp.FindStationeryDetailOfSupplier(lblSupplier.Text);
            var query = from odl in orderdetailslist
                        join sta in s on odl.ItemCode equals sta.ItemCode
                        join ss in ss3 on odl.ItemCode equals ss.ItemCode
                        select new { odl.ItemCode, sta.Description, odl.Quantity, ss.UnitPrice };
            gvNewOrder.DataSource = query;
            gvNewOrder.DataBind();
            double sum = 0;
            for (int i = 0; i < query.Count(); i++)
            {
                orderdetailslist.ElementAt(i).Quantity = query.ElementAt(i).Quantity;
                sum = sum + query.ElementAt(i).Quantity * query.ElementAt(i).UnitPrice;
            }
            lblTotalAmount.Text = sum.ToString();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelSearch.Visible = true;
            orderdetailslist.Clear();
            lblitemList.Text = "Item List";
            lblOrderDetails.Text = "";
            string supplierCode = gvSupplier.Rows[gvSupplier.SelectedIndex].Cells[0].Text;
            List<Stationery> st1 = iss.FindAllStationery();

            for (int i = 0; i < st1.Count(); i++)
            {
                if (st1.ElementAt(i).QuantityInStock <= st1.ElementAt(i).ReorderLevel)
                {
                    st1.ElementAt(i).AdjustmentRemark = "Low Stock";
                }
                else
                {
                    st1.ElementAt(i).AdjustmentRemark = "Still Enough";
                }
            }

            List<SupplierStationery> ss1 = isp.FindStationeryDetailOfSupplier(supplierCode);
            if (ss1.Count() == 0)
            {
                PanelItemList.Visible = false;
                lblMessage1.Text = "There's no item in this supplier!";
                Panelcheck1.Visible = true;
            }
            else
            {
                Panelcheck1.Visible = false;
                PanelItemList.Visible = true;
                if (orderdetailslist.Count() == 0)
                {
                    PanelNewOrder.Visible = false;
                }
                else
                {
                    PanelNewOrder.Visible = true;
                }
            }
            var q1 = from ss in ss1
                     join st in st1 on ss.ItemCode equals st.ItemCode
                     select new { ss.ItemCode, st.Description, st.AdjustmentRemark, ss.SupplierRank, ss.UnitPrice };
            gvItemList.DataSource = q1;
            gvItemList.DataBind();
            lblSupplier.Text = supplierCode;

            List<Stationery> s = iss.FindAllStationery();
            List<SupplierStationery> ss3 = isp.FindStationeryDetailOfSupplier(lblSupplier.Text);
            var query = from odl in orderdetailslist
                        join sta in s on odl.ItemCode equals sta.ItemCode
                        join ss in ss3 on odl.ItemCode equals ss.ItemCode
                        select new { odl.ItemCode, sta.Description, odl.Quantity, ss.UnitPrice };
            gvNewOrder.DataSource = query;
            gvNewOrder.DataBind();
            double sum = 0;
            for (int i = 0; i < query.Count(); i++)
            {
                orderdetailslist.ElementAt(i).Quantity = query.ElementAt(i).Quantity;
                sum = sum + query.ElementAt(i).Quantity * query.ElementAt(i).UnitPrice;
            }
            lblTotalAmount.Text = sum.ToString();
        }

        protected void gvItemList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = (string)DataBinder.Eval(e.Row.DataItem, "AdjustmentRemark");
                if (status == "Low Stock")
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}