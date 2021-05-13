Option Strict On

Imports SuperMarioNet.Miscellaneous

Public Class frmCredits

    Private iTick As Integer = 0
    Private iEfectStep As Integer = 0

    ReadOnly sTitles As String() = {cLanguaje.GetTextElement("frmCreditsTitles", 0),
                               cLanguaje.GetTextElement("frmCreditsTitles", 1),
                               cLanguaje.GetTextElement("frmCreditsTitles", 2),
                               cLanguaje.GetTextElement("frmCreditsTitles", 3),
                               cLanguaje.GetTextElement("frmCreditsTitles", 4),
                               cLanguaje.GetTextElement("frmCreditsTitles", 5),
                               cLanguaje.GetTextElement("frmCreditsTitles", 6)}
    ReadOnly sValues As String() = {"Andres Garcia Alves",
                                    "andres.garcia.alves@gmail.com",
                                    "Programacion III",
                                    "Universidad Tecnológica Nacional (UTN) - FRBA - Argentina",
                                    "Available under GNU GPL v3.0 (Open Source)",
                                    "Enjoy playing!",
                                    "Press 'Esc' to return."}

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmCredits_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.Escape Then
            Me.tmrEfect.Enabled = False
            Me.tmrChangeItem.Enabled = False
            Me.Close()
        End If
    End Sub

    Private Sub tmrChangeItem_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrChangeItem.Tick
        iTick += 1

        Select Case iTick
            Case 1
                Me.lblTitle.Text = sTitles(0)
                Me.lblValue.Text = sValues(0)
                StartEfect()
            Case 7
                Me.lblTitle.Text = sTitles(1)
                Me.lblValue.Text = sValues(1)
                StartEfect()
            Case 13
                Me.lblTitle.Text = sTitles(2)
                Me.lblValue.Text = sValues(2)
                StartEfect()
            Case 19
                Me.lblTitle.Text = sTitles(3)
                Me.lblValue.Text = sValues(3)
                StartEfect()
            Case 25
                Me.lblTitle.Text = sTitles(4)
                Me.lblValue.Text = sValues(4)
                StartEfect()
            Case 31
                Me.lblTitle.Text = sTitles(5)
                Me.lblValue.Text = sValues(5)
                StartEfect()
            Case 37
                Me.lblTitle.Text = sTitles(6)
                Me.lblValue.Text = sValues(6)
                Me.lblTop.Visible = False
                tmrChangeItem.Enabled = False
        End Select
    End Sub

    Private Sub tmrEfect_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrEfect.Tick
        iEfectStep += 1

        If iEfectStep < 50 Then
            Dim iPosX As Integer = Convert.ToInt32((iEfectStep * 568 / 50) + 32)
            Me.lblTop.Location = New Point(iPosX, 244)
        Else
            Me.tmrEfect.Enabled = False
            Me.lblTitle.Text = String.Empty
            Me.lblValue.Text = String.Empty
            Me.lblTop.Location = New Point(32, 244)
        End If
    End Sub

    Private Sub StartEfect()
        iEfectStep = 0
        Me.tmrEfect.Enabled = True
    End Sub

End Class