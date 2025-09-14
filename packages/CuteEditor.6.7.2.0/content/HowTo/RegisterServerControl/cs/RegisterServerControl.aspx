<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Page language="c#" AutoEventWireup="false"%>
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
	override protected void OnInit(EventArgs args)
	{
		Editor1.PostBackCommand += new System.Web.UI.WebControls.CommandEventHandler(Editor1_PostBackCommand);
		Editor1.TextChanged += new System.EventHandler(Editor1_TextChanged);
		Editor1.Initializing += new System.EventHandler(Editor1_Initializing);		
		base.OnInit(args);
	}
	private void Editor1_Initializing(object sender, System.EventArgs e)
	{
		//the controls could only register before the CuteEditor Initialized

		Button hello=new Button();
		hello.Text="Hello";
		hello.Style["vertical-align"]="middle";
		hello.CommandName="Hello";
		hello.Click+=new EventHandler(hello_Click);

		Button world=new Button();
		world.Text="World";
		world.Style["vertical-align"]="middle";
		world.CommandName="World";
		world.Click+=new EventHandler(world_Click);

		Editor1.RegisterCustomButton("hello",hello);
		Editor1.RegisterCustomButton("world",world);
	}

	private void Editor1_PostBackCommand(object sender, System.Web.UI.WebControls.CommandEventArgs e)
	{
		Label1.Text="You just click the button : "+e.CommandName;
	}

	private void hello_Click(object sender, EventArgs e)
	{
		Editor1.Text+="<div style='color:red'>Hello Clicked</div>";
	}

	private void world_Click(object sender, EventArgs e)
	{
		Editor1.Text+="<div style='color:red'>World Clicked</div>";
	}

	private void Editor1_TextChanged(object sender, System.EventArgs e)
	{
	
	}
</script>