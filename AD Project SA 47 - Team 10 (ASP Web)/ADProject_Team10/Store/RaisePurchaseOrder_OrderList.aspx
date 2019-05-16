<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaisePurchaseOrder_OrderList.aspx.cs" Inherits="ADProject_Team10.Store.RaisePurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>All Orders</h1>
    <br />

    <asp:Button ID="btnCreateNewOrder" runat="server" Text="Create New Order" OnClick="btnCreateNewOrder_Click" CssClass="btn btn-success" />
    <br />
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">There are no orders at the moment.</div>
    </asp:Panel>

    <asp:GridView ID="gvOrderList" runat="server" OnSelectedIndexChanged="gvOrderList_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" CssClass="table">
        <Columns>

            <asp:BoundField DataField="PONumber" HeaderText="PO Number" />
            <asp:BoundField DataField="SupplierCode" HeaderText="Supplier Code" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="DateOrdered" HeaderText="Date Ordered" HtmlEncode="false" DataFormatString="{0:dd MMM yyyy}" />
            <asp:BoundField DataField="DateSupply" HeaderText="Date Supplied By" HtmlEncode="false" DataFormatString="{0:dd MMM yyyy}" />

            <asp:CommandField HeaderText="View Details" ShowHeader="True" ShowSelectButton="True" SelectText="View Details">
                <ItemStyle ForeColor="#0E70C0" />
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <br />
    <br />
</asp:Content>
