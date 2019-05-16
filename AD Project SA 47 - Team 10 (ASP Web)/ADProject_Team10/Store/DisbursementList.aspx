<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisbursementList.aspx.cs" Inherits="ADProject_Team10.Store.DisbursmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Disbursements</h1>
    <br />

    <!--Search bar-->
    <div style="padding-top: 20px">
        <table style="width: 100%">
            <tr>
                <td>
                    <label>Collection Point</label></td>
                <td>
                    <asp:DropDownList ID="ddlCollectionPoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCollectionPoint_SelectedIndexChanged"></asp:DropDownList></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>
                    <label>Department</label>&nbsp;&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <label>Disbursement From</label>
                    &nbsp;&nbsp;</td>
                <td>
                    <asp:TextBox ID="tbFrom" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </td>
                <td></td>
                <td>
                    <label>To</label></td>
                <td>
                    <asp:TextBox ID="tbTo" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToCompare="tbFrom" ControlToValidate="tbTo" CultureInvariantValues="True" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" Font-Size="Large" ForeColor="Red"></asp:CompareValidator></td>
                <td>
                    <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-warning" Text="Search" OnClick="btnGenerate_Click" />
                &nbsp;&nbsp;
                    <asp:Button ID="btnViewAll" runat="server" CssClass="btn btn-primary" Text="View All" OnClick="btnViewAll_Click" />
                </td>
                <td>&nbsp;&nbsp;
                    </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">There are no Disbursements belonging to your Collection Points at the moment.</div>
    </asp:Panel>

    <!--Disbursements List-->
    <asp:GridView ID="gvDisbursementList" runat="server" AutoGenerateColumns="False" DataKeyNames="DisbursementId" OnRowCommand="gvDisbursementList_RowCommand" CssClass="table" OnRowDataBound="gvDisbursementList_RowDataBound">
        <Columns>
            <asp:BoundField DataField="DateDisbursed" HeaderText="Date Disbursed" DataFormatString="{0:dd MMM yyyy}" />
            <asp:TemplateField HeaderText="Department Name">
                <ItemTemplate>
                    <asp:HiddenField ID="hDisId" runat="server" Value='<%# Eval("DisbursementId") %>'></asp:HiddenField>
                    <%# GetDepartmentyDisbId(Int32.Parse(Eval("DisbursementId").ToString())).DeptName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Representative">
                <ItemTemplate>
                    <%# GetEmployeeById(Int32.Parse(Eval("RepresentativeId").ToString())).EmployeeName %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Status" DataField="Status" />
            <asp:BoundField DataField="Remark" HeaderText="Remarks" />
            <asp:ButtonField ButtonType="Button" CommandName="seeDetails" Text="See Details" ControlStyle-CssClass="btn btn-info" HeaderText="Action" >
<ControlStyle CssClass="btn btn-info"></ControlStyle>
            </asp:ButtonField>
        </Columns>

        <HeaderStyle BackColor="#666666" ForeColor="White" />

    </asp:GridView>
</asp:Content>

