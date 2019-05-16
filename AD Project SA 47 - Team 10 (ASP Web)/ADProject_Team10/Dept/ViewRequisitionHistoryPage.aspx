<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewRequisitionHistoryPage.aspx.cs" Inherits="ADProject_Team10.Dept.ViewRequisitionHistoryPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      
    <!--Title-->
    <h1>Department Requisition History</h1>
    <br />

    <!--Searh bar-->
    <table class="table">
        <tr>
            <td>From
                <asp:TextBox ID="TextBox1" runat="server" TextMode="Date" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please enter a valid From Date" ForeColor="Red">*</asp:RequiredFieldValidator></td>

            <td>To
                <asp:TextBox ID="TextBox2" runat="server" TextMode="Date" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDate2" runat="server" ControlToValidate="TextBox2" ErrorMessage="Please enter a valid To Date" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>

            <td>Employee's Name
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-warning" />
                &nbsp;&nbsp;
                <asp:Button ID="btnAll" runat="server" Text="View All" OnClick="btnAll_Click" CssClass="btn btn-primary" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:CompareValidator ID="CompareValidatorDate" runat="server" ControlToCompare="TextBox2" ControlToValidate="TextBox1" ErrorMessage="From Date should be earlier than To Date" ForeColor="Red" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" ForeColor="Red" Text="No Record!" Visible="False" /></td>
        </tr>
    </table>

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">Your Department does not have any requisitions are the moment.</div>
    </asp:Panel>

    <!--Requisition List-->
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="table" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="True" />
            <asp:BoundField DataField="RequisitionDate" HeaderText="Requisition Date" ReadOnly="True" DataFormatString="{0:dd MMM yyyy}" />
            <asp:BoundField DataField="RequisitionStatus" HeaderText="Requisition Status" ReadOnly="True" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:LinkButton ID="button" runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%#Eval("RequisitionId")+","+Eval("EmployeeName")%>' CausesValidation="False" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <SelectedRowStyle ForeColor="#FF9933" />
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />
    <br />

    <asp:Panel ID="PanelForm" runat="server">

        <!--Form-->
        <h3>Requisition Form -
            <asp:Label ID="lblReqId" runat="server" /></h3>

        <table class="table">
            <tr>
                <td>Employee Name:
                    <strong>
                        <asp:Label ID="lblName" runat="server" /></strong></td>
                <td>Requisition Date:
                    <strong>
                        <asp:Label ID="lblDate" runat="server" /></strong></td>
            </tr>
            <tr>
                <td>Approved/Rejected By:
                    <strong>
                        <asp:Label ID="lblApprovedBy" runat="server" /></strong></td>
                <td>Status:
                    <strong>
                        <asp:Label ID="lblStatus" runat="server" /></strong></td>
            </tr>
        </table>
        <br />

        <!--Items-->
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table" Width="50%">
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
                <asp:BoundField DataField="QuantityRequest" HeaderText="Quantity Request" ReadOnly="True" />
            </Columns>
            <HeaderStyle BackColor="#666666" ForeColor="White" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
