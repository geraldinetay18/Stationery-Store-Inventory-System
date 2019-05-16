<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStationeryCatalogue.aspx.cs" Inherits="ADProject_Team10.Dept.ViewStationeryCatalogue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Stationery Catalogue</h1>
    <br />

    <asp:Button ID="Button1" runat="server" PostBackUrl="submitrequest" Text="View Requisition Form" CssClass="btn btn-warning" UseSubmitBehavior="False" />
    <br />
    <br />

    <!--Dropdown list-->
    <div style="text-align: center;">
        <div style="display: inline-block">

            <asp:DropDownList ID="ddlStationery" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="CategoryName" DataValueField="CategoryName" OnSelectedIndexChanged="ddlStationery_SelectedIndexChanged" CssClass="form-control">
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [CategoryName] FROM [Category]"></asp:SqlDataSource>
            <asp:Label ID="lblItem" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>
    <br />

    <!--Gridview-->
    <asp:GridView ID="gvStationeryCatalogue" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="gvStationeryCatalogue_RowCommand" CssClass="table">
        <Columns>
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
            <asp:TemplateField HeaderText="Click to Request" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="AddToRequest" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ItemCode") %>' CommandName="AddToRequest" Text="Add To Requisition"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
</asp:Content>
