Imports System.Security.AccessControl
Imports System.IO

Public Class InputData

    Private nextKeyPressed As Boolean = True
    Private fullName As String = ""
    Private thisClose As Boolean = False

    Private intTotalFile As Integer = 0

    Private Sub InputData_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        thisClose = True
        nextKeyPressed = False
    End Sub


    Private Sub NextFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextFile.Click
        nextKeyPressed = False
    End Sub

    '准备调用系统打开文件函数
    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" _
    (ByVal hWnd As Long, ByVal lpOperation As String, ByVal lpFile As String, _
     ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long



    Private Sub OpenFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click
        subOpenFile(fullName)
    End Sub

    Private Sub subOpenFile(ByVal fileName As String)
        Dim openStatus As Long
        openStatus = ShellExecute(Me.Handle, "open", fileName, "", "", 1)
        Me.Status.Text = "正在打开文件……"
        Select Case openStatus
            Case Is > 32
                Me.Status.Text = "就绪"
            Case 26
                MsgBox("共享错误，请检查文件权限设置！", MsgBoxStyle.Exclamation, AcceptButton)
            Case 31
                MsgBox("没有关联的应用程序，请检查文件的扩展名！", MsgBoxStyle.Exclamation, AcceptButton)
            Case 28
                MsgBox("打开文件超时，请检查网络连接！", MsgBoxStyle.Exclamation, AcceptButton)

        End Select

    End Sub

    Private Sub MoveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveFile.Click

        Dim folder As New FolderBrowserDialog
        folder.Description = "请选择目标文件夹......"
        folder.ShowNewFolderButton = True


    End Sub

    Private Sub moveFile(ByVal currentFolder As String, ByVal moveToFolder As String)

    End Sub

    Private Sub btnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScan.Click
        Dim folder As New FolderBrowserDialog
        folder.Description = "请选择开始整理的文件夹根目录..."
        folder.ShowNewFolderButton = False
        'folder.SelectedPath = "\\adserver\技术服务部文件"
        folder.ShowDialog()
        Dim fileName As String = folder.SelectedPath
        DocInfo.ForeColor = Color.Black
        If fileName <> "" Then
            intTotalFile = 0
            Dim PGBFileScanned As New Progress
            PGBFileScanned.Show()
            Application.DoEvents()
            countFiles(fileName)
            With PGBFileScanned
                .LblCountFiles.Text = "0/" & intTotalFile.ToString
                .PGBFile.Maximum = intTotalFile
            End With
            scannedIntoDatabase(fileName, PGBFileScanned)

        End If



    End Sub

    Private Sub countFiles(ByVal folderPath As String)
        Dim s As DirectorySecurity = New DirectorySecurity(folderPath, AccessControlSections.Access)
        If Not (s.AreAccessRulesProtected) Then

            intTotalFile += Directory.GetFiles(folderPath).Length
            If Directory.GetDirectories(folderPath).Length > 0 Then
                Dim subDirectories() As String = Directory.GetDirectories(folderPath)

                For Each subDirectory As String In subDirectories
                    countFiles(subDirectory)
                Next

            End If


        End If
    End Sub

    Private Sub scannedIntoDatabase(ByVal folderPath As String, ByVal progressBar As Progress)

        Dim s As DirectorySecurity = New DirectorySecurity(folderPath, AccessControlSections.Access)


        If Not (s.AreAccessRulesProtected) Then

            If Directory.GetFiles(folderPath).Length > 0 Then


                Dim fileNames As String() = Directory.GetFiles(folderPath)
                For Each Me.fullName In fileNames

                    Dim filePath As String = ""
                    Dim fileName As String = ""

                    fileName = Path.GetFileName(fullName)
                    filePath = Path.GetDirectoryName(fullName)
                    '写入数据库

                    progressBar.ScanedOneFile()
                    progressBar.LblFileName.Text = fullName
                    Application.DoEvents()

                    'DocInfo.Text = "文件名:   " & fileName & vbCrLf & "文件夹:   " & filePath & vbLf
                    'Do While nextKeyPressed
                    '    Application.DoEvents()

                    'Loop
                    'nextKeyPressed = True
                    'If thisClose Then
                    '    Me.Dispose()
                    '    End
                    'End If

                Next

            End If

            '递归调用，处理当前文件夹下子文件夹
            If Directory.GetDirectories(folderPath).Length > 0 Then
                Dim subDirectories() As String = Directory.GetDirectories(folderPath)

                For Each subDirectory As String In subDirectories
                    scannedIntoDatabase(subDirectory, progressBar)
                Next

            End If


        End If

    End Sub

End Class
