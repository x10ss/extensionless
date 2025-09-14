<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Full screen mode</title>
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
						<h1>Full screen mode</h1>
						<p>Full screen mode button (F12 shortcut) allows you to stretch the Cute Editor control to the width 
				and height of your browser window. </p>
						<br />
						<CE:Editor id="Editor1" ThemeType="Office2007" EditorWysiwygModeCss="../example.css" runat="server" AutoConfigure="Simple"></CE:Editor><br />
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
			Editor1.Text = "Type Here"
		End If
		
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
		Literal1.Text = "<h2>The HTML you typed is...</h2><br>"
		Literal1.Text += Server.HtmlEncode(Editor1.Text)
	End Sub
</script>
