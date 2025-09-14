using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CuteEditorWeb.HowTo.RichDropDown
{
	/// <summary>
	/// Summary description for customize_dropdown.
	/// </summary>
	public class customize_dropdown : System.Web.UI.Page
	{
		protected CuteEditor.Editor Editor2;
		protected CuteEditor.Editor Editor1;
		private void Page_Load(object sender, System.EventArgs e)
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

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
