<%@ Page Language="C#"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>Personalization and Programmatic Security Example</title>
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
						<h1>Personalization and Programmatic Security Example</h1>
						<p style="width:770px">Cute Editor allows developers to assign a pre-defined set of 
							permissions by group or individual. This prevents a normal user 
							to access the administration functionality. The details of permissions are specified by an XML security policy file. Each 
							level maps to a specific file. The default mappings:
						</p>
						<ul>
							<li><b>admin</b>- maps to admin.config</li>
							<li><b>default</b>- maps to default.config</li>
							<li><b>guest</b>- maps to guest.config</li>
						</ul>
						<p style="width:770px">You can customize and extend each policy file by editing the XML security policy 
							file. You can also create your own policy files that define arbitrary permission sets.
						</p>
						<table cellpadding="8">
							<tr>
								<td>
<asp:radiobuttonlist style="BORDER:#c0c0c0 1px solid;" BackColor="#f5f5f5" id="RadioList" runat="server" autopostback="True" RepeatLayout="Table"
	RepeatColumns="3" CellPadding="2" CellSpacing="0" RepeatDirection="Horizontal" onselectedindexchanged="security_Changed">
	<asp:ListItem value="Administrators">Administrators</asp:ListItem>
	<asp:ListItem value="Members">Members</asp:ListItem>
	<asp:ListItem value="Guest">Guest</asp:ListItem>
	<asp:ListItem value="John">John (admin)</asp:ListItem>
	<asp:ListItem value="Mary">Mary (sales)</asp:ListItem>
	<asp:ListItem value="Tim">Tim (financial)</asp:ListItem>
</asp:radiobuttonlist>
								</td>
								<td style="color:red">	
									<asp:Literal ID="Literal1" Runat="server" />
								</td>
							</tr>
						</table>
						<CE:Editor id="Editor1" ThemeType="Office2007" EditorWysiwygModeCss="../example.css" runat="server" AutoConfigure="Simple"></CE:Editor>		
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
				Editor1.SecurityPolicyFile  = "default.config";
				SetUploadsFolder("~/Uploads/Member/");
				Editor1.ReadOnly = false;
			}
		}
		private void security_Changed(Object sender, EventArgs e)
		{
			string temp = "";
			switch(RadioList.SelectedItem.Value)
			{
				case "Administrators":
					Editor1.SecurityPolicyFile  = "Admin.config";
					SetUploadsFolder("~/Uploads/");
					break;
				case "Members":
					Editor1.SecurityPolicyFile  = "default.config";
					SetUploadsFolder("~/Uploads/Member/");
					break;
				case "Guest":
					Editor1.SecurityPolicyFile  = "Guest.config";
					SetUploadsFolder("~/Uploads/Guest/");
					break;
		//		case "Banned":
		//			Editor1.ReadOnly = true;
		//			break;
					case "John":
					Editor1.SecurityPolicyFile  = "Admin.config";
					SetUploadsFolder("~/Uploads/Users/John/");
					break;
				case "Mary":
					Editor1.SecurityPolicyFile  = "default.config";
					SetUploadsFolder("~/Uploads/Users/Mary/");
					break;
				case "Tim":
					Editor1.SecurityPolicyFile  = "default.config";
					SetUploadsFolder("~/Uploads/Users/Tim/");
					break;
			}
			Literal1.Text = ShowSecuritySetting();
		}
		
		string ShowSecuritySetting()
		{
			string temp = "";
			temp += "Policy file: "+ Editor1.SecurityPolicyFile;
			temp += "<br>";
			temp += "ImageGalleryPath: "+ Editor1.Setting["security:ImageGalleryPath"];
			temp += "<br>";
			temp += "FilesGalleryPath: "+ Editor1.Setting["security:FilesGalleryPath"];
			return temp;
		}
					
		void SetUploadsFolder(string folder)
		{
			string phyfolder=Server.MapPath(folder);
			System.IO.Directory.CreateDirectory(phyfolder);
			
			//see security.config
			Editor1.Setting["security:ImageGalleryPath"]=
			Editor1.Setting["security:ImageBrowserPath"]=
			Editor1.Setting["security:MediaGalleryPath"]=
			Editor1.Setting["security:FlashGalleryPath"]=
			Editor1.Setting["security:FilesGalleryPath"]=
				folder;
		}
</script>
