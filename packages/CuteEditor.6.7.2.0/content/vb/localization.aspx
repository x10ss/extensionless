<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Language localization </title>
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
						<h1>Localized (Spanish,German...)</h1>
<asp:radiobuttonlist style="border:1px solid #eeeeee" BackColor="#f4f4f4" id="RadioList" Width="770" runat="server" autopostback="True" RepeatLayout="Table" RepeatColumns="4" RepeatDirection="Horizontal" onselectedindexchanged="culture_Changed">
		<asp:ListItem value="en">English</asp:ListItem>
		<asp:ListItem value="fr-FR">French</asp:ListItem>
		<asp:ListItem value="de-de">German</asp:ListItem>
		<asp:ListItem value="nl-NL">Dutch</asp:ListItem>
		<asp:ListItem value="es-ES">Spanish</asp:ListItem>
		<asp:ListItem value="it-IT">Italian</asp:ListItem>
		<asp:ListItem value="nb-NO">Norwegian</asp:ListItem>
		<asp:ListItem value="ru-RU">Russian</asp:ListItem>
		<asp:ListItem value="ja-JP">Japanese</asp:ListItem>
		<asp:ListItem value="zh-cn">Chinese</asp:ListItem>
		<asp:ListItem value="sv-SE">Swedish</asp:ListItem>
		<asp:ListItem value="pt-BR">Portuguese</asp:ListItem>
		<asp:ListItem value="da">Danish</asp:ListItem>
		<asp:ListItem value="he-IL">Hebrew</asp:ListItem>
		<asp:ListItem value="ar">Arabic</asp:ListItem>								
		<asp:ListItem value="cs">CZech</asp:ListItem>
		<asp:ListItem value="tr-TR">Turkey</asp:ListItem>
		<asp:ListItem value="vi">Vietnam</asp:ListItem>
		<asp:ListItem value="th">Thai</asp:ListItem>
		<asp:ListItem value="ko-KR">Korean</asp:ListItem>
</asp:radiobuttonlist>
                        <br />
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" ThemeType="Office2007" runat="server" AutoConfigure="Simple" ></CE:Editor><br /><br />
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

	public Sub culture_Changed(sender As object, e As System.EventArgs)
		editor1.CustomCulture = RadioList.SelectedItem.Value
	End Sub
</script>

