<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Minimal Configuration example </title>
		<link rel="stylesheet" href="../example.css" type="text/css" />
	</head>
	<body>
        <form id="Form1" runat="server">
			<cutesoft:banner id="banner1" runat="server" />	
			<table cellpadding="15">
				<tr>
					<td id="leftcolumn">
						<cutesoft:leftmenu id="leftmenu1" runat="server" />				
					</td>
					<td>
						<h1>Minimal Configuration example</h1>
						<br />
						<CE:EDITOR id="Editor1" runat="server" Height="100" Width="500" ShowBottomBar="false" AutoConfigure="Minimal"></CE:EDITOR><br />
						<CE:EDITOR id="Editor2" runat="server" Height="100" Width="500" ShowBottomBar="false" ThemeType="OfficeXP" AutoConfigure="Minimal"></CE:EDITOR><br />
						<CE:EDITOR id="Editor3" runat="server" Height="100" Width="500" ShowBottomBar="false" ThemeType="Office2003_BlueTheme" AutoConfigure="Minimal"></CE:EDITOR><br />
						<CE:EDITOR id="Editor4" runat="server" Height="100" Width="500" ShowBottomBar="false" ThemeType="Office2003" AutoConfigure="Minimal"></CE:EDITOR><br />
						<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Submit"></asp:Button>
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>


<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		If Not Page.IsPostBack Then
			Editor1.Text = "<h3 style=""COLOR: red"">1. Easy for developers</h3>"
			Editor2.Text = "<h3 style=""COLOR: #339966"">2. Seamless Integration with Web Forms</h3>"
			Editor3.Text = "<h3 style=""COLOR: #ff00ff"">3. Easy customization and extension</h3>"
			Editor4.Text = "<h3 style=""COLOR: #999999"">4. Easy for end-users</h3>"
		End If
		
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
	End Sub
</script>