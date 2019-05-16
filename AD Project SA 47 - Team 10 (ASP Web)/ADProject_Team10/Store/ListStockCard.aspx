<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListStockCard.aspx.cs" Inherits="ADProject_Team10.Store.ListStockCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Stock Cards</h1>
    <br />

    <!--Search Bar-->
    <div id="searchbar">
        <asp:Label ID="lblMaintainStockCard1" runat="server" Font-Bold="True" Text="Catalogue Item Code" />
        <asp:TextBox ID="tbMaintainStockCard" runat="server" Font-Italic="True" ForeColor="Gray" Width="260px" />
        <strong>
            &nbsp;&nbsp;
            <asp:Button ID="btnMaintainStockCard1" runat="server" Font-Bold="True" Font-Overline="False" Text="Search" OnClick="btnMaintainStockCard1_Click" CssClass="btn btn-warning" />
            &nbsp;&nbsp;
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </strong>
    </div>
    <br />

    <!--Items-->
    <asp:GridView ID="gvItems" runat="server" HorizontalAlign="Center" CssClass="table" AutoGenerateColumns="False" DataKeyNames="ItemCode" OnRowDataBound="gvItems_RowDataBound" OnRowCommand="gvItems_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Category" SortExpression="CategoryId">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# FindCategoryName((string)Eval("ItemCode")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="QuantityInStock" HeaderText="Quantity In Stock" SortExpression="QuantityInStock" />
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit Of Measure" SortExpression="UnitOfMeasure" />
            <asp:BoundField DataField="Bin" HeaderText="Bin" SortExpression="Bin" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnStockCard" runat="server" CssClass="btn btn-info" Text="View Stock Card" CommandName="ViewCard" CommandArgument='<%#Eval("ItemCode")%>'/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
</asp:Content>
