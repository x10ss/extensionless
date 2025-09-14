<%@ Page Language="vb" ValidateRequest="false"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Relative vs. Absolute URLs Example</title>
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
						<h1>Relative or Absolute URLs, which do you prefer?</h1>
						<p>With Cute Editor, you have the choice of using either a relative or absolute URL.						
						</p>
						<br />
						<asp:radiobuttonlist id="UrlsList" runat="server" autopostback="True" RepeatDirection="Horizontal" onselectedindexchanged="Urls_Changed">
							<asp:ListItem value="Default"  Selected="True">Default</asp:ListItem>
							<asp:ListItem value="SiteRelative">SiteRelative Urls</asp:ListItem>
							<asp:ListItem value="Absolute">Absolute Urls</asp:ListItem>
						</asp:radiobuttonlist>									
						<CE:Editor EditorWysiwygModeCss="../example.css" id="Editor1" ThemeType="OfficeXP" AutoConfigure="Simple" Height="200" runat="server" ></CE:Editor><br />
									
						<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Show HTML"></asp:Button>
						<br />
						<br />
						<asp:textbox id="textbox1" runat="server" TextMode="MultiLine" Height="250px" Width="770px"></asp:TextBox><br />				
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>

<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		If Not Page.IsPostBack Then
			Editor1.Text = "<div><a href=""some.htm"">This is a relative path</a><br><br><a href=""/some.htm"">This is a Site Root Relative path</a> <br><br><a href=""Http://somesite/some.htm"">This is a absolute path.</a><br><br><a href=""#tips"">This is a link to anchor.</a><br><br></div>"
		End If
	
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
		textbox1.Text = Editor1.XHTML
	End Sub
	
	Private Sub Urls_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
		
		Select UrlsList.SelectedItem.Value.ToString()
			case "Default":
				Editor1.URLType  = URLType.Default
		    case "SiteRelative":
				Editor1.URLType  = URLType.SiteRelative
		    case "Absolute":
				Editor1.URLType  = URLType.Absolute
		End Select		
				
	End Sub
</script>