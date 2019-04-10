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

Public Class frmHowTo

    Dim iStep As Integer = 0
    Dim sPath As String = String.Empty

    Dim oImage As Image
    Dim oGraphics As Graphics

    Public Sub New()
        Try
            InitializeComponent()
            LoadLanguajeTexts()
            sPath = ConfigurationManager.AppSettings("pathImages")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Key down
    ''' </summary>
    Private Sub frmHowTo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.Escape Then Me.Close()
    End Sub

    ''' <summary>
    ''' Back to main
    ''' </summary>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Repeat tutorial
    ''' </summary>
    Private Sub btnRepeat_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRepeat.Click
        Me.btnBack.Visible = False
        Me.btnRepeat.Visible = False
        Me.lblLeyend.Visible = False

        iStep = 0
        Me.tmrTick.Enabled = True
    End Sub

    ''' <summary>
    ''' Ticks, in seconds
    ''' </summary>
    Private Sub tmrTick_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrTick.Tick
        iStep += 1
        Draw()
    End Sub

    ''' <summary>
    ''' Draw current tutorial's step
    ''' </summary>
    Private Sub Draw()
        Try
            oGraphics = Me.CreateGraphics()
            oGraphics.SmoothingMode = SmoothingMode.HighQuality

            Select Case iStep
                Case 1
                    oImage = Image.FromFile(sPath & "HowTo\Screen_01.png")
                    oGraphics.DrawImage(oImage, New Rectangle(0, 0, Me.Width, Me.Height))
                Case 2
                    oGraphics.DrawString(cLanguaje.GetTextElement("frmHowToStep1"), New Font("Verdana", 11), Brushes.White, 40, 245)
                Case 5
                    oImage = Image.FromFile(sPath & "HowTo\Screen_02.png")
                    oGraphics.DrawImage(oImage, New Rectangle(0, 0, Me.Width, Me.Height))
                    oGraphics.DrawString(cLanguaje.GetTextElement("frmHowToStep1"), New Font("Verdana", 11), Brushes.White, 40, 245)
                Case 10
                    oImage = Image.FromFile(sPath & "HowTo\Screen_03.png")
                    oGraphics.DrawImage(oImage, New Rectangle(0, 0, Me.Width, Me.Height))
                Case 11
                    oGraphics.DrawString(cLanguaje.GetTextElement("frmHowToStep2"), New Font("Verdana", 11), Brushes.White, 40, 245)
                Case 15
                    oImage = Image.FromFile(sPath & "HowTo\Screen_04.png")
                    oGraphics.DrawImage(oImage, New Rectangle(0, 0, Me.Width, Me.Height))
                    oGraphics.DrawString(cLanguaje.GetTextElement("frmHowToStep2"), New Font("Verdana", 11), Brushes.White, 40, 245)
                Case 20
                    oImage = Image.FromFile(sPath & "HowTo\Screen_05.png")
                    oGraphics.DrawImage(oImage, New Rectangle(0, 0, Me.Width, Me.Height))
                Case 21
                    oGraphics.DrawString(cLanguaje.GetTextElement("frmHowToStep3"), New Font("Verdana", 11), Brushes.White, 40, 200)
                Case 25
                    oImage = Image.FromFile(sPath & "HowTo\Screen_06.png")
                    oGraphics.DrawImage(oImage, New Rectangle(0, 0, Me.Width, Me.Height))
                Case 26
                    oGraphics.DrawString(cLanguaje.GetTextElement("frmHowToStep4"), New Font("Verdana", 11), Brushes.White, 40, 200)
                Case 30
                    oImage = Image.FromFile(sPath & "HowTo\Screen_07.png")
                    oGraphics.DrawImage(oImage, New Rectangle(0, 0, Me.Width, Me.Height))
                Case 31
                    oGraphics.DrawString(cLanguaje.GetTextElement("frmHowToStep5"), New Font("Verdana", 11), Brushes.White, 40, 200)
                Case 35 ' end of tutorial
                    oGraphics.Clear(Color.Black)
                    Me.tmrTick.Enabled = False
                    Me.btnBack.Visible = True
                    Me.btnRepeat.Visible = True
                    Me.lblLeyend.Visible = True
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Load form texts
    ''' </summary>
    Private Sub LoadLanguajeTexts()
        Try
            Me.lblLeyend.Text = cLanguaje.GetTextElement("frmHowToLblLeyend")
            Me.btnBack.Text = cLanguaje.GetTextElement("frmHowToBtnBack")
            Me.btnRepeat.Text = cLanguaje.GetTextElement("frmHowToBtnRepeat")

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class