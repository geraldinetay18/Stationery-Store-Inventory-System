/* Author: Tran Thi Ngoc Thuy (Saphira) */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ADProject_Team10.Models;
using ADProject_Team10.BizLogic;

namespace ADProject_Team10.Store
{
    public partial class GenerateRetrieval : System.Web.UI.Page
    {
        ReqRetrDisbBizLogic myBz = new ReqRetrDisbBizLogic();

        List<Retrieval> reList;
        int storeClerkId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
            else
            {
                PanelMessage.Visible = false;
                lblResult.Text = "";

                storeClerkId = (int)Session["employeeId"];
                reList = myBz.FindAllRetrievals();
                reList.Sort((x, y) => y.DateRetrieved.CompareTo(x.DateRetrieved));

                if (!IsPostBack)
                {
                    if (reList.Count > 0)
                    {
                        gvRetrievalList.DataSource = reList;
                        gvRetrievalList.DataBind();
                        if (HaveExistingRetrieval())
                        {
                            btnMoveToRetrieval.Visible = true;
                        }
                    }
                    else
                    {
                        PanelMessage.Visible = true;
                    }

                    BindDDL();
                }
            }
        }

        protected void BindDDL()
        {
            //employee
            List<Employee> es = myBz.FindEmployeesByRole("Store Clerk");
            if (es.Count > 1)
            {
                Employee all = new Employee();
                all.EmployeeId = 0;
                all.EmployeeName = "ALL";

                es.Add(all);
                es.Sort((i, j) => i.EmployeeId.CompareTo(j.EmployeeId));
            }

            ddlRetrievalBy.DataSource = es;
            ddlRetrievalBy.DataTextField = "EmployeeName";
            ddlRetrievalBy.DataValueField = "EmployeeId";
            ddlRetrievalBy.DataBind();

            //status
            List<string> statusList = new List<string>();
            statusList.Add("ALL");

            statusList.AddRange(myBz.FindRetrievalStatusList());
            ddlStatus.DataSource = statusList;
            ddlStatus.DataBind();
        }
        protected void gvRetrievalList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gv = gvRetrievalList.SelectedRow;
            string retrievalId = gv.Cells[1].Text;
            Response.Redirect($"~/Store/RetrievalDetails.aspx?id={retrievalId}");
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            List<Retrieval> retrListGenerate = new List<Retrieval>();
            if (ddlRetrievalBy.SelectedValue == "0")
            {
                if (ddlStatus.SelectedValue == "ALL")
                    retrListGenerate = checkDate(reList);
                else
                {
                    List<Retrieval> statusRetrList = myBz.FilterRetrByStatus(reList, ddlStatus.SelectedValue);
                    retrListGenerate = checkDate(statusRetrList);
                }
            }
            else
            {
                List<Retrieval> storeClerkRetrList = myBz.FilterRetrsByStoreClerkId(reList, Int32.Parse(ddlRetrievalBy.SelectedValue));
                if (ddlStatus.SelectedValue == "ALL")
                {
                    retrListGenerate = checkDate(storeClerkRetrList);
                }
                else
                {
                    List<Retrieval> storeClerkStatusRetrList = myBz.FilterRetrByStatus(storeClerkRetrList, ddlStatus.SelectedValue);
                    retrListGenerate = checkDate(storeClerkStatusRetrList);
                }
            }
            if (retrListGenerate.Count > 0)
            {
                gvRetrievalList.DataSource = retrListGenerate;
                gvRetrievalList.DataBind();
            }
            else
            {
                lblResult.Text = "No Retrieval Forms matching search criteria found.";
            }
        }
        protected List<Retrieval> checkDate(List<Retrieval> d)
        {
            List<Retrieval> retrListGenerate = new List<Retrieval>();
            if (tbFrom.Text != "" && tbFrom.Text != null)
            {
                if (tbTo.Text != "" && tbTo.Text != null)
                    retrListGenerate = myBz.FilterRetrsByTo(myBz.FilterRetrsByFrom(d, DateTime.Parse(tbFrom.Text)), DateTime.Parse(tbTo.Text));
                else
                    retrListGenerate = myBz.FilterRetrsByFrom(d, DateTime.Parse(tbFrom.Text));
            }
            else
            {
                if (tbTo.Text != "" && tbTo.Text != null)
                    retrListGenerate = myBz.FilterRetrsByTo(d, DateTime.Parse(tbFrom.Text));
                else
                    retrListGenerate = d;
            }
            return retrListGenerate;
        }


        //For Gridview data binding
        protected string GetEmployeeName(int emplId)
        {
            return myBz.FindEmpById(emplId).EmployeeName;
        }

        protected string MyNewRow(int retrievalId)
        {
            return string.Format(@"</td></tr><tr id='tr{0}' class='collapsed-row'><td colspan='100' style='padding:0px;margin:0px;'>", retrievalId);
        }

        protected void gvRetrievalList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int retrievalId = Int32.Parse(gvRetrievalList.DataKeys[e.Row.RowIndex].Value.ToString());
                var gvDetails = (GridView)e.Row.FindControl("gvDetails");

                List<RetrievalDetail> retrievalDetails = myBz.FindRetrDetByRetrId(retrievalId);
                gvDetails.DataSource = retrievalDetails;
                gvDetails.DataBind();
            }
        }

        protected void btnNewRetrieval_Click(object sender, EventArgs e)
        {
            Retrieval retrieval = new Retrieval();
            retrieval.EmployeeId = storeClerkId;
            retrieval.DateRetrieved = DateTime.Today;
            retrieval.Status = "In Progress";
            myBz.AddNewRetrieval(retrieval);
            Response.Redirect("~/Store/RetrievalNew.aspx");
        }

        //Check Existing Retrieval & Disbursement
        public bool HaveExistingRetrieval()
        {
            List<Retrieval> retrList = myBz.FindRetrListByEmplIdAndStatus(storeClerkId, "In Progress");
            return retrList.Count != 0 ? true : false;
        }

        protected void btnMoveToRetrieval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/RetrievalNew.aspx");
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            if (reList.Count > 0)
            {
                gvRetrievalList.DataSource = reList;
                gvRetrievalList.DataBind();
            }
            else
                PanelMessage.Visible = true;
        }
    }
}