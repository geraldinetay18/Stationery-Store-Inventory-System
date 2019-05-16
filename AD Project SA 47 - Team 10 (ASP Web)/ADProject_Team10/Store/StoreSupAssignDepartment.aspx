<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StoreSupAssignDepartment.aspx.cs" Inherits="ADProject_Team10.Store.StoreSupAssignDepartment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <!--Title-->
    <h1>Department Allocation</h1>
    <br />

    <p>
        <asp:GridView ID="AssignStoreClerkGridView" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="OnRowCancelingEdit" OnRowEditing="OnRowEditing" OnRowUpdating="OnRowUpdating" TabIndex="-1" CssClass="table">
            <Columns>
                <asp:TemplateField HeaderText="Department ID" Visible="false">
                    <EditItemTemplate>
                        <asp:Label ID="DeptIdTextBox" runat="server" Text='<%# Bind("DeptId1") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="DeptIdLabel" runat="server" Text='<%# Bind("DeptId1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department Name">
                    <ItemTemplate>
                        <asp:Label ID="DeptNameLabel" runat="server" Text='<%# Bind("DeptName1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Store Clerk ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="StoreClerkIdLabel" runat="server" Text='<%# Bind("StoreClerkId1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Store Clerk Name">
                    <EditItemTemplate>
                        <asp:DropDownList ID="StoreClerkIdList" runat="server" DataSourceID="SqlDataSource" DataTextField="EmployeeName" DataValueField="EmployeeId">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [EmployeeId], [EmployeeName] FROM [Employee] WHERE ([Role] = @Role)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="Store Clerk" Name="Role" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="StoreClerkNameLabel" runat="server" Text='<%# Bind("StoreClerkName1") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" HeaderText="Action" />
            </Columns>
            <EmptyDataTemplate>No records</EmptyDataTemplate>
            <HeaderStyle BackColor="#666666" ForeColor="White" />
        </asp:GridView>
    </p>
</asp:Content>
