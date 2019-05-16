<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequisitionListStaff.aspx.cs" Inherits="ADProject_Team10.Dept.RequisitionListStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>My Requisitions</h1>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelMessage" runat="server" Visible="False">
        <div class="alert alert-success">You do not have any submitted Requisitions at the moment.</div>
    </asp:Panel>

    <asp:GridView ID="gvRequisitions" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="table" OnRowDataBound="gvRequisitions_RowDataBound" OnRowCommand="gvRequisitions_RowCommand" DataKeyNames="RequisitionId">
        <Columns>
            <asp:BoundField DataField="RequisitionDate" HeaderText="Requisition Date" SortExpression="RequisitionDate" DataFormatString="{0: dd MMM yyyy}" />
            <asp:TemplateField HeaderText="Approved By">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ApprovedByEmployeeId") == DBNull .Value? "None yet" : FindEmployeeName((int)Eval("ApprovedByEmployeeId")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Remark" HeaderText="Remarks" SortExpression="Remark" />
            <asp:BoundField DataField="RequisitionStatus" HeaderText="Status" SortExpression="RequisitionStatus" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:LinkButton ID="btnViewDetails" runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%#Eval("RequisitionId")%>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <SelectedRowStyle BackColor="#FF9933" />
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT * FROM [Requisition] WHERE ([EmployeeId] = @EmployeeId) ORDER BY [RequisitionDate] DESC">
        <SelectParameters>
            <asp:SessionParameter Name="EmployeeId" SessionField="employeeId" Type="Int32" DefaultValue="10001" />
        </SelectParameters>
    </asp:SqlDataSource>

  <%--      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="table" OnRowDataBound="gvRequisitions_RowDataBound" OnRowCommand="gvRequisitions_RowCommand" DataKeyNames="RequisitionId">
        <Columns>
            <asp:BoundField DataField="RequisitionDate" HeaderText="Requisition Date" SortExpression="RequisitionDate" DataFormatString="{0: dd MMM yyyy}" />
            <asp:TemplateField HeaderText="Approved By">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ApprovedByEmployeeId") == DBNull .Value? "None yet" : FindEmployeeName((int)Eval("ApprovedByEmployeeId")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
            <asp:BoundField DataField="RequisitionStatus" HeaderText="Status" SortExpression="RequisitionStatus" />
            <asp:TemplateField HeaderText="View Details">
                <ItemTemplate>
                    <asp:LinkButton ID="btnViewDetails" runat="server" Text="View Details" CommandName="ViewDetails" CommandArgument='<%#Eval("RequisitionId")%>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <SelectedRowStyle BackColor="#FF9933" />
    </asp:GridView>--%>
    <br />
</asp:Content>
