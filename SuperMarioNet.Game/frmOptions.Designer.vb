<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOptions))
        Me.lblInput = New System.Windows.Forms.Label
        Me.lblMusic = New System.Windows.Forms.Label
        Me.chkMusic = New System.Windows.Forms.CheckBox
        Me.lblMessaje = New System.Windows.Forms.Label
        Me.btnRestart = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lblLives = New System.Windows.Forms.Label
        Me.lblLanguaje = New System.Windows.Forms.Label
        Me.cboLives = New System.Windows.Forms.ComboBox
        Me.cboLanguajes = New System.Windows.Forms.ComboBox
        Me.pnlInput = New System.Windows.Forms.Panel
        Me.radJoystick = New System.Windows.Forms.RadioButton
        Me.radKeyboard = New System.Windows.Forms.RadioButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.radDebug = New System.Windows.Forms.RadioButton
        Me.radNormal = New System.Windows.Forms.RadioButton
        Me.lblGameMode = New System.Windows.Forms.Label
        Me.lblFPS = New System.Windows.Forms.Label
        Me.numFPS = New System.Windows.Forms.NumericUpDown
        Me.pnlInput.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.numFPS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblInput
        '
        Me.lblInput.AutoSize = True
        Me.lblInput.BackColor = System.Drawing.Color.Transparent
        Me.lblInput.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInput.ForeColor = System.Drawing.Color.White
        Me.lblInput.Location = New System.Drawing.Point(170, 285)
        Me.lblInput.Name = "lblInput"
        Me.lblInput.Size = New System.Drawing.Size(34, 15)
        Me.lblInput.TabIndex = 20
        Me.lblInput.Text = "Input"
        '
        'lblMusic
        '
        Me.lblMusic.AutoSize = True
        Me.lblMusic.BackColor = System.Drawing.Color.Transparent
        Me.lblMusic.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMusic.ForeColor = System.Drawing.Color.White
        Me.lblMusic.Location = New System.Drawing.Point(170, 245)
        Me.lblMusic.Name = "lblMusic"
        Me.lblMusic.Size = New System.Drawing.Size(40, 15)
        Me.lblMusic.TabIndex = 19
        Me.lblMusic.Text = "Music"
        '
        'chkMusic
        '
        Me.chkMusic.AutoSize = True
        Me.chkMusic.ForeColor = System.Drawing.Color.White
        Me.chkMusic.Location = New System.Drawing.Point(295, 245)
        Me.chkMusic.Name = "chkMusic"
        Me.chkMusic.Size = New System.Drawing.Size(73, 17)
        Me.chkMusic.TabIndex = 18
        Me.chkMusic.Text = "(ON/OFF)"
        Me.chkMusic.UseVisualStyleBackColor = True
        '
        'lblMessaje
        '
        Me.lblMessaje.AutoSize = True
        Me.lblMessaje.BackColor = System.Drawing.Color.Transparent
        Me.lblMessaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessaje.ForeColor = System.Drawing.Color.White
        Me.lblMessaje.Location = New System.Drawing.Point(170, 332)
        Me.lblMessaje.Name = "lblMessaje"
        Me.lblMessaje.Size = New System.Drawing.Size(211, 15)
        Me.lblMessaje.TabIndex = 17
        Me.lblMessaje.Text = "Changes will apply after game restart."
        Me.lblMessaje.Visible = False
        '
        'btnRestart
        '
        Me.btnRestart.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnRestart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnRestart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnRestart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRestart.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestart.ForeColor = System.Drawing.Color.White
        Me.btnRestart.Location = New System.Drawing.Point(390, 387)
        Me.btnRestart.Name = "btnRestart"
        Me.btnRestart.Size = New System.Drawing.Size(100, 30)
        Me.btnRestart.TabIndex = 16
        Me.btnRestart.Text = "Restart"
        Me.btnRestart.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(270, 387)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.TabIndex = 15
        Me.btnCancel.Text = "Return"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(150, 387)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 30)
        Me.btnSave.TabIndex = 14
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lblLives
        '
        Me.lblLives.AutoSize = True
        Me.lblLives.BackColor = System.Drawing.Color.Transparent
        Me.lblLives.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLives.ForeColor = System.Drawing.Color.White
        Me.lblLives.Location = New System.Drawing.Point(170, 205)
        Me.lblLives.Name = "lblLives"
        Me.lblLives.Size = New System.Drawing.Size(35, 15)
        Me.lblLives.TabIndex = 11
        Me.lblLives.Text = "Lives"
        '
        'lblLanguaje
        '
        Me.lblLanguaje.AutoSize = True
        Me.lblLanguaje.BackColor = System.Drawing.Color.Transparent
        Me.lblLanguaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLanguaje.ForeColor = System.Drawing.Color.White
        Me.lblLanguaje.Location = New System.Drawing.Point(170, 165)
        Me.lblLanguaje.Name = "lblLanguaje"
        Me.lblLanguaje.Size = New System.Drawing.Size(59, 15)
        Me.lblLanguaje.TabIndex = 12
        Me.lblLanguaje.Text = "Languaje"
        '
        'cboLives
        '
        Me.cboLives.BackColor = System.Drawing.Color.Black
        Me.cboLives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLives.ForeColor = System.Drawing.Color.White
        Me.cboLives.FormattingEnabled = True
        Me.cboLives.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.cboLives.Location = New System.Drawing.Point(295, 205)
        Me.cboLives.Name = "cboLives"
        Me.cboLives.Size = New System.Drawing.Size(50, 21)
        Me.cboLives.TabIndex = 13
        '
        'cboLanguajes
        '
        Me.cboLanguajes.BackColor = System.Drawing.Color.Black
        Me.cboLanguajes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLanguajes.ForeColor = System.Drawing.Color.White
        Me.cboLanguajes.FormattingEnabled = True
        Me.cboLanguajes.Location = New System.Drawing.Point(295, 165)
        Me.cboLanguajes.Name = "cboLanguajes"
        Me.cboLanguajes.Size = New System.Drawing.Size(140, 21)
        Me.cboLanguajes.TabIndex = 10
        '
        'pnlInput
        '
        Me.pnlInput.Controls.Add(Me.radJoystick)
        Me.pnlInput.Controls.Add(Me.radKeyboard)
        Me.pnlInput.Location = New System.Drawing.Point(295, 285)
        Me.pnlInput.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlInput.Name = "pnlInput"
        Me.pnlInput.Size = New System.Drawing.Size(168, 24)
        Me.pnlInput.TabIndex = 23
        '
        'radJoystick
        '
        Me.radJoystick.AutoSize = True
        Me.radJoystick.ForeColor = System.Drawing.Color.White
        Me.radJoystick.Location = New System.Drawing.Point(100, 0)
        Me.radJoystick.Name = "radJoystick"
        Me.radJoystick.Size = New System.Drawing.Size(63, 17)
        Me.radJoystick.TabIndex = 24
        Me.radJoystick.TabStop = True
        Me.radJoystick.Text = "Joystick"
        Me.radJoystick.UseVisualStyleBackColor = True
        '
        'radKeyboard
        '
        Me.radKeyboard.AutoSize = True
        Me.radKeyboard.ForeColor = System.Drawing.Color.White
        Me.radKeyboard.Location = New System.Drawing.Point(0, 0)
        Me.radKeyboard.Margin = New System.Windows.Forms.Padding(0)
        Me.radKeyboard.Name = "radKeyboard"
        Me.radKeyboard.Size = New System.Drawing.Size(70, 17)
        Me.radKeyboard.TabIndex = 23
        Me.radKeyboard.TabStop = True
        Me.radKeyboard.Text = "Keyboard"
        Me.radKeyboard.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.radDebug)
        Me.Panel1.Controls.Add(Me.radNormal)
        Me.Panel1.Location = New System.Drawing.Point(295, 85)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(168, 24)
        Me.Panel1.TabIndex = 25
        '
        'radDebug
        '
        Me.radDebug.AutoSize = True
        Me.radDebug.ForeColor = System.Drawing.Color.White
        Me.radDebug.Location = New System.Drawing.Point(100, 0)
        Me.radDebug.Margin = New System.Windows.Forms.Padding(0)
        Me.radDebug.Name = "radDebug"
        Me.radDebug.Size = New System.Drawing.Size(57, 17)
        Me.radDebug.TabIndex = 24
        Me.radDebug.TabStop = True
        Me.radDebug.Text = "Debug"
        Me.radDebug.UseVisualStyleBackColor = True
        '
        'radNormal
        '
        Me.radNormal.AutoSize = True
        Me.radNormal.ForeColor = System.Drawing.Color.White
        Me.radNormal.Location = New System.Drawing.Point(0, 0)
        Me.radNormal.Margin = New System.Windows.Forms.Padding(0)
        Me.radNormal.Name = "radNormal"
        Me.radNormal.Size = New System.Drawing.Size(58, 17)
        Me.radNormal.TabIndex = 23
        Me.radNormal.TabStop = True
        Me.radNormal.Text = "Normal"
        Me.radNormal.UseVisualStyleBackColor = True
        '
        'lblGameMode
        '
        Me.lblGameMode.AutoSize = True
        Me.lblGameMode.BackColor = System.Drawing.Color.Transparent
        Me.lblGameMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGameMode.ForeColor = System.Drawing.Color.White
        Me.lblGameMode.Location = New System.Drawing.Point(170, 85)
        Me.lblGameMode.Name = "lblGameMode"
        Me.lblGameMode.Size = New System.Drawing.Size(76, 15)
        Me.lblGameMode.TabIndex = 24
        Me.lblGameMode.Text = "Game Mode"
        '
        'lblFPS
        '
        Me.lblFPS.AutoSize = True
        Me.lblFPS.BackColor = System.Drawing.Color.Transparent
        Me.lblFPS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFPS.ForeColor = System.Drawing.Color.White
        Me.lblFPS.Location = New System.Drawing.Point(170, 125)
        Me.lblFPS.Name = "lblFPS"
        Me.lblFPS.Size = New System.Drawing.Size(30, 15)
        Me.lblFPS.TabIndex = 24
        Me.lblFPS.Text = "FPS"
        '
        'numFPS
        '
        Me.numFPS.Location = New System.Drawing.Point(295, 125)
        Me.numFPS.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numFPS.Name = "numFPS"
        Me.numFPS.Size = New System.Drawing.Size(50, 20)
        Me.numFPS.TabIndex = 26
        Me.numFPS.Value = New Decimal(New Integer() {33, 0, 0, 0})
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(640, 504)
        Me.Controls.Add(Me.numFPS)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblFPS)
        Me.Controls.Add(Me.lblGameMode)
        Me.Controls.Add(Me.pnlInput)
        Me.Controls.Add(Me.lblInput)
        Me.Controls.Add(Me.lblMusic)
        Me.Controls.Add(Me.chkMusic)
        Me.Controls.Add(Me.lblMessaje)
        Me.Controls.Add(Me.btnRestart)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblLives)
        Me.Controls.Add(Me.lblLanguaje)
        Me.Controls.Add(Me.cboLives)
        Me.Controls.Add(Me.cboLanguajes)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Options"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.pnlInput.ResumeLayout(False)
        Me.pnlInput.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.numFPS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents lblInput As System.Windows.Forms.Label
    Private WithEvents lblMusic As System.Windows.Forms.Label
    Private WithEvents chkMusic As System.Windows.Forms.CheckBox
    Private WithEvents lblMessaje As System.Windows.Forms.Label
    Private WithEvents btnRestart As System.Windows.Forms.Button
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents btnSave As System.Windows.Forms.Button
    Private WithEvents lblLives As System.Windows.Forms.Label
    Private WithEvents lblLanguaje As System.Windows.Forms.Label
    Private WithEvents cboLives As System.Windows.Forms.ComboBox
    Private WithEvents cboLanguajes As System.Windows.Forms.ComboBox
    Friend WithEvents pnlInput As System.Windows.Forms.Panel
    Private WithEvents radJoystick As System.Windows.Forms.RadioButton
    Private WithEvents radKeyboard As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents radDebug As System.Windows.Forms.RadioButton
    Private WithEvents radNormal As System.Windows.Forms.RadioButton
    Private WithEvents lblGameMode As System.Windows.Forms.Label
    Private WithEvents lblFPS As System.Windows.Forms.Label
    Friend WithEvents numFPS As System.Windows.Forms.NumericUpDown
End Class
