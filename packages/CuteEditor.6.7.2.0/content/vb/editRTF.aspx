<%@ Page Language="VB" ValidateRequest="False" %>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
<head>
    <title>ASP.NET WYSIWYG Editor - RTF to HTML, HTML to RTF </title>
    <link rel="stylesheet" href="../example.css" type="text/css" />
</head>
<body>
    <form id="Form1" runat="server">
        <cutesoft:banner ID="banner1" runat="server" />
        <table cellpadding="15">
            <tr>
                <td id="leftcolumn">
                    <cutesoft:leftmenu ID="leftmenu1" runat="server" />
                </td>
                <td>
                    <h1>
                        RTF to HTML, HTML to RTF</h1>
                    <p style="width:770px">
                        This example demonstrates you can use Cute Editor to convert an HTML document into
                        an RTF file and RTF file into HTML or XHTML document. In the following example the
                        content is loaded from a RTF file. When you submit the form, the html code generated
                        by the Editor is saved back to RTF file. <a href="document.rtf"><b>Check the RTF file</b></a>
                    </p>
                    <br />
                    <CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" AutoConfigure="Simple"
                        ThemeType="Office2007" runat="server">
                    </CE:Editor><br />
                    <asp:Button ID="btnUpdate" OnClick="Submit" runat="server" Text="Submit"></asp:Button><br />
                    <asp:TextBox ID="textbox1" runat="server" TextMode="MultiLine" Height="250px" Width="770px"></asp:TextBox>
                </td>
                <tr>
        </table>
    </form>
</body>
</html>
<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		If Page.IsPostBack Then
			Editor1.SaveRTF("document.rtf")
			textbox1.Text = Editor1.Text
		Else
			Editor1.LoadRTF("document.rtf")
		End If
		
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
		Editor1.SaveRTF("document.rtf")
		textbox1.Text = Editor1.Text 
	End Sub
</script>