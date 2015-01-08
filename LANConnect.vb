Imports System
Imports System.IO

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


    Public Function remoteMan(ByVal serverName As String, ByVal serverDirectory As String) As String

        Dim conn As New ConnectionOptions
        'conn.Username = "DBuser"
        'conn.Password = "DBuser"
        'conn.Authority = "ntlmdomain:WORKGROUP"
        'conn.Authentication = AuthenticationLevel.Packet
        'conn.Impersonation = ImpersonationLevel.Impersonate
        Dim ms As New Management.ManagementScope(serverName)
        'ms.Options = conn
        Dim filename As String = ""
        Try
            ms.Connect()

            Dim di As New DirectoryInfo(serverDirectory)
            Dim sb As New Text.StringBuilder
            For Each subDIR As DirectoryInfo In di.GetDirectories()
                sb.Append(subDIR.Name + vbCrLf)
            Next
            filename = sb.ToString

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return filename

    End Function
End Class
