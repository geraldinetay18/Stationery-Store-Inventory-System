<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RequisitionTrendOnDepartment.aspx.cs" Inherits="ADProject_Team10.Store.RequisitionTrendOnDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Trend Analysis By Department</h1>
    <br />

    <p>
        Department 1:<asp:DropDownList ID="ddlDep1" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDep1_SelectedIndexChanged" Width="420px" DataSourceID="rtDept1" DataTextField="DeptId" DataValueField="DeptId">
        </asp:DropDownList>
        <asp:SqlDataSource ID="rtDept1" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [DeptId] FROM [Department]"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        Department 2:<asp:DropDownList ID="ddlDep2" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDep2_SelectedIndexChanged" Width="420px" DataSourceID="rtDept2" DataTextField="DeptId" DataValueField="DeptId">
        </asp:DropDownList>
        <asp:SqlDataSource ID="rtDept2" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [DeptId] FROM [Department]"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbldep" runat="server" Font-Italic="True" ForeColor="Red" Text="Label"></asp:Label>
    </p>
    <p>
        Category:<asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="420px" DataSourceID="rtCategory" DataTextField="CategoryName" DataValueField="CategoryName">
        </asp:DropDownList>
        <asp:SqlDataSource ID="rtCategory" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [CategoryName] FROM [Category]"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        Start Year:<asp:DropDownList ID="ddlStartYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlStartYear_SelectedIndexChanged" Width="420px">
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        Start Month:<asp:DropDownList ID="ddlStartMonth" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlStartMonth_SelectedIndexChanged" Width="420px">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        End Year:<asp:DropDownList ID="ddlEndYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEndYear_SelectedIndexChanged" Width="420px">
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        End Month:<asp:DropDownList ID="ddlEndMonth" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEndMonth_SelectedIndexChanged" Width="420px">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblDateError" runat="server" Font-Italic="True" ForeColor="Red" Text="Label"></asp:Label>
    </p>
    <p>
        &nbsp;
    </p>
        <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Generate Table and Graph" OnClick="Button1_Click" />
   <br />
   <br />
   <br />
    <asp:Panel ID="PanelRequisitionTrend" runat="server" Visible="False">
        <p class="text-center" style="font-size: large">
            <asp:Label ID="lblTableTitle" runat="server" Style="font-weight: 700" Text="Label"></asp:Label>
        </p>
        <p>
            <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table" ForeColor="Black" GridLines="Horizontal">
                <Columns>

                    <asp:BoundField DataField="Month" HeaderText="Month" />
                    <asp:BoundField DataField="Quantity" />
                    <asp:BoundField DataField="CompareQuantity" />

                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </p>
        <p>
            &nbsp;
        </p>
        <p>
            &nbsp;
        </p>
        <p>
            &nbsp;
        </p>
        <p class="text-center">
            <asp:Label ID="lblChartTitle" runat="server" Style="font-weight: 700; font-size: large" Text="Label"></asp:Label>
        </p>
        <p>

            <asp:Chart ID="Chart1" runat="server" Width="900px">
                <Series>
                    <asp:Series Legend="Department1">
                    </asp:Series>
                    <asp:Series ChartArea="ChartArea1" Legend="Department2">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Name="Department1">
                    </asp:Legend>
                    <asp:Legend Name="Department2">
                    </asp:Legend>
                </Legends>
            </asp:Chart>
        </p>
    </asp:Panel>
</asp:Content>
