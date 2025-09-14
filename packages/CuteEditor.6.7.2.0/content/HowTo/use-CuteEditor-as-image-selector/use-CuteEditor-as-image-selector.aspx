<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>

<%@ Page Language="C#" %>

<html>
<head>
    <title>ASP.NET WYSIWYG Editor - Use CuteEditor as an image selector</title>
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
    <form runat="server">
        <div class="demo">
            <h3>
                Use CuteEditor as an image selector</h3>
            <p>
                This example demonstrates how to use CuteEditor as an image selector.
            </p>
            <asp:TextBox ID="imageFld" Width="300" runat="server" />
            <input type="button" value="Change Image" onclick="callInsertImage()" id="Change"
                runat="server" name="Change">
            <CE:Editor id="Editor1" runat="server" Width="1" Height="1" AutoConfigure="None"
                ShowHtmlMode="False" ShowPreviewMode="False" EnableContextMenu="false" ShowGroupMenuImage="False"
                ShowBottomBar="False" BackColor="White" BorderColor="White">
            </CE:Editor>
        </div>
    </form>

    <script language="javascript"> 
	var editor1 = document.getElementById('<%=Editor1.ClientID%>');
	var imageFld = document.getElementById('imageFld');
    function callInsertImage()  
    {  
            editor1.FocusDocument();  
            editor1.ExecCommand('new');
            editor1.ExecCommand('ImageGalleryByBrowsing');
            InputURL();
    }    
    
    function InputURL()
    { 
        var editdoc = editor1.GetDocument();  
        var imgs = editdoc.getElementsByTagName("img");   
        if(imgs.length>0)  
        {	
            imageFld.value = imgs[imgs.length-1].src;
            editor1.ExecCommand('new');
            imageFld.focus(); 
        }  
        else
        {
			setTimeout(InputURL,500); 
        }  
    }       
    </script>

</body>
</html>
