<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequisitionsList.aspx.cs" Inherits="ADProject_Team10.Store.RequisitionsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Requisitions</h1>
    <p><asp:Label ID="lblNotification" runat="server" Text="Notification" Font-Size="Small" ForeColor="#FF3300"></asp:Label></p>
    <div>
        <table>
            <tr>
                <td><label>Status</label></td>
                <td><asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                    <asp:ListItem>ALL</asp:ListItem>
                    <asp:ListItem>Approved</asp:ListItem>
                    <asp:ListItem>In Progress</asp:ListItem>
                    <asp:ListItem>Completed</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><label>Department</label>&nbsp;&nbsp;</td>
                <td><asp:DropDownList ID="ddlDept" runat="server" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td><label>From</label></td>
                <td><asp:TextBox ID="tbFrom" runat="server" TextMode="Date" ControlToCompare="tbFrom" CssClass="form-control"></asp:TextBox></td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td><label>To&nbsp;&nbsp; </label></td>
                <td><asp:TextBox ID="tbTo" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox> </td>
                <td><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToCompare="tbFrom" ControlToValidate="tbTo" CultureInvariantValues="True" Operator="GreaterThanEqual" SetFocusOnError="True" Type="Date" Font-Size="Large" ForeColor="Red"></asp:CompareValidator></td>
            </tr>
        </table>
    <br />
    <table>
        <tr>
            <td><asp:Button ID="btnGenerate" runat="server" Text="Search" OnClick="btnGenerate_Click" CssClass="btn btn-warning"/></td>
            <td>&nbsp;&nbsp;</td>
            <td><asp:Button ID="btnListAll" runat="server" Text="View All" CssClass="btn btn-primary" OnClick="btnListAll_Click" CausesValidation="False"/></td>
            <td></td>
            <td style="align-content:flex-end">
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAddToRetrial" runat="server" Text="Proceed Approved Requisitions To Retrieval >" OnClick="btnAddToRetrial_Click" CssClass="btn btn-success" CausesValidation="False"/>
                &nbsp;
                <asp:Button ID="btnMoveToRetrieval" runat="server" Text="Complete Outstanding Retrieval &gt;" OnClick="btnMoveToRetrieval_Click" Visible="False" CssClass="btn btn-success" CausesValidation="False"/>
            </td>
        </tr>
    </table>
    </div>
    
    <p><asp:Label ID="lblHadRetrieval" runat="server" Text="Label" Visible="False"></asp:Label></p>
    

    <asp:GridView ID="gvDeptReqList" runat="server" AutoGenerateColumns="False" DataKeyNames="RequisitionId" style="margin-bottom: 0px" CssClass="table" OnRowDataBound="gvDeptReqList_RowDataBound" >
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataTextField="RequisitionId" HeaderText="Requisition ID" DataNavigateUrlFields="RequisitionId" DataNavigateUrlFormatString="~/Store/RequisitionDetails.aspx?id={0}" DataTextFormatString="RQ{0}" Text="Eval(&quot;RequisitionId&quot;)" />
            <asp:TemplateField HeaderText="Department Name">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# GetDeptNameByReqId(Int32.Parse(Eval("RequisitionId").ToString())) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RequisitionDate" HeaderText ="Date" DataFormatString="{0:dd MMM yyyy}" />
            <asp:BoundField DataField="RequisitionStatus" HeaderText="Status" />
            <asp:BoundField DataField="Remark" HeaderText="Remarks" />
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
</asp:Content>

