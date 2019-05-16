<%@ Page Title="" Language="C#" Debug="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderTrendOnCategory.aspx.cs" Inherits="ADProject_Team10.Store.OrderTrendOnCategory" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <!--Title-->
    <h1>Trend Analysis by Suppliers</h1>
    <br />

    <p>
        Supplier 1:&nbsp;
        <asp:DropDownList ID="ddlSupplier1" runat="server" CssClass="form-control" DataSourceID="otSupplier1" DataTextField="SupplierCode" DataValueField="SupplierCode" Width="300px" OnSelectedIndexChanged="ddlSupplier1_SelectedIndexChanged">

        </asp:DropDownList>
        <asp:SqlDataSource ID="otSupplier1" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [SupplierCode] FROM [Supplier]"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        Supplier 2:&nbsp;
        <asp:DropDownList ID="ddlSupplier2" runat="server" CssClass="form-control" DataSourceID="otSupplier2" DataTextField="SupplierCode" DataValueField="SupplierCode" Width="300px" OnSelectedIndexChanged="ddlSupplier2_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="otSupplier2" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [SupplierCode] FROM [Supplier]"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblSupplierError" runat="server" Font-Bold="False" Font-Italic="True" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" Text="Label"></asp:Label>
    </p>
    <p>
        Category:</p>
    <p>
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataSourceID="otCategory" DataTextField="CategoryName" DataValueField="CategoryName" Width="300px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="otCategory" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [CategoryName] FROM [Category]"></asp:SqlDataSource>
    </p>
    <p>
        &nbsp;</p>
    <p>
        Start Year:</p>
    <p>
        <asp:DropDownList ID="ddlStartYear" runat="server" CssClass="form-control" Width="300px" OnSelectedIndexChanged="ddlStartYear_SelectedIndexChanged" >
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        &nbsp;</p>
    <p>
        Start Month:</p>
    <p>
        <asp:DropDownList ID="ddlStartMonth" runat="server" CssClass="form-control" Width="300px" OnSelectedIndexChanged="ddlStartMonth_SelectedIndexChanged">
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
        &nbsp;</p>
    <p>
        End Year:</p>
    <p>
        <asp:DropDownList ID="ddlEndYear" runat="server" CssClass="form-control" Width="300px" OnSelectedIndexChanged="ddlEndYear_SelectedIndexChanged">
            <asp:ListItem>2019</asp:ListItem>
            <asp:ListItem>2020</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        &nbsp;</p>
    <p>
        End Month:</p>
    <p>
        <asp:DropDownList ID="ddlEndMonth" runat="server" CssClass="form-control" Width="300px" OnSelectedIndexChanged="ddlEndMonth_SelectedIndexChanged">
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblDateError" runat="server" Font-Italic="True" ForeColor="Red" Text="Label"></asp:Label>
    </p>
        <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success" OnClick="btnGenerate_Click" Text="Generate Table and Graph" />
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <asp:Panel ID="PanelOrderTrend" runat="server" Visible="False">
    <p class="text-center" style="font-size: large">
        <asp:Label ID="lblTableTiltle" runat="server" style="font-weight: 700" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="gvTable" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Month" HeaderText="Month" />
                <asp:BoundField DataField="Quantity" />
                <asp:BoundField DataField="CompareQuantity" />

            </Columns>
            <HeaderStyle BackColor="#666666" ForeColor="White" />
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
        &nbsp;</p>
<p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p class="text-center" style="font-size: large">
        <asp:Label ID="lblChartTitle" runat="server" style="font-weight: 700" Text="Label"></asp:Label>
    </p>
<p>
        <asp:Chart ID="Chart1" runat="server" Width="900px">
            <series>
                <asp:Series Legend="Supplier1" >
                </asp:Series>
                <asp:Series ChartArea="ChartArea1" Legend="Supplier2" >
                </asp:Series>
            </series>
            <chartareas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </chartareas>
            <Legends>
                <asp:Legend Name="Supplier1">
                </asp:Legend>
                <asp:Legend Name="Supplier2">
                </asp:Legend>
            </Legends>
        </asp:Chart>
    </p>
        </asp:Panel>
    <p>
        &nbsp;</p>
</asp:Content>
