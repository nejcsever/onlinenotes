<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Online_Notes.Login" %>

<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html"; charset=UTF-8">
        <meta name="description" content="Online notes">
        <meta name="keywords" content="Online, notes, todo, list, tasks">
        <meta name="author" content="Nejc Sever">

        <title>Login to online notes</title>

        <link rel="stylesheet" type="text/css" href="styles/style.css">
        <script src="scripts/loginValidation.js">
        </script>
        <script src="scripts/jquery.js">
        </script>
        <script src="scripts/languageChange.js">
        </script>
    </head>
    <body>
        <div id="login-wrapper">
            <a href="Default.aspx" title="settings"><img src="images/logo-login.png" alt="site-logo" /></a>
            <div id="login-container" class="basic-container">
                <form id="LoginForm" runat="server">
                    <asp:Label ID="CommonErrorMessage" class="error-message" runat="server" Text=""></asp:Label>
                    <label id = "email-label" class="login">
                        Email:
                    </label>
                    <asp:TextBox ID="EmailInput" class="login" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequiredValidator" runat="server" class="error-message" controltovalidate="EmailInput" ErrorMessage="Email field is required." Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="EmailRegValidator" runat="server" class="error-message" controltovalidate="EmailInput" ErrorMessage="Enter a valid email (example@domain.com)." Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                    <label id="email-error-message" class="error-message">
                    </label>
                    <label id="password-label" class="login">
                        Password:
                    </label>
                    <asp:TextBox ID="PasswordInput" TextMode="password" class="login" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server" class="error-message" controltovalidate="PasswordInput" ErrorMessage="Password field is required." Display="Dynamic" />
                    <label id="password-error-message" class="error-message">
                    </label>
                    <p>
                        <asp:Button ID="LoginButton" runat="server" class="basic-button" 
                            Text="Login" onclick="LoginButton_Click" />
                    </p>
                </form>
                <hr/>
                <p>
                    <span id="not-a-member">Not a member? </span>
                    <a id="register-link" href="Register.aspx">Register here</a>
                </p>
            </div>
            <p class="language">
                <span id="language-choice">Language:</span>
                <a href="#"><img id="english" class="select-language" src="images/english.png" height="15" width="27"/></a> <a href="#"><img id="slovene" class="select-language" src="images/slovene.png" height="15" width="27"/></a>
            </p>
        </div>
    </body>
</html>
