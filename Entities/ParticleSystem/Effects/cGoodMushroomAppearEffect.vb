Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Configuration

Imports Entities.SuperMarioNet.Interfaces

Namespace SuperMarioNet.Aux
    Public Class cGoodMushroomAppearEffect
        Inherits cEffectBase
        Implements iDrawable

        Private Const IMG_COUNT As Integer = 5
        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 36

        Private Const FRAMES_PER_STEP As Integer = 5

        Private Shared arrAnimation As Image()

        Private _bVisible As Boolean = True

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY

                ' load images only the first time
                If arrAnimation Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")

                    arrAnimation = New Image(IMG_COUNT - 1) {}
                    For i As Integer = 0 To IMG_COUNT - 1
                        arrAnimation(i) = Image.FromFile(sPath & "GoodMushroomAppear\" & (i).ToString().PadLeft(3, "0"c) & ".png")
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
                    oGraphics.DrawImage(arrAnimation(1), iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 2
                    oGraphics.DrawImage(arrAnimation(2), iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 3
                    oGraphics.DrawImage(arrAnimation(3), iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 4
                    oGraphics.DrawImage(arrAnimation(4), iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 5
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
