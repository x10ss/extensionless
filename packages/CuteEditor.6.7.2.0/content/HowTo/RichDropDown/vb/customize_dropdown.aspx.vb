Public Class customize_dropdown
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected WithEvents Editor1 As CuteEditor.Editor
    Protected WithEvents Editor2 As CuteEditor.Editor
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            If Not Editor1.ToolControls("CssClass") Is Nothing Then

                Dim dropdown As CuteEditor.RichDropDownList
                Dim richitem As CuteEditor.RichListItem

                dropdown = DirectCast(Editor1.ToolControls("CssClass").Control, CuteEditor.RichDropDownList)

                'the first item is just the caption
                richitem = dropdown.Items(0)

                'clear the items from configuration files
                'see Configuration/Shared/Common.config
                dropdown.Items.Clear()

                'add the caption - CssClass
                dropdown.Items.Add(richitem)

                'add value only
                dropdown.Items.Add("class1")

                'add text and value
                dropdown.Items.Add("myclass2", "class2")

                'Add html and text and value
                dropdown.Items.Add("<span style='color: green; font-weight: bold;'>Bold Green Text</font>", "Bold Green Text", "BoldGreen")

            End If


            'Editor2 - Add new dropdowns

            'see the AutoConfigure/Full.config
            '<item type="holder" name="insertcustombutonhere" />

            If Not Editor1.ToolControls("insertcustombutonhere") Is Nothing Then


                Dim container As System.Web.UI.Control

                container = Editor2.ToolControls("insertcustombutonhere").Control

                Dim dropdown As CuteEditor.RichDropDownList

                dropdown = New CuteEditor.RichDropDownList(Editor2)

                'set the onchange statement
                'use the CuteEditor_DropDownCommand => editor.ExecCommand('InsertLink',false,ddl.value)
                dropdown.Attributes("onchange") = "CuteEditor_DropDownCommand(this,'InsertLink')"

                'must set this css name
                dropdown.CssClass = "CuteEditorDropDown"

                'add the first item (caption)
                'the culture-text would be auto replaced..
                dropdown.Items.Add("[[Links]]", "")

                'hide the first item (caption) in the float-panel
                dropdown.RichHideFirstItem = True

                'add the items here every times
                dropdown.Items.Add("http://www.asp.net/")

                'add - !!!
                'if the statements put before Controls.Add , the statements must be executed every time
                container.Controls.Add(dropdown)
                'if the statements put after Controls.Add the statements could be executed only the first time

                'or add items here if(!IsPostBack)
                If Not Page.IsPostBack Then
                    dropdown.Items.Add("Microsoft", "http://www.microsoft.com/")
                    dropdown.Items.Add("<font color=red>CuteSoft</font>", "*CuteSoft*", "http://www.cutesoft.net/")
                End If

            End If
        End If
    End Sub

End Class
