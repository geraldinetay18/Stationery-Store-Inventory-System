<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewStationeryItem.aspx.cs" Inherits="ADProject_Team10.Store.AddNewStationeryItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <!--Title-->
    <h1>Add New Stationery </h1>
    <br />
    
    <table class="table">
        <tr>
            <td style="width: 314px"> <asp:Label ID="lblItemCode" runat="server" Text="Item Code"></asp:Label></td>
            <td><asp:TextBox ID="tbItemCode" runat="server"></asp:TextBox></td>
        </tr>
    <tr>
            <td style="width: 314px"><asp:Label ID="lblCategory" runat="server" Text="Category Name"></asp:Label></td>
            <td><asp:DropDownList ID="dllCategory" runat="server" DataSourceID="category" DataTextField="CategoryName" DataValueField="CategoryName"></asp:DropDownList>
                <asp:SqlDataSource ID="category" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [CategoryName] FROM [Category]"></asp:SqlDataSource>
            </td>
    </tr>
        <tr>
            <td colspan="2">Enter the category name if the category record is new or ignore the category name</td>
        </tr>
    <tr>
        <td style="width: 314px"><asp:Label ID="lblCategoryName" runat="server" Text="Category Name"></asp:Label></td>
        <td><asp:TextBox ID="tbCategoryName" runat="server"></asp:TextBox></td>
    </tr>
   <tr>
        <td style="width: 314px"><asp:Label ID="Description" runat="server" Text="Description"></asp:Label></td>
        <td><asp:TextBox ID="tbDescription" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 314px"><asp:Label ID="lblQuantityInStock" runat="server" Text="Quantity In Stock"></asp:Label></td>
        <td><asp:TextBox ID="tbQuantityInStock" runat="server"></asp:TextBox></td>
        <asp:RegularExpressionValidator ID="regQuantityStock" runat="server" ControlToValidate="tbQuantityInStock" ValidationExpression="^\d+" ErrorMessage="Only Numbers" />
    </tr>
    <tr>
        <td style="width: 314px"><asp:Label ID="lblQuantityReorder" runat="server" Text="Quantity Reorder"></asp:Label></td>
        <td><asp:TextBox ID="tbQuantityReorder" runat="server"></asp:TextBox></td>
        <asp:RegularExpressionValidator ID="regQuantityReorder" runat="server" ControlToValidate="tbQuantityReorder" ValidationExpression="^\d+" ErrorMessage="Only Numbers" />
     </tr>
    <tr>
        <td style="width: 314px"><asp:Label ID="lblUnitOfMeasure" runat="server" Text="Unit Of Measure"></asp:Label></td>
        <td><asp:TextBox ID="tbUnitOfMeasure" runat="server"></asp:TextBox></td>
        <asp:RegularExpressionValidator ID="regUnitOfMeasure" runat="server" ControlToValidate="tbUnitOfMeasure" ValidationExpression="^[a-zA-Z'.\s]{1,50}"
                           ErrorMessage="Enter valid unit of measure" />
    </tr>
    <tr>
        <td style="width: 314px"><asp:Label ID="lblBin" runat="server" Text="Bin"></asp:Label></td>
        <td><asp:TextBox ID="tbBin" runat="server"></asp:TextBox></td>
    </tr>
    <tr>   
       <td style="width: 314px"><asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn-danger" /></td>
        <td><asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Insert" CssClass="btn-success" /></td>
    </tr> 
 </table>
</asp:Content>
