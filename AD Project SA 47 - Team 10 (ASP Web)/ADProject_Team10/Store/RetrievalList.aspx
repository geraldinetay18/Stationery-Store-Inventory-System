<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RetrievalList.aspx.cs" Inherits="ADProject_Team10.Store.GenerateRetrieval" %>

<%--<asp:Content ID="RetrHeadContent" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .collapsed-row {
            display: none;
            padding: 0px;
            margin: 0px;
        }

        .auto-style1 {
            width: 100%;
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
    <h1>Retrievals</h1>
    <br />

    <!--Search bar-->
    <asp:Button ID="btnMoveToRetrieval" runat="server" Text="Complete outstanding retrieval >" OnClick="btnMoveToRetrieval_Click" Visible="False" CssClass="btn btn-success" /><br />
    <br />

    <table class="auto-style1">
        <tr>
            <td>
                <label>From</label></td>
            <td>
                <asp:TextBox ID="tbFrom" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>
                <label>To</label></td>
            <td>
                <asp:TextBox ID="tbTo" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox></td>
            <td>&nbsp;</td>
            <td>
                <asp:CompareValidator ID="CompareValidatorDate" runat="server" ControlToCompare="tbTo" ControlToValidate="tbFrom" ErrorMessage="* End Date should be earlier than Start Date" ForeColor="Red" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                <label>Retrieval By</label></td>
            <td>
                <asp:DropDownList ID="ddlRetrievalBy" runat="server" CssClass="form-control"></asp:DropDownList>
            </td>
            <td>&nbsp;</td>
            <td>
                <label>Status</label></td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList></td>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnGenerate" runat="server" Text="Search" OnClick="btnGenerate_Click" CssClass="btn btn-warning" />
                &nbsp;&nbsp;
                <asp:Button ID="btnViewAll" runat="server" Text="View All" OnClick="btnViewAll_Click" CssClass="btn btn-primary" /></td>
        </tr>
    </table>
    <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">There are no Retrieval Forms at the moment.</div>
    </asp:Panel>

    <asp:GridView ID="gvRetrievalList" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvRetrievalList_SelectedIndexChanged" DataKeyNames="RetrievalId" OnRowDataBound="gvRetrievalList_RowDataBound" CssClass="table">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <button class="btn btn-default glyphicon glyphicon-plus" onclick="return ToggleGridPanel(this,'tr<%# Eval("RetrievalId") %>')" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RetrievalId" HeaderText="Retrieval Id" InsertVisible="False" ReadOnly="True" SortExpression="RetrievalId" />
            <asp:BoundField DataField="DateRetrieved" HeaderText="Date" SortExpression="DateRetrieved" DataFormatString="{0:dd MMM yyyy}" />
            <asp:TemplateField HeaderText="Employee Name" SortExpression="EmployeeName">
                <ItemTemplate>
                    <asp:Label ID="lblEmplName" runat="server" Text='<%# GetEmployeeName(Int32.Parse(Eval("EmployeeId").ToString())) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <%--<asp:CommandField ShowSelectButton="True" />--%>
            <asp:TemplateField HeaderText="Remark" SortExpression="Remark">
                <ItemTemplate>
                    <%# Eval("Remark") %>
                    <%# MyNewRow(Int32.Parse(Eval("RetrievalId").ToString())) %>
                    <asp:GridView ID="gvDetails" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="false" DataKeyNames="RetrievalId">
                        <Columns>
                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                            <asp:BoundField DataField="QuantityNeeded" HeaderText="Requested Quantity" />
                            <asp:BoundField DataField="QuantityRetrieved" HeaderText="Retrieved Quantity" />
                        </Columns>
                    </asp:GridView>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>


</asp:Content>
