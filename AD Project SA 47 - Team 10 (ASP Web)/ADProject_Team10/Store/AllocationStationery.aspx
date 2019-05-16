<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllocationStationery.aspx.cs" Inherits="ADProject_Team10.Store.AllocationStationery" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .collapsed-row {
            /*display: none;*/
            padding: 0px;
            margin: 0px;
        }

        .auto-style1 {
            border-collapse: collapse;
            max-width: 100%;
            margin-bottom: 20px;
        }
    </style>
    <script type="text/javascript">
        function ToggleGridPanel(btn, row) {
            var current = $('#' + row).css('display');
            if (current == 'none') {
                $('#' + row).show();
                $(btn).removeClass('glyphicon-plus')
                $(btn).addClass('glyphicon-minus')
            } else {
                $('#' + row).hide();
                $(btn).removeClass('glyphicon-minus')
                $(btn).addClass('glyphicon-plus')
            }
            return false;
        }
    </script>

    <!--Title-->
    <h1>Current Allocation of Stationery</h1>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">There is no allocation at the moment. </div>
    </asp:Panel>

    <asp:Panel ID="PanelAll" runat="server">
        <asp:Button ID="btnNext" runat="server" Text="Proceed to Disbursement &gt;" OnClick="btnNext_Click" CssClass="btn btn-success" Style="height: 36px" />
        <br />
        <br />

        <asp:Label ID="lblNotification" runat="server" Text="Label" Visible="False" ForeColor="Red"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:GridView ID="gvAllocation" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAllocation_RowDataBound" DataKeyNames="ItemCode" CssClass="table">
                    <Columns>
                        <asp:TemplateField HeaderText="Stationery Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# GetStationeryByItemCode(Eval("ItemCode").ToString()).Description %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# GetStatus(Eval("ItemCode").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblReqQty" runat="server" Text='<%# Bind("QuantityNeeded") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Retrieved Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblRetrQty" runat="server" Text='<%# Bind("QuantityRetrieved") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <button class="btn btn-default glyphicon glyphicon-plus" onclick="return ToggleGridPanel(this,'tr<%# Eval("ItemCode") %>')" />
                                <%# MyNewRow(Eval("ItemCode").ToString()) %>
                                <asp:GridView ID="gvDepartments" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ItemCode" OnRowCommand="gvDepartments_RowCommand" OnRowDataBound="gvDepartments_RowDataBound" CssClass="auto-style1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>) 
                                            <asp:HiddenField ID="hDDId" runat="server" Value='<%# Eval("DisbursementDetailsId") %>'></asp:HiddenField>
                                                <%--<asp:HiddenField ID="hDeptId" runat="server" Text='<%# GetDeptByDisbDetId(Int32.Parse(Eval("DisbursementDetailsId").ToString())).DeptId %>'></asp:HiddenField>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeptName" runat="server" Text='<%# GetDeptByDisbDetId(Int32.Parse(Eval("DisbursementDetailsId").ToString())).DeptName %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requested Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("QuantityRequested") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Disbursing Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbDisbQty" runat="server" Text='<%# Eval("QuantityReceived") %>' TextMode="Number"></asp:Label>
                                                <asp:Button ID="btnChangeDisbQty" runat="server" Text="Change" CommandName="ChangeDisbQty" />
                                                <asp:TextBox ID="tbDisbQty" runat="server" Text='<%# Eval("QuantityReceived") %>' TextMode="Number" Visible="false"></asp:TextBox>
                                                <asp:Button ID="btnUpdateDisbQty" runat="server" Text="Update" CommandName="UpdateDisbQty" Visible="false" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="CancelUpdate" Visible="false" CausesValidation="False" />
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*Disbursing Quantity can not be less than generated Disbursing Quantity or more than Requested Quantity" ControlToValidate="tbDisbQty" MinimumValue='<%# Eval("QuantityReceived") %>' MaximumValue='<%# Eval("QuantityRequested") %>' ForeColor="Red" Type="Integer"></asp:RangeValidator>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#666666" ForeColor="White" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
