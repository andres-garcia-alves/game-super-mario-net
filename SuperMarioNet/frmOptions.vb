' 
'  Created by:
'      Juan Andres Garcia Alves de Borba
'  
'  Date & Version:
'      Nov-2010, version 1.0
' 
'  Contact:
'      andres_garcia_ao@hotmail.com
'      andres.garcia.alves@gmail.com
'  
'  Curse:
'      Programacion III
'      Tecnicatura Superior en Programación
'      Universidad Tecnológica Nacional (UTN) FRBA - Argentina
'
'  Licensing:
'      This software is Open Source and are available under de GNU LGPL license.
'      You can found a copy of the license at http://www.gnu.org/copyleft/lesser.html
'  
'  Enjoy playing!
'

Option Strict On
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Configuration

Imports Entities.SuperMarioNet.Aux
Imports Entities.SuperMarioNet.Entities
Imports Entities.SuperMarioNet.Base
Imports MiscLibrary.SuperMarioNet.Miscelaneous

Public Class frmOptions

    Public Sub New()
        InitializeComponent()
        LoadLanguajeTexts()
        LoadFormData()

        'add the events handlers at this point, to avoid handle previously events
        AddHandler Me.radNormal.CheckedChanged, AddressOf Me.DisplayRestartMessage
        AddHandler Me.numFPS.ValueChanged, AddressOf Me.DisplayRestartMessage
        AddHandler Me.cboLanguajes.SelectedIndexChanged, AddressOf Me.DisplayRestartMessage
        AddHandler Me.cboLives.SelectedIndexChanged, AddressOf Me.DisplayRestartMessage
        AddHandler Me.chkMusic.CheckedChanged, AddressOf Me.DisplayRestartMessage
        AddHandler Me.radKeyboard.CheckedChanged, AddressOf Me.DisplayRestartMessage
    End Sub

    Private Sub frmOptions_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        SaveFormData()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnRestart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRestart.Click
        Me.DialogResult = DialogResult.Abort
    End Sub

    Private Sub DisplayRestartMessage(ByVal sender As Object, ByVal e As EventArgs)
        Me.lblMessaje.Visible = True
    End Sub

    Private Sub LoadLanguajeTexts()
        Try
            Me.lblGameMode.Text = cLanguaje.GetTextElement("frmOptionsLblGameMode")
            Me.lblLanguaje.Text = cLanguaje.GetTextElement("frmOptionsLblLanguaje")
            Me.lblLives.Text = cLanguaje.GetTextElement("frmOptionsLblLives")
            Me.lblMusic.Text = cLanguaje.GetTextElement("frmOptionsLblMusic")
            Me.lblInput.Text = cLanguaje.GetTextElement("frmOptionsLblInput")
            Me.lblMessaje.Text = cLanguaje.GetTextElement("frmOptionsLblMenssaje")
            Me.btnSave.Text = cLanguaje.GetTextElement("frmOptionsBtnSave")
            Me.btnCancel.Text = cLanguaje.GetTextElement("frmOptionsBtnCancel")
            Me.btnRestart.Text = cLanguaje.GetTextElement("frmOptionsBtnRestart")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadFormData()
        Try
            If ConfigurationManager.AppSettings("mode") = "Normal" Then
                Me.radNormal.Checked = True
            Else
                Me.radDebug.Checked = True
            End If

            Me.numFPS.Value = Convert.ToDecimal(ConfigurationManager.AppSettings("fps"))

            Dim oDirectoryInfo As New DirectoryInfo(ConfigurationManager.AppSettings("pathLanguajes"))
            Dim arrFileInfo As FileInfo() = oDirectoryInfo.GetFiles("*.xml", SearchOption.TopDirectoryOnly)
            For Each o As FileInfo In arrFileInfo
                Me.cboLanguajes.Items.Add(o.Name.Replace(o.Extension, ""))
            Next

            Me.cboLanguajes.SelectedItem = ConfigurationManager.AppSettings("languaje")
            Me.cboLives.SelectedItem = ConfigurationManager.AppSettings("lives")
            If ConfigurationManager.AppSettings("music") = "ON" Then Me.chkMusic.Checked = True

            If ConfigurationManager.AppSettings("input") = "Joystick" Then
                Me.radJoystick.Checked = True
            Else
                Me.radKeyboard.Checked = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SaveFormData()
        Try
            Me.btnSave.Enabled = False

            Dim oFileMap As New ExeConfigurationFileMap()
            oFileMap.ExeConfigFilename = "SuperMarioNet.exe.config"

            Dim config As Configuration = ConfigurationManager.OpenMappedExeConfiguration(oFileMap, ConfigurationUserLevel.None)

            config.AppSettings.Settings.Remove("mode")
            config.AppSettings.Settings.Add("mode", CType(IIf(Me.radNormal.Checked, "Normal", "Debug"), String))
            config.AppSettings.Settings.Remove("fps")
            config.AppSettings.Settings.Add("fps", Me.numFPS.Value.ToString())
            config.AppSettings.Settings.Remove("languaje")
            config.AppSettings.Settings.Add("languaje", Me.cboLanguajes.SelectedItem.ToString())
            config.AppSettings.Settings.Remove("lives")
            config.AppSettings.Settings.Add("lives", Me.cboLives.SelectedItem.ToString())
            config.AppSettings.Settings.Remove("music")
            config.AppSettings.Settings.Add("music", CType(IIf(Me.chkMusic.Checked, "ON", "OFF"), String))
            config.AppSettings.Settings.Remove("input")
            config.AppSettings.Settings.Add("input", CType(IIf(Me.radJoystick.Checked, "Joystick", "Keyboard"), String))

            config.Save(ConfigurationSaveMode.Full)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class