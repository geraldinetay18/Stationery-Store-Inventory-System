/* Author: Nguyen Ngoc Doan Trang */

using ADProject_Team10.BizLogic;
using ADProject_Team10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Dept
{
    public partial class StationeryCollectionDetails : System.Web.UI.Page
    {
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
                    String disbursementId = Request.QueryString["id"];

                    DataBind();
                    gvSCDetails.DataSource = StationeryCollectionLogic.getStationeryCollectionDetailsLists(Convert.ToInt32(disbursementId));
                    gvSCDetails.DataBind();

                    Disbursement disbursement = StationeryCollectionLogic.getFirstDisbursement(disbursementId);
                    string dateDisbursed = disbursement.DateDisbursed.HasValue ? disbursement.DateDisbursed.Value.ToString("dd MMM yyyy") : "<not available>";
                    lblMaintainStockCard1.Text = dateDisbursed;
                    lblMaintainStockCard2.Text = disbursement.Status;

                    if (disbursement.Status == "Waiting for Acknowledgement")
                    {
                        lbRemark.Visible = false;
                    }
                    else
                    {
                        txtRemark.Visible = false;
                        lbRemark.Text = disbursement.Remark;
                        Acknowledge.Visible = false;
                    }
                }
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dept/StationeryCollectionListByDept.aspx");
        }

        protected void Acknowledge_Click(object sender, EventArgs e)
        {
            String disbursementId = Request.QueryString["id"];
            String remark = txtRemark.Text;

            StationeryCollectionLogic.acknowledge(disbursementId, remark);

            Response.Redirect("~/Dept/StationeryCollectionListByDept.aspx");
        }
    }
}