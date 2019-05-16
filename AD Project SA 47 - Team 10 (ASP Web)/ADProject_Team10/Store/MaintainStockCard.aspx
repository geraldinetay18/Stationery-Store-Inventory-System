<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MaintainStockCard.aspx.cs" Inherits="ADProject_Team10.Store.MaintainStockCard1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  
    <!--Title-->
    <h1>Stock Card</h1>
    <br />

    <!--Search bar-->
    <div id="searchbar">
        <asp:Label ID="lblMaintainStockCard1" runat="server" Font-Bold="True" Text="Catalogue Item Code" />
        &nbsp;&nbsp;
        <asp:TextBox ID="tbMaintainStockCard" runat="server" Font-Italic="True" ForeColor="Gray" Width="260px" />
        <strong>
            &nbsp;&nbsp;
            <asp:Button ID="btnMaintainStockCard1" runat="server" Font-Bold="True" Font-Overline="False" Text="Search" OnClick="btnMaintainStockCard1_Click" CssClass="btn btn-warning" />
            &nbsp;&nbsp;
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </strong>
    </div>
    <br />

    <!--Item details--> 
    <table class="table">
        <tr>
            <td>Item Code:</td>
            <td><strong><asp:Label ID="lblItemCode" runat="server" Text="No records" /></strong></td>
            <td>1st Supplier: </td>
            <td><strong><asp:Label ID="lblSupplier1" runat="server" Text="No records"/></strong></td>
        </tr>
        <tr>
            <td>Item Description:</td>
            <td><strong><asp:Label ID="lblItemDescription" runat="server" Text="No records" /></strong></td>
            <td>2nd Supplier: </td>
            <td><strong><asp:Label ID="lblSupplier2" runat="server" Text="No records"/></strong></td>
        </tr>
        <tr>
            <td>Bin #:</td>
            <td><strong><asp:Label ID="lblBin" runat="server" Text="No records"/></strong></td>
            <td>3rd Supplier: </td>
            <td><strong><asp:Label ID="lblSupplier3" runat="server" Text="No records"/></strong></td>
        </tr>
        <tr>
            <td>UOM:</td>
            <td><strong><asp:Label ID="lblUOM" runat="server" Text="No records" /></strong></td>
        </tr>
    </table>

    <!--Stock management history-->
    <asp:GridView ID="gvMaintainStockCard" runat="server" AutoGenerateColumns="False" TabIndex="-1" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Date">
                <ItemTemplate>
                    <asp:Label ID="dateLabel" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Supplier / Department">
                <ItemTemplate>
                    <asp:Label ID="suplDeptLabel" runat="server" Text='<%# Bind("Supl_dept") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty">
                <ItemTemplate>
                    <asp:Label ID="qtyLabel" runat="server" Text='<%# Bind("QtyAdjusted") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Balance">
                <ItemTemplate>
                    <asp:Label ID="balanceLabel" runat="server" Text='<%# Bind("Balance") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:Label ID="reasonLabel" runat="server" Text='<%# Bind("Reason") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No records</EmptyDataTemplate>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />
    <br />

</asp:Content>
