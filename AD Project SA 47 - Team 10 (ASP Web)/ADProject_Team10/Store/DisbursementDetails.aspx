<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DisbursementDetails.aspx.cs" Inherits="ADProject_Team10.Store.DisbursementDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Disbursement Details</h1>
    <br />

    <asp:Button ID="btnBack" runat="server" Text="< Back" CssClass="btn btn-primary" OnClick="btnBack_Click" />
    &nbsp;
    <asp:Button ID="btnNotCollected" runat="server" Text="Mark As Not Collected" CssClass="btn btn-danger" Visible="false" OnClick="btnNotCollected_Click" />&nbsp;

    <asp:Button ID="btnRequestAcknowledgement" runat="server" Text="Request for Acknowledgement >" CssClass="btn btn-success" Visible="false" OnClick="btnRequestAcknowledgement_Click" />
    <br />
    <br />
    <div>

        <table class="table">
            <tr>
                <td>
                    <label>Collection Point</label></td>
                <td>
                   <asp:Label ID="lblCollectionPoint" runat="server" Text="Label"></asp:Label></td>
                <td></td>
                <td>
                    <label>Department</label></td>
                <td>
                   <asp:Label ID="lblDept" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <label>Disbursement Date</label></td>
                <td class="auto-style2">
                    <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label></td>
                <td class="auto-style2"></td>
                <td class="auto-style2">
                    <label>Disbursement Time</label></td>
                <td class="auto-style2">
                    <asp:Label ID="lblTime" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <label>Representative</label></td>
                <td>
                    <asp:Label ID="lblRepName" runat="server" Text="Label"></asp:Label></td>
                <td>&nbsp;</td>
                <td>
                    <label>Status</label></td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <label>Disbursed By</label></td>
                <td>
                    <asp:Label ID="lblStoreClerk" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>
    </div>
    <br />
    <asp:Label ID="lblNotification" runat="server" Text="Label" Visible="false" ForeColor="Red"></asp:Label>

    <asp:GridView ID="gvDeptReqDetails" runat="server" AutoGenerateColumns="False" CssClass="table">
        <Columns>
            <asp:TemplateField HeaderText="Item Description">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# GetItem(Eval("ItemCode").ToString()).Description %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Requested">
                <ItemTemplate>
                    <asp:Label ID="lblQtyReq" runat="server" Text='<%# Bind("QuantityRequested") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Received">
                <ItemTemplate>
                    <asp:Label ID="lblQtyRec" runat="server" Text='<%# Bind("QuantityReceived") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remark">
                <ItemTemplate>
                    <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>


    <asp:GridView ID="gvUpdateReqDetails" runat="server" AutoGenerateColumns="False" CssClass="table" Visible="False">
        <Columns>
            <asp:TemplateField HeaderText="Item Description">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# GetItem(Eval("ItemCode").ToString()).Description %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Requested">
                <ItemTemplate>
                    <asp:Label ID="lblQtyReq" runat="server" Text='<%# Bind("QuantityRequested") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quantity Received">
                <ItemTemplate>
                    <asp:TextBox ID="tbQtyReceived" runat="server" TextMode="Number" Text='<%# Eval("QuantityReceived") %>'></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*Receive quantity cannot be more than Retrieved quantity" ControlToValidate="tbQtyReceived" MinimumValue="0" MaximumValue='<%# Eval("QuantityReceived") %>' Type="Integer" ForeColor="Red"></asp:RangeValidator>
                </ItemTemplate>
            </asp:TemplateField>
            <%--            <asp:TemplateField HeaderText="Remark">
                <ItemTemplate>
                    <asp:TextBox ID="tbRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Quantity to Raise SAV">
                <ItemTemplate>
                    <asp:TextBox ID="tbRaiseSAV" runat="server" TextMode="Number" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:TextBox ID="tbReason" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
</asp:Content>

