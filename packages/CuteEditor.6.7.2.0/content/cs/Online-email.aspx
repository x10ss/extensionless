<%@ Page Language="C#" ValidateRequest="false"%>
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
        <cutesoft:banner ID="banner1" runat="server" />
        <table cellpadding="15">
            <tr>
                <td id="leftcolumn">
                    <cutesoft:leftmenu ID="leftmenu1" runat="server" />
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
                                <asp:TextBox ID="SubjectTextBox" runat="server" value="Rich-text HTML email"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                From:
                            </td>
                            <td>
                                <asp:TextBox ID="FromTextBox" runat="server"></asp:TextBox>
                                email address
                                <asp:RegularExpressionValidator ControlToValidate="FromTextBox" Text="Invalid Email Address!"
                                    ValidationExpression="\S+@\S+\.\S{2,3}" runat="Server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="'Email' must not be left blank."
                                    ControlToValidate="FromTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                To:
                            </td>
                            <td>
                                <asp:TextBox ID="ToTextBox" runat="server"></asp:TextBox>
                                email address
                                <asp:RegularExpressionValidator ControlToValidate="ToTextBox" Text="Invalid Email Address!"
                                    ValidationExpression="\S+@\S+\.\S{2,3}" runat="Server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="'Email' must not be left blank."
                                    ControlToValidate="ToTextBox"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <CE:Editor EditorWysiwygModeCss="../example.css" ThemeType="Office2003" URLType="Absolute"
                        id="Editor1" Height="250px" AutoConfigure="Simple" runat="server">
                    </CE:Editor><br />
                    <asp:Button ID="btnUpdate" OnClick="Submit" runat="server" Text="Send email..."></asp:Button>
                    <br />
                    <br />
                    <asp:Label ID="ResultLabel" runat="server"></asp:Label>
                </td>
                <tr>
        </table>
    </form>
</body>
</html>

<script runat="server">
	void Page_Load(object sender, System.EventArgs e)
	 {
	    if (!IsPostBack) 
		{ 
			Editor1.Text = "Type Here";
		} 
	
	}
	public void Submit(object sender, System.EventArgs e)
	{	
		if (Page.IsValid)
		{
			try
            {
                SmtpMail.SmtpServer = "localhost";
                MailMessage mail = new MailMessage();
                mail.From = FromTextBox.Text;
                mail.Subject = SubjectTextBox.Text;
                mail.Body = Editor1.Text;
                mail.To = ToTextBox.Text;
                mail.BodyFormat = MailFormat.Html;
                SmtpMail.Send(mail);
    
                ResultLabel.Text = "Message sent successfully.";
            }
            catch (Exception exc)
            {
                ResultLabel.Text = "<b>Message could not be sent: " + exc.Message + "</b><br>"
                    + "Please verify that the following settings are correct:<ul>"
                    + "<li>You have installed a locale SMTP service"
                    + "<li>Your local SMTP service is set to allow relaying for IP 127.0.0.1</li>"
                    + @"<li>The ASPNET account has read/write permissions in mailroot directory (usually 'C:\inetpub\mailroot')</li>"
                    + "</ul>";
            }
        }
	}
</script>