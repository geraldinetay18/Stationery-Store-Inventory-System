<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifyCollectionPoint.aspx.cs" Inherits="ADProject_Team10.Dept.ModifyCollectionPoint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Title-->
    <h1>Change Collection Point</h1>
    <br />

    <p><strong><asp:Label ID="lblAttention" runat="server" Font-Italic="True"></asp:Label></strong></p>

<%--    <div class="OutlineElement Ltr SCXP83996906" style="margin: 0px; padding: 0px; user-select: text; -webkit-user-drag: none; -webkit-tap-highlight-color: transparent; touch-action: pan-x pan-y; overflow: visible; cursor: text; clear: both; position: relative; direction: ltr; color: rgb(0, 0, 0); font-family: Arial; font-size: 9.28924px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: normal; letter-spacing: normal; orphans: 2; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(255, 255, 255); text-decoration-style: initial; text-decoration-color: initial; top: 2px; left: 0px;">
        <p class="Paragraph SCXP83996906" data-ccp-props="{&quot;335551550&quot;:1,&quot;335551620&quot;:1,&quot;335559683&quot;:0,&quot;335559685&quot;:0,&quot;335559731&quot;:0,&quot;335559737&quot;:0,&quot;335562764&quot;:2,&quot;335562765&quot;:1,&quot;335562766&quot;:4,&quot;335562767&quot;:0,&quot;335562768&quot;:4,&quot;335562769&quot;:0}" lang="EN-SG" paraeid="{23254b95-b7ab-4b39-89a3-2e53291da203}{204}" paraid="0" style="margin: 0px; padding: 0px; user-select: text; -webkit-user-drag: none; -webkit-tap-highlight-color: transparent; touch-action: pan-x pan-y; word-wrap: break-word; font-weight: normal; font-style: normal; vertical-align: baseline; background-color: transparent; color: windowtext; text-align: left; text-indent: 0px;" xml:lang="EN-SG">
            <asp:Label ID="lblAttention" runat="server" Font-Bold="True" Font-Italic="True" Font-Size="Small"></asp:Label>
        </p>
        <p class="Paragraph SCXP83996906" data-ccp-props="{&quot;335551550&quot;:1,&quot;335551620&quot;:1,&quot;335559683&quot;:0,&quot;335559685&quot;:0,&quot;335559731&quot;:0,&quot;335559737&quot;:0,&quot;335562764&quot;:2,&quot;335562765&quot;:1,&quot;335562766&quot;:4,&quot;335562767&quot;:0,&quot;335562768&quot;:4,&quot;335562769&quot;:0}" lang="EN-SG" paraeid="{23254b95-b7ab-4b39-89a3-2e53291da203}{204}" paraid="0" style="margin: 0px; padding: 0px; user-select: text; -webkit-user-drag: none; -webkit-tap-highlight-color: transparent; touch-action: pan-x pan-y; word-wrap: break-word; font-weight: normal; font-style: normal; vertical-align: baseline; background-color: transparent; color: windowtext; text-align: left; text-indent: 0px;" xml:lang="EN-SG">
            &nbsp;
        </p>
    </div>--%>

    <!--Image-->
    <div style="text-align: right">
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/gmail image.jpg" Width="47px" OnClick="ImageButton1_Click" Height="40px" ToolTip="Click to send email to store clerk" />
    </div>

    <!--Details-->
    <table class="table">
        <tr>
            <td style="width: 192px">Store Clerk In-Charge:</td>
            <td><strong>
                <asp:Label ID="lblStoreClerkName" runat="server" /></strong></td>
        </tr>
        <tr>
            <td style="width: 192px">Time:</td>
            <td><strong>
                <asp:Label ID="lbltime" runat="server" /></strong></td>
        </tr>
        <tr>
            <td style="width: 192px">Contact Number:</td>
            <td><strong>
                <asp:Label ID="lblContactNumber" runat="server" /></strong></td>
        </tr>
        <tr>
            <td style="width: 192px">Current Collection Point:</td>
            <td><strong>
                <asp:Label ID="lblCurrentLocation" runat="server" /></strong></td>
        </tr>
        <tr>
            <td style="width: 192px">New Collection Point:</td>
            <td>
                <asp:RadioButtonList ID="rblCollectionPoint" runat="server" DataSourceID="SQLLocationName" DataTextField="LocationName" DataValueField="LocationName" OnSelectedIndexChanged="rblCollectionPoint_SelectedIndexChanged">
                </asp:RadioButtonList>
                <asp:SqlDataSource ID="SQLLocationName" runat="server" ConnectionString="<%$ ConnectionStrings:SSAEntities %>" SelectCommand="SELECT [LocationName] FROM [CollectionPoint] WHERE ([LocationName] &lt;&gt; @LocationName)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="lblCurrentLocation" DefaultValue="X" Name="LocationName" PropertyName="Text" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
