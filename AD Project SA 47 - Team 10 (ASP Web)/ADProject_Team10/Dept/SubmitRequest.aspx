<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubmitRequest.aspx.cs" Inherits="ADProject_Team10.Dept.SubmitRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Requisition Form</h1>
    <br />

    <!-- Button-->
    <asp:Button ID="btnGoCatalogue" runat="server" Text="Add Stationery" PostBackUrl="ViewStationeryCatalogue" UseSubmitBehavior="False" CssClass="btn btn-warning" />
    <br />
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">There are no stationeries in your Requisition Form at the moment.</div>
    </asp:Panel>


    <asp:Panel ID="PanelAll" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" CssClass="table">
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
                <asp:BoundField DataField="QuantityRequest" HeaderText="Requested Quantity" />

                <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" ReadOnly="True" />

                <asp:CommandField ButtonType="Button" HeaderText="Edit" ShowEditButton="True" ShowHeader="True" />
                <asp:CommandField ButtonType="Button" HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
            </Columns>
            <HeaderStyle BackColor="#666666" ForeColor="White" />
        </asp:GridView>

        <!--Button-->
        <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" Text="Clear All" OnClick="btnClear_Click" />
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" CssClass="btn btn-success" />
    </asp:Panel>
</asp:Content>
