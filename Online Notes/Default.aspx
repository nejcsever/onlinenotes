<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Online_Notes.Default" %>

<!DOCTYPE html>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<meta name="description" content="Online notes" />
		<meta name="keywords" content="Online, notes, todo, list, tasks" />
		<meta name="author" content="Nejc Sever" />
		<title>Online Notes</title>
		<link rel="stylesheet" type="text/css" href="styles/style.css" />
		<link rel="stylesheet" href="styles/custom-theme/jquery-ui-1.9.1.custom.css" />
		<script type="text/javascript" src="scripts/jquery.js"></script>
		<script type="text/javascript" src="scripts/jquery-ui.js"></script>
        <script type="text/javascript" src="scripts/languageChange.js"></script>
		<script type="text/javascript" src="scripts/dialog-script.js"></script>
		<script type="text/javascript" src="scripts/mainIndexScript.js"></script>
	</head>
    <form id="MainForm" runat="server">
	<body>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
		<header>
			<table>
				<tr>
					<td>
						<a href="Default.aspx" title="Home"><img src="images/logo-index.png" alt="site-logo"></img></a>
					</td>
					<td class="second-column">
						<p id="welcome-p"> <span id="welcome-label">Welcome </span> <asp:Label ID="WelcomeUserLabel" runat="server" Text=""></asp:Label></p>
					</td>
					<td>
						<nav id="header-menu">
							<ul>
								<li title="logout" id="logout">
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
			
			
			<div id="left-nav">
				<div class="basic-container" id="category-list-container">
					<h1 id="categories-heading">Categories</h1>
                    <asp:Panel ID="CategoryList" runat="server">
                    </asp:Panel>
					<button id="add-category" class="basic-button">Add Category</button>
				</div>
			</div>
			
			<div id="category-notes" class="basic-container">
				<div class="category-header">
					<div id="category-title">
                        <!-- trigger-ji dodani dinamicno -->
                        <asp:UpdatePanel ID="CategoryTitlePanel" runat="server">
                            <ContentTemplate>
						        <h1 id="category-name"><asp:Literal ID="CategoryName" runat="server" Text="Select category"></asp:Literal></h1>
						        <p id="category-subname"><asp:Literal ID="CategorySubname" runat="server" Text=""></asp:Literal></p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
					</div>
					<div id="category-navigation">
						<div id="control-icons">
                            <asp:HiddenField ID="SelectedCategory" runat="server" />
                            <asp:LinkButton ID="DeleteCategory" runat="server" 
                                oncommand="DeleteCategory_Click"><img id="delete-icon" src="images/delete-icon.png" alt="delete-icon" title="Delete category"/></asp:LinkButton>
						</div>
						<button id="add-note" class="basic-button navigate-category-button">Add note</button>
					</div>
				</div>
				<asp:Panel ID="NoteList" runat="server">
                </asp:Panel>
				
			</div>
			
			<footer>
				<p>Izdelal: <a href="mailto:sever.nejc@gmail.com">Nejc Sever</a>, 2012 | 
				<span id="language-choice">Language:</span>
                <a href="#"><img id="english" class="select-language" src="images/english.png" height="15" width="27"/></a> <a href="#"><img id="slovene" class="select-language" src="images/slovene.png" height="15" width="27"/></a>
				</p>
			</footer>
		</div>
		
		<div id="add-category-form" title="Add category">
			<p>
				<label for="title">Title:</label></p>
			<p>
                <asp:TextBox ID="CategoryTitle" class="dialog" runat="server"></asp:TextBox>
			</p>
			<p>
				<label for="description">Description:</label>
			</p>
			<p>
                <asp:TextBox id="CategoryDescription" TextMode="multiline" class="dialog" runat="server" />
			</p>
            <asp:Button ID="AddCategoryButton" runat="server" class="basic-button" 
                        Text="Add category" onclick="AddCategoryButton_Click"/>
		</div>
		
		<div id="add-note-form" title="Add note">
            <asp:HiddenField ID="ShownCategory" runat="server" Value="default" />
				<p>
					<label for="title">Title:</label>
				</p>
				<p>
                    <asp:TextBox ID="NoteTitle" class="dialog" runat="server" name="noteTitle"></asp:TextBox>
				</p>
				<p>
					<label for="deadline">Deadline:</label>
				</p>
				<p>
                    <asp:TextBox ID="datepicker" class="dialog" runat="server" name="deadline"></asp:TextBox>
				</p>
					<label for="priority">Priority:</label>
                    <asp:RadioButtonList ID="PriorityList" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="2">High</asp:ListItem>  
                        <asp:ListItem Value="1">Medium</asp:ListItem>  
                        <asp:ListItem Value="0" Selected="True">Low</asp:ListItem>
                    </asp:RadioButtonList>
					<label for="description">Description:</label>
				<p>
                    <asp:TextBox id="NoteDescription" TextMode="multiline" class="dialog" runat="server" name="noteDescription" />
				</p>
                <asp:Button ID="AddNoteButton" runat="server" class="basic-button" 
                        Text="Add note" onclick="AddNoteButton_Click"/>
		</div>
	</body>
    </form>
</html>