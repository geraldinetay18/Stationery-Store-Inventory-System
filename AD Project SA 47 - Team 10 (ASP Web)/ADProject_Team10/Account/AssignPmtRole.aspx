<%@ Page Title="Assigning Permanent Role to Staff" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignPmtRole.aspx.cs" Inherits="ADProject_Team10.Account.AssignPmtRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h4><%: Title %></h4>
    <h4>List of Employees</h4>
    <p>
        <asp:GridView ID="gvStaffList" DataKeyNames="Email" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="EmployeeName" HeaderText="EmployeeName" SortExpression="EmployeeName" />
                <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" Visible="False" />
            </Columns>
            <SelectedRowStyle BackColor="#FFFF66" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [EmployeeName], [Role], [Email] FROM [Employee]"></asp:SqlDataSource>
    </p>
    <p>
        <asp:RadioButtonList ID="rblRoleList" runat="server">
        </asp:RadioButtonList>
    </p>
    <p>
        <asp:Button ID="btnAssign" runat="server" OnClick="btnAssign_Click" Text="Assign" />
    </p>
</asp:Content>
