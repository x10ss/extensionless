<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Setting the max length</title>
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
						<h1>Use MaxHTMLLength or MaxTextLength to Protect Your Database</h1>
						<p style="width:770px">We often use a database backend such as SQL Server to store data. In such databases, you have to specify the length of any textual field when a table is designed. If you tried to insert a record whose text length is greater than allowed by your table, an error will be reported. <br /><br />
						To prevent this type of error from occurring, developers can use <b>MaxHTMLLength</b> or <b>MaxTextLength</b> in the Cute Editor to limit the length of the user’s input.
						</p><br />
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" MaxHTMLLength=8 runat="server" Height="200" AutoConfigure="Simple"></CE:Editor><br />
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