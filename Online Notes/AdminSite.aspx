<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSite.aspx.cs" Inherits="Online_Notes.AdminSite" %>

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
		<script type="text/javascript" src="scripts/mainIndexScript.js"></script>
	</head>
    <form id="MainForm" runat="server">
	<body>
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

            <div class="basic-container">
                <div id="wide-header" class="category-header">
				    <h1 id="heading-admin">Admin site</h1>
			    </div>
                <div id="gridview-container">
                    <asp:GridView ID="GridView" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" 
                        DataSourceID="UsersDataSource" DataKeyNames="Id" AllowPaging="True">
                        <Columns>
                            <asp:BoundField DataField="Username" HeaderText="Username" 
                                SortExpression="Username" />
                            <asp:CommandField ShowSelectButton="True" SelectText="Details" />
                        </Columns>
                    </asp:GridView>
                <br />
                <asp:SqlDataSource ID="UsersDataSource" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GridViewConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:GridViewConnectionString.ProviderName %>" 
                    SelectCommand="SELECT Id, Username, Email, Password FROM USERS" 
                    
                    UpdateCommand="UPDATE USERS SET Email = @Email, Username = @Username WHERE (Id = @Id)">
                </asp:SqlDataSource>
                <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" 
                    DataSourceID="DetailsView">
                    <Fields>
                        <asp:CommandField ShowEditButton="True" />
                    </Fields>
                </asp:DetailsView>
                <asp:SqlDataSource ID="DetailsView" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GridViewConnectionString %>" 
                    DeleteCommand="DELETE FROM USERS WHERE Id = ?" 
                    InsertCommand="INSERT INTO USERS (Username, Email, Password) VALUES (?, ?, ?)" 
                    ProviderName="<%$ ConnectionStrings:GridViewConnectionString.ProviderName %>" 
                    SelectCommand="SELECT * FROM USERS WHERE (Id = ?)" 
                    UpdateCommand="UPDATE USERS SET Username = ?, Email = ?, Password = ? WHERE Id = ?">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                        <asp:Parameter Name="Username" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="Password" Type="String" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="GridView" DefaultValue="" Name="Id" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Username" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="Password" Type="String" />
                        <asp:Parameter Name="Id" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                </div>
            </div>
            
			
			<footer>
				<p>Izdelal: <a href="mailto:sever.nejc@gmail.com">Nejc Sever</a>, 2012 | 
				<span id="language-choice">Language:</span>
                <a href="#"><img id="english" class="select-language" src="images/english.png" height="15" width="27"/></a> <a href="#"><img id="slovene" class="select-language" src="images/slovene.png" height="15" width="27"/></a>
				</p>
			</footer>
		</div>
	</body>
    </form>
</html>