<%@ Page Language="C#" ValidateRequest="false"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
<head>
    <title>ASP.NET WYSIWYG Editor - Relative vs. Absolute URLs Example</title>
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
                        Relative or Absolute URLs, which do you prefer?</h1>
                    <p>
                        With Cute Editor, you have the choice of using either a relative or absolute URL.
                    </p>
                    <br />
                    <asp:RadioButtonList ID="UrlsList" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="Urls_Changed">
                        <asp:ListItem Value="Default" Selected="True">Default</asp:ListItem>
                        <asp:ListItem Value="SiteRelative">SiteRelative Urls</asp:ListItem>
                        <asp:ListItem Value="Absolute">Absolute Urls</asp:ListItem>
                    </asp:RadioButtonList>
                    <CE:Editor EditorWysiwygModeCss="../example.css" id="Editor1" ThemeType="OfficeXP"
                        AutoConfigure="Simple" Height="200" runat="server">
                    </CE:Editor><br />
                    <asp:Button ID="btnUpdate" OnClick="Submit" runat="server" Text="Show HTML"></asp:Button>
                    <br />
                    <br />
                    <asp:TextBox ID="textbox1" runat="server" TextMode="MultiLine" Height="250px" Width="770px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script runat="server">
	void Page_Load(object sender, System.EventArgs e)
	 {
	    if (!IsPostBack) 
		{ 
			Editor1.Text = "<div><a href=\"some.htm\">This is a relative path</a><br><br><a href=\"/some.htm\">This is a Site Root Relative path</a> <br><br><a href=\"Http://somesite/some.htm\">This is a absolute path.</a><br><br><a href=\"#tips\">This is a link to anchor.</a><br><br></div>";
		} 
	
	}
	public void Submit(object sender, System.EventArgs e)
	{
		textbox1.Text = Editor1.XHTML; 
	}
	
	private void Urls_Changed(Object sender, EventArgs e)
	{
		switch(UrlsList.SelectedItem.Value)
		{
			case "Default":
				Editor1.URLType  = URLType.Default;
				break;
		    case "SiteRelative":
				Editor1.URLType  = URLType.SiteRelative;
				break;
		    case "Absolute":
				Editor1.URLType  = URLType.Absolute;
				break;
		}	
	}
</script>