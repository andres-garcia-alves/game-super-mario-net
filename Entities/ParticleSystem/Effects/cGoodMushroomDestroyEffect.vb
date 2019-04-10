Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Configuration

Imports Entities.SuperMarioNet.Interfaces
Imports Entities.SuperMarioNet.Base

Namespace SuperMarioNet.Aux
    Public Class cFlowerDestroyEffect
        Inherits cEffectBase
        Implements iDrawable

        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 36

        Private Const FRAMES_PER_STEP As Integer = 2

        Private Shared oImage As Image

        Private _bVisible As Boolean = True

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY

                ' load images only the first time
                If oImage Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")
                    oImage = Image.FromFile(sPath & "FlowerDestroy\000.png")
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
                Case 10 To 12
                    oGraphics.DrawImage(oImage, iPositionX, iPositionY - 20, IMG_WIDTH, IMG_HEIGHT)
                Case 13 To 15
                    oGraphics.DrawImage(oImage, iPositionX, iPositionY - 30, IMG_WIDTH, IMG_HEIGHT)
                Case 16 To 18
                    oGraphics.DrawImage(oImage, iPositionX, iPositionY - 40, IMG_WIDTH, IMG_HEIGHT)
                Case 19 To 21
                    oGraphics.DrawImage(oImage, iPositionX, iPositionY - 50, IMG_WIDTH, IMG_HEIGHT)
                Case 22
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
