<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top5.aspx.cs" Inherits="Online_Notes.Top5" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
		<meta name="description" content="Online notes">
		<meta name="keywords" content="Online, notes, todo, list, tasks">
		<meta name="author" content="Nejc Sever">
		<title>Today's top 5</title>
		<link rel="stylesheet" type="text/css" href="styles/style.css">
        <link rel="stylesheet" href="styles/custom-theme/jquery-ui-1.9.1.custom.css" />
		<script type="text/javascript" src="scripts/jquery.js"></script>
		<script type="text/javascript" src="scripts/jquery-ui.js"></script>
        <script type="text/javascript" src="scripts/topFiveScript.js"></script>
        <script src="scripts/languageChange.js"></script>
	</head>
	<body>
        <form runat="server">
		<header>
			<table>
				<tr>
					<td>
						<a href="Default.aspx" title="settings"><img src="images/logo-index.png" alt="site-logo"></a>
					</td>
					<td class="second-column">
						<p id="welcome-p"> <span id="welcome-label">Welcome </span> <asp:Label ID="WelcomeUserLabel" runat="server" Text=""></asp:Label></p>
					</td>
					<td id="logout">
						<nav id="header-menu">
							<ul>
								<li title="logout" id="Li1">
                                    <asp:LinkButton ID="LogoutButton" runat="server" onclick="LogoutButton_Click"></asp:LinkButton>
                                </li>
							</ul>
						</nav>
					</td>
				</tr>
			</table>
		</header>
		<div id="wrapper">	
			<nav id="main-menu">
				<ul>
					<li><a id="menu-home" href="Default.aspx">Home</a></li>
					<li><a id="menu-topfive" href="Top5.aspx">Today's Top 5</a></li>
					<li><a id="menu-completed" href="Completed.aspx">Completed</a></li>
                    <asp:PlaceHolder ID="AdminPlaceHolder" runat="server"></asp:PlaceHolder>
				</ul>
			</nav>
			
			<div class="basic-container wide-container">
				<div id="wide-header" class="category-header">
						<h1 id="heading-topfive">Today's top 5</h1>
				</div>

                <div class="note-list">
                    <asp:PlaceHolder ID="NotePlaceHolder" runat="server">
                        
                    </asp:PlaceHolder>
                </div>
		</div>
        <footer>
				<p>Izdelal: <a href="mailto:sever.nejc@gmail.com">Nejc Sever</a>, 2012 | 
				<span id="language-choice">Language:</span>
                <a href="#"><img id="english" class="select-language" src="images/english.png" height="15" width="27"/></a> <a href="#"><img id="slovene" class="select-language" src="images/slovene.png" height="15" width="27"/></a>
				</p>
		</footer>
        </form>
	</body>
</html>
