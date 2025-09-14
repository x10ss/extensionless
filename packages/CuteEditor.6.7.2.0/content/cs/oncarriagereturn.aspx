<%@ Page Language="C#"%>
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
        <cutesoft:banner ID="banner1" runat="server" />
        <table cellpadding="15">
            <tr>
                <td id="leftcolumn">
                    <cutesoft:leftmenu ID="leftmenu1" runat="server" />
                </td>
                <td>
                    <h1>
                        &lt;div&gt;,&lt;p&gt; or &lt;br&gt;, which do you prefer?</h1>
                    <p style="width:770px">
                        With Cute Editor, you can defines what happens when the "enter" key is pressed in
                        the editor&nbsp;in the <u><b>BreakElement</b></u> enumeration.This enumeration has
                        three values: Div, Br and P.
                    </p>
                    <br />
                    <asp:RadioButtonList ID="CarriageList" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="Carriage_Changed">
                        <asp:ListItem Value="div" Selected="True">Div</asp:ListItem>
                        <asp:ListItem Value="br">Br</asp:ListItem>
                        <asp:ListItem Value="paragraph">P</asp:ListItem>
                    </asp:RadioButtonList>
                    <CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" ThemeType="Office2007"
                        Height="200" Autoconfigure="Simple" runat="server" />
                    <br />
                    <asp:Button ID="btnUpdate" OnClick="Submit" runat="server" Text="Show HTML"></asp:Button>
                    <asp:Literal ID="Literal1" runat="server" />
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
			Literal1.Text = "<h2>The HTML you typed is...</h2><br>"; 
			Literal1.Text += Server.HtmlEncode(Editor1.XHTML); 
	    } 
		else 
		{ 
			Editor1.Text = "Type Here";
		} 
	
	}
	public void Submit(object sender, System.EventArgs e)
	{
		Literal1.Text = "<h2>The HTML you typed is...</h2><br>"; 
		Literal1.Text += Server.HtmlEncode(Editor1.XHTML); 
	}
	
	private void Carriage_Changed(Object sender, EventArgs e)
	{
		switch(CarriageList.SelectedItem.Value)
		{
			case "div":
				Editor1.BreakElement  = BreakElement.Div;
				Editor1.Text = "Editor1.BreakElement  = BreakElement.Div;";
				break;
		    case "br":
				Editor1.BreakElement  = BreakElement.Br;
				Editor1.Text = "Editor1.BreakElement  = BreakElement.Br;";
				break;
		    case "paragraph":
				Editor1.BreakElement  = BreakElement.P;
				Editor1.Text = "Editor1.BreakElement  = BreakElement.P;";
				break;
		}	
		Editor1.Visible = true;	
			
	}
</script>