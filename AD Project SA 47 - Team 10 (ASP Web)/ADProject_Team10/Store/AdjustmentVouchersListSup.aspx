<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdjustmentVouchersListSup.aspx.cs" Inherits="ADProject_Team10.Store.AdjustmentVouchersListSup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Adjustment Vouchers</h1>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">There are no vouchers at the moment.</div>
    </asp:Panel>

    <!--All Issued Vouchers-->
    <asp:GridView ID="gvVouchers" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="gvVouchers_SelectedIndexChanged" SelectedIndex="0" CssClass="table">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />

            <asp:BoundField DataField="VoucherNumber" HeaderText="Voucher #" SortExpression="VoucherNumber" />

            <asp:TemplateField HeaderText="Date Approved" SortExpression="DateApproved">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" DataFormatString="{0:dd-MMM-yyyy}" Text='<%# Bind("DateApproved" ,"{0:dd MMM yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Approved By" SortExpression="ClerkEmployeeId">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# FindEmpNameById((int)Eval("ApprovedByEmployeeId")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Total Cost">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# string.Format("{0:c}", FindTotalCost((int)Eval("VoucherNumber"))) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <SelectedRowStyle BackColor="#FF9933" ForeColor="Black" />
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />

    <!-- Selected Voucher -->
    <asp:Panel ID="PanelVoucherDetails" runat="server">
        <div class="jumbotron" style="background-color: #fff;">

            <!--Voucher Header-->
            <h2 style="text-align: center">Voucher# :
                <asp:Label ID="lblVoucherNumber" runat="server" Text="Label"></asp:Label></h2>

            <table style="width: 400px" class="table">
                <tr>
                    <td style="width: 150px">Date Issued:</td>
                    <td><strong>
                        <asp:Label ID="lblDateIssued" runat="server" Text="Label" /></strong></td>
                </tr>
                <tr>
                    <td style="width: 150px">Approved By:</td>
                    <td><strong>
                        <asp:Label ID="lblApprovedBy" runat="server" Text="Label" /></strong></td>
                </tr>
            </table>

            <!--Voucher Details-->
            <asp:GridView ID="gvItems" runat="server" Width="100%" AutoGenerateColumns="False" CssClass="table">
                <Columns>

                    <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# FindDescription((string)Eval("ItemCode")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity Adjusted">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("QuantityAdjusted") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reason">
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cost">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# string.Format("{0:c}", FindAdjustmentCost((string)Eval("ItemCode"), (int)Eval("QuantityAdjusted"))) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <HeaderStyle BackColor="#666666" ForeColor="White" />
            </asp:GridView>
            <br />

            <!--Voucher Footer-->
            <table class="table" style="width: 600px">
                <tr>
                    <td style="width: 97px">Remarks: </td>
                    <td style="width: 530px"><strong>
                        <asp:TextBox ID="txbRemarks" runat="server" CssClass="form-control" ReadOnly="True" TextMode="MultiLine"></asp:TextBox>
                    </strong></td>
                </tr>
                <tr>
                    <td style="width: 97px"> Total Cost:</td>
                    <td style="width: 530px"> <strong><asp:Label ID="lblTotalCost" runat="server" Text="Label" Font-Size="Small" ForeColor="Green" /></strong></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
