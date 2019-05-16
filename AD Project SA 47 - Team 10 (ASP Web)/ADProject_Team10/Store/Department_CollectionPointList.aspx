<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Department_CollectionPointList.aspx.cs" Inherits="ADProject_Team10.Store.Department_CollectionPointList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--My Departments-->
    <h1>My Departments (Retrieval) </h1>
    <br />

    <asp:Label ID="lblEemail" runat="server" Visible="False"></asp:Label>

    <asp:GridView ID="gvCollection_Point" runat="server" AutoGenerateColumns="False" DataSourceID="Department_CollectionList" CssClass="table">
        <Columns>
            <asp:BoundField DataField="DeptName" HeaderText="Department Name" SortExpression="DeptName" />
            <asp:TemplateField HeaderText="Department Representative" SortExpression="RepresentativeId">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# FindEmployeeName((int)Eval("RepresentativeId")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LocationName" HeaderText="Collection Point" SortExpression="LocationName" />
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <br />
    <asp:SqlDataSource ID="Department_CollectionList" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="getDepartCollectList" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="lblEemail" Name="Email" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <!-- My Collection Points-->
    <h1>My Collection Points (Disbursement)</h1>
    <br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table" DataKeyNames="LocationId" DataSourceID="MyCollectionPoint">
        <Columns>
            <asp:BoundField DataField="LocationName" HeaderText="Collection Point" SortExpression="LocationName" />
            <asp:BoundField DataField="Time" DataFormatString="{0:hh\:mm}" HeaderText="Time" SortExpression="Time" />
            <asp:BoundField DataField="DeptName" HeaderText="Department" SortExpression="DeptName" />
            <asp:BoundField DataField="RepresentativeName" HeaderText="Department Representative" SortExpression="RepresentativeName" />
        </Columns>
        <HeaderStyle BackColor="#666666" ForeColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="MyCollectionPoint" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="getCollectionPointList" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="clerkid" SessionField="employeeId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
