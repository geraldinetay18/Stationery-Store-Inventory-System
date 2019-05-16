<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequisitionApprovalPage.aspx.cs" Inherits="ADProject_Team10.Dept.RequisitionApprovalPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Approve Requisitions</h1>
    <br />

    <!--Message(1)-->
    <asp:Panel ID="PanelNoPending" runat="server" Visible="False">
        <div class="alert alert-success">No pending Requisition approvals at the moment.</div>
    </asp:Panel>

    <!--Message(2)-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <button type="button" class="close" data-dismiss="alert">&times;</button>
        <div class="alert alert-success">Successfully
            <asp:Label ID="lblApproveOrReject" runat="server" Text="approved " />
            Requisition!</div>
    </asp:Panel>

    <!--Filter-->
    <asp:Panel ID="PanelAll" runat="server" Visible="True">
        <table style="width: 50%">
            <tr>
                <td style="width: 30%">Sort By: </td>
                <td style="width: 70%">
                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem>Date</asp:ListItem>
                        <asp:ListItem>Employee Name</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
        </table>
        <br />

        <!-- List-->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" CssClass="table">
            <Columns>

                <asp:BoundField DataField="RequisitionId" HeaderText="RequisitionId" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId" ReadOnly="True" Visible="False" />
                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="True" />
                <asp:BoundField DataField="RequisitionDate" HeaderText="Requisition Date" ReadOnly="True" DataFormatString="{0:dd MMM yyyy}" />
                <asp:BoundField DataField="RequisitionStatus" HeaderText="Requisition Status" ReadOnly="True" />
                <asp:BoundField DataField="Remark" HeaderText="Remark" ReadOnly="True" Visible="False" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="button" runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%#Eval("RequisitionId")+","+Eval("EmployeeId")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#666666" ForeColor="White" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
