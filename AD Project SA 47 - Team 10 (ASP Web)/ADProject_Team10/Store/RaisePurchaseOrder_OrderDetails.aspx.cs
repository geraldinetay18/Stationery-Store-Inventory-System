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
    public partial class RaisePurchaseOrder_OrderDetails : System.Web.UI.Page
    {
        IOrderService ios = new OrderService();
        IOrderDetailsService Iod = new OrderDetailService();
        IStationeryService iss = new StationeryService();
        ISupplierStationeryService isps = new SupplierStationeryService();
        IStockManagementService ism = new StockManagementService();
        static List<OrderDetail> orderDetailslist = new List<OrderDetail>();
        static int poNumber;
        static string supplierCode;
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
                    poNumber = Convert.ToInt32(Session["PONumber"]);
                    supplierCode = Session["SupplierCode"].ToString();
                    string status = Session["Status"].ToString();
                    string orderedDate = Session["OrderedDate"].ToString();
                    string ordersupply = Session["DateSupply"].ToString();
                    lblPONumber.Text = poNumber.ToString();
                    lblSupplierCode.Text = supplierCode;
                    lblStatus.Text = status;
                    lblDateofOrder.Text = orderedDate;
                    lblDateSupply.Text = ordersupply;
                    orderDetailslist = Iod.FindOrderDetailByPONumber(poNumber);
                    List<Stationery> stationerylist = iss.FindAllStationery();
                    List<SupplierStationery> supplierstationerylist = isps.FindStationeryDetailOfSupplier(supplierCode);
                    var query = from odl in orderDetailslist
                                join sta in stationerylist on odl.ItemCode equals sta.ItemCode
                                join sp in supplierstationerylist on odl.ItemCode equals sp.ItemCode
                                select new { odl.ItemCode, sta.Description, odl.Quantity, sp.UnitPrice, odl.Remark };
                    if (query.Count() == 0)
                    {
                        PanelOrderDetail.Visible = false;
                        lblMessage1.Text = "Nothing in this order!";
                        Panelcheck1.Visible = true;
                    }
                    else
                    {
                        Panelcheck1.Visible = false;
                        PanelOrderDetail.Visible = true;
                    }
                    gvOrderDetails.DataSource = query;
                    gvOrderDetails.DataBind();
                    double sum = 0;
                    for (int i = 0; i < query.Count(); i++)
                    {
                        sum = sum + query.ElementAt(i).Quantity * query.ElementAt(i).UnitPrice;
                    }
                    lblTotalAmount.Text = sum.ToString();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/RaisePurchaseOrder_OrderList.aspx");
        }

        protected void btnReceive_Click(object sender, EventArgs e)
        {
            if (orderDetailslist.Count > 0)
            {
                Order o1 = new Order();
                o1 = ios.FindOrderById(poNumber);
                if (o1.Status != "Delivered")
                {
                    int p = o1.PoNumber;
                    o1.DateDelivery = DateTime.Today;
                    o1.Status = "Delivered";
                    ios.UpdateOrder(o1);
                    for (int i = 0; i < orderDetailslist.Count(); i++)
                    {
                        Stationery s0 = new Stationery();
                        StockManagement sm0 = new StockManagement();
                        s0 = iss.FindStationeryById(orderDetailslist.ElementAt(i).ItemCode);
                        s0.QuantityInStock = s0.QuantityInStock + orderDetailslist.ElementAt(i).Quantity;

                        sm0.Date = DateTime.Today;
                        sm0.ItemCode = orderDetailslist.ElementAt(i).ItemCode;
                        sm0.StoreClerkId = (int)Session["employeeId"];
                        sm0.Source = "ORD";
                        sm0.SourceId = p;
                        sm0.QtyAdjusted = orderDetailslist.ElementAt(i).Quantity;
                        sm0.Balance = s0.QuantityInStock;
                        ism.AddStockManagement(sm0);

                        iss.UpdateStationery(s0);
                        Iod.UpdateOrderDetail(orderDetailslist.ElementAt(i));
                    }
                }
                else
                {
                    for (int i = 0; i < orderDetailslist.Count(); i++)
                    {
                        Iod.UpdateOrderDetail(orderDetailslist.ElementAt(i));
                    }
                }
                Response.Redirect("~/Store/RaisePurchaseOrder_OrderList.aspx");
            }
            else
            {
                Response.Write("<script>alert('No item to receive!');</script>");
                Response.Write("<script language=javascript>history.go(-1);</script>");
            }
        }

        protected void gvOrderDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOrderDetails.EditIndex = e.NewEditIndex;
            btnReceive.Visible = false;
            List<Stationery> stationerylist = iss.FindAllStationery();
            List<SupplierStationery> supplierstationerylist = isps.FindStationeryDetailOfSupplier(supplierCode);
            var query = from odl in orderDetailslist
                        join sta in stationerylist on odl.ItemCode equals sta.ItemCode
                        join sp in supplierstationerylist on odl.ItemCode equals sp.ItemCode
                        select new { odl.ItemCode, sta.Description, odl.Quantity, sp.UnitPrice, odl.Remark };
            gvOrderDetails.DataSource = query;
            gvOrderDetails.DataBind();
        }

        protected void gvOrderDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOrderDetails.EditIndex = -1;
            btnReceive.Visible = true;
            List<Stationery> stationerylist = iss.FindAllStationery();
            List<SupplierStationery> supplierstationerylist = isps.FindStationeryDetailOfSupplier(supplierCode);
            var query = from odl in orderDetailslist
                        join sta in stationerylist on odl.ItemCode equals sta.ItemCode
                        join sp in supplierstationerylist on odl.ItemCode equals sp.ItemCode
                        select new { odl.ItemCode, sta.Description, odl.Quantity, sp.UnitPrice, odl.Remark };
            gvOrderDetails.DataSource = query;
            gvOrderDetails.DataBind();
        }

        protected void gvOrderDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            gvOrderDetails.EditIndex = -1;
            TextBox txbRemark = (TextBox)gvOrderDetails.Rows[e.RowIndex].Cells[4].Controls[0];
            orderDetailslist.ElementAt(e.RowIndex).Remark = txbRemark.Text;
            btnReceive.Visible = true;
            List<Stationery> stationerylist = iss.FindAllStationery();
            List<SupplierStationery> supplierstationerylist = isps.FindStationeryDetailOfSupplier(supplierCode);
            var query = from odl in orderDetailslist
                        join sta in stationerylist on odl.ItemCode equals sta.ItemCode
                        join sp in supplierstationerylist on odl.ItemCode equals sp.ItemCode
                        select new { odl.ItemCode, sta.Description, odl.Quantity, sp.UnitPrice, odl.Remark };
            gvOrderDetails.DataSource = query;
            gvOrderDetails.DataBind();
        }
    }
}