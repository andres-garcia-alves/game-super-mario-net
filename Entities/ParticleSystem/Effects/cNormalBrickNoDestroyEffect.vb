Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Configuration

Imports Entities.SuperMarioNet.Interfaces

Namespace SuperMarioNet.Aux
    Public Class cNormalBrickNoDestroyEffect
        Inherits cEffectBase
        Implements iDrawable

        Private Const FRAMES_PER_STEP As Integer = 2

        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 54

        Private Shared oImage1 As Image
        Private Shared oImage2 As Image
        Private Shared oImage3 As Image

        Private _bVisible As Boolean = True

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY - 18

                ' load images only the first time
                If oImage1 Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")
                    oImage1 = Image.FromFile(sPath & "NormalBrickNoDestroy\000.png")
                    oImage2 = Image.FromFile(sPath & "NormalBrickNoDestroy\001.png")
                    oImage3 = Image.FromFile(sPath & "NormalBrickNoDestroy\002.png")
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
                Case 1
                    oGraphics.DrawImage(oImage1, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 2
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 3
                    oGraphics.DrawImage(oImage3, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 4
                    oGraphics.DrawImage(oImage3, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 5
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 6
                    oGraphics.DrawImage(oImage1, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 7
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
