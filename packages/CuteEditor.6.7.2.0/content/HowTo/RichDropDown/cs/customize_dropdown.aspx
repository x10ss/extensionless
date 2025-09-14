<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Page language="c#"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd"> 
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>How to dynamically populate the dropdown menu?</title>
    <style>
            body { 
            margin-top:20px
            }
            .demo { 
            text-align: left; 
            width: 840px;
            font-family:Segoe UI, Arial,Verdana,Helvetica,sans-serif;
            font-size: 100%;
	        background-color: #ffffff;
	        margin-left: auto;
	        margin-right: auto;
	        padding:10px 30px 10px 30px;
            } 
        </style>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="demo">
            <h3>
                How to dynamically populate the dropdown menu?</h3>
            <h4>
                Populate the Css class dropdown menu</h4>
            <CE:EDITOR id="Editor1" Autoconfigure="Simple" runat="server" />
            <hr>
            <h4>
                Add new dropdown menu</h4>
            <CE:EDITOR id="Editor2" Autoconfigure="Minimal" runat="server" />
        </div>
    </form>
</body>
</html>


<script runat="server">
	void Page_Load(object sender, System.EventArgs e)
	{
		//Editor1 - Change the existing dropdowns

		if(!IsPostBack)
		{
			//see the AutoConfigure/Full.config
			//<item type="dropdown" name="CssClass" RenderItemBorder="true" text="[[CssClass]]" command="CssClass" />
			if(Editor1.ToolControls["CssClass"]!=null)
			{
				CuteEditor.RichDropDownList dropdown=(CuteEditor.RichDropDownList)Editor1.ToolControls["CssClass"].Control;
			
				//the first item is just the caption
				CuteEditor.RichListItem richitem=dropdown.Items[0];

				//clear the items from configuration files
				//see Configuration/Shared/Common.config
				dropdown.Items.Clear();

				//add the caption - CssClass
				dropdown.Items.Add(richitem);

				//add value only
				dropdown.Items.Add("class1");

				//add text and value
				dropdown.Items.Add("myclass2","class2");

				//Add html and text and value
				dropdown.Items.Add("<span style='color: green; font-weight: bold;'>Bold Green Text</font>","Bold Green Text","BoldGreen");
			}
		}

		//Editor2 - Add new dropdowns

		//see the AutoConfigure/Full.config
		//<item type="holder" name="insertcustombutonhere" />
		if(Editor2.ToolControls["insertcustombutonhere"]!=null)
		{
			Control container=Editor2.ToolControls["insertcustombutonhere"].Control;

			CuteEditor.RichDropDownList dropdown=new CuteEditor.RichDropDownList(Editor2);

			//set the onchange statement
			//use the CuteEditor_DropDownCommand => editor.ExecCommand('InsertLink',false,ddl.value)
			dropdown.Attributes["onchange"]="CuteEditor_DropDownCommand(this,'InsertLink')";

			//must set this css name
			dropdown.CssClass="CuteEditorDropDown";

			//add the first item (caption)
			//the culture-text would be auto replaced..
			dropdown.Items.Add("[[Links]]","");
			
			//hide the first item (caption) in the float-panel
			dropdown.RichHideFirstItem=true;
			
			//add the items here every times
			dropdown.Items.Add("http://www.asp.net/");

			//add - !!!
			//if the statements put before Controls.Add , the statements must be executed every time
			container.Controls.Add(dropdown);
			//if the statements put after Controls.Add the statements could be executed only the first time

			//or add items here if(!IsPostBack)
			if(!IsPostBack)
			{
				dropdown.Items.Add("Microsoft","http://www.microsoft.com/");
				dropdown.Items.Add("<font color=red>CuteSoft</font>","*CuteSoft*","http://www.cutesoft.net/");
			}
		}
	}

</script>
