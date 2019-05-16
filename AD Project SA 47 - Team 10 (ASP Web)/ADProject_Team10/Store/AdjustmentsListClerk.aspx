<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdjustmentsListClerk.aspx.cs" Inherits="ADProject_Team10.Store.AdjustmentsListClerk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>My Stock Adjustments</h1>
    <br />

    <!--Dropdown filter -->
    <asp:Panel ID="PanelDropdown" runat="server">
        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control" Width="200px">
            <asp:ListItem Selected="True" Value="All"> All </asp:ListItem>
            <asp:ListItem Value="Approved"> Approved </asp:ListItem>
            <asp:ListItem Value="Rejected"> Rejected </asp:ListItem>
            <asp:ListItem Value="Pending"> Pending </asp:ListItem>
            <asp:ListItem Value="Reported"> Reported </asp:ListItem>
            <asp:ListItem Value="In Progress"> In Progress </asp:ListItem>
        </asp:DropDownList>
    </asp:Panel>

    <!--New adjustment -->
    <asp:Button ID="btnCreate" runat="server" Text="New Adjustment" OnClick="btnCreate_Click" CssClass="btn btn-primary" />
    <br />
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">You do not have any adjustments at the moment.</div>
    </asp:Panel>

    <!-- List of Pending Requests-->
    <asp:GridView ID="gvItems" runat="server" Width="100%" AutoGenerateColumns="False" OnRowDataBound="gvItems_RowDataBound" CssClass="table">
        <Columns>

            <asp:TemplateField HeaderText="Date Created">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%# Bind("DateCreated" ,"{0:dd MMM yyyy}") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# FindDescription((string)Eval("ItemCode")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Quantity Adjusted">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("QuantityAdjusted") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("Reason") %>'>&gt;</asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Date Approved">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%# Bind("DateApproved" ,"{0:dd MMM yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Approver's Remarks">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("ApproverRemarks") %>'>></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkEdit" runat="server"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />

</asp:Content>
