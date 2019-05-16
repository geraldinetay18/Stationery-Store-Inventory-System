<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequisitionDetails.aspx.cs" Inherits="ADProject_Team10.Store.DeptRequisitionDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
        .auto-style2 {
            width: 150px;
        }
        .auto-style3 {
            height: 20px;
            width: 150px;
        }
    </style>

    <!--Title-->
    <h1>Requisition Details</h1>
    <br />

    <asp:Button ID="btnBack" runat="server" Text="< Back To Requisition List" OnClick="btnBack_Click" CssClass="btn btn-primary"/>
    <br />
    <br />
    <table>
        <tr>
            <td class="auto-style2"><label>Employee</label> </td>
            <td><asp:Label ID="lblEmployeName" runat="server" Text="Employee Name" ></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style2"><label>Requisition Id</label></td>
            <td><asp:Label ID="lblReqId" runat="server" Text="Requisition Id"></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style2"><label>Department</label></td>
            <td><asp:Label ID="lblDeptName" runat="server" Text="Departement Name"></asp:Label></td>
        </tr>
        <tr>
            <td class="auto-style3"><label>Requisition Status</label></td>
            <td class="auto-style1"><asp:Label ID="lblReqStatus" runat="server" Text="Requisition Status"></asp:Label></td>
        </tr>
    </table>
    <br />

    <asp:GridView ID="gvReqDetails" runat="server" CssClass="table" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
            <asp:TemplateField HeaderText="Item Description">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetStationery(Eval("ItemCode").ToString()).Description %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="QuantityRequest" HeaderText="Quantity Requested" />
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>

</asp:Content>

