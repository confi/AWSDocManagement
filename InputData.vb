Imports System.Security.AccessControl
Imports System.IO

Public Class InputData

    Private nextKeyPressed As Boolean = True
    Private fullName As String = ""
    Private thisClose As Boolean = False

    Private Sub InputData_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        thisClose = True
        nextKeyPressed = False
    End Sub



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Visible = False
        Dim folder As New FolderBrowserDialog
        folder.Description = "请选择开始整理的文件夹根目录..."
        folder.ShowNewFolderButton = False
        folder.ShowDialog()
        Me.TopMost = True
        Me.Visible = True

        Dim fileName As String = folder.SelectedPath
        DocInfo.ForeColor = Color.Black
        If fileName <> "" Then searchFolder(fileName)

        Me.Close()

    End Sub



    '选择文件夹，遍历文件夹。
    Private Sub searchFolder(ByVal folderPath As String)


        Dim s As DirectorySecurity = New DirectorySecurity(folderPath, AccessControlSections.Access)

        If Not (s.AreAccessRulesProtected) Then
            If Directory.GetFiles(folderPath).Length > 0 Then
                Dim fileNames As String() = Directory.GetFiles(folderPath)
                For Each Me.fullName In fileNames
                    Dim sectionNo As Integer = fullName.Split("\").Length - 1
                    Dim path As String = ""
                    Dim fileName As String = ""
                    For i As Integer = 0 To fullName.Split("\").Length - 2
                        path = path & fullName.Split("\")(i) & "\"
                    Next
                    fileName = fullName.Split("\")(sectionNo)
                    DocInfo.Text = "文件名:   " & fileName & vbCrLf & "文件夹:   " & path & vbLf
                    Do While nextKeyPressed
                        Application.DoEvents()

                    Loop
                    nextKeyPressed = True
                    If thisClose Then
                        Me.Dispose()
                        End
                    End If

                Next
            End If


            '递归调用，处理当前文件夹下子文件夹
            If Directory.GetDirectories(folderPath).Length > 0 Then
                Dim subDirectories() As String = Directory.GetDirectories(folderPath)

                For Each subDirectory As String In subDirectories
                    searchFolder(subDirectory)
                Next

            End If

        End If

    End Sub



    Private Sub NextFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextFile.Click
        nextKeyPressed = False
    End Sub

    '准备调用系统打开文件函数
    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" _
    (ByVal hWnd As Long, ByVal lpOperation As String, ByVal lpFile As String, _
     ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long



    Private Sub OpenFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenFile.Click
        Dim openStatus As Long
        openStatus = ShellExecute(Me.Handle, "open", fullName, "", "", 1)
        Select Case openStatus
            Case 26
                MsgBox("共享错误，请检查文件权限设置！", MsgBoxStyle.Exclamation, AcceptButton)
            Case 31
                MsgBox("没有关联的应用程序，请检查文件的扩展名！", MsgBoxStyle.Exclamation, AcceptButton)
            Case 28
                MsgBox("打开文件超时，请检查网络连接！", MsgBoxStyle.Exclamation, AcceptButton)
        End Select
    End Sub



    Private Sub MoveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveFile.Click
        Dim con As New LANConnect

        DocInfo.Text = con.remoteMan()
    End Sub
End Class
