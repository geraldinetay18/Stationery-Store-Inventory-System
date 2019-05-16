<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RetrievalNew.aspx.cs" Inherits="ADProject_Team10.Store.RetrievalNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function CheckBoxRequired_ClientValidate(sender, e) {
            e.IsValid = jQuery(".AcceptedAgreement input:checkbox").is(':checked');
        }
    </script>

    <h1>New Retrieval Form</h1>
    <p>
        <asp:Label ID="lblNotification" runat="server" Text="Notification" Visible="False"></asp:Label>
    </p>
    <div>
        <table>
            <tr>
                <td>
                    <label>Retrieval number </label>
                    &nbsp;&nbsp;&nbsp;</td>
                <td>
                    <asp:Label ID="lblRetrNum" runat="server" Text="Retrieval number"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <label>Retrieval date</label></td>
                <td>
                    <asp:Label ID="lblRetrDate" runat="server" Text="Retrieval date"></asp:Label></td>
            </tr>
        </table>
    </div>

    <asp:Button ID="btnTickAll" runat="server" Text="Click To Tick All" CssClass="btn btn-info" OnClick="btnTickAll_Click" CausesValidation="False" />
    &nbsp;&nbsp;
    <asp:Button ID="btnComplete" runat="server" Text="Proceed To Allocation >" CssClass="btn btn-success" OnClick="btnComplete_Click" />
    <br />
    <br />

    <asp:GridView ID="gvRetrList" runat="server" AutoGenerateColumns="False" CssClass="table" HeaderStyle-BackColor="#666666" HeaderStyle-ForeColor="White">
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bin#">
                <ItemTemplate>
                    <asp:Label ID="lblLocation" runat="server" Text='<%# GetStationeryByItemCode(Eval("ItemCode").ToString()).Bin %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Stationery Description">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# GetStationeryByItemCode(Eval("ItemCode").ToString()).Description %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Stock Quantity">
                <ItemTemplate>
                    <asp:Label ID="lblStock" runat="server" Text='<%# GetStationeryByItemCode(Eval("ItemCode").ToString()).QuantityInStock %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Requested Quantity">
                <ItemTemplate>
                    <asp:Label ID="lblReqQty" runat="server" Text='<%# Bind("QuantityNeeded") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Retrieved Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="tbRetrQty" runat="server" TextMode="Number" Text='<%# Eval("QuantityRetrieved") %>' Width="105px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Adjusted Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="tbRaiseSAV" runat="server" TextMode="Number" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason">
                <ItemTemplate>
                    <asp:TextBox ID="tbReason" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Retrieved">
                <ItemTemplate>
                    <asp:CheckBox ID="cbRetrieved" runat="server" CssClass="AcceptedAgreement" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%--<asp:CheckBox runat="server" ID="MyCheckBox" CssClass="AcceptedAgreement" />--%>
                    <asp:CustomValidator runat="server" ID="CheckBoxRequired" EnableClientScript="true"
                        ClientValidationFunction="CheckBoxRequired_ClientValidate" Font-Size="Small" ForeColor="Red">* Please select the checkbox to proceed</asp:CustomValidator>

                    <br />

                    <asp:RangeValidator ID="vtbRetrQty" runat="server" ErrorMessage="* Retrieval Quantity should be between 0 and  the lower of Stock Quantity and Requested Quantity" ControlToValidate="tbRetrQty" Type="Integer" MinimumValue="0" MaximumValue='<%# Math.Min((int)Eval("QuantityNeeded"), GetStationeryByItemCode(Eval("ItemCode").ToString()).QuantityInStock ) %>' Font-Size="Small" ForeColor="Red"></asp:RangeValidator>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

        <HeaderStyle BackColor="#666666" ForeColor="White"></HeaderStyle>
    </asp:GridView>
</asp:Content>
