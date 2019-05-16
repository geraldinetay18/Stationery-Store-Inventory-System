<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequisitionApprovalDetailPage.aspx.cs" Inherits="ADProject_Team10.Dept.RequisitionApprovalDetailPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Approve Requisitions</h1>
    <br />

    <!--Requisition details-->
    <table class="table">
        <tr>
            <td>Requisition ID: <strong>
                <asp:Label ID="lblReqId" runat="server" /></strong></td>
            <td>Employee Name: <strong>
                <asp:Label ID="lblName" runat="server" /></strong></td>
            <td>Requisition Date:<strong>
                <asp:Label ID="lblDate" runat="server" /></strong></td>
        </tr>
    </table>
    <br />

    <!--Items-->
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table">
        <Columns>
            <asp:BoundField DataField="StatTransId" HeaderText="StatTransId" ReadOnly="True" Visible="False" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
            <asp:BoundField DataField="QuantityRequest" HeaderText="Quantity Request" ReadOnly="True" />
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />

    <!--Actions-->
    <asp:Label ID="Label3" runat="server" Text="Remarks:"></asp:Label><br />
    <asp:TextBox ID="TextBox3" runat="server" Height="52px" TextMode="MultiLine" Width="183px"></asp:TextBox>
    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Cancel" CssClass="btn btn-secondary"/>
    &nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Reject" CssClass="btn btn-danger" />
    &nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Approve" CssClass="btn btn-success" />
    </asp:Content>
