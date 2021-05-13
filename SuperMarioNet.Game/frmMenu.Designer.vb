<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenu))
        Me.btnOptions = New System.Windows.Forms.Button
        Me.btnHighScores = New System.Windows.Forms.Button
        Me.btnHowTo = New System.Windows.Forms.Button
        Me.btnEditor = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnCredits = New System.Windows.Forms.Button
        Me.btnPlay = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnOptions
        '
        Me.btnOptions.BackColor = System.Drawing.Color.Black
        Me.btnOptions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnOptions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOptions.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOptions.ForeColor = System.Drawing.Color.White
        Me.btnOptions.Location = New System.Drawing.Point(238, 240)
        Me.btnOptions.Name = "btnOptions"
        Me.btnOptions.Size = New System.Drawing.Size(165, 30)
        Me.btnOptions.TabIndex = 17
        Me.btnOptions.Text = "Options"
        Me.btnOptions.UseVisualStyleBackColor = False
        '
        'btnHighScores
        '
        Me.btnHighScores.BackColor = System.Drawing.Color.Black
        Me.btnHighScores.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnHighScores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnHighScores.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHighScores.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHighScores.ForeColor = System.Drawing.Color.White
        Me.btnHighScores.Location = New System.Drawing.Point(238, 190)
        Me.btnHighScores.Name = "btnHighScores"
        Me.btnHighScores.Size = New System.Drawing.Size(165, 30)
        Me.btnHighScores.TabIndex = 16
        Me.btnHighScores.Text = "High Scores"
        Me.btnHighScores.UseVisualStyleBackColor = False
        '
        'btnHowTo
        '
        Me.btnHowTo.BackColor = System.Drawing.Color.Black
        Me.btnHowTo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnHowTo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnHowTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHowTo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHowTo.ForeColor = System.Drawing.Color.White
        Me.btnHowTo.Location = New System.Drawing.Point(238, 140)
        Me.btnHowTo.Name = "btnHowTo"
        Me.btnHowTo.Size = New System.Drawing.Size(165, 30)
        Me.btnHowTo.TabIndex = 15
        Me.btnHowTo.Text = "How To Play"
        Me.btnHowTo.UseVisualStyleBackColor = False
        '
        'btnEditor
        '
        Me.btnEditor.BackColor = System.Drawing.Color.Black
        Me.btnEditor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnEditor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnEditor.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEditor.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditor.ForeColor = System.Drawing.Color.White
        Me.btnEditor.Location = New System.Drawing.Point(238, 290)
        Me.btnEditor.Name = "btnEditor"
        Me.btnEditor.Size = New System.Drawing.Size(165, 30)
        Me.btnEditor.TabIndex = 18
        Me.btnEditor.Text = "Level Editor"
        Me.btnEditor.UseVisualStyleBackColor = False
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.Black
        Me.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Location = New System.Drawing.Point(238, 390)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(165, 30)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnCredits
        '
        Me.btnCredits.BackColor = System.Drawing.Color.Black
        Me.btnCredits.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnCredits.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnCredits.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCredits.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCredits.ForeColor = System.Drawing.Color.White
        Me.btnCredits.Location = New System.Drawing.Point(238, 340)
        Me.btnCredits.Name = "btnCredits"
        Me.btnCredits.Size = New System.Drawing.Size(165, 30)
        Me.btnCredits.TabIndex = 19
        Me.btnCredits.Text = "Credits"
        Me.btnCredits.UseVisualStyleBackColor = False
        '
        'btnPlay
        '
        Me.btnPlay.BackColor = System.Drawing.Color.Black
        Me.btnPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPlay.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPlay.ForeColor = System.Drawing.Color.White
        Me.btnPlay.Location = New System.Drawing.Point(238, 75)
        Me.btnPlay.Name = "btnPlay"
        Me.btnPlay.Size = New System.Drawing.Size(165, 30)
        Me.btnPlay.TabIndex = 14
        Me.btnPlay.Text = "Play"
        Me.btnPlay.UseVisualStyleBackColor = False
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.CancelButton = Me.btnExit
        Me.ClientSize = New System.Drawing.Size(640, 504)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOptions)
        Me.Controls.Add(Me.btnHighScores)
        Me.Controls.Add(Me.btnHowTo)
        Me.Controls.Add(Me.btnEditor)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnCredits)
        Me.Controls.Add(Me.btnPlay)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-1000, 500)
        Me.Name = "frmMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SuperMario"
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents btnOptions As System.Windows.Forms.Button
    Private WithEvents btnHighScores As System.Windows.Forms.Button
    Private WithEvents btnHowTo As System.Windows.Forms.Button
    Private WithEvents btnEditor As System.Windows.Forms.Button
    Private WithEvents btnExit As System.Windows.Forms.Button
    Private WithEvents btnCredits As System.Windows.Forms.Button
    Private WithEvents btnPlay As System.Windows.Forms.Button

End Class
