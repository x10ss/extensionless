<%@ Page Language="C#"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Dynamically Create Editors </title>
		<link rel="stylesheet" href="../example.css" type="text/css" />
	</head>
	<body>
        <form runat="server">
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
		int count = 1;
	    
		void IterateThroughChildren(Control parent)
		{
			foreach (Control c in parent.Controls)
			{
				if (c.GetType().ToString().Equals("CuteEditor.Editor") &&
					c.ID == null)
				{
				((CuteEditor.Editor) c).Text = "Editor " + count.ToString();
				((CuteEditor.Editor) c).AutoConfigure  = AutoConfigure.Simple;
				((CuteEditor.Editor) c).Height  = 200;				
				((CuteEditor.Editor) c).ThemeType  = ThemeType.OfficeXP;
				
				count++;
				}
		        
				if (c.Controls.Count > 0)
				{          
					IterateThroughChildren(c);          
				}
			}
		}

		void CreateEditors(Object sender, EventArgs e)
		{ 
			if (!Page.IsValid) return;
		      
			int n = Int32.Parse(txtTBCount.Text);
		      
			// now, create n Editors, adding them to the PlaceHolder EditorsHere
			for (int i = 0; i < n; i++)
			{
				EditorsHere.Controls.Add(new CuteEditor.Editor());
			}
		      
			// now, set the Text property of each CuteEditor.Editor
			IterateThroughChildren(this);
		}
</script>