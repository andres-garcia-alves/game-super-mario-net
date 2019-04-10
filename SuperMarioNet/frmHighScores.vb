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

Imports MiscLibrary.SuperMarioNet.Miscelaneous

Public Class frmHighScores

    Public Sub New()
        InitializeComponent()
        LoadFormData()
    End Sub

    Private Sub frmHighScores_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
        Dim sTitle As String = cLanguaje.GetTextElement("frmHighScoresHighScores", 0)
        Dim sMsg As String = cLanguaje.GetTextElement("frmHighScoresHighScores", 1)
        Dim oDialogResult As DialogResult = MessageBox.Show(sMsg, sTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question)

        If oDialogResult = DialogResult.OK Then
            Dim oHighScores = New cHighScores()
            oHighScores.ResetHightScores()
            LoadFormData()
        End If
    End Sub

    Private Sub LoadFormData()
        Me.lblPlayers.Text = String.Empty
        Me.lblPoints.Text = String.Empty
        Dim lstHighScores As List(Of cHighScoreItem)

        Try
            Dim oHighScores = New cHighScores()
            lstHighScores = oHighScores.GetHighScores()

            For i As Integer = 0 To lstHighScores.Count - 1
                Me.lblPlayers.Text &= lstHighScores(i).Name & vbNewLine
                Me.lblPoints.Text &= lstHighScores(i).Points.ToString().PadLeft(5, "0"c) & vbNewLine
            Next i

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class