<%@ Page Language="VB"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Editor Auto Adjusting Height</title>
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
						<h1>Editor Auto Adjusting Height</h1>
						<p style="width:770px">This example shows how to use Editor.ResizeMode to make the Editor change its height based on the content. 
						The Editor will now grow and shrink in height to match the content inside.</p>
						<br />				
						<asp:radiobuttonlist id="ResizeList" runat="server" autopostback="True" RepeatDirection="Horizontal" onselectedindexchanged="theme_Changed">
							<asp:ListItem value="AutoAdjust"  Selected="True">AutoAdjust</asp:ListItem>
							<asp:ListItem value="ResizeCorner">ResizeCorner</asp:ListItem>
							<asp:ListItem value="PlusMinus">PlusMinus</asp:ListItem>
							<asp:ListItem value="None">None</asp:ListItem>
						</asp:radiobuttonlist>						
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" AutoConfigure="Simple" Height="200" ResizeMode="AutoAdjust" ThemeType="Office2007" runat="server"/> <br />
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
		    Editor1.Text = "This is some more test text.<br>This is some more test text.<br>This is some more test text.<br>This is some more test text.<br>This is some more test text.<br>This is some more test text.<br>This is some more test text.<br>"
		End If
	
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
		Literal1.Text = "<h2>The HTML you typed is...</h2><br>"
		Literal1.Text += Server.HtmlEncode(Editor1.XHTML)
	End Sub
	
	Private Sub theme_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
				
		Select ResizeList.SelectedItem.Value
			case "AutoAdjust":
				Editor1.ResizeMode  = EditorResizeMode.AutoAdjust
				Editor1.Text = "Editor1.ResizeMode  = EditorResizeMode.AutoAdjust"
		    case "ResizeCorner":
				Editor1.ResizeMode  = EditorResizeMode.ResizeCorner
				Editor1.Text = "Editor1.ResizeMode  = EditorResizeMode.ResizeCorner"
		    case "PlusMinus":
				Editor1.ResizeMode  = EditorResizeMode.PlusMinus
				Editor1.Text = "Editor1.ResizeMode  = EditorResizeMode.PlusMinus"
		    case "None":
				Editor1.ResizeMode  = EditorResizeMode.None
				Editor1.Text = "Editor1.ResizeMode  = EditorResizeMode.None"
		End Select	
				
	End Sub
</script>
