Option Strict On
Imports System.IO
Imports System.Drawing
Imports System.Timers
Imports System.Configuration

Imports Entities.SuperMarioNet.Base
Imports Entities.SuperMarioNet.Interfaces
Imports Entities.SuperMarioNet.Aux

Namespace SuperMarioNet.Entities

    Public Class cMonster
        Inherits cCollisionBase
        Implements iDrawable

        Public Enum eMonsterType
            BadMushroom = 0
            Flower = 1
            GoodMushroom = 2
            Turtle = 3
        End Enum

        Public Enum eMonsterMoveStatus
            Move = 0
            DontMove = 1
        End Enum

        Private Const MUSHROOM_WIDTH As Integer = 44
        Private Const MUSHROOM_HEIGHT As Integer = 36
        Private Const TURTLE_WIDTH As Integer = 48
        Private Const TURTLE_HEIGHT As Integer = 52

        Public Const MONSTER_HORIZ_MOVEMENT As Integer = 3
        Public Const MONSTER_VERT_MOVEMENT As Integer = 5

        Private Const FRAMES_PER_STEP As Integer = 6
        Private Const TOTAL_ANIM_SPRITES As Integer = 2

        Private Const BAD_MUSHROOM_POINTS As Integer = 100
        Private Const GOOD_MUSHROOM_POINTS As Integer = 1000
        Private Const FLOWER_POINTS As Integer = 1000
        Private Const TURTLE_POINTS As Integer = 500

        Private _iWidth As Integer
        Private _iHeight As Integer
        Private _bVisible As Boolean = True
        Private _eMonsterType As eMonsterType
        Private _eMoveStatus As eMonsterMoveStatus

        Private oImage As Image
        Private lstMoveLeft As New List(Of Image)
        Private lstMoveRight As New List(Of Image)

        ' jump & gravity
        Private byGap As Integer = MONSTER_VERT_MOVEMENT
        Private tmrEnable As Timer
        Private tmrVisible As Timer

        Private iStartUpPosX, iStartUpPosY As Integer ' used on reset

        Public Sub New()
        End Sub

        Public Sub New(ByVal eMonstType As eMonsterType, ByVal bVisible As Boolean, ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                Dim sPath As String = ConfigurationManager.AppSettings("pathImages")

                Me._eMonsterType = eMonstType
                Me.Visible = bVisible

                MyBase.PositionX = iPosX
                MyBase.PositionY = iPosY
                MyBase.iCurrentSprite = 0

                Me.iStartUpPosX = iPosX ' used on reset
                Me.iStartUpPosY = iPosY ' used on reset

                ' sprites for monster movement animation
                Select Case _eMonsterType
                    Case eMonsterType.BadMushroom
                        Me._iWidth = MUSHROOM_WIDTH
                        Me._iHeight = MUSHROOM_HEIGHT
                        Me.lstMoveLeft.Add(Image.FromFile(sPath + "Monsters\Mushroom_Left_01.png", False))
                        Me.lstMoveLeft.Add(Image.FromFile(sPath + "Monsters\Mushroom_Left_02.png", False))
                        Me.lstMoveRight.Add(Image.FromFile(sPath + "Monsters\Mushroom_Right_01.png", False))
                        Me.lstMoveRight.Add(Image.FromFile(sPath + "Monsters\Mushroom_Right_02.png", False))
                        Me._eMoveStatus = eMonsterMoveStatus.Move
                        MyBase.MoveHorizDir = eMoveHorizDir.Left

                    Case eMonsterType.Turtle
                        Me._iWidth = TURTLE_WIDTH
                        Me._iHeight = TURTLE_HEIGHT
                        Me.lstMoveLeft.Add(Image.FromFile(sPath + "Monsters\Turtle_Left_01.png", False))
                        Me.lstMoveLeft.Add(Image.FromFile(sPath + "Monsters\Turtle_Left_02.png", False))
                        Me.lstMoveRight.Add(Image.FromFile(sPath + "Monsters\Turtle_Right_01.png", False))
                        Me.lstMoveRight.Add(Image.FromFile(sPath + "Monsters\Turtle_Right_02.png", False))
                        Me._eMoveStatus = eMonsterMoveStatus.Move
                        MyBase.MoveHorizDir = eMoveHorizDir.Left

                    Case eMonsterType.GoodMushroom
                        Me._iWidth = MUSHROOM_WIDTH
                        Me._iHeight = MUSHROOM_HEIGHT
                        Me.oImage = Image.FromFile(sPath + "Monsters\Good_Mushroom.png", False)
                        Me._eMoveStatus = eMonsterMoveStatus.DontMove
                        MyBase.MoveHorizDir = eMoveHorizDir.Right

                    Case eMonsterType.Flower
                        Me._iWidth = MUSHROOM_WIDTH
                        Me._iHeight = MUSHROOM_HEIGHT
                        Me.lstMoveRight.Add(Image.FromFile(sPath + "Monsters\Flower_01.png", False))
                        Me.lstMoveRight.Add(Image.FromFile(sPath + "Monsters\Flower_02.png", False))
                        Me._eMoveStatus = eMonsterMoveStatus.DontMove
                        MyBase.MoveHorizDir = eMoveHorizDir.Right
                End Select

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Monster Type (Mushroom, Turtle, GoodMushRoom, etc)
        ''' </summary>
        Public ReadOnly Property MonsterType() As eMonsterType
            Get
                Return _eMonsterType
            End Get
        End Property

        ''' <summary>
        ''' Horizontal position
        ''' </summary>
        Public Overloads Property PositionX() As Integer
            Get
                Return MyBase.PositionX
            End Get
            Set(ByVal value As Integer)
                If Me.PositionX + _iWidth >= -200 Then
                    MyBase.PositionX = value
                Else
                    Me.Destroy() ' monster get out of screen
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
                If (value < SCREEN_HEIGHT - _iHeight) Then
                    MyBase.PositionY = value
                Else
                    Me.Destroy() ' monster get out of screen
                End If
            End Set
        End Property

        ''' <summary>
        ''' Visibility
        ''' </summary>
        Public Property Visible() As Boolean Implements iDrawable.Visible
            Get
                Return _bVisible
            End Get
            Set(ByVal value As Boolean)
                _bVisible = value
            End Set
        End Property

        ''' <summary>
        ''' Movement status
        ''' </summary>
        Public Property MoveStatus() As eMonsterMoveStatus
            Get
                Return _eMoveStatus
            End Get
            Set(ByVal value As eMonsterMoveStatus)
                _eMoveStatus = value
            End Set
        End Property

        ''' <summary>
        ''' Reset monster status
        ''' </summary>
        Public Sub Reset()
            Try
                MyBase.PositionX = iStartUpPosX
                MyBase.PositionY = iStartUpPosY

                MyBase.iCurrentSprite = 0
                MyBase.MoveHorizDir = eMoveHorizDir.Left
                Me.Visible = True

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Get monster's width
        ''' </summary>
        Public Overrides Function GetWidth() As Integer
            Return _iWidth
        End Function

        ''' <summary>
        ''' Get monster's height
        ''' </summary>
        Public Overrides Function GetHeight() As Integer
            Return _iHeight
        End Function

        ''' <summary>
        ''' Returns a Rect struct with the current location of player
        ''' </summary>
        Public Function GetPositionRectangle() As Rectangle Implements iDrawable.GetPositionRectangle
            Return New Rectangle(PositionX, PositionY, GetWidth(), GetHeight())
        End Function

        ''' <summary>
        ''' Draw the monster's current sprite
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics) Implements iDrawable.Draw

            If Not _bVisible Then Return ' avoid draw invisible monsters

            If _eMonsterType = eMonsterType.BadMushroom Or _eMonsterType = eMonsterType.Turtle Or _eMonsterType = eMonsterType.Flower Then

                If MoveHorizDir = eMoveHorizDir.Left Then ' left direction
                    oGraphics.DrawImage(lstMoveLeft(iCurrentSprite), PositionX, PositionY, GetWidth(), GetHeight())
                Else ' right direction
                    oGraphics.DrawImage(lstMoveRight(iCurrentSprite), PositionX, PositionY, GetWidth(), GetHeight())
                End If
                CheckNextMoveSprite(TOTAL_ANIM_SPRITES, FRAMES_PER_STEP)

            ElseIf _eMonsterType = eMonsterType.GoodMushroom Then
                oGraphics.DrawImage(oImage, PositionX, PositionY, GetWidth(), GetHeight())
            End If

        End Sub

        ''' <summary>
        ''' Move monster to next position
        ''' </summary>
        Public Sub Move()

            If _eMoveStatus = eMonsterMoveStatus.DontMove Then Return

            If MyBase.MoveHorizDir = eMoveHorizDir.Left Then
                Me.PositionX -= MONSTER_HORIZ_MOVEMENT
            ElseIf MyBase.MoveHorizDir = eMoveHorizDir.Right Then
                Me.PositionX += MONSTER_HORIZ_MOVEMENT
            End If

        End Sub

        ''' <summary>
        ''' Adjust monster's position acording to gravity (if necesary)
        ''' </summary>
        Public Sub CheckGravityMovement()

            If cCollisionsSystem.CheckValidMovement(Me, False, MONSTER_VERT_MOVEMENT, eMoveVertDir.Down) Then ' below monster's position is empty
                PositionY += MONSTER_VERT_MOVEMENT ' falling down
            Else
                ' space below monster is less than MONSTER_VERT_MOVEMENT
                ' try 1 pixels by time until reach the floor
                byGap = MONSTER_VERT_MOVEMENT
                Do
                    If cCollisionsSystem.CheckValidMovement(Me, False, byGap, eMoveVertDir.Down) Then
                        PositionY += byGap ' adjust monster's position to floor
                        Exit Do
                    End If
                    byGap -= 1
                Loop While byGap > 0
            End If

        End Sub

        ''' <summary>
        ''' Program to make visible the monster after received time passed
        ''' </summary>
        ''' <param name="iMiliSeconds">Wait time to enable, in miliseconds</param>
        Public Sub ProgramVisible(ByVal iMiliSeconds As Integer)

            tmrVisible = New Timer(iMiliSeconds)
            AddHandler tmrVisible.Elapsed, AddressOf VisibleMonster
            tmrVisible.Start()

        End Sub

        ''' <summary>
        ''' Make monster visible
        ''' </summary>
        Private Sub VisibleMonster(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            tmrVisible.Stop()
            tmrVisible.Close()
            RemoveHandler tmrVisible.Elapsed, AddressOf VisibleMonster

            Me.Visible = True

        End Sub

        ''' <summary>
        ''' Program to enable the monster after received time passed
        ''' </summary>
        ''' <param name="iMiliSeconds">Wait time to enable, in miliseconds</param>
        Public Sub ProgramEnable(ByVal iMiliSeconds As Integer)

            tmrEnable = New Timer(iMiliSeconds)
            AddHandler tmrEnable.Elapsed, AddressOf EnableMonster
            tmrEnable.Start()

        End Sub

        ''' <summary>
        ''' Enable monster status
        ''' </summary>
        Private Sub EnableMonster(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            tmrEnable.Stop()
            tmrEnable.Close()
            RemoveHandler tmrEnable.Elapsed, AddressOf EnableMonster

            Me.MoveStatus = eMonsterMoveStatus.Move

        End Sub

        ''' <summary>
        ''' Monster die. Remove from cCollisionSystem, etc
        ''' </summary>
        Private Sub Destroy()

            Me.Visible = False
            'cCollisionsSystem.RemoveItemForCollision(Me)
            Me.ToRemoveFromCollision = True

        End Sub

        ''' <summary>
        ''' Invoked on collision with another entity
        ''' </summary>
        ''' <param name="oCollision">Entity object agains the collision was produced</param>
        Public Overrides Sub CollisionedBy(ByVal oCollision As cCollisionBase)

            If oCollision.GetType().Equals(Type.GetType("Entities.SuperMarioNet.Entities.cPlayer")) Then
                ' collision against cPlayer: destroy monster
                If Me._eMonsterType = eMonsterType.BadMushroom Then
                    cGameControl.AddScorePoints(BAD_MUSHROOM_POINTS) ' add points
                    cParticlesSystem.RegisterEfect(New cBadMushroomDestroyEffect1(PositionX, PositionY)) ' display destroy effect

                ElseIf Me._eMonsterType = eMonsterType.GoodMushroom Then
                    cGameControl.AddScorePoints(GOOD_MUSHROOM_POINTS) ' add points
                    cParticlesSystem.RegisterEfect(New cGoodMushroomDestroyEffect(PositionX, PositionY)) ' display destroy effect

                ElseIf Me._eMonsterType = eMonsterType.Flower Then
                    cGameControl.AddScorePoints(FLOWER_POINTS) ' add points
                    cParticlesSystem.RegisterEfect(New cFlowerDestroyEffect(PositionX, PositionY)) ' display destroy effect

                ElseIf Me._eMonsterType = eMonsterType.Turtle Then
                    cGameControl.AddScorePoints(TURTLE_POINTS) ' add points
                    cParticlesSystem.RegisterEfect(New cTurtleDestroyEffect(PositionX, PositionY)) ' display destroy effect
                End If

                Me.Destroy()

            ElseIf oCollision.GetType().Equals(Type.GetType("Entities.SuperMarioNet.Entities.cStaticObject")) Then
                ' collision against cStaticObject: invert monster horizontal direction
                If CType(oCollision, cStaticObject).ObjectType <> cStaticObject.eObjectType.Floor And Me.MonsterType <> eMonsterType.Flower Then
                    If MoveHorizDir = eMoveHorizDir.Left Then
                        MoveHorizDir = eMoveHorizDir.Right
                    ElseIf MoveHorizDir = eMoveHorizDir.Right Then
                        MoveHorizDir = eMoveHorizDir.Left
                    End If
                End If

            ElseIf oCollision.GetType().Equals(Type.GetType("Entities.SuperMarioNet.Entities.cMonster")) Then
                ' collision against another cMonster: invert monster horizontal direction
                If Me.MonsterType <> eMonsterType.GoodMushroom And Me.MonsterType <> eMonsterType.Flower Then
                    If MoveHorizDir = eMoveHorizDir.Left Then
                        MoveHorizDir = eMoveHorizDir.Right
                    ElseIf MoveHorizDir = eMoveHorizDir.Right Then
                        MoveHorizDir = eMoveHorizDir.Left
                    End If
                End If

            ElseIf oCollision.GetType().Equals(Type.GetType("Entities.SuperMarioNet.Entities.cShot")) Then
                ' collision against cShot: destroy monster
                If Me._eMonsterType = eMonsterType.BadMushroom Then
                    cGameControl.AddScorePoints(BAD_MUSHROOM_POINTS) ' add points
                    cParticlesSystem.RegisterEfect(New cBadMushroomDestroyEffect2(PositionX, PositionY)) ' display destroy effect
                    Me.Destroy()
                ElseIf Me._eMonsterType = eMonsterType.Turtle Then
                    cGameControl.AddScorePoints(BAD_MUSHROOM_POINTS) ' add points
                    cParticlesSystem.RegisterEfect(New cTurtleDestroyEffect(PositionX, PositionY)) ' display destroy effect
                    Me.Destroy()
                End If
            End If
        End Sub

    End Class
End Namespace
