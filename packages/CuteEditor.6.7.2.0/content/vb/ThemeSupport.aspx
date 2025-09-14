<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Built in theme support</title>
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
						<h1>Built in theme support</h1>
						<p>Cute Editor provides several built in themes that are ready to use. You can choose from several predefined themes or create your own.</p>
						<br />					
						<asp:radiobuttonlist id="themeList" runat="server" autopostback="True" RepeatDirection="Horizontal" onselectedindexchanged="theme_Changed">
							<asp:ListItem value="Office2007"  Selected="True">Office2007</asp:ListItem>
							<asp:ListItem value="Office2003_BlueTheme">Office2003_BlueTheme</asp:ListItem>
							<asp:ListItem value="Office2003">Office2003</asp:ListItem>
							<asp:ListItem value="OfficeXP">OfficeXP</asp:ListItem>
							<asp:ListItem value="Office2000">Office2000</asp:ListItem>
						</asp:radiobuttonlist>						
						<CE:Editor id="Editor1" ThemeType="Office2007" EditorWysiwygModeCss="../example.css" runat="server"/> <br />
						<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Submit"></asp:Button>
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
			Literal1.Text = "<h2>The HTML you typed is...</h2><br>" 
			Literal1.Text += Server.HtmlEncode(Editor1.XHTML)
		Else
			Editor1.Text = "Type Here"
		End If
	
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
		Literal1.Text = "<h2>The HTML you typed is...</h2><br>"
		Literal1.Text += Server.HtmlEncode(Editor1.XHTML)
	End Sub
	
	Private Sub theme_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
				
        Select Case themeList.SelectedItem.Value
            Case "Office2007"
                Editor1.ThemeType = ThemeType.Office2007
                Editor1.Text = "Editor1.ThemeType  = ThemeType.Office2007"
            Case "Office2003_BlueTheme"
                Editor1.ThemeType = ThemeType.Office2003_BlueTheme
                Editor1.Text = "Editor1.ThemeType  = ThemeType.Office2003_BlueTheme"
            Case "Office2003"
                Editor1.ThemeType = ThemeType.Office2003
                Editor1.Text = "Editor1.ThemeType  = ThemeType.Office2003"
            Case "OfficeXP"
                Editor1.ThemeType = ThemeType.OfficeXP
                Editor1.Text = "Editor1.ThemeType  = ThemeType.OfficeXP"
            Case "Office2000"
                Editor1.ThemeType = ThemeType.Office2000
                Editor1.Text = "Editor1.ThemeType  = ThemeType.Office2000"
        End Select
				
	End Sub
</script>
