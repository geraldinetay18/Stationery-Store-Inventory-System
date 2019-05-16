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
    public partial class RaisePurchaseOrder : System.Web.UI.Page
    {
        IOrderService io = new OrderService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                PanelMessage.Visible = false;
                if (!IsPostBack)
                {
                    List<Order> orderlist = io.FindAllOrders();

                    if (orderlist.Count > 0)
                    {
                        var query = from o in orderlist select new { o.PoNumber, o.SupplierCode, o.Status, o.DateOrdered, o.DateSupply };
                        gvOrderList.DataSource = query;
                        gvOrderList.DataBind();
                    }
                    else
                        PanelMessage.Visible = true;
                }
            }
        }

        protected void gvOrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["PONumber"] = gvOrderList.Rows[gvOrderList.SelectedIndex].Cells[0].Text;
            Session["SupplierCode"] = gvOrderList.Rows[gvOrderList.SelectedIndex].Cells[1].Text;
            Session["Status"] = gvOrderList.Rows[gvOrderList.SelectedIndex].Cells[2].Text;
            Session["OrderedDate"] = gvOrderList.Rows[gvOrderList.SelectedIndex].Cells[3].Text;
            Session["DateSupply"] = gvOrderList.Rows[gvOrderList.SelectedIndex].Cells[4].Text;
            Response.Redirect("~/Store/RaisePurchaseOrder_OrderDetails.aspx");
        }

        protected void btnCreateNewOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/RaisePurchaseOrder_NewOrder.aspx");
        }
    }
}