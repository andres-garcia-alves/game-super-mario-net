Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Configuration

Imports Entities.SuperMarioNet.Interfaces

Namespace SuperMarioNet.Aux
    Public Class cCoinBrickDestroyEffect
        Inherits cEffectBase
        Implements iDrawable

        Private Const IMG_COUNT As Integer = 5
        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 36

        Private Const FRAMES_PER_STEP As Integer = 2

        Private Shared arrAnimation As Image()

        Private _bVisible As Boolean = True

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY - IMG_HEIGHT

                ' load images only the first time
                If arrAnimation Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")

                    arrAnimation = New Image(IMG_COUNT - 1) {}
                    For i As Integer = 0 To IMG_COUNT - 1
                        arrAnimation(i) = Image.FromFile(sPath & "CoinBrickDestroy\" & (i).ToString().PadLeft(3, "0"c) & ".png")
                    Next i
                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Draw effect current step
        ''' </summary>
        Public Overrides Sub Draw(ByRef oGraphics As Graphics) Implements iDrawable.Draw
            iCurrentFrame += 1

            If iCurrentFrame = FRAMES_PER_STEP Then
                iCurrentFrame = 0
                iCurrentStep += 1
            End If

            Select Case iCurrentStep
                Case 0
                    oGraphics.DrawImage(arrAnimation(0), iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 1
                    oGraphics.DrawImage(arrAnimation(0), iPositionX, iPositionY - 15, IMG_WIDTH, IMG_HEIGHT)
                Case 2
                    oGraphics.DrawImage(arrAnimation(1), iPositionX, iPositionY - 15, IMG_WIDTH, IMG_HEIGHT)
                Case 3
                    oGraphics.DrawImage(arrAnimation(1), iPositionX, iPositionY - 30, IMG_WIDTH, IMG_HEIGHT)
                Case 4
                    oGraphics.DrawImage(arrAnimation(2), iPositionX, iPositionY - 30, IMG_WIDTH, IMG_HEIGHT)
                Case 5
                    oGraphics.DrawImage(arrAnimation(2), iPositionX, iPositionY - 45, IMG_WIDTH, IMG_HEIGHT)
                Case 6
                    oGraphics.DrawImage(arrAnimation(3), iPositionX, iPositionY - 45, IMG_WIDTH, IMG_HEIGHT)
                Case 7
                    oGraphics.DrawImage(arrAnimation(3), iPositionX, iPositionY - 60, IMG_WIDTH, IMG_HEIGHT)
                Case 8
                    oGraphics.DrawImage(arrAnimation(0), iPositionX, iPositionY - 60, IMG_WIDTH, IMG_HEIGHT)
                Case 9
                    oGraphics.DrawImage(arrAnimation(0), iPositionX, iPositionY - 75, IMG_WIDTH, IMG_HEIGHT)
                Case 10
                    oGraphics.DrawImage(arrAnimation(1), iPositionX, iPositionY - 75, IMG_WIDTH, IMG_HEIGHT)
                Case 11
                    oGraphics.DrawImage(arrAnimation(1), iPositionX, iPositionY - 90, IMG_WIDTH, IMG_HEIGHT)
                Case 12
                    oGraphics.DrawImage(arrAnimation(2), iPositionX, iPositionY - 90, IMG_WIDTH, IMG_HEIGHT)
                Case 13
                    oGraphics.DrawImage(arrAnimation(2), iPositionX, iPositionY - 75, IMG_WIDTH, IMG_HEIGHT)
                Case 14
                    oGraphics.DrawImage(arrAnimation(3), iPositionX, iPositionY - 75, IMG_WIDTH, IMG_HEIGHT)
                Case 15
                    oGraphics.DrawImage(arrAnimation(3), iPositionX, iPositionY - 60, IMG_WIDTH, IMG_HEIGHT)
                Case 16
                    oGraphics.DrawImage(arrAnimation(0), iPositionX, iPositionY - 60, IMG_WIDTH, IMG_HEIGHT)
                Case 17
                    oGraphics.DrawImage(arrAnimation(0), iPositionX, iPositionY - 45, IMG_WIDTH, IMG_HEIGHT)
                Case 18
                    oGraphics.DrawImage(arrAnimation(1), iPositionX, iPositionY - 45, IMG_WIDTH, IMG_HEIGHT)
                Case 19
                    oGraphics.DrawImage(arrAnimation(1), iPositionX, iPositionY - 30, IMG_WIDTH, IMG_HEIGHT)
                Case 20
                    oGraphics.DrawImage(arrAnimation(2), iPositionX, iPositionY - 30, IMG_WIDTH, IMG_HEIGHT)
                Case 21
                    oGraphics.DrawImage(arrAnimation(2), iPositionX, iPositionY - 15, IMG_WIDTH, IMG_HEIGHT)
                Case 22
                    oGraphics.DrawImage(arrAnimation(3), iPositionX, iPositionY - 15, IMG_WIDTH, IMG_HEIGHT)
                Case 23
                    oGraphics.DrawImage(arrAnimation(3), iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 24 To 26
                    oGraphics.DrawImage(arrAnimation(4), iPositionX, iPositionY - 20, IMG_WIDTH, IMG_HEIGHT)
                Case 27 To 29
                    oGraphics.DrawImage(arrAnimation(4), iPositionX, iPositionY - 30, IMG_WIDTH, IMG_HEIGHT)
                Case 30 To 32
                    oGraphics.DrawImage(arrAnimation(4), iPositionX, iPositionY - 40, IMG_WIDTH, IMG_HEIGHT)
                Case 33 To 35
                    oGraphics.DrawImage(arrAnimation(4), iPositionX, iPositionY - 50, IMG_WIDTH, IMG_HEIGHT)
                Case 36
                    cParticlesSystem.RemoveEfect(Me)
            End Select
        End Sub

        ''' <summary>
        ''' Unsoported. Don't use it.
        ''' </summary>
        Public Function GetPositionRectangle() As Rectangle Implements iDrawable.GetPositionRectangle
            Throw New NotSupportedException("Invalid method.")
        End Function

        ''' <summary>
        ''' Visibility
        ''' </summary>
        Public Overrides Property Visible() As Boolean Implements iDrawable.Visible
            Get
                Return MyBase.bVisible
            End Get
            Set(ByVal value As Boolean)
                bVisible = value
            End Set
        End Property

    End Class
End Namespace
