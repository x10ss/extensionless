<%@ Page Language="vb"%>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>JavaScript API</title>
		<link rel="stylesheet" href="../example.css" type="text/css" />
			<script language="JavaScript" type="text/javascript" >
			function getHTML()
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				
				// Get the editor HTML
				document.getElementById("myTextArea").value = editor1.getHTML();
			}		
			
			function setHTML()
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				
				// Set the editor 
				editor1.setHTML(document.getElementById("myTextArea").value);
			}
			
			
			function PasteHTML(html)
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				
				editor1.PasteHTML(html);
			}
			
			function SetActiveTab(tab)
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				
				editor1.SetActiveTab(tab);
			}
			
			
			function setFocus()
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				
				editor1.FocusDocument();
			}
			
			
			function ExecCommand()
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				
				var CommandObj = document.getElementById('Commands');
				var cmd = CommandObj.options[CommandObj.selectedIndex].text;
				var val = CommandObj.options[CommandObj.selectedIndex].value;
				editor1.ExecCommand(cmd,false,val);
			}
			
			function CE_attachEvent()
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				
				//Get the editor content  
				var editdoc=editor1.GetDocument(); 

				// attach Event
				if(editdoc.attachEvent)
					editdoc.attachEvent('onkeypress',HandleChange);
				else if(editdoc.addEventListener)
					editdoc.addEventListener('keypress',HandleChange,true);
			}			
			
			function CE_detachEvent()
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				
				//Get the editor content  
				var editdoc=editor1.GetDocument(); 

				// detach Event
				if(editdoc.detachEvent)
					editdoc.detachEvent('onkeypress',HandleChange);
				else if(editdoc.removeEventListener)
					editdoc.removeEventListener('keypress',HandleChange,true);
			}
			
			function HandleChange()
			{
				// get the cute editor instance
				var editor1 = document.getElementById('<%=Editor1.ClientID%>');
				//Get the editor content  
				var editdoc=editor1.GetDocument(); 
				alert(editdoc.body.innerHTML);
			}		
				
		</script>
		<script runat="server">
		Overrides Protected  Sub OnInit(ByVal e As EventArgs)
				MyBase.OnInit(e)		
	 
				Editor1.Text = "<table cellspacing=""4"" cellpadding=""4"" border=""0""> <tbody> <tr> <td> <p> <img src=""http://cutesoft.net/Uploads/j0262681.jpg"" width=""80"" alt=""""/></p></td> <td> <p>When your algorithmic and programming skills have reached a level which you cannot improve any further, refining your team strategy will give you that extra edge you need to reach the top. We practiced programming contests with different team members and strategies for many years, and saw a lot of other teams do so too.  </p></td></tr> <tr> <td> <p>  <img src=""http://cutesoft.net/Uploads/PH02366J.jpg"" width=""80"" alt="""" /></p></td> <td> <p>From this we developed a theory about how an optimal team should behave during a contest. However, a refined strategy is not a must: The World Champions of 1995, Freiburg University, were a rookie team, and the winners of the 1994 Northwestern European Contest, Warsaw University, met only two weeks before that contest.  </p></td></tr></tbody></table> <br /> <br />" 
				Editor1.SetScriptProperty("ServerTime",DateTime.Now.ToString("HH:mm:ss"))
				Editor1.AddInitializeScriptCode("alert(editor.id+' loaded! server time : '+editor.GetScriptProperty('ServerTime'))")
		End Sub
		</script>
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
                        JavaScript API</h1>
                    <p>
                        This example shows you how to use CuteEditor JavaScript API to customize the application.
                    </p>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <CE:Editor id="Editor1" Width="560" TemplateItemList="[Save,Bold,Italic,Underline,InsertChars,InsertEmotion]"
                                    ThemeType="OfficeXP" Height="250" EditorWysiwygModeCss="../example.css" runat="server">
                                </CE:Editor><br />
                                <textarea cols="60" rows="5" id="myTextArea" style="width: 550px" name="myTextArea">Try click the "get HTML" button</textarea>
                                <br />
                                <br />
                                <p style="width: 580px">
                                    <input type="button" value="get HTML" onclick="getHTML()" id="Button1">
                                    <input type="button" value="set HTML" onclick="setHTML()" id="Button2">
                                    <input type="button" value="insert HTML" onclick="PasteHTML('This is a test!')">
                                    <input type="button" value="set ActiveTab to Code" onclick="SetActiveTab('Code')">
                                    <input type="button" value="set Focus" onclick="setFocus()">
                                    <input type="button" value="attach Event (onkeypress)" onclick="CE_attachEvent()">
                                    <input type="button" value="detach Event (onkeypress)" onclick="CE_detachEvent()">
                                </p>
                                <br />
                                <select id="Commands">
                                    <option value="Cut">Cut</option>
                                    <option value="Copy">Copy</option>
                                    <option value="Delete">Delete</option>
                                    <option value="InsertParagraph">InsertParagraph</option>
                                    <option value="Bold">Bold</option>
                                    <option value="Italic">Italic</option>
                                    <option value="Underline">Underline</option>
                                    <option value="InsertOrderedList">InsertOrderedList</option>
                                    <option value="InsertUnorderedList">InsertUnorderedList</option>
                                    <option value="Indent">Indent</option>
                                    <option value="Outdent">Outdent</option>
                                    <option value="Superscript">Superscript</option>
                                    <option value="Subscript">Subscript</option>
                                    <option value="StrikeThrough">StrikeThrough</option>
                                    <option value="Unlink">Unlink</option>
                                    <option value="SelectAll">SelectAll</option>
                                    <option value="tabedit">tabedit</option>
                                    <option value="tabcode">tabcode</option>
                                    <option value="tabview">tabview</option>
                                    <option value="ucase">ucase</option>
                                    <option value="lcase">lcase</option>
                                    <option value="break">break</option>
                                    <option value="undo">undo</option>
                                    <option value="redo">redo</option>
                                    <option value="This is a test">pastehtml</option>
                                    <option value="This is a test">pastetext</option>
                                    <option value="This is a test">paste</option>
                                    <option value="This is a test">pasteword</option>
                                    <option value="cleancode">cleancode</option>
                                    <option value="inserttext">inserttext</option>
                                    <option value="insertfieldset">insertfieldset</option>
                                    <option value="toggleborder">toggleborder</option>
                                    <option value="imagegallerybybrowsing">imagegallerybybrowsing</option>
                                    <option value="insertimage">insertimage</option>
                                    <option value="insertmedia">insertmedia</option>
                                    <option value="insertflash">insertflash</option>
                                    <option value="insertdocument">insertdocument</option>
                                    <option value="inserttemplate">inserttemplate</option>
                                    <option value="tabledropdown">tabledropdown</option>
                                    <option value="inserttable">inserttable</option>
                                    <option value="insertleftcolumn">insertleftcolumn</option>
                                    <option value="insertcolumnleft">insertcolumnleft</option>
                                    <option value="insertcolumn">insertcolumn</option>
                                    <option value="insertrightcolumn">insertrightcolumn</option>
                                    <option value="insertcolumnright">insertcolumnright</option>
                                    <option value="inserttoprow">inserttoprow</option>
                                    <option value="insertrowtop">insertrowtop</option>
                                    <option value="insertrow">insertrow</option>
                                    <option value="insertbottomrow">insertbottomrow</option>
                                    <option value="insertrowbottom">insertrowbottom</option>
                                    <option value="deleterow">deleterow</option>
                                    <option value="deletecolumn">deletecolumn</option>
                                    <option value="insertcell">insertcell</option>
                                    <option value="deletecell">deletecell</option>
                                    <option value="editrow">editrow</option>
                                    <option value="editcell">editcell</option>
                                    <option value="mergeright">mergeright</option>
                                    <option value="mergebottom">mergebottom</option>
                                    <option value="horsplitcell">horsplitcell</option>
                                    <option value="versplitcell">versplitcell</option>
                                    <option value="insertform">insertform</option>
                                    <option value="inserttextbox">inserttextbox</option>
                                    <option value="insertlistbox">insertlistbox</option>
                                    <option value="insertdropdown">insertdropdown</option>
                                    <option value="insertradiobox">insertradiobox</option>
                                    <option value="insertcheckbox">insertcheckbox</option>
                                    <option value="insertinputtext">insertinputtext</option>
                                    <option value="insertinputimage">insertinputimage</option>
                                    <option value="insertinputsubmit">insertinputsubmit</option>
                                    <option value="insertinputreset">insertinputreset</option>
                                    <option value="insertinputbutton">insertinputbutton</option>
                                    <option value="insertinputpassword">insertinputpassword</option>
                                    <option value="insertinputhidden">insertinputhidden</option>
                                    <option value="help">help</option>
                                    <option value="red">forecolor</option>
                                    <option value="yellow">backcolor</option>
                                    <option value="150">zoom</option>
                                    <option value="normal">cssclass</option>
                                    <option value="color:red">cssstyle</option>
                                    <option value="Arial">fontname</option>
                                    <option value="3">fontsize</option>
                                    <option value="removeformat">removeformat</option>
                                    <option value="justifyleft">justifyleft</option>
                                    <option value="justifycenter">justifycenter</option>
                                    <option value="justifyright">justifyright</option>
                                    <option value="justifyfull">justifyfull</option>
                                    <option value="justifynone">justifynone</option>
                                    <option value="inserttime">inserttime</option>
                                    <option value="insertdate">insertdate</option>
                                    <option value="bringforward">bringforward</option>
                                    <option value="bringbackward">bringbackward</option>
                                    <option value="sizeplus">sizeplus</option>
                                    <option value="sizeminus">sizeminus</option>
                                    <option value="documentpropertypage">documentpropertypage</option>
                                    <option value="netspell">netspell</option>
                                    <option value="Http://CuteSoft.net">insertlink</option>
                                    <option value="find">find</option>
                                    <option value="insertchars">insertchars</option>
                                    <option value="insertemotion">insertemotion</option>
                                    <option value="fullpage">fullpage</option>
                                    <option value="tofullpage">tofullpage</option>
                                    <option value="fromfullpage">fromfullpage</option>
                                </select>
                                <input type="button" value="ExecCommand" onclick="ExecCommand()"></td>
                            <td>
                                <div style="padding: 5px; border: solid 1px #cccccc; width: 180px; height: 300px;
                                    background-color: #ffffcc">
                                    <b>Test CuteEditor_OnChange</b>
                                    <p>
                                        Start typing...</p>
                                    <p id="ctl_onchange">
                                        The content is changed at:
                                    </p>
                                    <br />
                                    <br />
                                    <b>Test CuteEditor_OnCommand</b>
                                    <p>
                                        Click the <font color="red"><b>InsertEmotion</b></font> button.
                                    </p>
                                    <br />
                                    <br />
                                    <b>Test CuteEditor_OnInitialized</b>
                                    <p>
                                        Click the <font color="red"><b>InsertChars</b></font> button.
                                    </p>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<script language="JavaScript" type="text/javascript" >
		var editor1=document.getElementById("<%=Editor1.ClientID%>");
		if(editor1.IsReady)CuteEditor_OnInitialized(editor);

		function CuteEditor_OnChange(editor)
		{	
			//when the content be changed..
			document.getElementById("ctl_onchange").innerHTML=editor.id+" changed at "+ new Date().toLocaleTimeString();
		}
		function CuteEditor_OnCommand(editor,command,ui,value)
		{
			//handle the command by yourself
			if(command=="InsertEmotion")
			{
				var answer = confirm("Click OK to stop this command.")
				if (answer){
					return true;
				}
				else{
					return false;
				}
			}
		}
				
		function CuteEditor_OnInitialized(editor)
		{
			var oldexec=editor1.ExecCommand;
			editor1.ExecCommand=function(cmd,ui,val)
			{
				if(cmd=="InsertChars")
				{
					alert("Run some code here ....");
					//return;
				}
				return oldexec.apply(this,arguments);
			}
		}
</script>