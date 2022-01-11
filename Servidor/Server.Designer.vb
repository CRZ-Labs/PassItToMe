<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Server
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Server))
        Me.BTN_Limpiar = New System.Windows.Forms.Button()
        Me.BTN_EnviarTodo = New System.Windows.Forms.Button()
        Me.BTN_Enviar = New System.Windows.Forms.Button()
        Me.BTN_Quitar = New System.Windows.Forms.Button()
        Me.BTN_Agregar = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.CMS_Options = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AbrirEnLocalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AbrirEnRemotoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.EliminarEnLocalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarEnRemotoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LBL_Title = New System.Windows.Forms.Label()
        Me.CMS_Options.SuspendLayout()
        Me.SuspendLayout()
        '
        'BTN_Limpiar
        '
        Me.BTN_Limpiar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_Limpiar.Location = New System.Drawing.Point(673, 426)
        Me.BTN_Limpiar.Name = "BTN_Limpiar"
        Me.BTN_Limpiar.Size = New System.Drawing.Size(109, 23)
        Me.BTN_Limpiar.TabIndex = 13
        Me.BTN_Limpiar.Text = "Limpiar"
        Me.BTN_Limpiar.UseVisualStyleBackColor = True
        '
        'BTN_EnviarTodo
        '
        Me.BTN_EnviarTodo.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.BTN_EnviarTodo.Location = New System.Drawing.Point(673, 298)
        Me.BTN_EnviarTodo.Name = "BTN_EnviarTodo"
        Me.BTN_EnviarTodo.Size = New System.Drawing.Size(109, 30)
        Me.BTN_EnviarTodo.TabIndex = 12
        Me.BTN_EnviarTodo.Text = "Enviar todo"
        Me.BTN_EnviarTodo.UseVisualStyleBackColor = True
        '
        'BTN_Enviar
        '
        Me.BTN_Enviar.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.BTN_Enviar.Location = New System.Drawing.Point(673, 262)
        Me.BTN_Enviar.Name = "BTN_Enviar"
        Me.BTN_Enviar.Size = New System.Drawing.Size(109, 30)
        Me.BTN_Enviar.TabIndex = 11
        Me.BTN_Enviar.Text = "Enviar"
        Me.BTN_Enviar.UseVisualStyleBackColor = True
        '
        'BTN_Quitar
        '
        Me.BTN_Quitar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_Quitar.Location = New System.Drawing.Point(673, 136)
        Me.BTN_Quitar.Name = "BTN_Quitar"
        Me.BTN_Quitar.Size = New System.Drawing.Size(109, 36)
        Me.BTN_Quitar.TabIndex = 10
        Me.BTN_Quitar.Text = "Quitar"
        Me.BTN_Quitar.UseVisualStyleBackColor = True
        '
        'BTN_Agregar
        '
        Me.BTN_Agregar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTN_Agregar.Location = New System.Drawing.Point(673, 94)
        Me.BTN_Agregar.Name = "BTN_Agregar"
        Me.BTN_Agregar.Size = New System.Drawing.Size(109, 36)
        Me.BTN_Agregar.TabIndex = 9
        Me.BTN_Agregar.Text = "Agregar"
        Me.BTN_Agregar.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.ContextMenuStrip = Me.CMS_Options
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(12, 94)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(655, 355)
        Me.ListBox1.TabIndex = 8
        '
        'CMS_Options
        '
        Me.CMS_Options.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AbrirEnLocalToolStripMenuItem, Me.AbrirEnRemotoToolStripMenuItem, Me.ToolStripMenuItem1, Me.EliminarEnLocalToolStripMenuItem, Me.EliminarEnRemotoToolStripMenuItem})
        Me.CMS_Options.Name = "CMS_Options"
        Me.CMS_Options.Size = New System.Drawing.Size(176, 98)
        '
        'AbrirEnLocalToolStripMenuItem
        '
        Me.AbrirEnLocalToolStripMenuItem.Name = "AbrirEnLocalToolStripMenuItem"
        Me.AbrirEnLocalToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.AbrirEnLocalToolStripMenuItem.Text = "Abrir en local"
        '
        'AbrirEnRemotoToolStripMenuItem
        '
        Me.AbrirEnRemotoToolStripMenuItem.Name = "AbrirEnRemotoToolStripMenuItem"
        Me.AbrirEnRemotoToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.AbrirEnRemotoToolStripMenuItem.Text = "Abrir en remoto"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(172, 6)
        '
        'EliminarEnLocalToolStripMenuItem
        '
        Me.EliminarEnLocalToolStripMenuItem.Name = "EliminarEnLocalToolStripMenuItem"
        Me.EliminarEnLocalToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.EliminarEnLocalToolStripMenuItem.Text = "Eliminar en local"
        '
        'EliminarEnRemotoToolStripMenuItem
        '
        Me.EliminarEnRemotoToolStripMenuItem.Name = "EliminarEnRemotoToolStripMenuItem"
        Me.EliminarEnRemotoToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.EliminarEnRemotoToolStripMenuItem.Text = "Eliminar en remoto"
        '
        'LBL_Title
        '
        Me.LBL_Title.AutoSize = True
        Me.LBL_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_Title.Location = New System.Drawing.Point(12, 9)
        Me.LBL_Title.Name = "LBL_Title"
        Me.LBL_Title.Size = New System.Drawing.Size(355, 31)
        Me.LBL_Title.TabIndex = 7
        Me.LBL_Title.Text = "Servidor Emisor de Ficheros"
        '
        'Server
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(794, 461)
        Me.Controls.Add(Me.BTN_Limpiar)
        Me.Controls.Add(Me.BTN_EnviarTodo)
        Me.Controls.Add(Me.BTN_Enviar)
        Me.Controls.Add(Me.BTN_Quitar)
        Me.Controls.Add(Me.BTN_Agregar)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.LBL_Title)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Server"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Servidor"
        Me.CMS_Options.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BTN_Limpiar As Button
    Friend WithEvents BTN_EnviarTodo As Button
    Friend WithEvents BTN_Enviar As Button
    Friend WithEvents BTN_Quitar As Button
    Friend WithEvents BTN_Agregar As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents LBL_Title As Label
    Friend WithEvents CMS_Options As ContextMenuStrip
    Friend WithEvents AbrirEnLocalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AbrirEnRemotoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents EliminarEnLocalToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EliminarEnRemotoToolStripMenuItem As ToolStripMenuItem
End Class
