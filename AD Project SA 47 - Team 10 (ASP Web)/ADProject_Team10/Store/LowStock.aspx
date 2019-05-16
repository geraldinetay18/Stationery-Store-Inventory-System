<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LowStock.aspx.cs" Inherits="ADProject_Team10.Store.LowStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Low Stock Items</h1>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">The quantity levels for all stocks are good. No low stock items.</div>
    </asp:Panel>

    <p>The Quantity in Stock for the following items have fallen below the Reorder Level.</p>

    <asp:GridView ID="gvItems" runat="server" HorizontalAlign="Center" CssClass="table" AutoGenerateColumns="False" DataKeyNames="ItemCode" OnRowDataBound="gvItems_RowDataBound" OnRowCommand="gvItems_RowCommand">
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="QuantityInStock" HeaderText="Quantity In Stock" SortExpression="QuantityInStock">
                <ControlStyle ForeColor="Red"></ControlStyle>
            </asp:BoundField>
            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:Button ID="btnStockCard" runat="server" CssClass="btn btn-info" Text="View Stock Card" CommandName="ViewCard" CommandArgument='<%#Eval("ItemCode")%>'/>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>

</asp:Content>
