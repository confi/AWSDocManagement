<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Progress
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Progress))
        Me.PGBFile = New System.Windows.Forms.ProgressBar
        Me.LblFileName = New System.Windows.Forms.Label
        Me.LblCountFiles = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'PGBFile
        '
        Me.PGBFile.Location = New System.Drawing.Point(22, 79)
        Me.PGBFile.Name = "PGBFile"
        Me.PGBFile.Size = New System.Drawing.Size(250, 23)
        Me.PGBFile.TabIndex = 0
        Me.PGBFile.Tag = ""
        '
        'LblFileName
        '
        Me.LblFileName.AutoSize = True
        Me.LblFileName.Location = New System.Drawing.Point(22, 17)
        Me.LblFileName.MaximumSize = New System.Drawing.Size(230, 50)
        Me.LblFileName.Name = "LblFileName"
        Me.LblFileName.Size = New System.Drawing.Size(42, 19)
        Me.LblFileName.TabIndex = 1
        Me.LblFileName.Text = "文件名"
        Me.LblFileName.UseCompatibleTextRendering = True
        '
        'LblCountFiles
        '
        Me.LblCountFiles.AutoSize = True
        Me.LblCountFiles.Location = New System.Drawing.Point(201, 115)
        Me.LblCountFiles.MinimumSize = New System.Drawing.Size(72, 12)
        Me.LblCountFiles.Name = "LblCountFiles"
        Me.LblCountFiles.Size = New System.Drawing.Size(72, 12)
        Me.LblCountFiles.TabIndex = 2
        Me.LblCountFiles.Text = "/"
        '
        'Progress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 144)
        Me.Controls.Add(Me.LblCountFiles)
        Me.Controls.Add(Me.LblFileName)
        Me.Controls.Add(Me.PGBFile)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Progress"
        Me.Text = "扫描进度"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PGBFile As System.Windows.Forms.ProgressBar
    Friend WithEvents LblCountFiles As System.Windows.Forms.Label
    Friend WithEvents LblFileName As System.Windows.Forms.Label
End Class
