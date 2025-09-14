<%@ Page Language="vb" ValidateRequest="False"%>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>

<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Disable Javascript</title>
		<link rel="stylesheet" href="example.css" type="text/css" />
	</head>
    <body >
        <form runat="server">	
			<h2>Disable Javascript </h2>
			<CE:Editor id="Editor1" EnableClientScript="false" runat="server" ></CE:Editor><br /><br />
			<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Show HTML"></asp:Button>
			<asp:Literal ID="Literal1" Runat="server" />
		</form>
	</body>
</html>



<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		If Page.IsPostBack Then
			Literal1.Text = "<h2>The HTML you typed is...</h2><br>" 
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

