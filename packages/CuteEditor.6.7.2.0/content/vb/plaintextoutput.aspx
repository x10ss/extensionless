<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Extract plain text example</title>
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
						<h1>Extract plain text example</h1>
						<p>This sample demonstrates retrieving the CuteEditor HTML content in the plain text format. </p>
						<br />
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" AutoConfigure="Simple" ThemeType="Office2007" runat="server" ></CE:Editor><br />
						<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Submit"></asp:Button><br/>
						<asp:textbox id="textbox1" runat="server" TextMode="MultiLine" Height="250px" Width="770px"></asp:TextBox>		
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>

<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		If Page.IsPostBack Then
			textbox1.Text = Editor1.PlainText
		Else
            Editor1.Text = "<table cellspacing=""4"" cellpadding=""4"" border=""0""> <tbody> <tr> <td> <p> <img src=""http://cutesoft.net/Uploads/j0262681.jpg"" width=""80"" /></p></td> <td> <p>When your algorithmic and programming skills have reached a level which you cannot improve any further, refining your team strategy will give you that extra edge you need to reach the top. We practiced programming contests with different team members and strategies for many years, and saw a lot of other teams do so too.  </p></td></tr> <tr> <td> <p> <img src=""http://cutesoft.net/Uploads/PH02366J.jpg"" width=""80"" /></p></td> <td> <p>From this we developed a theory about how an optimal team should behave during a contest. However, a refined strategy is not a must: The World Champions of 1995, Freiburg University, were a rookie team, and the winners of the 1994 Northwestern European Contest, Warsaw University, met only two weeks before that contest.  </p></td></tr></tbody></table> <br /> <br />"
		End If
		
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
		textbox1.Text = Editor1.PlainText
	End Sub
</script>