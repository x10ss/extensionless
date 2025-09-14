<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Multiple Editors in one page</title>
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
						<h1>Multiple Editors in one page</h1>
						<br />
						<CE:EDITOR id="Editor1" EditorWysiwygModeCss="../example.css" runat="server" Height="200" AutoConfigure="Simple"></CE:EDITOR><br />
						<CE:EDITOR id="Editor2" EditorWysiwygModeCss="../example.css" runat="server" Height="200" ThemeType="Office2007" AutoConfigure="Simple"></CE:EDITOR><br />
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
		End If 
	End Sub
</script>