<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Dynamically Create Editors </title>
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
						<h1>Dynamically Create Editors</h1>
						<h4>How many Editors would you like to create? (<i>Please choose vaule between 1 and 10</i>)</h4> 
			            <asp:textbox runat="Server" id="txtTBCount" Columns="3" />
			            <asp:RangeValidator runat="server" ControlToValidate="txtTBCount"
					            MinimumValue="1" MaximumValue="10" Type="Integer"
					            ErrorMessage="Make sure that you choose a value between 1 and 10!" ID="Rangevalidator1" NAME="Rangevalidator1"/>
			            <br /><br />
			            <asp:button runat="server" Text="Create Editors" OnClick="CreateEditors" ID="Button1" NAME="Button1"/>
			            <p>
				            <asp:PlaceHolder runat="server" id="EditorsHere" />
			            </p>
			           
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>

<script runat="server">
		
    Dim count As Integer = 1

    Private Sub IterateThroughChildren(ByVal parent As Control)
        Dim c As Control
        For Each c In parent.Controls
            If c.GetType().ToString().Equals("CuteEditor.Editor") Then
                Dim edit As CuteEditor.Editor
                edit = CType(c, CuteEditor.Editor)
                edit.Text = "Editor " + count.ToString()
                edit.AutoConfigure = AutoConfigure.Simple
                edit.Height = System.Web.UI.WebControls.Unit.Parse(200)
                edit.ThemeType = ThemeType.Office2003_BlueTheme
 
                count = count + 1
            End If

            If c.Controls.Count > 0 Then
                IterateThroughChildren(c)
            End If
        Next
    End Sub

    Private Sub CreateEditors(ByVal sender As Object, ByVal e As EventArgs)
        If Not Page.IsValid Then
            Return
        End If

        Dim n As Integer = Int32.Parse(txtTBCount.Text)

        ' now, create n Editors, adding them to the PlaceHolder EditorsHere
        Dim i As Integer
        For i = 0 To n - 1 Step i + 1
            EditorsHere.Controls.Add(New CuteEditor.Editor())
        Next

        ' now, set the Text property of each CuteEditor.Editor
        IterateThroughChildren(Me)
    End Sub

</script>