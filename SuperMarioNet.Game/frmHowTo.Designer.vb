<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHowTo
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHowTo))
        Me.lblLeyend = New System.Windows.Forms.Label
        Me.btnRepeat = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'lblLeyend
        '
        Me.lblLeyend.AutoSize = True
        Me.lblLeyend.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeyend.ForeColor = System.Drawing.Color.White
        Me.lblLeyend.Location = New System.Drawing.Point(270, 142)
        Me.lblLeyend.Name = "lblLeyend"
        Me.lblLeyend.Size = New System.Drawing.Size(107, 16)
        Me.lblLeyend.TabIndex = 8
        Me.lblLeyend.Text = "End of tutorial."
        Me.lblLeyend.Visible = False
        '
        'btnRepeat
        '
        Me.btnRepeat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnRepeat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnRepeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRepeat.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRepeat.ForeColor = System.Drawing.Color.White
        Me.btnRepeat.Location = New System.Drawing.Point(340, 332)
        Me.btnRepeat.Name = "btnRepeat"
        Me.btnRepeat.Size = New System.Drawing.Size(100, 30)
        Me.btnRepeat.TabIndex = 7
        Me.btnRepeat.Text = "Repeat"
        Me.btnRepeat.UseVisualStyleBackColor = True
        Me.btnRepeat.Visible = False
        '
        'btnBack
        '
        Me.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.White
        Me.btnBack.Location = New System.Drawing.Point(200, 332)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(100, 30)
        Me.btnBack.TabIndex = 6
        Me.btnBack.Text = "Return"
        Me.btnBack.UseVisualStyleBackColor = True
        Me.btnBack.Visible = False
        '
        'tmrTick
        '
        Me.tmrTick.Enabled = True
        Me.tmrTick.Interval = 1000
        '
        'frmHowTo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(640, 504)
        Me.Controls.Add(Me.lblLeyend)
        Me.Controls.Add(Me.btnRepeat)
        Me.Controls.Add(Me.btnBack)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHowTo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "HowTo Play"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents lblLeyend As System.Windows.Forms.Label
    Private WithEvents btnRepeat As System.Windows.Forms.Button
    Private WithEvents btnBack As System.Windows.Forms.Button
    Private WithEvents tmrTick As System.Windows.Forms.Timer
End Class
