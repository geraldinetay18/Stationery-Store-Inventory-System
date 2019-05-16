/* Author: Shalin Christina Stephen Selvaraja */

using ADProject_Team10.Models;
using ADProject_Team10.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADProject_Team10.Store
{
    public partial class AddNewStationeryItem : System.Web.UI.Page
    {

        IStationeryService iss = new StationeryService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.IsInRole("Store Clerk"))
            {
                Response.Redirect("~/ErrorPages/Unauthorised");
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int result =iss.CheckItemCodeUniqueness(tbItemCode.Text);
            int desc_result=iss.CheckDescriptionUniqueness(tbDescription.Text);

            try
            {
                if (tbItemCode.Text == ""  || tbDescription.Text == "" || tbQuantityInStock.Text == "" || tbQuantityReorder.Text == "" || tbUnitOfMeasure.Text == ""
                    || tbBin.Text == "")
                {
                    Response.Write("<script>alert('All Fields are mandatory , Enter required data !!')</script>");
                }
                else if(result ==1)
                {
                    Response.Write("<script>alert('ItemCode already exists.. Enter a new ItemCode !!')</script>");
                    Clear();
                }
                else if (desc_result == 1)
                {
                    Response.Write("<script>alert('Stationery Description already exists.. Enter a new Description !!')</script>");
                    Clear();
                }
                else
                {
                    if (tbCategoryName.Text != "")
                    {
                        if (iss.FindCategoryByName(tbCategoryName.Text) != null)
                        {
                            Category c = new Category()
                            {
                                CategoryName = tbCategoryName.Text
                            };

                            int j = iss.AddCategory(c);
                            Stationery s = new Stationery()
                            {
                                ItemCode = tbItemCode.Text,
                                CategoryId = iss.FindCategoryByName(tbCategoryName.Text).CategoryId,
                                Description = tbDescription.Text,
                                QuantityInStock = Convert.ToInt32(tbQuantityInStock.Text),
                                QuantityReorder = Convert.ToInt32(tbQuantityReorder.Text),
                                ReorderLevel = Convert.ToInt32(tbQuantityInStock.Text) % 30,
                                UnitOfMeasure = tbUnitOfMeasure.Text,
                                Bin = Convert.ToInt32(tbBin.Text),
                                OverRequestFrequency = 0,
                                RecommandQuantity = 0

                            };

                            int i = iss.AddStationery(s);

                            if (i == 1 && j == 1)
                            {
                                Response.Write("<script>alert('New Stationery is created !!')</script>");
                                Clear();
                            }
                            else
                            {
                                Response.Write("<script>alert('Cannot create a new stationery !!')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Category already exists !!')</script>");
                            Clear();
                        }
                    }
                    else
                    {
                        Stationery s = new Stationery()
                        {
                            ItemCode = tbItemCode.Text,
                            CategoryId = iss.FindCategoryByName(dllCategory.SelectedValue).CategoryId,
                            Description = tbDescription.Text,
                            QuantityInStock = Convert.ToInt32(tbQuantityInStock.Text),
                            QuantityReorder = Convert.ToInt32(tbQuantityReorder.Text),
                            ReorderLevel = Convert.ToInt32(tbQuantityInStock.Text) % 30,
                            UnitOfMeasure = tbUnitOfMeasure.Text,
                            Bin = Convert.ToInt32(tbBin.Text),
                            OverRequestFrequency = 0,
                            RecommandQuantity = 0
                        };
                        int i = iss.AddStationery(s);

                        if (i == 1)
                        {
                            Response.Write("<script>alert('New Stationery is created !!')</script>");
                            Clear();
                        }
                        else
                        {
                            Response.Write("<script>alert('Cannot create a new stationery !!')</script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errormsg = string.Format("<script>Error:{0}</script>", ex.Message);
                Response.Write(errormsg);
            }
        }

        protected void Clear()
        {
            tbItemCode.Text = "";
            tbDescription.Text = "";
            tbQuantityInStock.Text = "";
            tbQuantityReorder.Text = "";
            tbUnitOfMeasure.Text = "";
            tbBin.Text = "";
            tbCategoryName.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Store/AnnualCatalogUpdate.aspx");
        }
    }
}
