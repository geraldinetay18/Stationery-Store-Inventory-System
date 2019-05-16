<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnnualCatalogUpdate.aspx.cs" Inherits="ADProject_Team10.Store.AnnualCatalogUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Update Stationery Catalogue</h1>
    <br />
    <br />

    <div class="row">
        <asp:TextBox ID="tbSearch" runat="server" CssClass="form-control" Width="50%"/>
        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-warning" Width="20%"/>
    </div>
    <br />

    <div style="text-align: right">
        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert New Item" CssClass="btn btn-info" />
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SearchItems10" UpdateCommand="UPDATE Stationery SET QuantityInStock = @QuantityInStock, QuantityReorder = @QuantityReorder, ReorderLevel = @ReorderLevel, UnitOfMeasure = @UnitOfMeasure, Bin = @Bin, AdjustmentRemark = @AdjustmentRemark WHERE (ItemCode = @ItemCode)" SelectCommandType="StoredProcedure" DeleteCommand="DELETE FROM Stationery WHERE (ItemCode = @ItemCode)">
        <DeleteParameters>
            <asp:Parameter Name="ItemCode" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="tbSearch" Name="Description" PropertyName="Text" Type="String" DefaultValue="  " />
            <asp:ControlParameter ControlID="tbSearch" DefaultValue="  " Name="ItemCode" PropertyName="Text" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="UnitOfMeasure" Type="String" />
            <asp:Parameter Name="Bin" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemCode,CategoryId" DataSourceID="SqlDataSource1" HorizontalAlign="Center" CssClass="table table-hover">
        <Columns>
            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" SortExpression="CategoryName" ReadOnly="True" />
            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" ReadOnly="True" />
            <asp:TemplateField HeaderText="Description" SortExpression="Description">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="QuantityInStock" HeaderText="Quantity In Stock" SortExpression="QuantityInStock" ReadOnly="True" />
            <asp:BoundField DataField="QuantityReorder" HeaderText="Quantity Reorder" SortExpression="QuantityReorder" ReadOnly="True" />
            <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" SortExpression="ReorderLevel" ReadOnly="True" />
            <asp:TemplateField HeaderText="Unit Of Measure" SortExpression="UnitOfMeasure">
                <EditItemTemplate>
                    <asp:RegularExpressionValidator ID="EditUnitOfMeasure" runat="server" ControlToValidate="TextBox1" ErrorMessage="Only text" ValidationExpression="/^[a-z]+$/">*</asp:RegularExpressionValidator>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bin" SortExpression="Bin">
                <EditItemTemplate>
                    <asp:RegularExpressionValidator ID="EditBin" runat="server" ControlToValidate="TextBox2" ErrorMessage="only number" ValidationExpression="/^[0-9]+$/"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Number" Text='<%# Bind("Bin") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Bin") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="OverRequestFrequency" HeaderText="Over Request Frequency" SortExpression="OverRequestFrequency" ReadOnly="True" />

            <asp:BoundField DataField="RecommandQuantity" HeaderText="Recommended Quantity" SortExpression="RecommandQuantity" ReadOnly="True" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>

</asp:Content>
