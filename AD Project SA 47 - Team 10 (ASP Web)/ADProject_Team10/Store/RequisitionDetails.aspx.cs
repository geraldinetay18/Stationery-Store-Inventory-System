/* Author: Nguyen Ngoc Doan Trang */

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
    public partial class DeptRequisitionDetails : System.Web.UI.Page
    {
        ReqRetrDisbBizLogic myBz = new ReqRetrDisbBizLogic();
        Requisition req;
        List<RequisitionDetail> reqDetails;
        int reqId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                if (Request.QueryString["id"] != null)
                {
                    if (!IsPostBack)
                    {
                        reqId = Int32.Parse(Request.QueryString["id"]);
                        lblReqId.Text = "RQ" + reqId.ToString();
                        lblEmployeName.Text = myBz.FindEmpNameByReqId(reqId).EmployeeName;
                        lblDeptName.Text = myBz.FindDeptByReqId(reqId).DeptName;
                        req = myBz.FindReqById(reqId);
                        lblReqStatus.Text = req.RequisitionStatus;

                        BindGrid();
                    }
                }
                else
                    Response.Redirect("~/Store/RequisitionsList.aspx");
            }
        }

        protected void BindGrid()
        {
            reqDetails = myBz.FindAllReqDetByReqId(reqId);
            gvReqDetails.DataSource = reqDetails;
            gvReqDetails.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/RequisitionsList.aspx");
        }

        protected Stationery GetStationery(string itemCode)
        {
            return myBz.FindStationeryByItemCode(itemCode);
        }
    }
}