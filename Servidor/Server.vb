Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports Microsoft.Win32
Public Class Server

    Private Sub Server_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Init()
        InitServer()
    End Sub
    Private Sub Server_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Process.Start(DIRCommons & "\Selector.exe")
        Catch
        End Try
        End
    End Sub

    Private Sub BTN_Agregar_Click(sender As Object, e As EventArgs) Handles BTN_Agregar.Click
        Dim openFile As New OpenFileDialog
        openFile.Filter = "All file types (*.*)|*.*"
        openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        openFile.Title = "Enviar fichero..."
        openFile.Multiselect = True
        If openFile.ShowDialog() = DialogResult.OK Then
            For Each item As String In openFile.FileNames
                ServerFileList.Add(item)
                ListBox1.Items.Add(item)
            Next
        End If
    End Sub
    Private Sub BTN_Quitar_Click(sender As Object, e As EventArgs) Handles BTN_Quitar.Click
        ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        ServerFileList.RemoveAt(ListBox1.SelectedIndex)
    End Sub

    Private Sub BTN_Enviar_Click(sender As Object, e As EventArgs) Handles BTN_Enviar.Click
        'enviar uno (seleccionado)
        ENVIAR("[PRESEND]")
    End Sub
    Private Sub BTN_EnviarTodo_Click(sender As Object, e As EventArgs) Handles BTN_EnviarTodo.Click
        'enviar todos (todos los de la lista)
        BTN_EnviarTodo.Enabled = False
        ListBox1.SelectedIndex = 0
        ENVIAR("[MULTI]")
        AskForActions = False
        ThreadEnvios = New Threading.Thread(AddressOf EnvioCola)
        ThreadEnvios.Start()
    End Sub

    Private Sub BTN_Limpiar_Click(sender As Object, e As EventArgs) Handles BTN_Limpiar.Click
        ListBox1.Items.Clear()
        ServerFileList.Clear()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        ActualFileItem = ServerFileList(ListBox1.SelectedIndex)
    End Sub
End Class
Module General
    Public DIRCommons As String = "C:\Users\" & Environment.UserName & "\AppData\Local\CRZ_Labs\PassItToMe"

    Public ConfigRegedit As RegistryKey
    Public ConfigRegeditPath As String = "SOFTWARE\\CRZ Labs\\PassItToMe"

    Public ClientIP As String = "localhost"
    Public ClientPort As Integer = 21110
    Public ClientChatPort As Integer = 21111
    Public AskForIncoming As Boolean = True
    Public DefaultSavePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

    Public ServerIP As String = "localhost"
    Public ServerPort As Integer = 21110
    Public ServerChatPort As Integer = 21111

    Public ServerFileList As New ArrayList
    Dim TAMAÑOBUFFER As Integer = 1024
    Public ActualFileItem As String
    Public AskForActions As Boolean = True

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
            Console.WriteLine("[AddToLog@General(Server)]Error: " & ex.Message)
        End Try
    End Sub
    Sub SaveConfig()
        Try
            ConfigRegedit.SetValue("ClientIP", ClientIP, RegistryValueKind.String)
            ConfigRegedit.SetValue("ClientPort", ClientPort, RegistryValueKind.String)
            ConfigRegedit.SetValue("ClientChatPort", ClientChatPort, RegistryValueKind.String)
            ConfigRegedit.SetValue("ServerIP", ServerIP, RegistryValueKind.String)
            ConfigRegedit.SetValue("ServerPort", ServerPort, RegistryValueKind.String)
            ConfigRegedit.SetValue("ServerChatPort", ServerChatPort, RegistryValueKind.String)
            ConfigRegedit.SetValue("DefaultSavePath", DefaultSavePath, RegistryValueKind.String)
            LoadConfig()
        Catch ex As Exception
            AddToLog("SaveConfig@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub LoadConfig()
        Try
            ClientIP = ConfigRegedit.GetValue("ClientIP")
            ClientPort = ConfigRegedit.GetValue("ClientPort")
            ClientChatPort = ConfigRegedit.GetValue("ClientChatPort")
            ServerIP = ConfigRegedit.GetValue("ServerIP")
            ServerPort = ConfigRegedit.GetValue("ServerPort")
            ServerChatPort = ConfigRegedit.GetValue("ServerChatPort")
            DefaultSavePath = ConfigRegedit.GetValue("DefaultSavePath")
        Catch ex As Exception
            AddToLog("LoadConfig@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub Init()
        Try
            CommonActions()
            CheckIfAlreadyExist()
        Catch ex As Exception
            AddToLog("Init@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub InitServer()
        Try
            Dim INserverIP = InputBox("Ingrese la direccion del servidor de comandos", "Direccion IP Cliente", ServerIP)
            Dim INserverPort = InputBox("Ingrese el puerto del servidor de comandos", "Puerto Cliente", ServerChatPort)
            If INserverIP <> Nothing Then
                If INserverPort <> Nothing Then
                    ServerIP = INserverIP
                    ServerChatPort = INserverPort
                    SaveConfig()
                Else
                    Server.Close()
                End If
            Else
                Server.Close()
            End If
            Starter()
        Catch ex As Exception
            AddToLog("InitServer@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub Starter()
        Try
            InitChatClient()
        Catch ex As Exception
            AddToLog("Starter@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub CheckIfAlreadyExist()
        Try
            ConfigRegedit = Registry.CurrentUser.OpenSubKey(ConfigRegeditPath, True)
            If ConfigRegedit Is Nothing Then
                Registry.CurrentUser.CreateSubKey(ConfigRegeditPath)
                ConfigRegedit = Registry.CurrentUser.OpenSubKey(ConfigRegeditPath, True)
                SaveConfig()
            Else
                LoadConfig()
            End If
        Catch ex As Exception
            AddToLog("CheckIfAlreadyExist@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub CommonActions()
        Try
            If Not My.Computer.FileSystem.DirectoryExists(DIRCommons) Then
                My.Computer.FileSystem.CreateDirectory(DIRCommons)
            End If
            If Not My.Computer.FileSystem.FileExists(DIRCommons & "\Activity.log") Then
                My.Computer.FileSystem.WriteAllText(DIRCommons & "\Activity.log", Nothing, False)
            Else
                My.Computer.FileSystem.WriteAllText(DIRCommons & "\Activity.log", vbCrLf & vbCrLf & DateTime.Now.ToString("hh:mm:ss tt dd/MM/yyyy") & " Servidor Iniciado!", True)
            End If
        Catch ex As Exception
            AddToLog("CommonActions@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub EnviarFichero(ByVal filePath As String)
        Try
            Dim CLIENTE As New TcpClient(ServerIP, ServerPort)
            Dim NS As NetworkStream = CLIENTE.GetStream
            Dim FS As New FileStream(filePath, FileMode.Open, FileAccess.Read)
            Dim PAQUETES As Integer = CInt(Math.Ceiling(CDbl(FS.Length) / CDbl(TAMAÑOBUFFER)))
            Dim LONGITUDTOTAL As Integer = CInt(FS.Length)
            Dim LONGITUDPAQUETEACTUAL As Integer = 0
            Dim CONTADOR As Integer = 0
            For I As Integer = 0 To PAQUETES - 1
                If LONGITUDTOTAL > TAMAÑOBUFFER Then
                    LONGITUDPAQUETEACTUAL = TAMAÑOBUFFER
                    LONGITUDTOTAL = LONGITUDTOTAL - LONGITUDPAQUETEACTUAL
                Else
                    LONGITUDPAQUETEACTUAL = LONGITUDTOTAL
                End If
                Dim ENVIARBUFFER As Byte() = New Byte(LONGITUDPAQUETEACTUAL - 1) {}
                FS.Read(ENVIARBUFFER, 0, LONGITUDPAQUETEACTUAL)
                NS.Write(ENVIARBUFFER, 0, CInt(ENVIARBUFFER.Length))
            Next
            FS.Close()
            NS.Close()
            CLIENTE.Close()
            If AskForActions Then
                MsgBox("Enviado correctamente", MsgBoxStyle.Information, "Sended!")
            End If
        Catch ex As Exception
            AddToLog("EnviarFichero@General(Server)", "Error: " & ex.Message, True)
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error sending...")
        End Try
    End Sub
    Public ClienteMULTIREADY As Boolean = False
    Public ClienteREADY As Boolean = False
    Public ClienteOKAY As Boolean = False
    Dim SwitchPreparedClient As Boolean = True
    Public ThreadEnvios As Threading.Thread
    Dim indice As Integer = 0
    Sub EnvioCola()
        Try
            'verifica que puede enviar el fichero actual
            While indice < ServerFileList.Count
                If (indice + 1) > ServerFileList.Count Then
                    Exit While
                End If
                While ClienteMULTIREADY 'Activa al recibir [MULTIREADY]
                    If (indice + 1) > ServerFileList.Count Then
                        Exit While
                    End If
                    If SwitchPreparedClient Then
                        ENVIAR("[PRESEND]")
                        SwitchPreparedClient = False
                    End If
                    While ClienteREADY 'Activa al recibir [READY]
                        ActualFileItem = ServerFileList(indice)
                        While ClienteOKAY 'Activa al recibir [OKAY]
                            ENVIAR("[PRESEND]")
                            ClienteOKAY = False
                            ClienteREADY = False
                            SwitchPreparedClient = True
                            If (indice + 1) > ServerFileList.Count Then
                                Exit While
                            End If
                            indice += 1
                        End While
                    End While
                End While
            End While
            Server.BTN_EnviarTodo.Enabled = True
            ClienteMULTIREADY = False
            ClienteREADY = False
            ClienteOKAY = False
            SwitchPreparedClient = False
            ENVIAR("[MULTIEND]")
            ThreadEnvios.Abort()
        Catch ex As Exception
            AddToLog("EnvioCola@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub

#Region "TCP Chat Client"
    Dim CLIENTE As TcpClient
    Dim NS As NetworkStream
    Dim WithEvents TimerLeer As New Windows.Forms.Timer

    Sub InitChatClient()
        Try
            AddHandler TimerLeer.Tick, AddressOf LEER
            CLIENTE = New TcpClient
            CLIENTE.Connect(ServerIP, ServerChatPort)
            NS = CLIENTE.GetStream()
            TimerLeer.Interval = 150
            TimerLeer.Start()
        Catch ex As Exception
            AddToLog("Init@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub ENVIAR(ByVal MENSAJE As String)
        Try
            Dim MIBUFFER() As Byte = Encoding.ASCII.GetBytes(MENSAJE)
            NS.Write(MIBUFFER, 0, MIBUFFER.Length)
            AddToLog("Server TCP Chat>", MENSAJE, False)
        Catch ex As Exception
            AddToLog("ENVIAR@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub LEER()
        Try
            Dim MISBYTES() As Byte = New Byte(1024) {} 'PARA LA RECEPCION DE BYTES
            Dim VACIO As Boolean = True ' POR SI HAY UN ENVIO DE CADENAS VACIAS.
            If NS.DataAvailable Then
                NS.Read(MISBYTES, 0, MISBYTES.Length)
                For I = 0 To MISBYTES.Length - 1 ' POR SI HAY UN ENVIO DE CADENAS VACIAS.
                    If MISBYTES(I) <> 0 Then
                        VACIO = False
                        Exit For
                    End If
                Next
                If VACIO = False Then
                    Dim MENSAJE As String = Encoding.ASCII.GetString(MISBYTES).Replace(vbNullChar, Nothing) 'CONVERSION DE BYTES A STRING
                    AddToLog("Server TCP Chat<", MENSAJE, False)
                    If MENSAJE.StartsWith("[READY]") Then '[READY] Listo para el envio
                        ClienteREADY = True
                        ENVIAR("[FILE]>" & ActualFileItem & "|" & IO.Path.GetExtension(ActualFileItem) & "|" & FileLen(ActualFileItem) & "|" & Environment.UserName)
                        EnviarFichero(ActualFileItem)
                    ElseIf MENSAJE.StartsWith("[OKAY]") Then '[OKAY] Envio correcto
                        ClienteOKAY = True
                        If AskForActions Then
                            MsgBox("Fichero enviado y recibido correctamente", MsgBoxStyle.Information, "Confirmacion")
                        End If
                    ElseIf MENSAJE.StartsWith("[ERROR]") Then '[ERROR] Error al recibir
                        If AskForActions Then
                            If MessageBox.Show("El envio fallo." & vbCrLf & MENSAJE & vbCrLf & "¿Reenviar?", "Error de envio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                Exit Sub
                            End If
                        End If
                        ENVIAR("[PRESEND]")
                    ElseIf MENSAJE.StartsWith("[REJECTED]") Then '[REJECTED] Recibo rechazado
                        If AskForActions Then
                            If MessageBox.Show("El envio del fichero fue rechazado por el cliente." & vbCrLf & "¿Reenviar?", "Envio rechazado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                                Exit Sub
                            End If
                        End If
                        ENVIAR("[PRESEND]")
                    ElseIf MENSAJE.StartsWith("[MULTIREADY]") Then '[MULTIREADY] Listo para ficheros multiples
                        ClienteMULTIREADY = True
                    ElseIf MENSAJE.StartsWith("[BYE]") Then '[BYE] Conexion terminada
                        MsgBox("Se ha cerrado la conexion.", MsgBoxStyle.Critical, "Conexion finalizada")
                        Server.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            AddToLog("LEER@General(Server)", "Error: " & ex.Message, True)
        End Try
    End Sub
#End Region
End Module