Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports Microsoft.Win32
Public Class Client

    Private Sub Client_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Init()
        InitClient()
    End Sub
    Private Sub Client_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            ENVIARTODOS("[BYE]")
            CERRARTODO()
            Process.Start(DIRCommons & "\Selector.exe")
        Catch
        End Try
        End
    End Sub

    Sub IndexFilesToList()
        Try
            ListBox1.Items.Clear()
            For Each item As String In ClientFileListInfo
                ListBox1.Items.Add(IO.Path.GetFileName(item.Split("|")(0)))
                ListBox1.TopIndex = ListBox1.Items.Count - 1
            Next
        Catch ex As Exception
            AddToLog("IndexFilesToList@Client", "Error: " & ex.Message, True)
        End Try
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim fileInfo As String() = ClientFileListInfo(ListBox1.SelectedIndex).ToString.Split("|")
        RichTextBox1.Text = "File name: " & IO.Path.GetFileName(fileInfo(0)) &
            vbCrLf & "Remote path: " & fileInfo(0) &
            vbCrLf & "Local path: " & DefaultSavePath & "\" & IO.Path.GetFileName(fileInfo(0)) &
            vbCrLf & "File format: " & fileInfo(1) &
            vbCrLf & "File size: " & fileInfo(2) &
            vbCrLf & "User: " & fileInfo(3)
    End Sub
    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        Process.Start(DefaultSavePath & "\" & IO.Path.GetFileName(ClientFileListInfo(ListBox1.SelectedIndex).ToString.Split("|")(0)))
    End Sub

#Region "General"
    Public DIRCommons As String = "C:\Users\" & Environment.UserName & "\AppData\Local\CRZ_Labs\PassItToMe"

    Public ConfigRegedit As RegistryKey
    Public ConfigRegeditPath As String = "SOFTWARE\\CRZ Labs\\PassItToMe"

    Public ClientIP As String = "localhost"
    Public ClientPort As Integer = 21110
    Public ClientChatPort As Integer = 21111
    Public DontAskForIncoming As Boolean = True
    Public DefaultSavePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

    Public ServerIP As String = "localhost"
    Public ServerPort As Integer = 21110
    Public ServerChatPort As Integer = 21111

    Public ClientFileListInfo As New ArrayList

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
            Console.WriteLine("[AddToLog@General(Client)]Error: " & ex.Message)
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
        Catch ex As Exception
            AddToLog("SaveConfig@General(Client)", "Error: " & ex.Message, True)
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
            AddToLog("LoadConfig@General(Client)", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub Init()
        Try
            CommonActions()
            CheckIfAlreadyExist()
        Catch ex As Exception
            AddToLog("Init@General(Client)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub InitClient()
        Try
            Starter()
        Catch ex As Exception
            AddToLog("InitClient@General(Client)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub Starter()
        Try
            InitChatServer()
        Catch ex As Exception
            AddToLog("Starter@General(Client)", "Error: " & ex.Message, True)
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
            AddToLog("CheckIfAlreadyExist@General(Client)", "Error: " & ex.Message, True)
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
                My.Computer.FileSystem.WriteAllText(DIRCommons & "\Activity.log", vbCrLf & vbCrLf & DateTime.Now.ToString("hh:mm:ss tt dd/MM/yyyy") & " Cliente Iniciado!", True)
            End If
        Catch ex As Exception
            AddToLog("CommonActions@General(Client)", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub RecibirFichero(ByVal fileInfo As String)
        Try
            Dim CLIENTE_TCP As TcpClient
            Dim fileArgs As String() = fileInfo.Split("|")
            Dim filePath As String = fileArgs(0)
            Dim fileName As String = IO.Path.GetFileName(filePath)
            Dim fileSize As String = fileArgs(1)
            Dim fileFormat As String = fileArgs(2)
            Dim fileOthers As String = fileArgs(3)
            '0 = Nombre
            '1 = Formato
            '2 = Tamaño
            '3 = Usuario
            Dim fileInfoMessage As String = "File name: " & fileName &
            vbCrLf & "File format: " & fileFormat &
            vbCrLf & "File size: " & fileSize &
            vbCrLf & "User: " & fileArgs(3)

            ClientFileListInfo.Add(fileInfo)

            IndexFilesToList()

            Dim TAMAÑOBUFFER As Integer = 1024
            Dim ARCHIVORECIBIDO As Byte() = New Byte(TAMAÑOBUFFER - 1) {}
            Dim BYTESRECIBIDOS As Integer
            Dim FIN As Integer = 0
            Dim SaveFile As New SaveFileDialog
            SaveFile.Title = "Guardar archivo entrante..."
            SaveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            SaveFile.FileName = IO.Path.GetFileName(fileArgs(0))
            Dim SERVIDOR_TCP As New TcpListener(IPAddress.Any, ClientPort)
            SERVIDOR_TCP.Start()
            While FIN = 0
                Dim NS As NetworkStream = Nothing
                Dim RESULTADO As DialogResult
                If SERVIDOR_TCP.Pending Then
                    CLIENTE_TCP = SERVIDOR_TCP.AcceptTcpClient
                    NS = CLIENTE_TCP.GetStream
                    If Not DontAskForIncoming Then
                        RESULTADO = MessageBox.Show("Fichero entrante..." & vbCrLf & vbCrLf & fileInfoMessage & vbCrLf & vbCrLf & "¿Recibirlo?", "Archivo entrante", MessageBoxButtons.YesNo)
                        If RESULTADO = Windows.Forms.DialogResult.No Then
                            ENVIARTODOS("[REJECTED]")
                            Exit Sub
                        End If
                    End If
                    Dim FICHERORECIBIDO As String = Nothing
                    FICHERORECIBIDO = DefaultSavePath & "\" & fileName
                    If FICHERORECIBIDO <> String.Empty Then
                        Dim TOTALBYTESRECIBIDOS As Integer = 0
                        Dim FS As New FileStream(FICHERORECIBIDO, FileMode.OpenOrCreate, FileAccess.Write)
                        While (AYUDAENLINEA(BYTESRECIBIDOS, NS.Read(ARCHIVORECIBIDO, 0, ARCHIVORECIBIDO.Length))) > 0
                            FS.Write(ARCHIVORECIBIDO, 0, BYTESRECIBIDOS)
                            TOTALBYTESRECIBIDOS = TOTALBYTESRECIBIDOS + BYTESRECIBIDOS
                        End While
                        FS.Close()
                    End If
                    NS.Close()
                    CLIENTE_TCP.Close()
                    If Not DontAskForIncoming Then
                        MsgBox("Archivo recibido correctamente!", MsgBoxStyle.OkOnly, "Archivo entrante")
                    End If
                    FIN = 1
                    ENVIARTODOS("[OKAY]")
                End If
            End While
            SERVIDOR_TCP.Stop()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error receiving...")
            AddToLog("RecibirFichero@Cliente", "Error: " & ex.Message, True)
            ENVIARTODOS("[ERROR]" & ex.Message)
        End Try
    End Sub
    Function AYUDAENLINEA(Of T)(ByRef OBJETIVO As T, VALOR As T)
        OBJETIVO = VALOR
        Return VALOR
    End Function

#Region "TCP Chat Server"
    Dim SERVIDOR As TcpListener
    Dim THREADSERVIDOR As Thread
    Dim CLIENTES As New Hashtable
    Dim CLIENTEIP As IPEndPoint
    Private Structure NUEVOCLIENTE
        Public SOCKETCLIENTE As Socket
        Public THREADCLIENTE As Thread
        Public MENSAJE As String
    End Structure

    Sub InitChatServer()
        Try
            SERVIDOR = New TcpListener(IPAddress.Any, ClientChatPort)
            SERVIDOR.Start()
            THREADSERVIDOR = New Thread(AddressOf Escuchar)
            THREADSERVIDOR.Start()
        Catch ex As Exception
            AddToLog("Init@General(Client)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub Escuchar()
        Try
            Dim CLIENTE As New NUEVOCLIENTE
            While True
                CLIENTE.SOCKETCLIENTE = SERVIDOR.AcceptSocket
                CLIENTEIP = CLIENTE.SOCKETCLIENTE.RemoteEndPoint
                CLIENTE.THREADCLIENTE = New Thread(AddressOf Leer)
                CLIENTES.Add(CLIENTEIP, CLIENTE)
                CLIENTE.THREADCLIENTE.Start()
            End While
        Catch ex As Exception
            AddToLog("Escuchar@General(Client)", "Error: " & ex.Message, True)
        End Try
    End Sub
    Sub Leer()
        Try
            Dim CLIENTE As New NUEVOCLIENTE
            Dim DATOS() As Byte
            Dim IP As IPEndPoint = CLIENTEIP
            CLIENTE = CLIENTES(IP)
            While True
                If CLIENTE.SOCKETCLIENTE.Connected Then
                    DATOS = New Byte(1024) {}
                    If CLIENTE.SOCKETCLIENTE.Receive(DATOS, DATOS.Length, 0) > 0 Then
                        CLIENTE.MENSAJE = Encoding.ASCII.GetString(DATOS)
                        CLIENTES(IP) = CLIENTE
                        ChatReader(IP)
                    Else
                        Exit While
                    End If
                End If
            End While
            CERRARTHREAD(IP)
        Catch ex As Exception
            AddToLog("Leer@General(Client)", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub ChatReader(ByVal IDTerminal As IPEndPoint)
        Try
            Dim MENSAJE As String = OBTENERDATOS(IDTerminal).Replace(vbNullChar, Nothing)
            AddToLog("Client TCP Chat<", MENSAJE, False)
            If MENSAJE.StartsWith("[PRESEND]") Then '[PRESEND] Preparacion para recibir
                ENVIARTODOS("[READY]")
            ElseIf MENSAJE.StartsWith("[FILE]") Then '[FILE] Informacion del fichero
                Dim myMsg As String() = MENSAJE.Split(">")
                RecibirFichero(myMsg(1))
            ElseIf MENSAJE.StartsWith("[MULTI]") Then '[MULTI] Preparacion para multiples ficheros
                CheckBox1.Enabled = False
                ClientFileListInfo.Clear()
                ListBox1.Items.Clear()
                DontAskForIncoming = True
                ENVIARTODOS("[MULTIREADY]")
            ElseIf MENSAJE.StartsWith("[MULTIEND]") Then '[MULTIEND] Fin de multiples ficheros
                CheckBox1.Enabled = True
                ListBox1.Items.RemoveAt(0)
                ClientFileListInfo.RemoveAt(0)
                Process.Start(DefaultSavePath)
            ElseIf MENSAJE.StartsWith("[OPEN]") Then '[OPEN] Abrir fichero
                Dim openArgs As String() = MENSAJE.Split(">")
                Dim openCanDoIt As Boolean = False
                Dim localFile As String = DefaultSavePath & "\" & IO.Path.GetFileName(ClientFileListInfo(openArgs(1)).ToString.Split("|")(0))
                If Not DontAskForIncoming Then
                    If MessageBox.Show("El equipo remoto quiere inciar el fichero '" & IO.Path.GetFileName(localFile) & "'" & vbCrLf & "¿Desea abrirlo?", "Confirmar abrir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        openCanDoIt = True
                    End If
                Else
                    openCanDoIt = True
                End If
                If openCanDoIt Then
                    Process.Start(localFile)
                End If
            ElseIf MENSAJE.StartsWith("[DELETE]") Then '[DELETE] Eliminar fichero
                Dim deleteArgs As String() = MENSAJE.Split(">")
                Dim deleteCanDoIt As Boolean = False
                Dim localFile As String = DefaultSavePath & "\" & IO.Path.GetFileName(ClientFileListInfo(deleteArgs(1)).ToString.Split("|")(0))
                If Not DontAskForIncoming Then
                    If MessageBox.Show("El equipo remoto quiere eliminar el fichero '" & IO.Path.GetFileName(localFile) & "'" & vbCrLf & "¿Desea eliminarlo?", "Confirmar eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        deleteCanDoIt = True
                    End If
                Else
                    deleteCanDoIt = True
                End If
                If deleteCanDoIt Then
                    If My.Computer.FileSystem.FileExists(localFile) Then
                        My.Computer.FileSystem.DeleteFile(localFile)
                    End If
                End If
            ElseIf MENSAJE.StartsWith("[BYE]") Then '[BYE] Conexion terminada
                MsgBox("Se ha cerrado la conexión.", MsgBoxStyle.Critical, "Conexión finalizada")
                Close()
            Else
                ENVIARTODOS(MENSAJE)
            End If
        Catch ex As Exception
            AddToLog("ChatReader@General(Client)", "Error: " & ex.Message, True)
        End Try
    End Sub

    Sub ENVIARUNO(ByVal IDCliente As IPEndPoint, ByVal Datos As String)
        Dim Cliente As NUEVOCLIENTE
        Cliente = CLIENTES(IDCliente)
        Cliente.SOCKETCLIENTE.Send(Encoding.ASCII.GetBytes(Datos))
        AddToLog("Client TCP Chat>", Datos, False)
    End Sub
    Sub ENVIARTODOS(ByVal Datos As String)
        Dim CLIENTE As NUEVOCLIENTE
        For Each CLIENTE In CLIENTES.Values
            CLIENTE.SOCKETCLIENTE.Send(Encoding.ASCII.GetBytes(Datos))
        Next
        AddToLog("Client TCP Chat>>", Datos, False)
    End Sub
    Function OBTENERDATOS(ByVal IDCliente As IPEndPoint) As String
        Dim CLIENTE As NUEVOCLIENTE
        CLIENTE = CLIENTES(IDCliente)
        Return CLIENTE.MENSAJE
    End Function
    Public Sub CERRARTHREAD(ByVal IP As IPEndPoint)
        Dim CLIENTE As NUEVOCLIENTE = CLIENTES(IP)
        Try
            CLIENTE.THREADCLIENTE.Abort()
        Catch ex As Exception
            CLIENTES.Remove(IP)
        End Try
    End Sub
    Public Sub CERRARTODO()
        Dim CLIENTE As NUEVOCLIENTE
        For Each CLIENTE In CLIENTES.Values
            CLIENTE.SOCKETCLIENTE.Close()
            CLIENTE.THREADCLIENTE.Abort()
        Next
    End Sub
#End Region
#End Region
End Class