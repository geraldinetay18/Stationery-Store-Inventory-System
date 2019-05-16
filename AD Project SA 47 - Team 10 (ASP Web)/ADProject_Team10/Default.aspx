<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ADProject_Team10.Default1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Stationery Store Inventory System</title>
    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <!-- Bootstrap CSS CDN -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.0/css/bootstrap.min.css" integrity="sha384-9gVQ4dYFwwWSjIDZnLEWnxCjeSWFphJiwGPXr1jddIhOegiu1FwO5qRGvFXOdJZ4" crossorigin="anonymous">
</head>
<body>
    <style>
        body {
            background: url("Images/background3.jpg") no-repeat fixed center;
            background-size: 100% 100%;
        }

        .default {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 100%;
            min-height: 200px;
            background: transparent;
            color: red;
            font-family: Cambria;
            margin: 0 auto;
            text-align: center;
        }

        .btnLogin {
            position: relative;
            border: none;
            outline: none;
            height: 40px;
            width: 150px;
            padding-left: 0px;
            font-size: 16px;
            background: #ff005d;
            color: #ffffff;
            border-radius: 20px;
        }
    </style>

    <div class="default">
        <img alt="Stationery Store Inventory System" src="Images/logo7.png" width="60%" />
        <br />
        <a href="Login.aspx">
            <input id="btnGoLogin" type="button" value="Login" class="btnLogin" /></a>
    </div>
</body>
</html>

