<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryCollectionDetails.aspx.cs" Inherits="ADProject_Team10.Dept.StationeryCollectionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Collection Details</h1>
    <br />

    <table class="table">
        <tr>
            <td style="width: 73px">Date: </td>
            <td><strong><asp:Label ID="lblMaintainStockCard1" runat="server" Text="Label"></asp:Label></strong></td>
        </tr>
        <tr>
            <td style="width: 73px">Status: </td>
            <td><strong><asp:Label ID="lblMaintainStockCard2" runat="server" Text="Label"></asp:Label></strong></td>
        </tr>
    </table>

    <asp:GridView ID="gvSCDetails" runat="server" AutoGenerateColumns="False" TabIndex="-1" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Stationery Description">
                <ItemTemplate>
                    <asp:Label ID="stationeryDescriptionLabel" runat="server" Text='<%# Bind("stationeryDescription") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Need">
                <ItemTemplate>
                    <asp:Label ID="quantityNeedLabel" runat="server" Text='<%# Bind("quantityNeed") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Disbursed">
                <ItemTemplate>
                    <asp:Label ID="quantityDisbursedLabel" runat="server" Text='<%# Bind("quantityDisbursed") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="statusLabel" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Request Date">
                <ItemTemplate>
                    <asp:Label ID="requestDate" runat="server" Text='<%# Bind("requestDate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>No records</EmptyDataTemplate>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />

    <strong>Remarks:</strong>
    <br />
    <asp:TextBox ID="txtRemark" runat="server" Height="95px" Width="370px" CssClass="form-control"></asp:TextBox>
    <asp:Label ID="lbRemark" runat="server" Text=""></asp:Label>
    <br />
    <br />
    <asp:Button ID="Back" runat="server" Font-Bold="True" Text="Back" Width="71px" OnClick="Back_Click" CssClass="btn btn-primary" />
    &nbsp;&nbsp;
    <asp:Button ID="Acknowledge" OnClientClick="return confirm('Are you sure?');" runat="server" Font-Bold="True" Text="Acknowledge" Width="120px" OnClick="Acknowledge_Click" CssClass="btn btn-success" />
    <br />
</asp:Content>
