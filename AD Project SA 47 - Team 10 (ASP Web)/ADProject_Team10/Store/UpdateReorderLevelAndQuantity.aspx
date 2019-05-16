<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateReorderLevelAndQuantity.aspx.cs" Inherits="ADProject_Team10.Store.UpdateReorderLevelAndQuantity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Update Reorder Level and Quantity</h1>
    <br />

    <div style="text-align: center">

        <div style="text-align: center">
            <asp:DropDownList ID="ddlCatDes" runat="server" CssClass="dropdown" Style="left: 0px; top: 0px">
                <asp:ListItem Value="ViewAll">View All</asp:ListItem>
                <asp:ListItem Value="CategoryName/Description">Category Name/ Description</asp:ListItem>
                <asp:ListItem Value="ItemCode">Item Code</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-info" />
        </div>
        <div style="text-align: center">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <br />

        <asp:GridView ID="gvUpadateReorderLevelQuantity" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowEditing="gvUpadateReorderLevelQuantity_RowEditing" OnRowUpdating="gvUpadateReorderLevelQuantity_RowUpdating" OnRowCancelingEdit="gvUpadateReorderLevelQuantity_RowCancelingEdit" CssClass="table" ViewStateMode="Enabled">
            <Columns>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="ItemCode" runat="server" Text='<%# Eval("ItemCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category Name">
                    <ItemTemplate>
                        <asp:Label ID="CategoryName" runat="server" Text='<%# FindCategoryName((string)Eval("ItemCode")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Description" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reorder Level">
                    <EditItemTemplate>
                        <asp:TextBox ID="ReorderLevel" runat="server" TextMode="Number" Min="0" Text='<%# Bind("ReorderLevel") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorReorderLevel" runat="server" ControlToValidate="ReorderLevel" ErrorMessage="Please enter a positive number for Reorder Level" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="ReorderLevel" runat="server" Text='<%# Eval("ReorderLevel") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity Reorder">
                    <EditItemTemplate>
                        <asp:TextBox ID="QuantityReorder" runat="server" TextMode="Number" Min="0" Text='<%# Bind("QuantityReorder") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorQuantityReorder" runat="server" ControlToValidate="QuantityReorder" ErrorMessage="Please enter a positive number for Quantity Reorder" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="QuantityReorder" runat="server" Text='<%# Eval("QuantityReorder") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Over-Request Frequency">
                    <ItemTemplate>
                        <asp:Label ID="OverRequestFrequency" runat="server" Text='<%# Bind("OverRequestFrequency") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Recommended Quantity">
                    <ItemTemplate>
                        <asp:Label ID="RecommandQuantity" runat="server" Text='<%# Eval("RecommandQuantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Recommended Reorder Level">
                    <ItemTemplate>
                        <asp:Label ID="RecommendedReorderLevel" runat="server" Text='<%# FindRecomLevel((int)Eval("RecommandQuantity"),(int)Eval("QuantityReorder"),(int)Eval("ReorderLevel")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" HeaderText="Action" />
            </Columns>
            <HeaderStyle BackColor="#666666" ForeColor="White" />
        </asp:GridView>

    </div>

</asp:Content>
