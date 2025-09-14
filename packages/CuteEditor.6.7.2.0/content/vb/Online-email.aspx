<%@ Page Language="vb" ValidateRequest="false"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ import Namespace="System.Web.Mail" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Online email example</title>
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
						<h1>
                        Online email example</h1>
                    <p>
                        You can use CuteEditor in all sorts of applications, and this demonstration shows
                        how it can be used to send richly-formatted emails in HTML format.
                    </p>
                    <br />
						<table>
							<tr>
								<td width="80">
										Subject:
									</td>
									<td>
										<asp:textbox id="SubjectTextBox" runat="server" value="Rich-text HTML email"></asp:textbox>
									</td>
								</tr>
								<tr>
									<td>
										From:
									</td>
									<td>
										<asp:textbox id="FromTextBox" runat="server"></asp:textbox>
										email address
										<asp:RegularExpressionValidator ID="RegularExpressionValidator1"
											ControlToValidate="FromTextBox"
											Text="Invalid Email Address!"
											ValidationExpression="\S+@\S+\.\S{2,3}"
											Runat="Server" 
										/>		
										<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="'Email' must not be left blank." ControlToValidate="FromTextBox"></asp:RequiredFieldValidator>								
									</td>
								</tr>
								<tr>
									<td>
										To:
									</td>
									<td>
										<asp:textbox id="ToTextBox" runat="server"></asp:textbox>
										email address 
										<asp:RegularExpressionValidator ID="RegularExpressionValidator2"
											ControlToValidate="ToTextBox"
											Text="Invalid Email Address!"
											ValidationExpression="\S+@\S+\.\S{2,3}"
											Runat="Server" 
										/>		
										<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="'Email' must not be left blank." ControlToValidate="ToTextBox"></asp:RequiredFieldValidator>								
														
									</td>
								</tr>
							</table>
				            
							<CE:Editor EditorWysiwygModeCss="../example.css" ThemeType="Office2007" URLType="Absolute" id="Editor1" Height="250px" AutoConfigure="Simple" runat="server" ></CE:Editor><br />
										
							<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Send email..."></asp:Button>
							<br /><br />
							<asp:Label id="ResultLabel" runat="server"></asp:Label> 
					</td>
				<tr>
			</table>			
		</form>
	</body>
</html>

<script runat="server">
	Public Sub Page_Load(sender As object, e As System.EventArgs)
	
		If Not Page.IsPostBack Then
			Editor1.Text = "Type Here"
		End If
	
	End Sub

	public Sub Submit(sender As object, e As System.EventArgs)
		If Page.IsValid Then
			Try
				SmtpMail.SmtpServer = "localhost"
				Dim Mail As New MailMessage()
				Mail.From = FromTextBox.Text
				Mail.Subject = SubjectTextBox.Text
				Mail.Body = Editor1.Text
				Mail.To = ToTextBox.Text
				Mail.BodyFormat = MailFormat.Html
				SmtpMail.Send(Mail)
			Catch exc As Exception
				ResultLabel.Text = "<b>Message could not be sent: " & exc.Message & "</b><br>" _
					& "Please verify that the following settings are correct:<ul>" _
					& "<li>You have installed a locale SMTP service" _
					& "<li>Your local SMTP service is set to allow relaying for IP 127.0.0.1</li>" _
					& "<li>The ASPNET account has read/write permissions in mailroot directory (usually 'C:\inetpub\mailroot')</li>" _
					& "</ul>"
			End Try
		End If
	End Sub
</script>