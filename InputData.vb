Imports System.Security.AccessControl
Imports System.IO

Public Class InputData

    Private nextKeyPressed As Boolean = True

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        Dim folder As New FolderBrowserDialog
        folder.Description = "请选择开始整理的文件夹根目录..."
        folder.ShowNewFolderButton = False
        folder.ShowDialog()
        Me.Visible = True
        Dim fileName As String = folder.SelectedPath
        DocInfo.ForeColor = Color.Black
        searchFolder(fileName)

    End Sub

    '选择文件夹，遍历文件夹。
    Private Sub searchFolder(ByVal folderPath As String)

        '处理当前文件夹下的*.dwg文件
        Dim s As DirectorySecurity = New DirectorySecurity(folderPath, AccessControlSections.Access)

        If Not (s.AreAccessRulesProtected) Then
            If Directory.GetFiles(folderPath).Length > 0 Then
                Dim fileNames As String() = Directory.GetFiles(folderPath)
                For Each fullName As String In fileNames
                    Dim sectionNo As Integer = fullName.Split("\").Length - 1
                    Dim path As String = ""
                    Dim fileName As String = ""
                    For i As Integer = 0 To fullName.Split("\").Length - 2
                        path = path & fullName.Split("\")(i) & "\"
                    Next
                    fileName = fullName.Split("\")(sectionNo)
                    DocInfo.Text = "文件名:   " & fileName & vbCrLf & "文件夹:   " & path & vbCrLf
                    Do While nextKeyPressed
                        Application.DoEvents()
                    Loop
                    nextKeyPressed = True
                Next
            End If


            ''递归调用，处理当前文件夹下子文件夹
            'If Directory.GetDirectories(folderPath).Length > 0 Then
            '    Dim subDirectories() As String = Directory.GetDirectories(folderPath)

            '    For Each subDirectory As String In subDirectories
            '        searchFolder(subDirectory)
            '    Next

            'End If

        End If

    End Sub



    Private Sub NextFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextFile.Click
        nextKeyPressed = False
    End Sub
End Class
