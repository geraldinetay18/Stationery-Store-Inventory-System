<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierView.aspx.cs" Inherits="ADProject_Team10.Store.SupplierView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Supplier Details</h1>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-danger">Supplier does not exists.</div>
    </asp:Panel>

    <!-- Supplier Details -->
    <table class="table " border="0">
        <tr>
            <td>Supplier Name </td>
            <td><strong>
                <asp:Label ID="lblName" runat="server"></asp:Label></strong></td>
            <td>Contact Person</td>
            <td><strong>
                <asp:Label ID="lblContactPerson" runat="server"></asp:Label></strong></td>
        </tr>
        <tr>
            <td>Supplier Code</td>
            <td><strong>
                <asp:Label ID="lblCode" runat="server"></asp:Label></strong></td>
            <td>Phone</td>
            <td><strong>
                <asp:Label ID="lblPhone" runat="server"></asp:Label></strong></td>
        </tr>
        <tr>
            <td>GST Registration No</td>
            <td><strong>
                <asp:Label ID="lblGST" runat="server"></asp:Label></strong></td>
            <td>Fax</td>
            <td><strong>
                <asp:Label ID="lblFax" runat="server"></asp:Label></strong></td>
        </tr>
        <tr>
            <td>Address</td>
            <td colspan="3"><strong>
                <asp:Label ID="lblAddress" runat="server"></asp:Label></strong></td>
        </tr>
    </table>
    <br />

    <!-- Items of Supplier-->
    <h3>Stationeries supplied</h3>
    <p>Total: <strong><asp:Label ID="lblTotal" runat="server" /> stationeries</strong></p>
    <br />
    <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# FindCategoryNameOfItem((string)Eval("ItemCode")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# FindDescription((string)Eval("ItemCode")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Unit Price">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%#  string.Format("{0:c}", (double) Eval("UnitPrice"))%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
</asp:Content>
