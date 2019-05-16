<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequisitionDetailStaff.aspx.cs" Inherits="ADProject_Team10.Dept.RequisitionDetailStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Requisition Details</h1>
    <br />

    <!--Requisition details-->
    <table class="table">
        <tr>
            <td>Requisition ID: <strong>
                <asp:Label ID="lblReqId" runat="server" /></strong></td>
            <td>Requisition Date:<strong>
                <asp:Label ID="lblDate" runat="server" /></strong></td>
            <td>Status: <strong>
                <asp:Label ID="lblStatus" runat="server" /></strong></td>
        </tr>
    </table>
    <br />

    <!--Items-->
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table">
        <Columns>
            <asp:BoundField DataField="StatTransId" HeaderText="StatTransId" ReadOnly="True" Visible="False" />
            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
            <asp:BoundField DataField="QuantityRequest" HeaderText="Requested Quantity" ReadOnly="True" />
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />

    <!--Footer-->
    <table class="table">
        <tr>
            <td>Total:</td>
            <td><strong>
                <asp:Label ID="lblTotal" runat="server" />
                items</strong></td>
        </tr>
        <tr>
            <td>Remarks:</td>
            <td><strong>
                <asp:Label ID="lblRemark" runat="server" /></strong></td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>
