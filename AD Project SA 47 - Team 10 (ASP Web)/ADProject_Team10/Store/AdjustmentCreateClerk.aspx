<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdjustmentCreateClerk.aspx.cs" Inherits="ADProject_Team10.Store.AdjustmentCreateClerk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Add/Edit Adjustment</h1>
    <br />

    <!--Input details-->
    <table>
        <asp:Panel ID="PanelAdjusmentId" runat="server" Visible="False">
            <tr>
                <td>Adjustment ID</td>
                <td>
                    <asp:TextBox ID="txbAdjustmentId" runat="server" TextMode="Number" Width="130px" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td>Category</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Item Code</td>
            <td>
                <asp:DropDownList ID="ddlCode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCode_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Description</td>
            <td>
                <asp:DropDownList ID="ddlDescription" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDescription_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>Current Quantity</td>
            <td>
                <asp:TextBox ID="txbCurrentQty" runat="server" TextMode="Number" Width="130px" ReadOnly="True" CssClass="form-control"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Actual Quantity</td>
            <td>
                <asp:TextBox ID="txbActualQty" runat="server" TextMode="Number" Width="130px" ReadOnly="True" CssClass="form-control" AutoPostBack="True" OnTextChanged="txbActualQty_TextChanged"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSwap1" runat="server" Height="36px" Text="Swap" Width="106px" CssClass="btn btn-info" OnClick="btnSwap1_Click" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td>Adjusted Quantity</td>
            <td>
                <asp:TextBox ID="txbAdjustQty" runat="server" TextMode="Number" Width="130px" AutoPostBack="True" CssClass="form-control" OnTextChanged="txbAdjustQty_TextChanged"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorAdjusted" runat="server" ErrorMessage="Adjusted quantity required" ControlToValidate="txbAdjustQty" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidatorAdjusted" runat="server" ControlToValidate="txbAdjustQty" ErrorMessage="Invalid adjustment. Actual quantity must be more than 0." ForeColor="Red" MaximumValue="100000000" MinimumValue="-100000000" Type="Integer">*</asp:RangeValidator>
                <asp:CompareValidator ID="CompareValidatorAdjustedQuantity" runat="server" ControlToValidate="txbAdjustQty" ErrorMessage="Invalid adjustment. Adjusted Quantity cannot be zero." ForeColor="Red" Operator="NotEqual" Type="Integer" ValueToCompare="0">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Reason</td>
            <td>
                <asp:TextBox ID="txbReason" runat="server" Width="400px" Height="88px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorReason" runat="server" ErrorMessage="Reason required" ControlToValidate="txbReason" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    </table>
    <br />

    <!-- Error display -->
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
    <br />

    <!--Actions (Cancel, Save, Submit)-->
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" CausesValidation="False" />
    &nbsp;&nbsp;
    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-warning" OnClick="btnSave_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" />

</asp:Content>
