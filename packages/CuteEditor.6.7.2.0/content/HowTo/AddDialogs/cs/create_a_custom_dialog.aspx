<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Page language="c#"%>
<html>
<head>
    <title>ASP.NET WYSIWYG Editor - How to create a custom button which displays a dialog?</title>

    <script language="JavaScript" type="text/javascript">
			function ShowMyDialog(button)
			{
				//use CuteEditor_GetEditor(elementinsidetheEditor) to get the cute editor instance
				var editor=CuteEditor_GetEditor(button);
				//show the dialog page , and pass the editor as newwin.dialogArguments
				//(handler,url,args,feature)
				var newwin=editor.ShowDialog(null,"My_Custom_Text.html?_rand="+new Date().getTime()
					,editor,"dialogWidth:400px;dialogHeight:240px");
			}
    </script>

    <style>
        body { 
        text-align: center; 
        margin-top:20px
        }
        .demo { 
        text-align: left; 
        width: 800px;
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
                How to create a custom button which displays a dialog?</h3>
            <p>
                In this example, first we get the location of Italic button. Then we add a custom
                dialog after it.</p>
            <CE:Editor id="Editor1" ThemeType="OfficeXP" AutoConfigure="Minimal" runat="server" />
        </div>
    </form>
</body>
</html>
<script runat="server">
	private void Page_Load(object sender, System.EventArgs e)
	{
		//about Italic, see Full.config
		//<item type="image" name="Italic" imagename="Italic" />
		//get the pos after the Italic
		int pos=Editor1.ToolControls.IndexOf("Italic")+1;

		//Themes/%ThemeName%/Images/text.gif
		WebControl ctrl=Editor1.CreateCommandButton("MyButton","text.gif","Insert My Custom Text");

		ctrl.Attributes["onclick"]="ShowMyDialog(this)";
	
		//add this custom button into the editor
		Editor1.InsertToolControl(pos,"MyButton",ctrl);
	}
</script>