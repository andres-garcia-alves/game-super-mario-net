Option Strict On
Imports System.Drawing
Imports System.Timers
Imports System.Configuration

Imports Entities.SuperMarioNet.Base
Imports Entities.SuperMarioNet.Interfaces
Imports Entities.SuperMarioNet.Aux

Namespace SuperMarioNet.Entities

    Public Class cPlayer
        Inherits cCollisionBase
        Implements iDrawable

        Public Event PlayerDie(ByVal eReason As cGameControl.eDieReason)

        Public Enum ePlayerLiveStatus
            Small = 0
            Normal = 1
            Fire = 2
        End Enum

        Public Enum ePlayerMoveStatus
            StandBy = 0
            Walking = 1
            Running = 2
        End Enum

        Public Enum ePlayerJumpType
            Lineal = 0
            Sinusoidal = 1
        End Enum

        Public Enum ePlayerJumpStatus
            NoJumping = 0
            JumpRisingPhase = 1
            JumpLoweringPhase = 2
        End Enum

        Public Const SCREEN_LEFT_GAP As Integer = 5
        Public Const SCREEN_RIGHT_GAP As Integer = 300

        Private Const PLAYER_DEFAULT_X As Integer = 120
        Private Const PLAYER_DEFAULT_Y As Integer = 395

        Private Const PLAYER_WIDTH As Integer = 44
        Private Const PLAYER_HEIGHT_SMALL As Integer = 36
        Private Const PLAYER_HEIGHT_NORMAL As Integer = 72
        Private Const PLAYER_HEIGHT_FIRE As Integer = 72

        Public Const PLAYER_HORIZ_WALK_MOV As Integer = 7
        Public Const PLAYER_HORIZ_RUN_MOV As Integer = 10
        Public Const PLAYER_VERT_MOVEMENT As Integer = 11

        Private Const NORMAL_JUMP_STEPS As Integer = 14
        Private Const MINI_JUMP_STEPS As Integer = 6

        Private Const INVULNERABLE_TIME As Integer = 10 ' seconds

        Private Const NORMAL_FRAMES_PER_STEP As Integer = 4
        Private Const RUN_FRAMES_PER_STEP As Integer = 3
        Private Const TOTAL_ANIM_SPRITES As Integer = 3

        Private _oShots As New cShots
        Private _bVisible As Boolean = True
        Private _bScreenGapReached As Boolean = False

        Private oImageAux As Image

        ' player's sprites, for level 1
        Private oSmallStandByLeft, oSmallStandByRight As Image
        Private oSmallJumpLeft, oSmallJumpRight As Image
        Private lstSmallMoveLeft As New List(Of Image)
        Private lstSmallMoveRight As New List(Of Image)

        ' player's sprites, for level 2
        Private oNormalStandByLeft, oNormalStandByRight As Image
        Private oNormalJumpLeft, oNormalJumpRight As Image
        Private lstNormalMoveLeft As New List(Of Image)
        Private lstNormalMoveRight As New List(Of Image)

        ' player's sprites, for level 3
        Private oFireStandByLeft, oFireStandByRight As Image
        Private oFireJumpLeft, oFireJumpRight As Image
        Private lstFireMoveLeft As New List(Of Image)
        Private lstFireMoveRight As New List(Of Image)
        Private oShotingLeft, oShotingRight As Image

        ' jump & gravity
        Private byGap As Integer = PLAYER_VERT_MOVEMENT
        Private iJumpStep As Integer = 0
        Private bFallingDown As Boolean = False

        ' invulnerable time
        Private iInvulnerableTime As Integer = 0
        Private tmrInvulnerable As Timer

        ' enumerations vars
        Private eLiveStatus As ePlayerLiveStatus = ePlayerLiveStatus.Small
        Private eMoveStatus As ePlayerMoveStatus = ePlayerMoveStatus.StandBy
        Private eJumpStatus As ePlayerJumpStatus = ePlayerJumpStatus.NoJumping
        Private eJumpType As ePlayerJumpType = ePlayerJumpType.Lineal

        ' misc
        Private bShotingStatus As Boolean = False
        Private tmrDead As Timer

        Public Sub New()
            Try
                Me.PositionX = PLAYER_DEFAULT_X
                Me.PositionY = PLAYER_DEFAULT_Y
                MyBase.iCurrentSprite = 0

                ' load player's images from disk
                LoadPlayerImages()

                ' contains a list of shots
                Me._oShots = New cShots

                ' register for collision check
                cCollisionsSystem.RegisterItemForCollision(Me)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub LoadPlayerImages()
            Try
                Dim sPath As String = ConfigurationManager.AppSettings("pathImages")

                ' player's sprites, for level 1
                oSmallStandByLeft = Image.FromFile(sPath + "Mario\Small\StandBy_Left.png", False)      ' stand-by mode
                oSmallStandByRight = Image.FromFile(sPath + "Mario\Small\StandBy_Right.png", False)    ' stand-by mode
                oSmallJumpLeft = Image.FromFile(sPath + "Mario\Small\Jump_Left.png", False)            ' jump mode
                oSmallJumpRight = Image.FromFile(sPath + "Mario\Small\Jump_Right.png", False)          ' jump mode
                lstSmallMoveLeft.Add(Image.FromFile(sPath + "Mario\Small\Move_Left01.png", False))     ' movement animation
                lstSmallMoveLeft.Add(Image.FromFile(sPath + "Mario\Small\Move_Left02.png", False))     ' movement animation
                lstSmallMoveLeft.Add(Image.FromFile(sPath + "Mario\Small\Move_Left03.png", False))     ' movement animation
                lstSmallMoveRight.Add(Image.FromFile(sPath + "Mario\Small\Move_Right01.png", False))   ' movement animation
                lstSmallMoveRight.Add(Image.FromFile(sPath + "Mario\Small\Move_Right02.png", False))   ' movement animation
                lstSmallMoveRight.Add(Image.FromFile(sPath + "Mario\Small\Move_Right03.png", False))   ' movement animation

                ' player's sprites, for level 2
                oNormalStandByLeft = Image.FromFile(sPath + "Mario\Normal\StandBy_Left.png", False)    ' stand-by mode
                oNormalStandByRight = Image.FromFile(sPath + "Mario\Normal\StandBy_Right.png", False)  ' stand-by mode
                oNormalJumpLeft = Image.FromFile(sPath + "Mario\Normal\Jump_Left.png", False)          ' jump mode
                oNormalJumpRight = Image.FromFile(sPath + "Mario\Normal\Jump_Right.png", False)        ' jump mode
                lstNormalMoveLeft.Add(Image.FromFile(sPath + "Mario\Normal\Move_Left01.png", False))   ' movement animation
                lstNormalMoveLeft.Add(Image.FromFile(sPath + "Mario\Normal\Move_Left02.png", False))   ' movement animation
                lstNormalMoveLeft.Add(Image.FromFile(sPath + "Mario\Normal\Move_Left03.png", False))   ' movement animation
                lstNormalMoveRight.Add(Image.FromFile(sPath + "Mario\Normal\Move_Right01.png", False)) ' movement animation
                lstNormalMoveRight.Add(Image.FromFile(sPath + "Mario\Normal\Move_Right02.png", False)) ' movement animation
                lstNormalMoveRight.Add(Image.FromFile(sPath + "Mario\Normal\Move_Right03.png", False)) ' movement animation

                ' player's sprites, for level 3
                oFireStandByLeft = Image.FromFile(sPath + "Mario\Fire\StandBy_Left.png", False)        ' stand-by mode
                oFireStandByRight = Image.FromFile(sPath + "Mario\Fire\StandBy_Right.png", False)      ' stand-by mode
                oFireJumpLeft = Image.FromFile(sPath + "Mario\Fire\Jump_Left.png", False)              ' jump mode
                oFireJumpRight = Image.FromFile(sPath + "Mario\Fire\Jump_Right.png", False)            ' jump mode
                oShotingLeft = Image.FromFile(sPath + "Mario\Fire\Shoting_Left.png", False)            ' shoting
                oShotingRight = Image.FromFile(sPath + "Mario\Fire\Shoting_Right.png", False)          ' shoting
                lstFireMoveLeft.Add(Image.FromFile(sPath + "Mario\Fire\Move_Left01.png", False))       ' movement animation
                lstFireMoveLeft.Add(Image.FromFile(sPath + "Mario\Fire\Move_Left02.png", False))       ' movement animation
                lstFireMoveLeft.Add(Image.FromFile(sPath + "Mario\Fire\Move_Left03.png", False))       ' movement animation
                lstFireMoveRight.Add(Image.FromFile(sPath + "Mario\Fire\Move_Right01.png", False))     ' movement animation
                lstFireMoveRight.Add(Image.FromFile(sPath + "Mario\Fire\Move_Right02.png", False))     ' movement animation
                lstFireMoveRight.Add(Image.FromFile(sPath + "Mario\Fire\Move_Right03.png", False))     ' movement animation

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Reset player status
        ''' </summary>
        Public Sub Reset()

            Me.PositionX = PLAYER_DEFAULT_X
            Me.PositionY = PLAYER_DEFAULT_Y

            Me.eLiveStatus = ePlayerLiveStatus.Small
            Me.eMoveStatus = ePlayerMoveStatus.StandBy
            Me.eJumpStatus = ePlayerJumpStatus.NoJumping
            Me.eJumpType = ePlayerJumpType.Lineal

            MyBase.MoveHorizDir = eMoveHorizDir.Right
            MyBase.MoveVertDir = eMoveVertDir.None

            Me.iInvulnerableTime = 0
            Me.Visible = True

        End Sub

        ''' <summary>
        ''' Horizontal position
        ''' </summary>
        Public Overloads Property PositionX() As Integer
            Get
                Return MyBase.PositionX
            End Get
            Set(ByVal value As Integer)
                If (value > SCREEN_LEFT_GAP) And (value < SCREEN_WIDTH - SCREEN_RIGHT_GAP) Then
                    MyBase.PositionX = value
                    _bScreenGapReached = False
                Else
                    _bScreenGapReached = True
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
                If (value < SCREEN_HEIGHT - PLAYER_HEIGHT_SMALL) Then
                    MyBase.PositionY = value
                Else
                    Me.Destroy()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Player live status (small/normal/fire)
        ''' </summary>
        Public ReadOnly Property LiveStatus() As ePlayerLiveStatus
            Get
                Return eLiveStatus
            End Get
        End Property

        ''' <summary>
        ''' Movement status
        ''' </summary>
        Public ReadOnly Property MoveStatus() As ePlayerMoveStatus
            Get
                Return eMoveStatus
            End Get
        End Property

        ''' <summary>
        ''' Jump status
        ''' </summary>
        Public ReadOnly Property JumpStatus() As ePlayerJumpStatus
            Get
                Return eJumpStatus
            End Get
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
        ''' Player PositionX on minimium/maximium value posible
        ''' </summary>
        Public ReadOnly Property ScreenGapReached() As Boolean
            Get
                Return _bScreenGapReached
            End Get
        End Property

        ''' <summary>
        ''' Current shots of player
        ''' </summary>
        Public ReadOnly Property Shots() As cShots
            Get
                Return _oShots
            End Get
        End Property

        ''' <summary>
        ''' Get player's height
        ''' </summary>
        Public Overrides Function GetHeight() As Integer

            Select Case eLiveStatus
                Case ePlayerLiveStatus.Small : Return PLAYER_HEIGHT_SMALL
                Case ePlayerLiveStatus.Normal : Return PLAYER_HEIGHT_NORMAL
                Case ePlayerLiveStatus.Fire : Return PLAYER_HEIGHT_FIRE
                Case Else : Return PLAYER_HEIGHT_SMALL
            End Select

        End Function

        ''' <summary>
        ''' Get player's width
        ''' </summary>
        Public Overrides Function GetWidth() As Integer
            Return PLAYER_WIDTH
        End Function

        ''' <summary>
        ''' Returns a Rect struct with the current location of player
        ''' </summary>
        Public Function GetPositionRectangle() As Rectangle Implements iDrawable.GetPositionRectangle
            Return New Rectangle(PositionX, PositionY, GetWidth(), GetHeight())
        End Function

        ''' <summary>
        ''' Move to left
        ''' </summary>
        Public Sub MoveLeft()

            MyBase.MoveHorizDir = eMoveHorizDir.Left
            If eMoveStatus = ePlayerMoveStatus.StandBy Then eMoveStatus = ePlayerMoveStatus.Walking

            PositionX -= CType(IIf(eMoveStatus = ePlayerMoveStatus.Running, PLAYER_HORIZ_RUN_MOV, PLAYER_HORIZ_WALK_MOV), Int32)

        End Sub

        ''' <summary>
        ''' Move to right
        ''' </summary>
        Public Sub MoveRight()

            MyBase.MoveHorizDir = eMoveHorizDir.Right
            If eMoveStatus = ePlayerMoveStatus.StandBy Then eMoveStatus = ePlayerMoveStatus.Walking

            PositionX += CType(IIf(eMoveStatus = ePlayerMoveStatus.Running, PLAYER_HORIZ_RUN_MOV, PLAYER_HORIZ_WALK_MOV), Int32)

        End Sub

        ''' <summary>
        ''' Move to up
        ''' </summary>
        Public Sub MoveUp()
            PositionY -= PLAYER_VERT_MOVEMENT
        End Sub

        ''' <summary>
        ''' Move to down
        ''' </summary>
        Public Sub MoveDown()
            PositionY += PLAYER_VERT_MOVEMENT
        End Sub

        ''' <summary>
        ''' Increase player status
        ''' </summary>
        Public Sub GrowUpStatus()

            If eLiveStatus = ePlayerLiveStatus.Small Then
                'Me.Visible = False
                PositionY -= PLAYER_HEIGHT_SMALL
                eLiveStatus = ePlayerLiveStatus.Normal
                ' mejorar -->
                'cParticlesSystem.RegisterEfect(New cMarioNormalEffect(Me.PositionX, Me.PositionY, Me.MoveHorizDir)) ' display grow effect

            ElseIf eLiveStatus = ePlayerLiveStatus.Normal Then
                eLiveStatus = ePlayerLiveStatus.Fire

            ElseIf eLiveStatus = ePlayerLiveStatus.Fire Then
                eLiveStatus = ePlayerLiveStatus.Fire
            End If

        End Sub

        ''' <summary>
        ''' Reduce player status
        ''' </summary>
        Public Sub BringDownStatus()

            ' Invulnerable for a brief period of time
            SetInvulnerableMode(INVULNERABLE_TIME)

            If eLiveStatus = ePlayerLiveStatus.Fire Then
                eLiveStatus = ePlayerLiveStatus.Normal

            ElseIf eLiveStatus = ePlayerLiveStatus.Normal Then
                eLiveStatus = ePlayerLiveStatus.Small
                PositionY -= PLAYER_HEIGHT_SMALL

            ElseIf eLiveStatus = ePlayerLiveStatus.Small Then
                Me.Visible = False
                cParticlesSystem.RegisterEfect(New cMarioDeadEffect(Me.PositionX, Me.PositionY)) ' display mario dead effect
                ProgramPlayerDead(2500) ' wait a second and raise dead event
            End If

        End Sub

        ''' <summary>
        ''' Draw the player's current sprite
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics) Implements iDrawable.Draw

            If Not _bVisible Then Return ' avoid draw if player is invisible

            ' shoting
            If bShotingStatus AndAlso eMoveStatus = ePlayerMoveStatus.StandBy Then
                bShotingStatus = False
                oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, oShotingLeft, oShotingRight), Image)
                oGraphics.DrawImage(oImageAux, PositionX, PositionY, GetWidth(), GetHeight()) ' draw player image
                Return
            End If

            ' jumping
            If eJumpStatus <> ePlayerJumpStatus.NoJumping Then
                Select Case eLiveStatus
                    Case ePlayerLiveStatus.Small : oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, oSmallJumpLeft, oSmallJumpRight), Image)
                    Case ePlayerLiveStatus.Normal : oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, oNormalJumpLeft, oNormalJumpRight), Image)
                    Case ePlayerLiveStatus.Fire : oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, oFireJumpLeft, oFireJumpRight), Image)
                End Select
                oGraphics.DrawImage(oImageAux, PositionX, PositionY, GetWidth(), GetHeight()) ' draw player image
                Return
            End If

            ' no jumping
            Select Case eLiveStatus
                Case ePlayerLiveStatus.Small
                    Select Case eMoveStatus
                        Case ePlayerMoveStatus.StandBy
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, oSmallStandByLeft, oSmallStandByRight), Image)
                        Case ePlayerMoveStatus.Walking
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, lstSmallMoveLeft(iCurrentSprite), lstSmallMoveRight(iCurrentSprite)), Image)
                        Case ePlayerMoveStatus.Running
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, lstSmallMoveLeft(iCurrentSprite), lstSmallMoveRight(iCurrentSprite)), Image)
                    End Select

                Case ePlayerLiveStatus.Normal
                    Select Case eMoveStatus
                        Case ePlayerMoveStatus.StandBy
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, oNormalStandByLeft, oNormalStandByRight), Image)
                        Case ePlayerMoveStatus.Walking
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, lstNormalMoveLeft(iCurrentSprite), lstNormalMoveRight(iCurrentSprite)), Image)
                        Case ePlayerMoveStatus.Running
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, lstNormalMoveLeft(iCurrentSprite), lstNormalMoveRight(iCurrentSprite)), Image)
                    End Select

                Case ePlayerLiveStatus.Fire
                    Select Case eMoveStatus
                        Case ePlayerMoveStatus.StandBy
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, oFireStandByLeft, oFireStandByRight), Image)
                        Case ePlayerMoveStatus.Walking
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, lstFireMoveLeft(iCurrentSprite), lstFireMoveRight(iCurrentSprite)), Image)
                        Case ePlayerMoveStatus.Running
                            oImageAux = CType(IIf(MoveHorizDir = eMoveHorizDir.Left, lstFireMoveLeft(iCurrentSprite), lstFireMoveRight(iCurrentSprite)), Image)
                    End Select
            End Select

            ' check change current sprite of animation
            If eMoveStatus = ePlayerMoveStatus.Walking Then
                CheckNextMoveSprite(TOTAL_ANIM_SPRITES, NORMAL_FRAMES_PER_STEP)
            ElseIf eMoveStatus = ePlayerMoveStatus.Running Then
                CheckNextMoveSprite(TOTAL_ANIM_SPRITES, RUN_FRAMES_PER_STEP)
            End If

            ' draw correct player image
            oGraphics.DrawImage(oImageAux, PositionX, PositionY, GetWidth(), GetHeight())

        End Sub

        ''' <summary>
        ''' Jump
        ''' </summary>
        Public Sub ActivateJump()
            If eJumpStatus = ePlayerJumpStatus.NoJumping Then eJumpStatus = ePlayerJumpStatus.JumpRisingPhase
        End Sub

        ''' <summary>
        ''' Change player's position to perform the 'jump' movement
        ''' </summary>
        Public Sub CheckJumpMovement()

            If eJumpStatus = ePlayerJumpStatus.NoJumping Then ' avoid check if player is not jumping
                Return

            ElseIf eJumpStatus = ePlayerJumpStatus.JumpRisingPhase Then ' rising phase
                iJumpStep += 1 ' increase current step
                PositionY -= CalculateJumpMovement(ePlayerJumpType.Lineal) ' player movement

                ' check next phase
                If (iJumpStep = NORMAL_JUMP_STEPS) Or (Not cCollisionsSystem.CheckValidMovement(Me, True, PLAYER_VERT_MOVEMENT, eMoveVertDir.Up)) Then
                    iJumpStep = 0
                    eJumpStatus = ePlayerJumpStatus.JumpLoweringPhase
                End If
            End If

        End Sub

        ''' <summary>
        ''' Calculate the current Jump's vertical movement
        ''' </summary>
        ''' <param name="eJumpType">Type desired motion (lineal/sinusoidal)</param>
        Private Function CalculateJumpMovement(ByVal eJumpType As ePlayerJumpType) As Integer

            If eJumpType = ePlayerJumpType.Lineal Then
                Return PLAYER_VERT_MOVEMENT * 2

            ElseIf eJumpType = ePlayerJumpType.Sinusoidal Then
                Dim dAux As Double
                dAux = (iJumpStep * 90 / NORMAL_JUMP_STEPS) ' value between 0 to 90
                dAux = 90 - dAux ' value between 90 to 0
                dAux = dAux * Math.PI / 180 ' degree to radians convertion
                dAux = Math.Sin(dAux) ' value between 1 to 0
                Return Convert.ToInt32(dAux * PLAYER_VERT_MOVEMENT) * 2
            End If

        End Function

        ''' <summary>
        ''' Adjust player's position acording to gravity (if necesary)
        ''' </summary>
        Public Sub CheckGravityMovement()

            If cCollisionsSystem.CheckValidMovement(Me, False, PLAYER_VERT_MOVEMENT, eMoveVertDir.Down) Then ' below player's position is empty
                PositionY += PLAYER_VERT_MOVEMENT ' falling down
            Else
                ' space below player is less than PLAYER_VERT_MOVEMENT
                If eJumpStatus = ePlayerJumpStatus.JumpLoweringPhase Then eJumpStatus = ePlayerJumpStatus.NoJumping ' disable jump

                ' try 1 pixels by time until reach the floor
                byGap = PLAYER_VERT_MOVEMENT
                Do
                    If cCollisionsSystem.CheckValidMovement(Me, True, byGap, eMoveVertDir.Down) Then
                        PositionY += byGap ' adjust player's position to floor
                        Exit Do
                    End If
                    byGap -= 1
                Loop While byGap > 0
            End If

        End Sub

        ''' <summary>
        ''' Set player to StandBy mode
        ''' </summary>
        Public Sub SetStandBy()
            eMoveStatus = ePlayerMoveStatus.StandBy
        End Sub

        ''' <summary>
        ''' Activate Run/Fire
        ''' </summary>
        Public Sub ActivateRunFire()

            ' run status
            If eMoveStatus = ePlayerMoveStatus.Walking Then eMoveStatus = ePlayerMoveStatus.Running

            ' shoting status
            If eLiveStatus = ePlayerLiveStatus.Fire Then
                bShotingStatus = True
                If Me.MoveHorizDir = eMoveHorizDir.Left Then
                    _oShots.CreateNewShot(Me.PositionX - 22, Me.PositionY + 20, Me.MoveHorizDir) ' fire to left
                Else
                    _oShots.CreateNewShot(Me.PositionX + 44, Me.PositionY + 20, Me.MoveHorizDir) ' fire to right
                End If

            End If

        End Sub

        ''' <summary>
        ''' Disable Run/Fire
        ''' </summary>
        Public Sub DisableRunFire()
            If eMoveStatus = ePlayerMoveStatus.Running Then eMoveStatus = ePlayerMoveStatus.Walking
        End Sub

        ''' <summary>
        ''' Player die. Raise MonsterDie event, etc
        ''' </summary>
        Private Sub Destroy()
            RaiseEvent PlayerDie(cGameControl.eDieReason.FallDown)
        End Sub

        ''' <summary>
        ''' Program to enable the monster after received time passed
        ''' </summary>
        ''' <param name="iMiliSeconds">Wait time to raise, in miliseconds</param>
        Public Sub ProgramPlayerDead(ByVal iMiliSeconds As Integer)

            tmrDead = New Timer(iMiliSeconds)
            AddHandler tmrDead.Elapsed, AddressOf PlayerDead
            tmrDead.Start()

        End Sub

        ''' <summary>
        ''' Raise PlayerDie event
        ''' </summary>
        Private Sub PlayerDead(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            tmrDead.Stop()
            tmrDead.Close()
            RemoveHandler tmrDead.Elapsed, AddressOf PlayerDead

            RaiseEvent PlayerDie(cGameControl.eDieReason.MonsterHit)

        End Sub

        ''' <summary>
        ''' Setup invulnerable mode for the received time
        ''' </summary>
        ''' <param name="iSeconds">Invulnerable time in seconds</param>
        Private Sub SetInvulnerableMode(ByVal iSeconds As Integer)

            If iSeconds < 1 Then Return ' validation

            Me.CollisionCheck = False ' disable collision
            If iInvulnerableTime <= 0 Then ' create a new 1 second timer
                Me.tmrInvulnerable = New Timer(1000) ' 1 second tick
                AddHandler tmrInvulnerable.Elapsed, AddressOf UndoInvulnerableMode
                Me.tmrInvulnerable.Start()
            End If
            Me.iInvulnerableTime += iSeconds ' countdown time

        End Sub

        ''' <summary>
        ''' Check if countdown reach zero
        ''' </summary>
        Private Sub UndoInvulnerableMode(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)

            iInvulnerableTime -= 1 ' decrease countdown

            If iInvulnerableTime <= 0 Then
                Me.tmrInvulnerable.Stop()
                Me.tmrInvulnerable.Close()
                RemoveHandler tmrInvulnerable.Elapsed, AddressOf UndoInvulnerableMode
                Me.CollisionCheck = True
            End If

        End Sub

        ''' <summary>
        ''' Invoked on collision with another entity
        ''' </summary>
        ''' <param name="oCollision">Entity object agains the collision was produced</param>
        Public Overrides Sub CollisionedBy(ByVal oCollision As cCollisionBase)

            ' collision against cMonster: BringDown/GrowUp player status
            If oCollision.GetType.Equals(Type.GetType("Entities.SuperMarioNet.Entities.cMonster")) Then ' player hit a monster
                Dim oMonster As cMonster = CType(oCollision, cMonster)

                If oMonster.MonsterType = cMonster.eMonsterType.BadMushroom Or oMonster.MonsterType = cMonster.eMonsterType.Turtle Then
                    Me.BringDownStatus()

                ElseIf oMonster.MonsterType = cMonster.eMonsterType.GoodMushroom Or oMonster.MonsterType = cMonster.eMonsterType.Flower Then
                    Me.GrowUpStatus()
                End If
            End If
        End Sub

    End Class
End Namespace
