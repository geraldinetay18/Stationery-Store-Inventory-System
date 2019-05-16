<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryStatusReport.aspx.cs" Inherits="ADProject_Team10.Store.InventoryStatusReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Inventory Status Report</h1>
    <br />

    <div id="searchbar">
        Search
        <asp:TextBox ID="tbSearch" runat="server" /><asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-warning" />
    </div>
    <br />

    <asp:GridView ID="gvInventoryStatusReport" runat="server" HorizontalAlign="Center" CssClass="table table-hover" AutoGenerateColumns="False" DataKeyNames="ItemCode">
        <Columns>
            <asp:TemplateField HeaderText="Category" SortExpression="CategoryId">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# FindCategoryName((string)Eval("ItemCode")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="QuantityInStock" HeaderText="Quantity In Stock" SortExpression="QuantityInStock" />
            <asp:BoundField DataField="QuantityReorder" HeaderText="Quantity Reorder" SortExpression="QuantityReorder" />
            <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" SortExpression="ReorderLevel" />
            <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit Of Measure" SortExpression="UnitOfMeasure" />
            <asp:BoundField DataField="Bin" HeaderText="Bin" SortExpression="Bin" />
            <asp:BoundField DataField="OverRequestFrequency" HeaderText="Over Request Frequency" SortExpression="OverRequestFrequency" />
            <asp:BoundField DataField="RecommandQuantity" HeaderText="Recommended Quantity" SortExpression="RecommandQuantity" />
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>

    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT * FROM [Stationery]"></asp:SqlDataSource>--%>

</asp:Content>
