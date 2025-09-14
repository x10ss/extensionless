<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Carriage Return Example</title>
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
						<h1>&lt;div&gt;,&lt;p&gt; or &lt;br&gt;, which do you prefer?</h1>
						<p style="width:770px">With Cute Editor, you can defines what happens when the "enter" key is pressed in the editor&nbsp;in the <U><b>BreakElement</b></U> enumeration.This enumeration has three values: Div, Br and P.
						</p>
						<br />
						<asp:radiobuttonlist id="CarriageList" runat="server" autopostback="True" RepeatDirection="Horizontal" onselectedindexchanged="Carriage_Changed">
							<asp:ListItem value="div"  Selected="True">Div</asp:ListItem>
							<asp:ListItem value="br">Br</asp:ListItem>
							<asp:ListItem value="paragraph">P</asp:ListItem>
						</asp:radiobuttonlist>
									
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" ThemeType="Office2007" Height="200" Autoconfigure="Simple" runat="server"/> <br />
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
	
	Private Sub Carriage_Changed(ByVal sender As Object, ByVal e As System.EventArgs)
		
		Select CarriageList.SelectedItem.Value.ToString()
			case "div":
				Editor1.BreakElement  = BreakElement.Div
				Editor1.Text = "Editor1.BreakElement  = BreakElement.Div"
		    case "br":
				Editor1.BreakElement  = BreakElement.Br
				Editor1.Text = "Editor1.BreakElement  = BreakElement.Br"
			case "paragraph":		
				Editor1.BreakElement  = BreakElement.P
				Editor1.Text = "Editor1.BreakElement  = BreakElement.P"
		End Select		
				
	End Sub
</script>