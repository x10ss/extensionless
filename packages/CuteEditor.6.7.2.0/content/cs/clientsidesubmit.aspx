<%@ Page Language="C#"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Client-side submit and reset example</title>
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
						<h1>Client-side submit and reset example</h1>
						<p>Does your online html editor work with Client-side submit and reset? 
						This example demonstrates Cute Editor can handle this situation easily. </p><br />					
						<asp:Literal ID="Literal1" Runat="server" />
						<br />
						<CE:Editor id="Editor1" runat="server" EditorWysiwygModeCss="../example.css" Height="200" ThemeType="OfficeXP" DisableItemList="save" AutoConfigure="Simple"></CE:Editor>
						<br />
						<asp:Button id="btnUpdate" Runat="server" Text="Server Submit"></asp:Button>						
						<br /><br />
						<table>
							<tr>
								<td>
									<input type="submit" value="submit Button">
								</td>
								<td width=10></td>
								<td><input type="button" value="JavaScript Submit" onclick="document.getElementById('Form1').submit()" />
								</td>
							</tr>
							<tr>
								<td><input type="reset" value="Reset Button">
								</td>
								<td width=10></td>
								<td><input type="button" value="JavaScript Reset" onclick="document.getElementById('Form1').reset()" />
								</td>
							</tr>
						</table>						
						
						<table width="80%">
							<tr bgcolor=#eeeeee>
								<th width="20%">Button</th>
								<th width="80%">Code</th>
							</tr>
							<tr>
								<td><input type="submit" value="submit Button" /></td>
								<td>&lt;input type="submit" value="submit Button"&gt;</td>
							</tr>
							<tr>
								<td><input type="button" value="JavaScript Submit" onclick="document.getElementById('Form1').submit()" /></td>
								<td>&lt;input type="submit" value="submit Button"&gt;</td>
							</tr>
							<tr>
								<td><input type="reset" value="Reset Button" /></td>
								<td>&lt;input type="reset" value="Reset Button"&gt;</td>
							</tr>
							<tr>
								<td><input type="button" value="JavaScript Reset" onclick="document.getElementById('Form1').reset()" /></td>
								<td>&lt;input type="button" value="JavaScript Reset" onclick="Form1.reset()"&gt;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>

<script runat="server">
	void Page_Load(object sender, System.EventArgs e)
	 {
	    if (IsPostBack) 
		{ 
			Literal1.Text = "<h4>The content you typed is...</h4>"; 
			Literal1.Text += Editor1.XHTML; 
		} 
	}
</script>
