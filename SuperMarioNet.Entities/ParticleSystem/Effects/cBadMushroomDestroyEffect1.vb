Imports System.Configuration
Imports System.Drawing

Imports SuperMarioNet.Entities.Interfaces

Namespace SuperMarioNet.Entities.ParticleSystem
    Public Class cBadMushroomDestroyEffect1
        Inherits cEffectBase
        Implements IDrawable

        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 36

        Private Const FRAMES_PER_STEP As Integer = 2

        Private Shared oImage1 As Image
        Private Shared oImage2 As Image

        ' Private ReadOnly _bVisible As Boolean = True

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY

                ' load images only the first time
                If oImage1 Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")
                    oImage1 = Image.FromFile(sPath & "BadMushroomDestroy1\000.png")
                    oImage2 = Image.FromFile(sPath & "BadMushroomDestroy1\001.png")
                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Draw effect current step
        ''' </summary>
        Public Overrides Sub Draw(ByRef oGraphics As Graphics) Implements IDrawable.Draw
            iCurrentFrame += 1

            If iCurrentFrame = FRAMES_PER_STEP Then
                iCurrentFrame = 0
                iCurrentStep += 1
            End If

            Select Case iCurrentStep
                Case 1 To 9
                    oGraphics.DrawImage(oImage1, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)
                Case 10 To 12
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY - 20, IMG_WIDTH, IMG_HEIGHT)
                Case 13 To 15
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY - 30, IMG_WIDTH, IMG_HEIGHT)
                Case 16 To 18
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY - 40, IMG_WIDTH, IMG_HEIGHT)
                Case 19 To 21
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY - 50, IMG_WIDTH, IMG_HEIGHT)
                Case 22
                    cParticlesSystem.RemoveEfect(Me)
            End Select
        End Sub

        ''' <summary>
        ''' Unsoported. Don't use it.
        ''' </summary>
        Public Function GetPositionRectangle() As Rectangle Implements IDrawable.GetPositionRectangle
            Throw New NotSupportedException("Invalid method.")
        End Function

        ''' <summary>
        ''' Visibility
        ''' </summary>
        Public Overrides Property Visible() As Boolean Implements IDrawable.Visible
            Get
                Return MyBase.bVisible
            End Get
            Set(ByVal value As Boolean)
                bVisible = value
            End Set
        End Property

    End Class
End Namespace
