<%@ Page Language="C#"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Edit Static Html Example </title>
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
						<h1>Edit Static Html</h1>		
						<p style="width:770px">This example demonstrates you can use Cute Editor to edit static html page. Below is an example page that displays a document held in an HTML file on the hard drive. When you submit the form, the document is saved back to the drive. <a href="document.htm"><b>Check the document.htm</b></a>
						</p><br />
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" ThemeType="Office2007" EditCompleteDocument="true" AllowPasteHtml="false" runat="server" ></CE:Editor><br />
						<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Submit"></asp:Button><br />			
						<asp:textbox id="textbox1" runat="server" TextMode="MultiLine" Height="250px" Width="770px"></asp:TextBox>							
					
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
			Editor1.SaveFile("document.htm");
			textbox1.Text = Editor1.Text; 
	    } 
		else 
		{ 
			Editor1.LoadHtml("document.htm");
		} 	
	}
	public void Submit(object sender, System.EventArgs e)
	{
			Editor1.SaveFile("document.htm");
			textbox1.Text = Editor1.Text;  
	}
</script>