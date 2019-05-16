<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChargeBack.aspx.cs" Inherits="ADProject_Team10.Store.ChargeBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1 style="text-align: center">Charge Back</h1>
    <br />

    <div style="text-align: right">
        <asp:Button ID="PDF" runat="server" Text="Download as PDF" CssClass="btn btn-warning" OnClick="PDF_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SendEmail" runat="server" Text="Charge & Send Email" OnClick="SendEmail_Click" CssClass="btn btn-warning" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblEmail" runat="server" Text="" ToolTip="Email of Department Head"></asp:Label>
    </div>
    <br />
    <div style="text-align: center">
        <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="dropdown-toggle" Width="143px" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
            <asp:ListItem>Architecture Dept</asp:ListItem>
            <asp:ListItem>Art Dept</asp:ListItem>
            <asp:ListItem>Commerce Dept</asp:ListItem>
            <asp:ListItem>Computer Science</asp:ListItem>
            <asp:ListItem>English Dept</asp:ListItem>
            <asp:ListItem>Language Studies Dept</asp:ListItem>
            <asp:ListItem>Physiology Dept</asp:ListItem>
            <asp:ListItem>Registrar Dept</asp:ListItem>
            <asp:ListItem>Sociology Dept</asp:ListItem>
            <asp:ListItem>Logic University Stationery Store</asp:ListItem>
            <asp:ListItem>Zoology Dept</asp:ListItem>
        </asp:DropDownList>
    </div>
    <br />
    <div style="text-align: center">

        <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbFromDate" runat="server" TextMode="Date"></asp:TextBox>


        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblToDate" runat="server" Text="To Date"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="tbToDate" runat="server" TextMode="Date"></asp:TextBox>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate Charge Back" CssClass="btn btn-info" Width="179px" />
        <br />
        <asp:CompareValidator ID="CompareValidatorDate" runat="server" ControlToCompare="tbToDate" ControlToValidate="tbFromDate" ErrorMessage="From Date should be earlier than To Date" ForeColor="Red" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
    </div>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">There is no Disbursements that have not been Charged Back for selected Department and time frame.</div>
    </asp:Panel>
    <br />

    <!--Items to be charged-->
    <asp:GridView ID="gvChargeBack" runat="server" HorizontalAlign="Center" ViewStateMode="Enabled" AutoGenerateColumns="False" CssClass="table table-hover" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="Item Code">

                <ItemTemplate>
                    <asp:Label ID="ItemCode" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category Name">

                <ItemTemplate>
                    <asp:Label ID="CategoryName" runat="server" Text='<%# FindCategoryName((string)Eval("ItemCode")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">

                <ItemTemplate>
                    <asp:Label ID="Description" runat="server" Text='<%# FindDescriptionName((string)Eval("ItemCode")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Received">

                <ItemTemplate>
                    <asp:Label ID="QuantityReceived" runat="server" Text='<%# FindQuanRecv((string)Eval("ItemCode"))%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Unit Price">

                <ItemTemplate>
                    <asp:Label ID="UnitPrice" runat="server" Text='<%# string.Format("{0:c}", FindUnitPrice((string)Eval("ItemCode"))) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cost">

                <ItemTemplate>
                    <asp:Label ID="Cost" runat="server" Text='<%# string.Format("{0:c}", FindCost()) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <br />

</asp:Content>
