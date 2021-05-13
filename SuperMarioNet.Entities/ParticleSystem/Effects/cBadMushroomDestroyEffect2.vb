Imports System.Configuration
Imports System.Drawing

Imports SuperMarioNet.Entities.Base
Imports SuperMarioNet.Entities.Interfaces

Namespace SuperMarioNet.Entities.ParticleSystem
    Public Class cBadMushroomDestroyEffect2
        Inherits cEffectBase
        Implements IDrawable

        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 36
        Private Const FRAMES_PER_STEP As Integer = 2

        Private Shared oImage1 As Image
        Private Shared oImage2 As Image

        Private stPosition As Point

        Private iMovementOffset As Single = 0
        ' Private ReadOnly iUpperYPos
        ' Private ReadOnly iLowerYPos

        ' Private ReadOnly _bVisible As Boolean = True

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY

                stPosition = New Point(0, 0)

                ' load images only the first time
                If oImage1 Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")
                    oImage1 = Image.FromFile(sPath & "BadMushroomDestroy2\000.png")
                    oImage2 = Image.FromFile(sPath & "BadMushroomDestroy2\001.png")
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

            ' draw shield
            oGraphics.DrawImage(oImage1, iPositionX + stPosition.X, iPositionY + stPosition.Y, IMG_WIDTH, IMG_HEIGHT)

            ' draw score points
            Select Case iCurrentStep
                Case 10 To 12
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY - 20, IMG_WIDTH, IMG_HEIGHT)
                Case 13 To 15
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY - 30, IMG_WIDTH, IMG_HEIGHT)
                Case 16 To 18
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY - 40, IMG_WIDTH, IMG_HEIGHT)
                Case 19 To 21
                    oGraphics.DrawImage(oImage2, iPositionX, iPositionY - 50, IMG_WIDTH, IMG_HEIGHT)
            End Select

            ' update shield position for next time
            iMovementOffset += 2
            stPosition.X += 1
            stPosition.Y = -CalculateVertPos(iMovementOffset)

            ' check effect end
            If stPosition.Y > cCollisionBase.SCREEN_HEIGHT Then
                cParticlesSystem.RemoveEfect(Me)
            End If
        End Sub

        ''' <summary>
        ''' Mathematical function. f(x)= (-((0.20X-5.5)^2))+30
        ''' </summary>
        Public Function CalculateVertPos(ByVal X As Integer) As Integer
            Return (-((0.2 * X - 5.5) ^ 2)) + 30
        End Function

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
