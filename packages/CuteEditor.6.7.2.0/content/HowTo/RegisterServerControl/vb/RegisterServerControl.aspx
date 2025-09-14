<%@ Page Language="vb" AutoEventWireup="false"%>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
<head>
    <title>How to add a Server Control into Editor Toolbar?</title>
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
    <form id="Form1" runat="server">
        <div class="demo">
            <h3>
                How to add a Server Control into Editor Toolbar?</h3>
            <p>
                This example uses the following events: <b>PostBackCommand, TextChanged and Initializing</b>.
            </p>
            <p>
                <asp:Label ID="Label1" runat="server">Command Message Here</asp:Label></p>
            <CE:EDITOR id="Editor1" runat="server" TemplateItemList="[Bold,Italic],[Hello|World]" />
        </div>
    </form>
</body>
</html>


<script runat="server">

    Overrides Protected  Sub OnInit(ByVal args As EventArgs)		
        AddHandler Editor1.PostBackCommand, AddressOf Editor1_PostBackCommand
        AddHandler Editor1.TextChanged, AddressOf Editor1_TextChanged
        AddHandler Editor1.Initializing, AddressOf Editor1_Initializing
        AddHandler Me.Load, AddressOf Page_Load		
		MyBase.OnInit(args)
	End Sub
	
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub
    
	 Private Sub Editor1_Initializing(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not Page.IsPostBack Then
            Editor1.Text = "Type Here"
        End If

        Dim hello As New System.Web.UI.WebControls.Button
        hello.Text = "Hello"
        hello.Style("vertical-align") = "middle"
        hello.CommandName = "Hello"
        AddHandler hello.Click, AddressOf hello_Click

        Dim world As New System.Web.UI.WebControls.Button
        world.Text = "Hello"
        world.Style("vertical-align") = "middle"
        world.CommandName = "world"
        AddHandler hello.Click, AddressOf world_Click


        Editor1.RegisterCustomButton("hello", hello)
        Editor1.RegisterCustomButton("world", world)

    End Sub

    Private Sub Editor1_PostBackCommand(ByVal Sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Label1.Text = "You just click the button : " + e.CommandName
    End Sub

    Private Sub hello_Click(ByVal obj As Object, ByVal args As EventArgs)
        Editor1.Text += "<div style='color:red'>Hello Clicked</div>"
    End Sub

    Private Sub world_Click(ByVal obj As Object, ByVal args As EventArgs)
        Editor1.Text += "<div style='color:red'>World Clicked</div>"
    End Sub

    Private Sub Editor1_TextChanged(ByVal obj As Object, ByVal args As EventArgs)
        Editor1.Text += "<div style='color:red'>Hello Clicked</div>"
    End Sub

</script>
