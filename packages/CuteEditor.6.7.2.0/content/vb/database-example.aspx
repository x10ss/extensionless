<%@ Page Language="vb" Debug="true"%>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Database Example</title>
		<link rel="stylesheet" href="../example.css" type="text/css" />
	</head>
	<body>
        <form id="Form1" runat="server">
			<cutesoft:banner id="banner1" runat="server" />	
			<table cellpadding="15">
				<tr>
					<td id="leftcolumn">
						<cutesoft:leftmenu id="leftmenu1" runat="server" />				
					</td>
					<td>
						<h1>Database Example</h1>
						<p>This example show you how easy it can be to save the CuteEditor's content in a database. 
						</p><br />
						<asp:Datagrid runat="server"
							Id="MyDataGrid"
							cellpadding="3"
							cellspacing="0"
							Headerstyle-BackColor="#eeeeee"
    						Headerstyle-Font-Bold="True"
							BackColor="#f5f5f5"
							BorderWidth="1"
							Width=760
							BorderColor="#999999"
							AutogenerateColumns="False"
							OnItemCommand="UpdateItem"
							>
							<Columns>
									<asp:BoundColumn DataField="EventID" Visible="False" />
									<asp:BoundColumn  ItemStyle-Width="50px" DataField="EventID" HeaderText="ID" />
									<asp:BoundColumn  ItemStyle-Width="430px" DataField="Notes" HeaderText="Note" />
									<asp:BoundColumn  ItemStyle-Width="120px" DataField="EventDate" HeaderText="Date" />
									<asp:ButtonColumn ItemStyle-Width="50px" ButtonType="LinkButton"  CommandName="Edit" HeaderText="Edit" Text="Edit" />
									<asp:ButtonColumn ItemStyle-Width="50px" ButtonType="LinkButton"  CommandName="Delete" HeaderText="Delete" Text="Delete" />
							</Columns>
						</asp:datagrid>
						<br />
						<CE:Editor id="Editor1" EditorWysiwygModeCss="../example.css" Autoconfigure="Simple" Height="200" runat="server" ></CE:Editor><br />
						<asp:Button id="btnUpdate" onclick="Submit" Runat="server" Text="Add"></asp:Button>
						<asp:Literal ID="Literal1" Runat="server" />
						<br /><br />
						<input type="hidden" name="eventid" runat="server" id="eventid">
					</td>
				</tr>
			</table>			
		</form>
	</body>
</html>
<script runat="server">
	Sub Page_Load(Source as Object, E as EventArgs)
		if not Page.IsPostBack then
			BindData
		end if
	End Sub
	
	Sub BindData()
		Dim sql as string = "Select EventID, Notes, EventDate from Events"
		Dim conn As OleDbConnection = CreateConnection()
		Dim objDR as OleDbDataReader
		Dim Cmd as New OleDbCommand(sql, conn)
		objDR=Cmd.ExecuteReader(system.data.CommandBehavior.CloseConnection)
		MyDataGrid.DataSource = objDR
		MyDataGrid.DataBind()
	End Sub
		
	Sub UpdateItem(s As Object, e As DataGridCommandEventArgs )
		Dim conn As OleDbConnection = CreateConnection()
		
		'Check if the CommandName==Delete
		If e.CommandName = "Delete" Then
			Dim com As OleDbCommand = New OleDbCommand("DELETE FROM Events WHERE EventID = @id", conn)
			com.Parameters.Add("id", e.Item.Cells(0).Text)
			com.ExecuteNonQuery()
			conn.Close()		
		else If (e.CommandName = "Edit") then
			Dim com As OleDbCommand = New OleDbCommand("SELECT Notes FROM Events WHERE EventID = @id", conn)
			com.Parameters.Add("id", e.Item.Cells(0).Text)	
            Dim result As OleDbDataReader = com.ExecuteReader()
            If result.Read() Then
				'set the editor text 
				Editor1.Text = result.GetString(0)
                eventid.Value = e.Item.Cells(0).Text
				btnUpdate.Text="Update"
            Else
				Editor1.Text = ""
				eventid.Value = ""
				btnUpdate.Text="Add"
            End If
			result.Close()
		End If
		BindData
	End Sub
	
	Sub Submit(s As Object, e As System.EventArgs )
		Dim conn As OleDbConnection = CreateConnection()
        Dim com As OleDbCommand = Nothing

		If Not eventid.Value = String.Empty Then
			com = New OleDbCommand("UPDATE Events SET EventDate = Date(), Notes = @content WHERE EventID = @id", conn)
			com.Parameters.Add("content", Editor1.Text)
			com.Parameters.Add("id", Convert.ToInt32(eventid.Value))
		Else
			com = New OleDbCommand("INSERT INTO Events (EventDate, Notes) VALUES (Date(), @content)", conn)
			com.Parameters.Add("content", Editor1.Text)
		End If
		com.ExecuteNonQuery()
		conn.Close()
		BindData
		Me.Response.Redirect(Me.Request.Url.PathAndQuery)
	End Sub
	
	Function CreateConnection() As OleDbConnection
        Dim conn As OleDbConnection = New OleDbConnection
        conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Context.Server.MapPath("../uploads/Northwind.mdb") + ";"
		conn.Open()
        Return conn
    End Function
</script>

