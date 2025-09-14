<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP and ASP.NET HTML Editor - Handle pasted text</title>
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
						<h1>Handle pasted text</h1>
						<p>With Cute Editor, you can specify if formatting is stripped when text is pasted into the editor.</p>
						<br />
						<asp:radiobuttonlist id="EditorOnPaste" runat="server" autopostback="True" RepeatDirection="Horizontal" onselectedindexchanged="PasteBehavior_Changed">
							<asp:ListItem value="Disabled"  Selected="True">Disabled</asp:ListItem>
							<asp:ListItem value="PastePureText">PastePureText </asp:ListItem>
							<asp:ListItem value="PasteText">PasteText</asp:ListItem>
							<asp:ListItem value="PasteCleanHTML">PasteCleanHTML</asp:ListItem>
							<asp:ListItem value="PasteWord">PasteWord</asp:ListItem>
							<asp:ListItem value="ConfirmWord">ConfirmWord</asp:ListItem>
						</asp:radiobuttonlist>									
						<CE:Editor ThemeType="Office2007" EditorWysiwygModeCss="../example.css" EditorOnPaste="Disabled" id="Editor1" Height="250px" AutoConfigure="Simple" runat="server" ></CE:Editor>
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>

<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		If Not Page.IsPostBack Then
			Editor1.Text = "Type Here"
		End If
	
	End Sub
	
	Private Sub PasteBehavior_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
		
		Select EditorOnPaste.SelectedItem.Value.ToString()
			case "Disabled":
				Editor1.EditorOnPaste = PasteBehavior.Disabled		   
			case "Disabled":
				Editor1.EditorOnPaste = PasteBehavior.Disabled
		    case "PastePureText":
				Editor1.EditorOnPaste = PasteBehavior.PastePureText
		    case "PasteText":
				Editor1.EditorOnPaste = PasteBehavior.PasteText
		    case "PasteCleanHTML":
				Editor1.EditorOnPaste = PasteBehavior.PasteCleanHTML
		    case "PasteWord":
				Editor1.EditorOnPaste = PasteBehavior.PasteWord
		    case "ConfirmWord":
				Editor1.EditorOnPaste = PasteBehavior.ConfirmWord
		End Select		
				
	End Sub
</script>