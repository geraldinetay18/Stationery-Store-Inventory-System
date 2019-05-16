<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationeryCollectionListByDept.aspx.cs" Inherits="ADProject_Team10.Dept.StationeryCollectionListByDept" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>All Collections</h1>
    <br />

    <!--Search bar-->
    <table class="table">
        <tr>
            <td>Year</td>
            <td><asp:DropDownList ID="ddlSearchYear" runat="server" OnSelectedIndexChanged="ddlSearchYear_SelectedIndexChanged" CssClass="form-control" /></td>
            <td><asp:TextBox ID="tbSearchDate" runat="server" Style="width: 128px" TextMode="Date" CssClass="form-control"></asp:TextBox> 
                <%--<ajaxToolkit:CalendarExtender ID="cldChooseDate" runat="server" DaysModeTitleFormat="dd MMM yyyy" Format="dd MMM yyyy" PopupPosition="TopRight" TodaysDateFormat="dd MMM yyyy" PopupButtonID="imbCalendar" TargetControlID="tbSearchDate" />--%></td>
            <td> <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-warning" />
                &nbsp;&nbsp;
                <asp:Button ID="btnViewAll" runat="server" CssClass="btn btn-primary" OnClick="btnViewAll_Click" Text="View All" /></td>
        </tr>
    </table>
    <br />
    <br />

    <asp:GridView ID="gvStationeryCollectionList" runat="server" DataKeyNames="DisbursementId" AutoGenerateColumns="False" OnSelectedIndexChanged="Disbursement_SelectedIndexChanged" TabIndex="-1" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="No.">

                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Disbursement ID">
                <ItemTemplate>
                    <asp:Label ID="DisbursementId" runat="server" Text='<%# Bind("DisbursementId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date of Disbursement">
                <ItemTemplate>
                    <asp:Label ID="DateOfDisbursementLabel" runat="server" Text='<%# Bind("DateOfDisbursement") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="StatusLabel" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remark">
                <ItemTemplate>
                    <asp:Label ID="RemarkLabel" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
        </Columns>
        <EmptyDataTemplate>No records</EmptyDataTemplate>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />
</asp:Content>
