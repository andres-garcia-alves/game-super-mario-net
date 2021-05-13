Option Strict On
Imports System.Configuration
Imports System.Drawing

Imports SuperMarioNet.Entities.Base
Imports SuperMarioNet.Entities.Interfaces

Namespace SuperMarioNet.Entities.Entities

    Public Class cShot
        Inherits cCollisionBase
        Implements iDrawable

        Public Event ShotDisapear(ByRef oShot As cShot)

        Private Const SHOT_WIDTH As Integer = 22
        Private Const SHOT_HEIGHT As Integer = 18

        Public Const SHOT_HORIZ_MOVEMENT As Integer = 12
        Public Const SHOT_VERT_MOVEMENT As Integer = 6

        Private Const FRAMES_PER_STEP As Integer = 4
        Private Const TOTAL_ANIM_SPRITES As Integer = 4
        Private Const MAX_UPPER_STEPS As Integer = 5

        Private _bVisible As Boolean = True

        Private iUpperSteps As Integer = 0
        Private ReadOnly lstSprites As New List(Of Image)

        Public Sub New(ByVal iPosX As Integer, ByVal iPosY As Integer, ByVal eHorizDir As eMoveHorizDir)
            Try
                PositionX = iPosX
                PositionY = iPosY

                MoveHorizDir = eHorizDir
                MoveVertDir = eMoveVertDir.Down

                iCurrentSprite = 0

                Dim sPath As String = ConfigurationManager.AppSettings("pathImages")
                lstSprites.Add(Image.FromFile(sPath + "Misc/Shot_01.png", False))
                lstSprites.Add(Image.FromFile(sPath + "Misc/Shot_02.png", False))
                lstSprites.Add(Image.FromFile(sPath + "Misc/Shot_03.png", False))
                lstSprites.Add(Image.FromFile(sPath + "Misc/Shot_04.png", False))

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Property Visible() As Boolean Implements iDrawable.Visible
            Get
                Return _bVisible
            End Get
            Set(ByVal value As Boolean)
                _bVisible = value
            End Set
        End Property

        ''' <summary>
        ''' Horizontal position
        ''' </summary>
        Public Overloads Property PositionX() As Integer
            Get
                Return MyBase.PositionX
            End Get
            Set(ByVal value As Integer)
                If Me.PositionX >= 0 AndAlso Me.PositionX + SHOT_WIDTH < SCREEN_WIDTH Then
                    MyBase.PositionX = value
                Else
                    Me.Destroy() ' shot get out of screen
                End If
            End Set
        End Property

        ''' <summary>
        ''' Vertical position
        ''' </summary>
        Public Overloads Property PositionY() As Integer
            Get
                Return MyBase.PositionY
            End Get
            Set(ByVal value As Integer)
                If (value < SCREEN_HEIGHT - SHOT_HEIGHT) Then
                    MyBase.PositionY = value
                Else
                    Me.Destroy() ' shot get out of screen
                End If
            End Set
        End Property

        ''' <summary>
        ''' Shot disapear. Remove from cCollisionSystem, raise ShotDisapear event, etc
        ''' </summary>
        Private Sub Destroy()

            Me.Visible = False
            RaiseEvent ShotDisapear(Me)

        End Sub

        ''' <summary>
        ''' Get shot's height
        ''' </summary>
        Public Overrides Function GetHeight() As Integer
            Return SHOT_HEIGHT
        End Function

        ''' <summary>
        ''' Get shot's width
        ''' </summary>
        Public Overrides Function GetWidth() As Integer
            Return SHOT_WIDTH
        End Function

        ''' <summary>
        ''' Returns a Rect struct with the current location of shot
        ''' </summary>
        Public Function GetPositionRectangle() As System.Drawing.Rectangle Implements iDrawable.GetPositionRectangle
            Return New Rectangle(PositionX, PositionY, GetWidth(), GetHeight())
        End Function

        ''' <summary>
        ''' Move shot
        ''' </summary>
        Public Function Move() As Integer

            ' horizontal movement
            If MoveHorizDir = eMoveHorizDir.Left Then
                PositionX -= SHOT_HORIZ_MOVEMENT
            Else
                PositionX += SHOT_HORIZ_MOVEMENT
            End If

            ' vertical movement
            If MoveVertDir = eMoveVertDir.Down Then
                PositionY += SHOT_VERT_MOVEMENT
            Else
                PositionY -= SHOT_VERT_MOVEMENT

                iUpperSteps += 1 ' control max upper movement
                If iUpperSteps = MAX_UPPER_STEPS Then
                    iUpperSteps = 0
                    MoveVertDir = eMoveVertDir.Down
                End If
            End If

        End Function

        ''' <summary>
        ''' Draw the shot's image
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics) Implements iDrawable.Draw

            If Not _bVisible Then Return ' avoid draw if shot is invisible

            ' shot sprite
            Select Case iCurrentSprite
                Case 0 : oGraphics.DrawImage(lstSprites(0), PositionX, PositionY, GetWidth(), GetHeight())
                Case 1 : oGraphics.DrawImage(lstSprites(1), PositionX, PositionY, GetWidth(), GetHeight())
                Case 2 : oGraphics.DrawImage(lstSprites(2), PositionX, PositionY, GetWidth(), GetHeight())
                Case 3 : oGraphics.DrawImage(lstSprites(3), PositionX, PositionY, GetWidth(), GetHeight())
            End Select

            CheckNextMoveSprite(TOTAL_ANIM_SPRITES, FRAMES_PER_STEP)

        End Sub

        ''' <summary>
        ''' Invoked on collision with another entity
        ''' </summary>
        ''' <param name="oCollision">Entity object agains the collision was produced</param>
        Public Overrides Sub CollisionedBy(ByVal oCollision As cCollisionBase)

            If oCollision.GetType().Equals(Type.GetType("SuperMarioNet.Entities.Entities.cStaticObject")) Then
                Dim oStaticObject As cStaticObject = CType(oCollision, cStaticObject)

                If oStaticObject.ObjectType = cStaticObject.eObjectType.Floor Then ' change vertical direction
                    iUpperSteps = 0
                    MoveVertDir = eMoveVertDir.Up
                Else ' destroy
                    Me.Destroy()
                End If

            ElseIf oCollision.GetType().Equals(Type.GetType("SuperMarioNet.Entities.Entities.cMonster")) Then
                Me.Destroy()
            End If

        End Sub

    End Class
End Namespace
