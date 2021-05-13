Imports System.Configuration
Imports System.Drawing

Imports SuperMarioNet.Entities.Interfaces

Namespace SuperMarioNet.Entities.ParticleSystem
    Public Class cGoalReachedEffect
        Inherits cEffectBase
        Implements IDrawable

        Private Const FRAMES_PER_STEP As Integer = 5
        Private Const MAX_STEP As Integer = 1

        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 36

        Private Shared arrAnimation As Image()

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY

                ' load images only the first time
                If arrAnimation Is Nothing Then
                    arrAnimation = New Image(MAX_STEP - 1) {}
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")

                    For i As Integer = 0 To MAX_STEP - 1
                        arrAnimation(i) = Image.FromFile(sPath & "GoalReached\" & (i).ToString().PadLeft(3, "0"c) & ".png")
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Draw effect current step
        ''' </summary>
        Public Overrides Sub Draw(ByRef oGraphics As Graphics) Implements IDrawable.Draw

            oGraphics.DrawImage(arrAnimation(iCurrentStep), MyBase.iPositionX, MyBase.iPositionY, IMG_WIDTH, IMG_HEIGHT)
            MyBase.iCurrentFrame += 1

            If iCurrentFrame = FRAMES_PER_STEP Then
                MyBase.iCurrentFrame = 0
                MyBase.iCurrentStep += 1
            End If

            If MyBase.iCurrentStep = MAX_STEP Then
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
