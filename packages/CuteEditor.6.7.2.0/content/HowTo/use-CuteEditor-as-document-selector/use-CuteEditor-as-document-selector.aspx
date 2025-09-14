<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<%@ Page Language="C#"%>
<html>
<head>
    <title>ASP.NET WYSIWYG Editor - Use CuteEditor as a document selector</title>
    <style>
        body { 
        text-align: center; 
        margin-top:20px
        }
        .demo { 
        text-align: left; 
        width: 700px;
        border:solid 5px #CBCAC6;
        background-color:#fbfbfb;
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
                Use CuteEditor as a document selector</h3>
            <p>
                This example demonstrates how to use CuteEditor as a document selector.
            </p>
            <br />
            <asp:TextBox ID="docFld" Width="300" runat="server" />
            <input type="button" value="Pick a file" onclick="callInsertImage()" id="Change"
                runat="server" name="Change">
            <CE:Editor id="Editor1" runat="server" Width="1" Height="1" AutoConfigure="None"
                ShowHtmlMode="False" ShowPreviewMode="False" EnableContextMenu="false" ShowGroupMenuImage="False"
                ShowBottomBar="False" BackColor="White" BorderColor="White">
            </CE:Editor>
        </div>
    </form>

    <script language="javascript"> 
    
     function callInsertImage()  
    {  
			var editor1 = document.getElementById('<%=Editor1.ClientID%>');
            var editdoc = editor1.GetDocument();  
            editdoc.body.innerHTML="";
            editor1.ExecCommand('insertdocument');
            InputURL();
            document.getElementById("<%=docFld.ClientID%>").focus(); 
    }   
    
    function InputURL()
    { 
		var editor1 = document.getElementById('<%=Editor1.ClientID%>');
        var editdoc = editor1.GetDocument();  
        var links = editdoc.getElementsByTagName("a");       
        if(links.length>0&&links[links.length-1].href!="")  
		{	document.getElementById("<%=docFld.ClientID%>").value = links[links.length-1].href;       
		}  
		else
		{
			setTimeout(InputURL,500); 
		}   
    }      
    </script>

</body>
</html>