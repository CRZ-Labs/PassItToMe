Public Class Main
    Dim DIRCommons As String = "C:\Users\" & Environment.UserName & "\AppData\Local\CRZ_Labs\PassItToMe"

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddToLog("Instance", vbCrLf & vbCrLf & "-- INICADO ---", False)
    End Sub
    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    Private Sub BTN_Emisor_Click(sender As Object, e As EventArgs) Handles BTN_Emisor.Click
        Try
            AddToLog("Instance", "Called: TakeItFromMe.exe", False)
            Process.Start(DIRCommons & "\TakeItFromMe.exe")
            Me.Close()
        Catch ex As Exception
            AddToLog("Instance", "Error: " & ex.Message, True)
        End Try
    End Sub
    Private Sub BTN_Receptor_Click(sender As Object, e As EventArgs) Handles BTN_Receptor.Click
        Try
            AddToLog("Instance", "Called: GiveItToMe.exe", False)
            Process.Start(DIRCommons & "\GiveItToMe.exe")
            Me.Close()
        Catch ex As Exception
            AddToLog("Instance", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub AddToLog(ByVal from As String, ByVal content As String, Optional ByVal flag As Boolean = False)
        Try
            Dim finalContent As String = Nothing
            If flag = True Then
                finalContent = " [!!!]"
            End If
            Dim Message As String = DateTime.Now.ToString("hh:mm:ss tt dd/MM/yyyy") & finalContent & " [" & from & "] " & content
            Console.WriteLine("[" & from & "]" & finalContent & " " & content)
            Try
                My.Computer.FileSystem.WriteAllText(DIRCommons & "\Activity.log", vbCrLf & Message, True)
            Catch
            End Try
        Catch ex As Exception
            Console.WriteLine("[AddToLog@Main(Selector)]Error: " & ex.Message)
        End Try
    End Sub
End Class