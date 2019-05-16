<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierList.aspx.cs" Inherits="ADProject_Team10.Store.SupplierList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Suppliers</h1>
    <br />

    <!--List-->
    <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="False" DataKeyNames="SupplierCode" DataSourceID="SqlDataSource1" OnRowDataBound="gvSuppliers_RowDataBound" CssClass="table">
        <Columns>
            <asp:BoundField DataField="SupplierCode" HeaderText="Supplier Code" ReadOnly="True" SortExpression="SupplierCode" />
            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" SortExpression="SupplierName" />
            <asp:BoundField DataField="ContactName" HeaderText="Contact Name" SortExpression="ContactName" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkView" runat="server">View</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [SupplierCode], [SupplierName], [ContactName], [Phone] FROM [Supplier]"></asp:SqlDataSource>

</asp:Content>
