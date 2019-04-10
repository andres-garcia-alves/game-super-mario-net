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
Imports System.Configuration
Imports MiscLibrary.SuperMarioNet.Miscelaneous

Public Class frmMenu

    Private Const THEME_FRM_MENU As cMusic.eMusicTheme = cMusic.eMusicTheme.MarioThemeTechno
    Private Const THEME_FRM_GAME As cMusic.eMusicTheme = cMusic.eMusicTheme.MarioTheme
    Private Const THEME_FRM_HOWTO As cMusic.eMusicTheme = cMusic.eMusicTheme.MarioThemeReggae
    Private Const THEME_FRM_SCORES As cMusic.eMusicTheme = cMusic.eMusicTheme.MarioThemeTechno
    Private Const THEME_FRM_OPTS As cMusic.eMusicTheme = cMusic.eMusicTheme.MarioThemeTechno
    Private Const THEME_FRM_CREDIT As cMusic.eMusicTheme = cMusic.eMusicTheme.MarioThemeReggae

    Private oMusic As cMusic
 
    Public Sub New()

        InitializeComponent()

        Try
            Me.Text = My.Application.Info.ProductName & " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor

            cLanguaje.Initialize()
            LoadLanguajeTexts()

            Me.BackgroundImage = Image.FromFile(ConfigurationManager.AppSettings("pathImages") & "MainBackground.png", False)

            oMusic = New cMusic()
            oMusic.PlayMusic(THEME_FRM_MENU)

            Me.BringToFront()
            Me.Activate()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlay.Click

        Me.Visible = False
        Dim oGame = New frmGame(oMusic)
        oGame.Location = Me.Location
        oGame.Text = My.Application.Info.ProductName & " " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor

        oMusic.PlayMusic(THEME_FRM_GAME)
        oGame.ShowDialog()
        oMusic.PlayMusic(THEME_FRM_MENU)

        Me.BringToFront()
        Me.Visible = True
    End Sub

    Private Sub btnHowTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHowTo.Click

        Me.Visible = False
        Dim oHowTo = New frmHowTo()
        oHowTo.Location = Me.Location

        oMusic.PlayMusic(THEME_FRM_HOWTO)
        oHowTo.ShowDialog()
        oMusic.PlayMusic(THEME_FRM_MENU)

        Me.BringToFront()
        Me.Visible = True
    End Sub

    Private Sub btnHighScores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHighScores.Click

        Me.Visible = False
        Dim oHighScores = New frmHighScores()
        oHighScores.Location = Me.Location

        oMusic.PlayMusic(THEME_FRM_SCORES)
        oHighScores.ShowDialog()
        oMusic.PlayMusic(THEME_FRM_MENU)

        Me.BringToFront()
        Me.Visible = True
    End Sub

    Private Sub btnOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOptions.Click

        Me.Visible = False
        Dim oOptions = New frmOptions()
        oOptions.Location = Me.Location

        oMusic.PlayMusic(THEME_FRM_OPTS)
        Dim oDialogResult As DialogResult = oOptions.ShowDialog()
        If (oDialogResult = DialogResult.Abort) Then Application.Restart()
        oMusic.PlayMusic(THEME_FRM_MENU)

        Me.BringToFront()
        Me.Visible = True
    End Sub

    Private Sub btnEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditor.Click
        Try
            'Dim sPath As String = ConfigurationManager.AppSettings("pathMapEditor")
            'Dim oAppDomain As AppDomain = System.AppDomain.CreateDomain("AD")
            'oAppDomain.ExecuteAssembly(sPath)
            MessageBox.Show("Feature no available yet.", "SuperMarioNet", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCredits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCredits.Click

        Me.Visible = False
        Dim oCredits = New frmCredits
        oCredits.Location = Me.Location

        oMusic.PlayMusic(THEME_FRM_CREDIT)
        oCredits.ShowDialog()
        oMusic.PlayMusic(THEME_FRM_MENU)

        Me.BringToFront()
        Me.Visible = True
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnGeneric_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles btnPlay.GotFocus, btnOptions.GotFocus, btnHowTo.GotFocus, btnHighScores.GotFocus, btnExit.GotFocus, btnEditor.GotFocus, btnCredits.GotFocus
        For i As Integer = 0 To Me.Controls.Count - 1
            If Me.Controls(i).GetType.FullName = "System.Windows.Forms.Button" Then
                If CType(Me.Controls(i), System.Windows.Forms.Button).Focused Then
                    Me.Controls(i).Font = New Font("Verdana", 12, FontStyle.Bold)
                Else
                    Me.Controls(i).Font = New Font("Verdana", 8)
                End If
            End If
        Next i
    End Sub

    Private Sub LoadLanguajeTexts()
        Try
            Me.btnPlay.Text = cLanguaje.GetTextElement("frmMenuBtnPlay")
            Me.btnHowTo.Text = cLanguaje.GetTextElement("frmMenuBtnHowTo")
            Me.btnHighScores.Text = cLanguaje.GetTextElement("frmMenuBtnHighScores")
            Me.btnOptions.Text = cLanguaje.GetTextElement("frmMenuBtnOptions")
            Me.btnEditor.Text = cLanguaje.GetTextElement("frmMenuBtnEditor")
            Me.btnCredits.Text = cLanguaje.GetTextElement("frmMenuBtnCredits")
            Me.btnExit.Text = cLanguaje.GetTextElement("frmMenuBtnExits")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

End Class
