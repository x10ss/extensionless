<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Page Language="vb" AutoEventWireup="false"%>
<html>
<head>
    <title>ASP.NET WYSIWYG Editor - How to capture the save button click event? </title>
    <style>
        body { 
        text-align: center; 
        margin-top:20px
        }
        .demo { 
        text-align: left; 
        width: 840px;
        padding: 30px 30px 50px 30px; 
        font-family:Segoe UI, Arial,Verdana,Helvetica,sans-serif;
        font-size: 100%;
        margin: 0 auto; 
        } 
    </style>
</head>
<body>
    <form runat="server" id="Form1">
        <div class="demo">
            <h3>
                How to capture the save button click event?</h3>
            <p>
                <asp:Label ID="Label1" runat="server"><b>Capture at server side</b></asp:Label></p>
            <CE:Editor id="Editor1" Height="200" OnPostBackCommand="Editor1_PostBackCommand"
                ThemeType="OfficeXP" AutoConfigure="Minimal" runat="server">
            </CE:Editor><br />
            <p>
                <b>Capture at client side (Try click the save button)</b></p>
            <div id="msg">
            </div>
            <CE:Editor id="Editor2" Height="200" runat="server" ThemeType="OfficeXP" AutoConfigure="Minimal">
            </CE:Editor></div>
    </form>
</body>
</html>

<script runat="server">
	 Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            Editor1.Text = "<table cellspacing=""4"" cellpadding=""4"" border=""0""> <tbody> <tr> <td> <p> <img src=""http://cutesoft.net/Uploads/j0262681.jpg"" width=""80"" /></p></td> <td> <p>When your algorithmic and programming skills have reached a level which you cannot improve any further, refining your team strategy will give you that extra edge you need to reach the top. We practiced programming contests with different team members and strategies for many years, and saw a lot of other teams do so too.  </p></td></tr> <tr> <td> <p> <img src=""http://cutesoft.net/Uploads/PH02366J.jpg"" width=""80"" /></p></td> <td> <p>From this we developed a theory about how an optimal team should behave during a contest. However, a refined strategy is not a must: The World Champions of 1995, Freiburg University, were a rookie team, and the winners of the 1994 Northwestern European Contest, Warsaw University, met only two weeks before that contest.  </p></td></tr></tbody></table> <br /> <br />"
	    End If


        Dim btn As New System.Web.UI.WebControls.Button
		btn.Text="Hello"
		'this command would fire to the parent of the btn until be processed
		'CuteEditor catch the CommandEventArgs of child controls,and fired it as PostBackCommand
		btn.CommandName="Hello"
        btn.Style("vertical-align") = "middle"
		'Editor1.AddToolbarLineBreak(Editor1.ToolControls)
		Editor1.AddToolbarGroupStart(Editor1.ToolControls)
		Editor1.AddToolControl(btn)
		Editor1.AddToolbarGroupEnd(Editor1.ToolControls)
    End Sub

    Private Sub Editor1_PostBackCommand(ByVal Sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        If String.Compare(e.CommandName, "Save", True) = 0 Then
            Label1.Text = "<h3>You just clicked the <font color=red>Save</font> button</h3>"
        Else
            Label1.Text = "<h3>Just recived the bubbled command : " + e.CommandName + " </h3>"
        End If
    End Sub

</script>


<script language="JavaScript" type="text/javascript" >
	function CuteEditor_OnCommand(editor,command,ui,value)
	{
		//handle the command by yourself
		if(command.toLowerCase()=="postback"&&value=="Save"&&editor.id=="CE_Editor2_ID")
		{
			alert("The Save Command is Captured at the client side. You can hook up your business logic here.\n You can also stop this command.");
			return true;//return true if you want to stop the command
		}
	}
	function CuteEditor_OnChange(editor)
	{
		//when the content be changed..
		document.getElementById("msg").innerHTML=editor.id+" changed at "+ new Date().toLocaleTimeString();
	}
</script>

