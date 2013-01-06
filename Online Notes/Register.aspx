<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Online_Notes.Register" %>

<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html"; charset=UTF-8" />
        <meta name="description" content="Online notes" />
        <meta name="keywords" content="Online, notes, todo, list, tasks" />
        <meta name="author" content="Nejc Sever" />
        <title>Online Notes Registration</title>
        <link rel="stylesheet" type="text/css" href="styles/style.css" />
        <script src="scripts/registerValidation.js">
        </script>
		<script src="scripts/jquery.js">
        </script>
		<script src="scripts/languageChange.js">
        </script>
    </head>
    <body>
        <div id="register-wrapper">
            <a href="Default.aspx" title="settings"><img src="images/logo-register.png" alt="site-logo" /></a>
            <div id="register-container" class="basic-container">
                <form id="RegisterForm" runat="server">
                    <asp:Label ID="CommonErrorMessage" class="error-message" runat="server" Text=""></asp:Label>
                    <label id="username-label" class="login">
                        Username:
                    </label>
                    <asp:TextBox ID="UsernameInput" class="login" runat="server" />
                    <asp:RequiredFieldValidator ID="UsernameRequiredValidator" runat="server" class="error-message" controltovalidate="UsernameInput" ErrorMessage="Username field is required." Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="UsernameRegValidator" runat="server" class="error-message" controltovalidate="UsernameInput" ErrorMessage="Not valid username (Allowed special char.: ._-)." Display="Dynamic" ValidationExpression="^[a-zA-Z0-9._-]+$" />
                    <label id="username-error-message" class="error-message">
                    </label>
                    <label id="email-label" class="login">
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
                    <asp:RegularExpressionValidator ID="PasswordRegValidator" runat="server" class="error-message" controltovalidate="PasswordInput" ErrorMessage="Password must be at least 8 characters long." Display="Dynamic" ValidationExpression=".{8,}" />
                    <label id="password-error-message" class="error-message">
                    </label>
                    <label id="retype-password-label" class="login">
                        Retype password:
                    </label>
                    <asp:TextBox ID="RetypePasswordInput" TextMode="password" class="login" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="RetypePasswordValidator" runat="server" class="error-message" ControlToValidate="PasswordInput" ControlToCompare="RetypePasswordInput" ErrorMessage="Password and retyped password does not match." Display="Dynamic" />
                    <label id="retype-password-error-message" class="error-message">
                    </label>
                    <p>
                        <asp:Button ID="RegisterButton" runat="server" class="basic-button" 
                            Text="Register" onclick="RegisterButton_Click" />
                    </p>
                </form>
            </div>
            <p class="language">
                <span id="language-choice">Language:</span>
                <a href="#"><img id="english" class="select-language" src="images/english.png" height="15" width="27"/></a> <a href="#"><img id="slovene" class="select-language" src="images/slovene.png" height="15" width="27"/></a>
            </p>
        </div>
    </body>
</html>
