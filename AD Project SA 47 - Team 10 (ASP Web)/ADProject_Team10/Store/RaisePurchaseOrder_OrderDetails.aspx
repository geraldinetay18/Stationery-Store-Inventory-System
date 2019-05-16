<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaisePurchaseOrder_OrderDetails.aspx.cs" Inherits="ADProject_Team10.Store.RaisePurchaseOrder_OrderDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Order Details</h1>
    <br />

    <p style="font-size: medium">
        <em>PO Number:&nbsp;
        <asp:Label ID="lblPONumber" runat="server" Text="lblPONumber"></asp:Label>
        </em>
    </p>
    <span style="font-size: medium">
    <p style="font-size: medium">
        <em>Supplier Code:&nbsp;
        <asp:Label ID="lblSupplierCode" runat="server" Text="lblSupplierCode"></asp:Label>
        </em>
    </p>
    <p style="font-size: medium">
        <em>Status:&nbsp;
        <asp:Label ID="lblStatus" runat="server" Text="lblStatus"></asp:Label>
        </em>
    </p>
    <p style="font-size: medium">
        <em>Date of Order:&nbsp;
        <asp:Label ID="lblDateofOrder" runat="server" Text="lblDateofOrder"></asp:Label>
        </em></span>
    <p style="font-size: medium">
        <em>Date of Should Supply:&nbsp; </em>
        <asp:Label ID="lblDateSupply" runat="server" style="font-style: italic" Text="lblDateSupply"></asp:Label>
    </p>
    <asp:Panel ID="Panelcheck1" runat="server" BackColor="White" Visible="False">
        <asp:Label ID="lblMessage1" runat="server" Text="Label" Font-Italic="True" ForeColor="Red" style="font-size: large; font-weight: 700"></asp:Label>
        <br />
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelOrderDetail" runat="server" Visible="False">
    <p class="text-center">
        <strong>Order Details</strong></p>
    <p>
        <asp:GridView ID="gvOrderDetails" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCancelingEdit="gvOrderDetails_RowCancelingEdit" OnRowEditing="gvOrderDetails_RowEditing" OnRowUpdating="gvOrderDetails_RowUpdating" CssClass="table">
            <Columns>
                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ReadOnly="True" />
                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" />
                <asp:TemplateField HeaderText="Unit Price">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:c}",Eval("UnitPrice")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Remark" HeaderText="Remark" />
                <asp:CommandField ButtonType="Button" HeaderText="Edit" ShowEditButton="True" ShowHeader="True" />
            </Columns>
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
    </p>
    <p>
        Total Amount:&nbsp; $
        <asp:Label ID="lblTotalAmount" runat="server" Text="Label" ForeColor="#009933"></asp:Label>
    </p>
        </asp:Panel>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn btn-danger" />
        &nbsp;&nbsp;
        <asp:Button ID="btnReceive" runat="server" CssClass="btn btn-success" OnClick="btnReceive_Click" Text="Receive" />
    </p>
</asp:Content>
