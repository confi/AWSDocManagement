Public Class Progress


    Public Sub ScanedOneFile()
        PGBFile.Value += 1
        LblCountFiles.Text = PGBFile.Value.ToString & "/" & PGBFile.Maximum.ToString
    End Sub

    

    Public Sub New()

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.LblCountFiles.Text = ""
        Me.LblFileName.Text = "正在发现文件……"
        Me.PGBFile.Value = 0

    End Sub
End Class