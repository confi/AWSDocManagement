Public Class LANConnect
    Public Function ping(ByVal remoteHost As String) As Boolean
        Dim flag As Boolean = False
        Dim proc As New Process
        Try
            With proc.StartInfo
                .FileName = "cmd.exe"
                .RedirectStandardInput = True
                .RedirectStandardOutput = True
                .RedirectStandardError = True
                .CreateNoWindow = True
            End With
            proc.Start()
            Dim dosLine As String = "ping -n 1" + remoteHost
            With proc
                Do While Not (.HasExited)
                    .WaitForExit(500)
                Loop

                Dim pingResult As String = .StandardOutput.ReadToEnd
                If (pingResult.IndexOf("(0% loss)") <> -1) Then
                    flag = True
                End If
                .StandardOutput.Close()

            End With
        Catch ex As Exception

        Finally
            With proc
                .Close()
                .Dispose()
            End With
        End Try
        Return flag
    End Function

    Public Function connect(ByVal remoteHost As String, ByVal userName As String, ByVal passWord As String)
        Dim flag As Boolean = False
        Dim proc As New Process
        Try
            With proc.StartInfo
                .FileName = "cmd.exe"
                .RedirectStandardInput = True
                .RedirectStandardOutput = True
                .RedirectStandardError = True
                .CreateNoWindow = True
            End With

            proc.Start()
            Dim dosLine As String = "net use //" + remoteHost + " " + _
            passWord + " " + "/user:" + userName + ">NUL"
            With proc
                .StandardInput.WriteLine(dosLine)
                .StandardInput.WriteLine("exit")
                Do While Not (.HasExited)
                    .WaitForExit(1000)
                Loop
                Dim errorMsg As String = .StandardError.ReadToEnd()
                .StandardError.Close()
                If (String.IsNullOrEmpty(errorMsg)) Then
                    flag = True
                End If
            End With
        Catch ex As Exception
        Finally
            With proc
                .Close()
                .Dispose()
            End With
        End Try
        Return flag
    End Function

End Class
