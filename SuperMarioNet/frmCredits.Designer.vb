<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCredits
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCredits))
        Me.lblTop = New System.Windows.Forms.Label
        Me.lblValue = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.tmrChangeItem = New System.Windows.Forms.Timer(Me.components)
        Me.tmrEfect = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'lblTop
        '
        Me.lblTop.BackColor = System.Drawing.Color.Black
        Me.lblTop.Location = New System.Drawing.Point(36, 244)
        Me.lblTop.Name = "lblTop"
        Me.lblTop.Size = New System.Drawing.Size(568, 23)
        Me.lblTop.TabIndex = 5
        '
        'lblValue
        '
        Me.lblValue.BackColor = System.Drawing.Color.Black
        Me.lblValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblValue.ForeColor = System.Drawing.Color.White
        Me.lblValue.Location = New System.Drawing.Point(36, 244)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(568, 23)
        Me.lblValue.TabIndex = 4
        Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.Black
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(36, 196)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(568, 23)
        Me.lblTitle.TabIndex = 3
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tmrChangeItem
        '
        Me.tmrChangeItem.Enabled = True
        Me.tmrChangeItem.Interval = 1000
        '
        'tmrEfect
        '
        '
        'frmCredits
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(640, 504)
        Me.Controls.Add(Me.lblTop)
        Me.Controls.Add(Me.lblValue)
        Me.Controls.Add(Me.lblTitle)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCredits"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Credits"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents lblTop As System.Windows.Forms.Label
    Private WithEvents lblValue As System.Windows.Forms.Label
    Private WithEvents lblTitle As System.Windows.Forms.Label
    Private WithEvents tmrChangeItem As System.Windows.Forms.Timer
    Private WithEvents tmrEfect As System.Windows.Forms.Timer
End Class
