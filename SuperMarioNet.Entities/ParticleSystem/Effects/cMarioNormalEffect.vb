Imports System.Configuration
Imports System.Drawing

Imports SuperMarioNet.Entities.Base
Imports SuperMarioNet.Entities.Interfaces

Namespace SuperMarioNet.Entities.ParticleSystem
    Public Class cMarioNormalEffect
        Inherits cEffectBase
        Implements IDrawable

        Private Const IMG_WIDTH As Integer = 44
        Private Const IMG_HEIGHT As Integer = 72

        Private Const FRAMES_PER_STEP As Integer = 2

        Private Shared oImageAux As Image
        Private Shared oImageLeft1 As Image
        Private Shared oImageLeft2 As Image
        Private Shared oImageLeft3 As Image
        Private Shared oImageRight1 As Image
        Private Shared oImageRight2 As Image
        Private Shared oImageRight3 As Image

        ' Private ReadOnly _bVisible As Boolean = True
        Private ReadOnly eHorizDir As cCollisionBase.eMoveHorizDir = cCollisionBase.eMoveHorizDir.Right

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer, ByVal eHorDir As cCollisionBase.eMoveHorizDir)
            Try
                MyBase.iPositionX = iPosX
                MyBase.iPositionY = iPosY
                Me.eHorizDir = eHorDir

                ' load images only the first time
                If oImageRight1 Is Nothing Then
                    Dim sPath As String = ConfigurationManager.AppSettings("pathAnimations")
                    oImageRight1 = Image.FromFile(sPath & "MarioNormal\000.png")
                    oImageRight2 = Image.FromFile(sPath & "MarioNormal\001.png")
                    oImageRight3 = Image.FromFile(sPath & "MarioNormal\002.png")
                    oImageLeft1 = Image.FromFile(sPath & "MarioNormal\003.png")
                    oImageLeft2 = Image.FromFile(sPath & "MarioNormal\004.png")
                    oImageLeft3 = Image.FromFile(sPath & "MarioNormal\005.png")
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

            ' select correct image
            Select Case iCurrentStep
                Case 0
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft1, oImageRight1), Image)
                Case 1
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft2, oImageRight2), Image)
                Case 2 To 3
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft1, oImageRight1), Image)
                Case 4
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft2, oImageRight2), Image)
                Case 5 To 6
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft1, oImageRight1), Image)
                Case 7
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft2, oImageRight2), Image)
                Case 8
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft3, oImageRight3), Image)
                Case 9
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft1, oImageRight1), Image)
                Case 10
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft2, oImageRight2), Image)
                Case 11
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft3, oImageRight3), Image)
                Case 12
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft1, oImageRight1), Image)
                Case 13
                    oImageAux = CType(IIf(eHorizDir = cCollisionBase.eMoveHorizDir.Left, oImageLeft3, oImageRight3), Image)
                Case 20
                    cParticlesSystem.RemoveEfect(Me) ' effect end
            End Select

            ' draw
            oGraphics.DrawImage(oImageAux, iPositionX, iPositionY, IMG_WIDTH, IMG_HEIGHT)

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
