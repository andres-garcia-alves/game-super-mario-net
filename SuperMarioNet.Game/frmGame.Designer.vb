<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGame
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGame))
        Me.tmrStart = New System.Windows.Forms.Timer(Me.components)
        Me.picKeyboard = New System.Windows.Forms.PictureBox
        CType(Me.picKeyboard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tmrStart
        '
        Me.tmrStart.Interval = 1500
        '
        'picKeyboard
        '
        Me.picKeyboard.BackColor = System.Drawing.Color.Transparent
        Me.picKeyboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.picKeyboard.Location = New System.Drawing.Point(40, 100)
        Me.picKeyboard.Name = "picKeyboard"
        Me.picKeyboard.Size = New System.Drawing.Size(270, 120)
        Me.picKeyboard.TabIndex = 0
        Me.picKeyboard.TabStop = False
        Me.picKeyboard.Visible = False
        '
        'frmGame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(640, 504)
        Me.ControlBox = False
        Me.Controls.Add(Me.picKeyboard)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(-1000, 500)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGame"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        CType(Me.picKeyboard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents tmrStart As System.Windows.Forms.Timer
    Friend WithEvents picKeyboard As System.Windows.Forms.PictureBox
End Class
