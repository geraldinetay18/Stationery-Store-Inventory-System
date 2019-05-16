<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdjustmentRequestsListSup.aspx.cs" Inherits="ADProject_Team10.Store.AdjustmentRequestsListSup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Adjustment Requests</h1>
    <br />

    <asp:Panel ID="PanelNotes" runat="server">
        <p>
            Each of the following stock adjustments cost <strong>less than $250</strong>. 
        Selected adjustments that are <strong>approved together</strong> would be stored
        in the <strong>same voucher.</strong>
        </p>

        <p>
            <span style="color: red">Any adjusted quantities in red</span> suggest that the negative adjustment has exceeded current quantity. If approval is made, negative adjustment would be modified accordingly and <strong>current quantity would be reduced to zero</strong>.
        </p>
    </asp:Panel>

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">There are no stock adjustment requests <strong>below $250</strong> at the moment.</div>
    </asp:Panel>

    <!-- List of Pending Requests-->
    <asp:Panel ID="PanelGridViewAndActions" runat="server">

        <asp:GridView ID="gvItems" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnRowDataBound="gvItems_RowDataBound" CssClass="table">
            <Columns>

                <asp:TemplateField HeaderText="Select">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectHeader" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectHeader_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
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

                <asp:TemplateField HeaderText="Adjusted Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQtyAdjusted" runat="server" Text='<%# Eval("QuantityAdjusted") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Current Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQtyCurrent" runat="server" Text='<%# FindCurrentQuantity((string)Eval("ItemCode")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reason">
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Cost">
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# string.Format("{0:c}", FindAdjustmentCost((string)Eval("ItemCode"), (int)Eval("QuantityAdjusted"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                            <asp:TemplateField HeaderText="Adjustment ID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblAdjustmentId" runat="server" Text='<%# Eval("AdjustmentId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#666666" ForeColor="White" />
        </asp:GridView>
        <br />

        <!--Actions (Remarks, Approve, Reject)-->
        <p><strong>Remarks:</strong></p>
        <asp:TextBox ID="txbRemarks" runat="server" Height="100px" Width="350px" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" CssClass="btn btn-danger" />
        &nbsp;&nbsp;
        <asp:Button ID="btnApprove" runat="server" Text="Issue Voucher" OnClick="btnApprove_Click" CssClass="btn btn-success" />
    </asp:Panel>

</asp:Content>
