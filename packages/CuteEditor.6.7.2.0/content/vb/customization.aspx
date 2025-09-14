<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>Add custom buttons</title>
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
						<h1>Add custom buttons</h1>
						<p>This example shows you how easy it can be to <b>add your own functions</b> to the CuteEditor with the help of CuteEditor extended functionalities. </p> 
						<br />
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" Height="200" runat="server" TemplateItemList="Bold,Italic,Underline,JustifyLeft,JustifyCenter,JustifyRight,InsertUnorderedList,Separator,Indent, Outdent, insertcustombutonhere"></CE:Editor><br />							
						<asp:textbox id="TextBox1" Runat="server">OK</asp:textbox>
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>

<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		Dim tc as CuteEditor.ToolControl
		
		tc = Editor1.ToolControls("insertcustombutonhere")
		
		If Not tc Is Nothing Then

			Dim Image1 As New System.Web.UI.WebControls.Image()
			Image1.ToolTip				= "Insert today's date"
			Image1.ImageUrl				= "tools.gif"
			Image1.CssClass				= "CuteEditorButton"
			SetMouseEvents(Image1)
			Image1.Attributes("onclick")="var d = new Date(); CuteEditor_GetEditor(this).ExecCommand('PasteHTML',false,d.toLocaleDateString())"
			tc.Control.Controls.Add(Image1)
			
			Dim postbutton As New Button()
			AddHandler postbutton.Click, AddressOf postbutton_Click
			postbutton.Text="PostBack"
			postbutton.Style("vertical-align")="middle"
			tc.Control.Controls.Add(postbutton)
			
			Dim Image2 As New System.Web.UI.WebControls.Image()
			Image2.ToolTip				= "Using oncommand"
			Image2.ImageUrl				= "tools.gif"
			Image2.CssClass				= "CuteEditorButton"
			SetMouseEvents(Image2)
			Image2.Attributes("Command")="MyCmd"
			tc.Control.Controls.Add(Image2)	
		End If
	End Sub
	
	public Sub SetMouseEvents(ByVal control as WebControl )
		control.Attributes("onmouseover")="CuteEditor_ButtonCommandOver(this)"
		control.Attributes("onmouseout")="CuteEditor_ButtonCommandOut(this)"
		control.Attributes("onmousedown")="CuteEditor_ButtonCommandDown(this)"
		control.Attributes("onmouseup")="CuteEditor_ButtonCommandUp(this)"
		control.Attributes("ondragstart")="CuteEditor_CancelEvent()"
	End Sub
	Private Sub postbutton_Click(ByVal obj As Object, ByVal args As EventArgs)
		TextBox1.Text="PostButton Clicked"
    End Sub
</script>


<script language="JavaScript" type="text/javascript" >
		var editor1=document.getElementById("<%=Editor1.ClientID%>");
		
		function CuteEditor_OnCommand(editor,command,ui,value)
		{
			//handle the command by yourself
			if(command=="MyCmd")
			{
				editor.ExecCommand("InsertTable");
				return true;
			}
		}
</script>