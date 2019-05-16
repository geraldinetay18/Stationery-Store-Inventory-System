<%@ Page Title="Manage Department Representative" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageDeptRep.aspx.cs" Inherits="ADProject_Team10.Dept.ManageDeptRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Authorize Department Representative</h1>
    <br />

    <!--Message-->
    <asp:Panel ID="PanelGoodMessage" runat="server" Visible="False">
        <div class="alert alert-success alert-dismissible"><button type="button" class="close" data-dismiss="alert">&times;</button>
            <asp:Label ID="lblGoodMessage" runat="server" Text="Label"></asp:Label>
        </div>
    </asp:Panel>
    <asp:Panel ID="PanelFailMessage" runat="server" Visible="False">
        <div class="alert alert-danger alert-dismissible"><button type="button" class="close" data-dismiss="alert">&times;</button>
            <asp:Label ID="lblFailMessage" runat="server" Text="Label"></asp:Label>
        </div>
    </asp:Panel>
    <div>
        <table class="table" >
            <tr>
                <td>Current Department Representative
                </td>
                <td><strong>
                    <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></strong>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />
    <div >
            <h3>Select New Department Representative</h3>
        <br />

        <table class="table" >
                   <tr>
                <td style="width: 164px">Search Employee by Name
                </td>
                <td style="width: 272px">
                    <asp:TextBox ID="txtNameSearch" runat="server" ToolTip="Enter Employee Name" CssClass="form-control" Width="230px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="SearchName" CssClass="btn btn-warning" />

                &nbsp;&nbsp;

                    <asp:Button ID="btnReset" runat="server" Text="View All" OnClick="btnReset_Click" CssClass="btn btn-primary" />
                </td>
            </tr>
            <tr>
                <td style="width: 164px"></td>
                <td style="width: 272px">

                    &nbsp;&nbsp;
                    
                    <asp:RequiredFieldValidator ID="rfvNameSearch" runat="server" ErrorMessage="*Employee Name is Required" ControlToValidate="txtNameSearch" ValidationGroup="SearchName" ForeColor="Red">*Employee Name is Required</asp:RequiredFieldValidator>
                    
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

        <asp:GridView ID="gvStaffList" runat="server" AutoGenerateColumns="False" OnRowCommand="gvStaffList_RowCommand" OnRowDataBound="gvStaffList_RowDataBound" GridLines="None" CssClass="table" AllowPaging="True" OnPageIndexChanging="gvStaffList_PageIndexChanging" PageSize="5" >
            <Columns>
                <asp:BoundField DataField="EmployeeId" HeaderText="EmployeeId" Visible="False" />
                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="btnReplace" runat="server" CommandName="Replace" CommandArgument='<%# Eval("EmployeeId")%>' CausesValidation="false" Text="Replace" CssClass="btn btn-success" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#666666" ForeColor="White" />
            <PagerStyle HorizontalAlign="Center" CssClass="gridViewPager" />
        </asp:GridView>
    </div>
</asp:Content>
