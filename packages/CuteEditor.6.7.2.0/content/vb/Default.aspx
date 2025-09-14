<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Default Configuration </title>
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
						<h1>Enable All Toolbars</h1>
						<p>This example show you <b>all the predefined buttons</b>. </p><br />								
						<asp:radiobuttonlist id="ConfigureList" runat="server" autopostback="True" RepeatDirection="Horizontal" onselectedindexchanged="Configure_Changed">
							<asp:ListItem value="Full">Full</asp:ListItem>
							<asp:ListItem value="Full_noform">Full_noform</asp:ListItem>
							<asp:ListItem value="Default" Selected="True">Default</asp:ListItem>
							<asp:ListItem value="Compact">Compact</asp:ListItem>
							<asp:ListItem value="Simple">Simple</asp:ListItem>
							<asp:ListItem value="Minimal">Minimal</asp:ListItem>
							<asp:ListItem value="None">None</asp:ListItem>
						</asp:radiobuttonlist>
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" runat="server" ></CE:Editor><br />
						<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Show HTML"></asp:Button>
						<asp:Literal ID="Literal1" Runat="server" />
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>

<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		If Page.IsPostBack Then
			Literal1.Text = "<h3>The HTML you typed is...</h3><br>" 
			Literal1.Text += Server.HtmlEncode(Editor1.Text)
        Else
            Editor1.Text = "<table cellspacing=""4"" cellpadding=""4"" border=""0""> <tbody> <tr> <td> <p> <img src=""http://cutesoft.net/Uploads/j0262681.jpg"" width=""80"" alt=""""/></p></td> <td> <p>When your algorithmic and programming skills have reached a level which you cannot improve any further, refining your team strategy will give you that extra edge you need to reach the top. We practiced programming contests with different team members and strategies for many years, and saw a lot of other teams do so too.  </p></td></tr> <tr> <td> <p>  <img src=""http://cutesoft.net/Uploads/PH02366J.jpg"" width=""80"" alt="""" /></p></td> <td> <p>From this we developed a theory about how an optimal team should behave during a contest. However, a refined strategy is not a must: The World Champions of 1995, Freiburg University, were a rookie team, and the winners of the 1994 Northwestern European Contest, Warsaw University, met only two weeks before that contest.  </p></td></tr></tbody></table> <br /> <br />"
        End If
		
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
		Literal1.Text = "<h2>The HTML you typed is...</h2><br>"
		Literal1.Text += Server.HtmlEncode(Editor1.Text)
	End Sub
	
	
	Private Sub Configure_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
		
		Select ConfigureList.SelectedItem.Value.ToString()
			case "Full":
				Editor1.AutoConfigure  = AutoConfigure.Full
				
			case "Compact":
				Editor1.AutoConfigure  = AutoConfigure.Compact
				
			case "Full_noform":
				Editor1.AutoConfigure  = AutoConfigure.Full_noform
				
			case "Simple":
				Editor1.AutoConfigure  = AutoConfigure.Simple
				
			case "Minimal":
				Editor1.AutoConfigure  = AutoConfigure.Minimal				
			case "None":
				Editor1.AutoConfigure  = AutoConfigure.None
		End Select		
				
	End Sub
</script>



