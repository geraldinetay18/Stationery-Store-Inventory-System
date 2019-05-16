<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ADProject_Team10.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stationery Store Inventory System</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%: Scripts.Render("~/bundles/modernizr") %>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <style>
        body {
            background: url("Images/background4.jpg") no-repeat fixed center;
            background-size: 100% 100%;
        }

        .loginBox {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 370px;
            min-height: 200px;
            background: transparent;
            border-radius: 10px;
            padding: 30px;
            box-sizing: border-box;
        }

        .failure{
            color: #ffffff;
            padding-left: 15px;
        }

        .slogo {
            display: block;
            margin: 0 auto;
            margin-bottom: 35px;
            position: relative;
            left: -15px;
        }

        .inputBox {
            position: relative;
            padding: 0px;
        }

        .loginBox input {
            width: 100%;
            border: none;
            outline: none;
            height: 40px;
            background: #ffffff;
            font-size: 15px;
            padding-left: 40px;
            box-sizing: border-box;
            border-radius: 20px;
            margin-right: 0px;
        }

            .inputBox span {
                position: absolute;
                top: 10px;
                left: 20px;
                color: #262626;
            }

        .validator {
            width: 100%;
            margin-bottom: 10px;
            color: #ffffff;
            padding-left: 15px;
        }

        .checkbox {
            position: relative;
            height: 30px;
        }

        .checkbox #RememberMe {
            width: 20%;
            height: 15px;
            position: relative;
            left:15px;
        }

        .checkbox span {
            width: 80%;
            position: relative;
            left: 40px;
            color: #ffffff;
            margin-bottom: 10px;

        }

        .loginBox input[type="submit"] {
            position: relative;
            border: none;
            outline: none;
            height: 40px;
            width: 280px;
            padding-left: 0px;
            font-size: 16px;
            background: #ff005d;
            color: #ffffff;
            border-radius: 20px;
        }
    </style>


    <form id="form1" runat="server">

        <div class="loginBox">
            <asp:Image CssClass="slogo" ID="Image1" runat="server" ImageUrl="~/Images/logo4.png" Height="100px" Width="100px" />

            <!--Error-->
            <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                <p class="failure">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
            </asp:PlaceHolder>

            <!--Email-->
            <div class="inputBox">
                <asp:TextBox runat="server" ID="Email" TextMode="Email" placeholder="Email" /><br />
                <span><i class="fa fa-user" aria-hidden="true"></i></span>
            </div>
            <div class="validator">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    ErrorMessage="The email field is required." />
            </div>

            <!--Password-->
            <div class="inputBox">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" placeholder="Password"/><br />
                <span><i class="fa fa-lock" aria-hidden="true"></i></span>
            </div>
            <div class="validator">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    ErrorMessage="The password field is required." />
            </div>

            <!--Remember Me-->
            <div class="checkbox">
                <asp:CheckBox runat="server" ID="RememberMe" />
                <span>Remember me?</span>
            </div>

            <!--Submit-->
            <asp:Button runat="server" type="submit" OnClick="LogIn" Text="Login" />
        </div>
    </form>
</body>
</html>
