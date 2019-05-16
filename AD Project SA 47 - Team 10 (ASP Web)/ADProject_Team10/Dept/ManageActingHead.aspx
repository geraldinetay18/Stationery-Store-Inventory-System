<%@ Page Title="Manage Acting Department Head" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageActingHead.aspx.cs" Inherits="ADProject_Team10.Dept.ManageActingHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Authorize Acting Department Head</h1>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelGoodMessage" runat="server" Visible="False">
        <div class="alert alert-success alert-dismissible">
            <button type="button" class="close" data-dismiss="alert">&times;</button>
            <asp:Label ID="lblGoodMessage" runat="server" Text="Label"></asp:Label>
        </div>
    </asp:Panel>
    <asp:Panel ID="PanelFailMessage" runat="server" Visible="False">
        <div class="alert alert-danger alert-dismissible">
            <button type="button" class="close" data-dismiss="alert">&times;</button>
            <asp:Label ID="lblFailMessage" runat="server" Text="Label"></asp:Label>
        </div>
    </asp:Panel>
    <div>
        <table class="table">
            <tr>
                <td>Current Acting Department Head</td>
                <td colspan="3">
                    <asp:TextBox ID="tbEmpName" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvEmpName" runat="server" ErrorMessage="*No Employee is selected" ControlToValidate="tbEmpName" ValidationGroup="Delegate" ForeColor="Red" Text="*No Employee is selected"></asp:RequiredFieldValidator></td>
                <td></td>
            </tr>
            <tr>
                <td>Delegation Period</td>
                <td>From</td>
                <td>
                    <asp:TextBox ID="tbStartDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="*Start Date is Required" ControlToValidate="tbStartDate" ValidationGroup="Delegate" Display="Static" ForeColor="Red" Text="*Start Date is Required"></asp:RequiredFieldValidator>
                </td>
                <td>To</td>
                <td>
                    <asp:TextBox ID="tbEndDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="*End Date is Required" ControlToValidate="tbEndDate" ValidationGroup="Delegate" Display="Static" ForeColor="Red" Text="*End Date is Required"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:CompareValidator ID="cvDate" runat="server" ErrorMessage="*Start Date must be earlier than End Date" ControlToCompare="tbStartDate" ControlToValidate="tbEndDate" CultureInvariantValues="True" ForeColor="Red" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="Delegate" Display="Static">*Start Date must be earlier than End Date</asp:CompareValidator><br />
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="4">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="False" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure to remove current delegation?');" OnClick="btnRemove_Click" CausesValidation="False" />&nbsp;&nbsp;
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnUpdate_Click" ValidationGroup="Delegate" OnClientClick="if (!confirm('Are you sure to update?'))return false;" />
                    &nbsp;
                    <asp:Button ID="btnDelegate" runat="server" CssClass="btn btn-success" Text="Delegate" OnClientClick="if (!confirm('Are you sure to delegate?'))return false;" OnClick="btnDelegate_Click" ValidationGroup="Delegate" />
                </td>
                <td></td>
            </tr>

        </table>
    </div>
    <br />
    <br />
    <div>
        <h3>Select New Acting Department Head</h3>
        <br />
        <table>
            <tr>
                <td>Search Employee by Name
                </td>
                <td>
                    <asp:TextBox ID="txtNameSearch" runat="server" ToolTip="Enter Employee Name" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="SearchName" CssClass="btn btn-warning" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnReset" runat="server" Text="View All" OnClick="btnReset_Click" CssClass="btn btn-primary" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:RequiredFieldValidator ID="rfvNameSearch" runat="server" ErrorMessage="*Employee Name is Required" ControlToValidate="txtNameSearch" ValidationGroup="SearchName" Display="Static" ForeColor="Red" Text="*Employee Name is Required"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
        </table>
        <asp:GridView ID="gvStaffList" runat="server" AutoGenerateColumns="False" OnRowCommand="gvStaffList_RowCommand" GridLines="None" CssClass="table" AllowPaging="True" OnPageIndexChanging="gvStaffList_PageIndexChanging" OnRowDataBound="gvStaffList_RowDataBound" PageSize="5">
            <Columns>
                <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId" Visible="False" />
                <asp:BoundField DataField="EmployeeName" HeaderText="Employee" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("EmployeeId")%>' CausesValidation="false" Text="Select" CssClass="btn btn-info" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" CssClass="gridViewPager" />
            <HeaderStyle BackColor="#666666" ForeColor="White" />
        </asp:GridView>
    </div>
    <asp:HiddenField ID="hfActingHeadEmployeeId" runat="server" />
    <asp:HiddenField ID="hfSelectedEmployeeId" runat="server" />

</asp:Content>
