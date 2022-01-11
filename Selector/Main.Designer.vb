<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.LBL_ReceptorDesc = New System.Windows.Forms.Label()
        Me.LBL_EmisorDesc = New System.Windows.Forms.Label()
        Me.BTN_Receptor = New System.Windows.Forms.Button()
        Me.BTN_Emisor = New System.Windows.Forms.Button()
        Me.LBL_Title = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LBL_ReceptorDesc
        '
        Me.LBL_ReceptorDesc.Location = New System.Drawing.Point(241, 127)
        Me.LBL_ReceptorDesc.Name = "LBL_ReceptorDesc"
        Me.LBL_ReceptorDesc.Size = New System.Drawing.Size(147, 40)
        Me.LBL_ReceptorDesc.TabIndex = 9
        Me.LBL_ReceptorDesc.Text = "Recibe ficheros del Emisor"
        Me.LBL_ReceptorDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LBL_EmisorDesc
        '
        Me.LBL_EmisorDesc.Location = New System.Drawing.Point(30, 127)
        Me.LBL_EmisorDesc.Name = "LBL_EmisorDesc"
        Me.LBL_EmisorDesc.Size = New System.Drawing.Size(147, 40)
        Me.LBL_EmisorDesc.TabIndex = 8
        Me.LBL_EmisorDesc.Text = "Envía ficheros al Receptor"
        Me.LBL_EmisorDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'BTN_Receptor
        '
        Me.BTN_Receptor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Receptor.Location = New System.Drawing.Point(238, 76)
        Me.BTN_Receptor.Name = "BTN_Receptor"
        Me.BTN_Receptor.Size = New System.Drawing.Size(150, 48)
        Me.BTN_Receptor.TabIndex = 7
        Me.BTN_Receptor.Text = "Receptor"
        Me.BTN_Receptor.UseVisualStyleBackColor = True
        '
        'BTN_Emisor
        '
        Me.BTN_Emisor.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTN_Emisor.Location = New System.Drawing.Point(27, 76)
        Me.BTN_Emisor.Name = "BTN_Emisor"
        Me.BTN_Emisor.Size = New System.Drawing.Size(150, 48)
        Me.BTN_Emisor.TabIndex = 6
        Me.BTN_Emisor.Text = "Emisor"
        Me.BTN_Emisor.UseVisualStyleBackColor = True
        '
        'LBL_Title
        '
        Me.LBL_Title.AutoSize = True
        Me.LBL_Title.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBL_Title.Location = New System.Drawing.Point(12, 9)
        Me.LBL_Title.Name = "LBL_Title"
        Me.LBL_Title.Size = New System.Drawing.Size(294, 31)
        Me.LBL_Title.TabIndex = 5
        Me.LBL_Title.Text = "Seleccione una función"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 191)
        Me.Controls.Add(Me.LBL_ReceptorDesc)
        Me.Controls.Add(Me.LBL_EmisorDesc)
        Me.Controls.Add(Me.BTN_Receptor)
        Me.Controls.Add(Me.BTN_Emisor)
        Me.Controls.Add(Me.LBL_Title)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Selección de función | PassItToMe"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LBL_ReceptorDesc As Label
    Friend WithEvents LBL_EmisorDesc As Label
    Friend WithEvents BTN_Receptor As Button
    Friend WithEvents BTN_Emisor As Button
    Friend WithEvents LBL_Title As Label
End Class
