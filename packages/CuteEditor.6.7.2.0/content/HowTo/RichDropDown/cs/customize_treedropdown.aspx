<%@ Page Language="C#"%>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>How to dynamically populate the tree view dropdown menu?</title>
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
    <form runat="server">
        <div class="demo">
            <h3>
                How to dynamically populate the tree view dropdown menu?</h3>
            <h4>
                Populate the link tree view dropdown menu</h4>
            <CE:Editor id="Editor1" Autoconfigure="Compact" runat="server">
            </CE:Editor><br />
            <hr>
            <h4>
                Add new tree view dropdown menu</h4>
            <CE:EDITOR id="Editor2" Autoconfigure="Minimal"
                runat="server">
            </CE:EDITOR>
        </div>
    </form>
</body>
</html>



<script runat="server">
	void Page_Load(object sender, System.EventArgs e)
	 {
	    if (!IsPostBack) 
		{ 
			Editor1.Text = "Type Here";
   			//Get the TreeDropDownList
			//check <item type="treedropdown" name="LinkTree" text="[[Links]]" width="50" command="InsertLink" />
			//check Common.config
			CuteEditor.ToolControl tc = Editor1.ToolControls["LinkTree"];
			CuteEditor.TreeDropDownList tdd = (CuteEditor.TreeDropDownList) tc.Control;			
			
			//clear the items from configuration files
			//see Configuration/Shared/Common.config
			//tdd.Items.Clear();
					
			//Add items by code
			CuteEditor.TreeListItem rootitem=new CuteEditor.TreeListItem("Root",null);
			rootitem.Selectable=false;
			tdd.Items.Add(rootitem);
			rootitem.Items.Add("Asp<font color=red>.Net</font>","Asp.Net","http://asp.net");
			rootitem.Items.Add("DotNetNuke.Net","DotNetNuke.Net","http://DotNetNuke.com");
			rootitem.Items.Add("CuteSoft","CuteSoft","http://CuteSoft.net");
	
			
			Editor2.Text = "Type Here";
						
			int index=Editor2.ToolControls.IndexOf("insertcustombutonhere");
			CuteEditor.TreeDropDownList ddl=new TreeDropDownList(Editor2);

			// ddl.RenderItemBorder=true;
			ddl.CssClass="CuteEditorDropDown";
            ddl.Width = 100;

			//set the command and event handler
			ddl.Attributes["Command"]="PasteHTML";
			//each item's value is just the parameter of Command
			ddl.Attributes["onchange"]="CuteEditor_DropDownCommand(this,'"+ddl.Attributes["Command"]+"')";

			//set the title
			ddl.RootItem.Text="MyTree";
			
			//Add items recursive
			AddItems(ddl.Items,"~/Uploads");			

			Editor2.InsertToolControl(index,"MyTree",ddl);
		} 
	
	}
	
	void AddItems(CuteEditor.TreeListItemCollection items,string virpath)
	{
		virpath=virpath.TrimEnd('/');
  
		string dir=Server.MapPath(virpath);
  
		string[] files=System.IO.Directory.GetFiles(dir);
		if(files==null||files.Length==0)
		{
			return;//Skip empty folder
		}
  
  
		CuteEditor.TreeListItem diritem=new CuteEditor.TreeListItem(System.IO.Path.GetFileName(dir),null);
		diritem.Selectable=false;
		items.Add(diritem);
  
		//for each sub directories
		foreach(string subdir in System.IO.Directory.GetDirectories(dir))
		{
			string subdirname=System.IO.Path.GetFileName(subdir);
			string subvirpath=virpath+"/"+subdirname;
   
			// Recursive .
			AddItems( diritem.Items , subvirpath );
		}
  
		foreach(string subfile in files)
		{
			string filename=System.IO.Path.GetFileName(subfile);
			string filepath=ResolveUrl(virpath).TrimEnd('/')+"/"+filename;
			string filetype=System.IO.Path.GetExtension(filename).ToLower();
			if(filetype==".gif"||filetype==".jpg"||filetype==".png")	
			{
				CuteEditor.TreeListItem fileitem=new CuteEditor.TreeListItem(filename,"<img src='"+filepath+"' />");
				diritem.Items.Add(fileitem);
			}
		}
	}
</script>
