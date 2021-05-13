Imports System.Configuration
Imports System.Drawing

Imports SuperMarioNet.Entities.Base
Imports SuperMarioNet.Entities.Interfaces

Namespace SuperMarioNet.Entities.ParticleSystem
    Public Class cMarioDeadEffect
        Inherits cEffectBase
        Implements IDrawable

        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 36

        Private Const FRAMES_PER_STEP As Integer = 2

        Private Shared oImage As Image

        ' Private ReadOnly _bVisible As Boolean = True

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY

                ' load images only the first time
                If oImage Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")
                    oImage = Image.FromFile(sPath & "MarioDead\000.png")
                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Draw effect current step
        ''' </summary>
        Public Overrides Sub Draw(ByRef oGraphics As Graphics) Implements IDrawable.Draw

            ' step control
            iCurrentFrame += 1
            If iCurrentFrame = FRAMES_PER_STEP Then
                iCurrentFrame = 0
                iCurrentStep += 1
            End If

            ' draw
            oGraphics.DrawImage(oImage, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)

            ' adjunt position
            Select Case iCurrentStep
                Case 0 : iPositionY -= 10
                Case 1 : iPositionY -= 10
                Case 2 : iPositionY -= 10
                Case 3 : iPositionY -= 10
                Case 4 : iPositionY -= 10
                Case 5 : iPositionY -= 8
                Case 6 : iPositionY -= 5
                Case 7 : iPositionY -= 2
                Case 8
                Case 9 : iPositionY += 2
                Case 10 : iPositionY += 5
                Case 11 : iPositionY += 8
                Case 12 : iPositionY += 10
                Case Else : iPositionY += 12
            End Select

            ' check effect end
            If iPositionY > cCollisionBase.SCREEN_HEIGHT Then
                cParticlesSystem.RemoveEfect(Me)
            End If

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
