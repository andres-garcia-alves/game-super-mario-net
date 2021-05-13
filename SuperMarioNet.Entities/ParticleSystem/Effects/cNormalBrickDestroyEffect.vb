Imports System.Configuration
Imports System.Drawing

Imports SuperMarioNet.Entities.Base
Imports SuperMarioNet.Entities.Interfaces

Namespace SuperMarioNet.Entities.ParticleSystem
    Public Class cNormalBrickDestroyEffect
        Inherits cEffectBase
        Implements IDrawable

        Private Const FRAMES_PER_IMAGE As Integer = 20

        Private Const IMG_WIDTH As Integer = 22
        Private Const IMG_HEIGHT As Integer = 18

        Private Shared oImage1 As Image
        Private Shared oImage2 As Image

        Private stPosPiece1 As Point
        Private stPosPiece2 As Point
        Private stPosPiece3 As Point
        Private stPosPiece4 As Point

        Private bImageSelector As Boolean = True
        Private iMovementOffset As Single = 0
        Private iUpperYPos, iLowerYPos

        ' Private ReadOnly _bVisible As Boolean = True

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY

                stPosPiece1 = New Point(0, 0)                   ' upper left piece
                stPosPiece2 = New Point(IMG_WIDTH, 0)           ' upper right piece
                stPosPiece3 = New Point(0, 0)          ' lower left piece IMG_HEIGHT
                stPosPiece4 = New Point(IMG_WIDTH, 0)  ' lower right piece IMG_HEIGHT

                ' load images only the first time
                If oImage1 Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")
                    oImage1 = Image.FromFile(sPath & "NormalBrickDestroy\000.png")
                    oImage2 = Image.FromFile(sPath & "NormalBrickDestroy\001.png")
                End If

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Draw effect current step
        ''' </summary>
        Public Overrides Sub Draw(ByRef oGraphics As Graphics) Implements IDrawable.Draw

            ' draw
            If bImageSelector Then
                oGraphics.DrawImage(oImage1, iPositionX + stPosPiece1.X, iPositionY + stPosPiece1.Y, IMG_WIDTH, IMG_HEIGHT)
                oGraphics.DrawImage(oImage1, iPositionX + stPosPiece2.X, iPositionY + stPosPiece2.Y, IMG_WIDTH, IMG_HEIGHT)
                oGraphics.DrawImage(oImage1, iPositionX + stPosPiece3.X, iPositionY + stPosPiece3.Y + IMG_HEIGHT, IMG_WIDTH, IMG_HEIGHT)
                oGraphics.DrawImage(oImage1, iPositionX + stPosPiece4.X, iPositionY + stPosPiece4.Y + IMG_HEIGHT, IMG_WIDTH, IMG_HEIGHT)
            Else
                oGraphics.DrawImage(oImage2, iPositionX + stPosPiece1.X, iPositionY + stPosPiece1.Y, IMG_WIDTH, IMG_HEIGHT)
                oGraphics.DrawImage(oImage2, iPositionX + stPosPiece2.X, iPositionY + stPosPiece2.Y, IMG_WIDTH, IMG_HEIGHT)
                oGraphics.DrawImage(oImage2, iPositionX + stPosPiece3.X, iPositionY + stPosPiece3.Y + IMG_HEIGHT, IMG_WIDTH, IMG_HEIGHT)
                oGraphics.DrawImage(oImage2, iPositionX + stPosPiece4.X, iPositionY + stPosPiece4.Y + IMG_HEIGHT, IMG_WIDTH, IMG_HEIGHT)
            End If

            ' invert image selector
            iCurrentFrame += 1
            If iCurrentFrame = FRAMES_PER_IMAGE Then
                iCurrentFrame = 0
                bImageSelector = Not bImageSelector
            End If

            ' update brick pieces position for next time
            iMovementOffset += 2
            iUpperYPos = -UpperBrickCalculateVertPos(iMovementOffset)
            iLowerYPos = -BottomBrickCalculateVertPos(iMovementOffset)

            stPosPiece1.X -= 1
            stPosPiece1.Y = iUpperYPos
            stPosPiece2.X += 1
            stPosPiece2.Y = iUpperYPos
            stPosPiece3.X -= 1
            stPosPiece3.Y = iLowerYPos
            stPosPiece4.X += 1
            stPosPiece4.Y = iLowerYPos

            ' disable effect check
            If stPosPiece1.Y > cCollisionBase.SCREEN_HEIGHT Then
                cParticlesSystem.RemoveEfect(Me)
            End If

        End Sub

        ''' <summary>
        ''' Mathematical function. f(x)= (-((0.25X-9)^2))+80
        ''' </summary>
        Public Function UpperBrickCalculateVertPos(ByVal X As Integer) As Integer
            Return (-((0.25 * X - 9) ^ 2)) + 80
        End Function

        ''' <summary>
        ''' Mathematical function. f(x)= (-((0.20X-5.5)^2))+30
        ''' </summary>
        Public Function BottomBrickCalculateVertPos(ByVal X As Integer) As Integer
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
