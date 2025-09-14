<%@ Page Language="vb"%>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register TagPrefix="cutesoft" TagName="banner" Src="banner.ascx" %>
<%@ Register TagPrefix="cutesoft" TagName="leftmenu" Src="leftmenu.ascx" %>
<%@ Register TagPrefix="CE" Namespace="CuteEditor" Assembly="CuteEditor" %>
<html>
    <head>
		<title>ASP.NET WYSIWYG Editor - Working with DataGrid</title>
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
						<h1>Working with DataGrid</h1>
						<p>This example show you how easy it can be to integrate the CuteEditor with the DataGrid. 
						</p><br />
						<asp:Datagrid runat="server"
						Id="MyDataGrid"
						cellpadding="3"
						cellspacing="0"
						Headerstyle-BackColor="#eeeeee"
    					Headerstyle-Font-Bold="True"
						BackColor="#f5f5f5"
						BorderWidth="1"
						Width=650
						Font-Name="Arial"
						Font-Size="12px"
						BorderColor="#999999"
						AutogenerateColumns="False"
						OnEditcommand="MyDataGrid_EditCommand"
						OnCancelcommand="MyDataGrid_Cancel"
						OnUpdateCommand="MyDataGrid_UpdateCommand">
						<Columns>
							<asp:EditCommandColumn 
								ButtonType="LinkButton" 
								UpdateText="Update" 
								CancelText="Cancel" 
								EditText="Edit"
            					ItemStyle-HorizontalAlign="Center" 
		    					HeaderText="Edit">
							</asp:EditCommandColumn>
							<asp:BoundColumn 
								DataField="EmployeeID" 
								HeaderText="ID" 
								ReadOnly="True">
							</asp:BoundColumn>
							
							<asp:TemplateColumn HeaderText = "Title">
								<ItemTemplate>
									<%# DataBinder.Eval(Container.DataItem, "FirstName") %>
								</ItemTemplate>
								<EditItemTemplate>
									<CE:Editor id = "FirstName_box" EditorWysiwygModeCss="../example.css" TemplateItemList="Bold,Italic,Underline" Height=150 Width=300 ShowBottomBar="false" Text = '<%#DataBinder.Eval(Container.DataItem, "FirstName") %>' runat = "Server" ></CE:Editor>
								</EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText = "Title">
								<ItemTemplate>
									<%# DataBinder.Eval(Container.DataItem, "LastName") %>
								</ItemTemplate>
								<EditItemTemplate>
									<CE:Editor id = "LastName_box" EditorWysiwygModeCss="../example.css" TemplateItemList="Bold,Italic,Underline" Height=150 Width=200 ShowBottomBar="false" Text = '<%#DataBinder.Eval(Container.DataItem, "LastName") %>' runat = "Server" ></CE:Editor>
								</EditItemTemplate>
							</asp:TemplateColumn>
							<asp:TemplateColumn HeaderText = "Title">
								<ItemTemplate>
									<%# DataBinder.Eval(Container.DataItem, "Title") %>
								</ItemTemplate>
								<EditItemTemplate>
									<CE:Editor id = "Title_box" EditorWysiwygModeCss="../example.css" TemplateItemList="Bold,Italic,Underline" Height=150 Width=200 ShowBottomBar="false" Text = '<%#DataBinder.Eval(Container.DataItem, "Title") %>' runat = "Server" ></CE:Editor>
								</EditItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:DataGrid>
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
		Dim sql as string = "Select EmployeeID, FirstName, LastName, Title from Employees"
		Dim conn As OleDbConnection = CreateConnection()
		Dim objDR as OleDbDataReader
		Dim Cmd as New OleDbCommand(sql, conn)
		objDR=Cmd.ExecuteReader(system.data.CommandBehavior.CloseConnection)
		MyDataGrid.DataSource = objDR
		MyDataGrid.DataBind()
		conn.close
	End Sub
	
	Sub MyDataGrid_EditCommand(s As Object, e As DataGridCommandEventArgs )
		MyDataGrid.EditItemIndex = e.Item.ItemIndex
		BindData
	End Sub

	Sub MyDataGrid_Cancel(Source As Object,   E As DataGridCommandEventArgs)
		MyDataGrid.EditItemIndex = -1
		BindData()
	End Sub
	
	Sub MyDataGrid_UpdateCommand(s As Object, e As DataGridCommandEventArgs )
		Dim conn As OleDbConnection = CreateConnection()
		Dim MyCommand As OleDbCommand

		Dim txtFirstName As CuteEditor.Editor = e.Item.FindControl("FirstName_Box")
		Dim txtLastName As CuteEditor.Editor = e.Item.FindControl("LastName_Box")
		Dim txtTitle As CuteEditor.Editor = e.Item.FindControl("Title_Box")

		Dim strUpdateStmt As String
			strUpdateStmt =" UPDATE Employees SET" & _
			" FirstName =@Fname, LastName =@Lname, Title = @Title " & _
			" WHERE EmployeeID = @EmpID"
		MyCommand = New OleDbCommand(strUpdateStmt, conn)
		MyCommand.Parameters.Add(New OleDbParameter("@Fname", txtFirstName.text))
		MyCommand.Parameters.Add(New OleDbParameter("@Lname", txtLastName.text))
		MyCommand.Parameters.Add(New OleDbParameter("@Title", txtTitle.text))
		MyCommand.Parameters.Add(New OleDbParameter("@EmpID", e.Item.Cells(1).Text ))
		MyCommand.ExecuteNonQuery()
		MyDataGrid.EditItemIndex = -1
			conn.close
		BindData

	End Sub
	
	Function CreateConnection() As OleDbConnection
        Dim myConnection As OleDbConnection = New OleDbConnection
        myConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Context.Server.MapPath("../uploads/Northwind.mdb") + ";"
		myConnection.Open()
        Return myConnection
    End Function
</script>

