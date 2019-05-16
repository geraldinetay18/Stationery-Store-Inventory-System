<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaisePurchaseOrder_NewOrder.aspx.cs" Inherits="ADProject_Team10.Store.RaisePurchaseOrder_NewOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>New Purchase Order</h1>
    <br />

    <p style="font-size: medium">
        <em>PO Number:&nbsp;
        <asp:Label ID="lblPONumber" runat="server" Text="lblPONumber"></asp:Label>
        </em>
    </p>
    <h2>Suppliers</h2>
    <p>
        <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CellPadding="4" CssClass="table" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>

                <asp:BoundField DataField="SupplierCode" HeaderText="Supplier" />
                <%--<asp:BoundField DataField="SupplierRank" HeaderText="Rank" />--%>

                <asp:CommandField ButtonType="Button" HeaderText="Action" ShowHeader="True" ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </p>
    <p>
        <span style="font-size: medium"><em>Supplier:
            <asp:Label ID="lblSupplier" runat="server" Text="Label"></asp:Label>
        </em></span>
    </p>
    <p>
        <span style="font-size: medium"><em>Date of Order:&nbsp;
        <asp:Label ID="lblDateofOrder" runat="server" Text="lblDateofOrder"></asp:Label>
        </em></span>
    </p>
    <br />



    <asp:Panel ID="PanelSearch" runat="server" Visible="False">
        <!--Items-->
        <h2>Stationery supplied by selected supplier</h2>
        <br />
        <p>
            <asp:TextBox ID="txbSearch" runat="server" Width="453px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-warning" />
        </p>
    </asp:Panel>
    <br />

    <asp:Panel ID="Panelcheck1" runat="server" BackColor="White" Visible="False">
        <asp:Label ID="lblMessage1" runat="server" Text="Label" Font-Italic="True" ForeColor="Red" Style="font-size: medium; font-weight: 700"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="PanelItemList" runat="server" Visible="False">
        <p class="text-center" style="font-size: large">
            <asp:Label ID="lblitemList" runat="server" Style="font-weight: 700" Text="Label"></asp:Label>
        </p>
        <p>
            <asp:GridView ID="gvItemList" runat="server" OnSelectedIndexChanged="gvItemList_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnRowDataBound="gvItemList_RowDataBound" CssClass="table">
                <Columns>

                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="AdjustmentRemark" HeaderText="Status" />
                    <asp:BoundField DataField="SupplierRank" HeaderText="Supplier Rank" />
                    <asp:TemplateField HeaderText="Unit Price">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:c}",Eval("UnitPrice")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField ButtonType="Button" HeaderText="Action" ShowHeader="True" ShowSelectButton="True">
                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </p>
        <br />
        <br />

        <asp:Panel ID="PanelNewOrder" runat="server" Visible="False">
            <p class="text-center" style="font-size: large">
                <asp:Label ID="lblOrderDetails" runat="server" Style="font-weight: 700" Text="Label"></asp:Label>
            </p>
            <p>
                <asp:GridView ID="gvNewOrder" runat="server" OnRowCancelingEdit="gvNewOrder_RowCancelingEdit" OnRowEditing="gvNewOrder_RowEditing" OnRowUpdating="gvNewOrder_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowDeleting="gvNewOrder_RowDeleting" CssClass="table">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ReadOnly="True" />
                        <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" />
                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:c}",Eval("UnitPrice")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ButtonType="Button" HeaderText="Edit" ShowEditButton="True" ShowHeader="True" />
                        <asp:CommandField ButtonType="Button" HeaderText="Delete" ShowDeleteButton="True" ShowHeader="True" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </p>
            <br />


            <h2>Supply By:</h2>
        <asp:Calendar ID="cldSupply" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <WeekendDayStyle BackColor="#FFFFCC" />
        </asp:Calendar>
            <br />

            <p>
                Total Amount:&nbsp; $
                <asp:Label ID="lblTotalAmount" runat="server" ForeColor="#009933" Text="Label"></asp:Label>
            </p>
        </asp:Panel>
    </asp:Panel>
    <p>
        &nbsp;
    </p>
    <p>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-danger" />
        &nbsp;&nbsp;
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click2" CssClass="btn btn-success" />
    </p>
</asp:Content>
