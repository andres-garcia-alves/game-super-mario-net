Option Strict On
Imports System.Configuration
Imports System.Drawing

Imports SuperMarioNet.Entities.Base
Imports SuperMarioNet.Entities.Interfaces
Imports SuperMarioNet.Entities.ParticleSystem

Namespace SuperMarioNet.Entities.Entities

    Public Class cStaticObject
        Inherits cCollisionBase
        Implements iDrawable

        Public Event GoalReached()
        Public Event CreateReward(ByRef oPlayer As cPlayer, ByVal iPosX As Integer, ByVal iPosY As Integer)

        Public Enum eObjectType
            Floor = 0
            NormalBrick = 1
            NormalRewardBrick = 2
            CoinBrick = 3
            CoinRewardBrick = 4
            SolidBlock = 5
            PipeSmall = 6
            PipeMedium = 7
            PipeLarge = 8
            Goal = 9
        End Enum

        Public Enum eDrawType
            StaticImage = 0
            Animation = 1
            FillRectangle = 2
        End Enum

        Private Const BRICK_WIDTH As Integer = 44
        Private Const BRICK_HEIGHT As Integer = 36
        Private Const GOAL_WIDTH As Integer = 44
        Private Const GOAL_HEIGHT As Integer = 324
        Private Const PIPE_SMALL_WIDTH As Integer = 88
        Private Const PIPE_SMALL_HEIGHT As Integer = 72
        Private Const PIPE_MEDIUM_WIDTH As Integer = 88
        Private Const PIPE_MEDIUM_HEIGHT As Integer = 108
        Private Const PIPE_LARGE_WIDTH As Integer = 88
        Private Const PIPE_LARGE_HEIGHT As Integer = 144

        Private Const NORMAL_BRICK_POINTS As Integer = 50
        Private Const COIN_BRICK_POINTS As Integer = 200

        Private Const FRAMES_PER_STEP As Integer = 6
        Private Const TOTAL_ANIM_SPRITES As Integer = 6

        Private ReadOnly _iWidth As Integer
        Private ReadOnly _iHeight As Integer
        Private _bVisible As Boolean = True
        Private ReadOnly _eObject As eObjectType

        Private ReadOnly oImage As Image
        Private ReadOnly lstImages As New List(Of Image)

        Private eDraw As eDrawType = eDrawType.StaticImage

        Private ReadOnly bStartUpVisible As Boolean ' used on reset
        Private ReadOnly eStartUpDrawType As eDrawType ' used on reset
        Private ReadOnly iStartUpPosX As Integer
        Private ReadOnly iStartUpPosY As Integer

        Public Sub New()
        End Sub

        ''' <summary>
        ''' Normal constructor. Set object's default Width and Height
        ''' </summary>
        Public Sub New(ByVal eObjType As eObjectType, ByVal bVisible As Boolean, ByVal iPosX As Integer, ByVal iPosY As Integer)
            Try
                Dim sPath As String = ConfigurationManager.AppSettings("pathImages")

                MyBase.PositionX = iPosX
                MyBase.PositionY = iPosY

                MyBase.iCurrentSprite = 0
                MyBase.MoveHorizDir = eMoveHorizDir.None

                Me._eObject = eObjType
                Me.Visible = bVisible

                Me.iStartUpPosX = iPosX ' used on reset
                Me.iStartUpPosY = iPosY ' used on reset
                Me.bStartUpVisible = bVisible ' used on reset

                ' sprites for object image
                Select Case _eObject
                    Case eObjectType.NormalBrick
                        Me._iWidth = BRICK_WIDTH
                        Me._iHeight = BRICK_HEIGHT
                        Me.oImage = Image.FromFile(sPath + "Objects\Normal_Brick.png", False)
                        Me.eStartUpDrawType = eDrawType.StaticImage ' used on reset

                    Case eObjectType.NormalRewardBrick
                        Me._iWidth = BRICK_WIDTH
                        Me._iHeight = BRICK_HEIGHT
                        Me.oImage = Image.FromFile(sPath + "Objects\Normal_Brick.png", False)
                        Me.eStartUpDrawType = eDrawType.StaticImage ' used on reset

                    Case eObjectType.CoinBrick
                        Me._iWidth = BRICK_WIDTH
                        Me._iHeight = BRICK_HEIGHT
                        Me.eDraw = eDrawType.Animation
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_01.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_01.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_02.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_03.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_02.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_01.png", False))
                        Me.oImage = Image.FromFile(sPath + "Objects\Coin_Brick_04.png", False) ' used after its destruction
                        Me.eStartUpDrawType = eDrawType.Animation ' used on reset

                    Case eObjectType.CoinRewardBrick
                        Me._iWidth = BRICK_WIDTH
                        Me._iHeight = BRICK_HEIGHT
                        Me.eDraw = eDrawType.Animation
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_01.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_01.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_02.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_03.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_02.png", False))
                        Me.lstImages.Add(Image.FromFile(sPath + "Objects\Coin_Brick_01.png", False))
                        Me.oImage = Image.FromFile(sPath + "Objects\Coin_Brick_04.png", False) ' used after its destruction
                        Me.eStartUpDrawType = eDrawType.Animation ' used on reset

                    Case eObjectType.SolidBlock
                        Me._iWidth = BRICK_WIDTH
                        Me._iHeight = BRICK_HEIGHT
                        Me.oImage = Image.FromFile(sPath + "Objects\Solid_Block.png", False)
                        Me.eStartUpDrawType = eDrawType.StaticImage ' used on reset

                    Case eObjectType.PipeSmall
                        Me._iWidth = PIPE_SMALL_WIDTH
                        Me._iHeight = PIPE_SMALL_HEIGHT
                        Me.oImage = Image.FromFile(sPath + "Objects\Pipe_Small.png", False)
                        Me.eStartUpDrawType = eDrawType.StaticImage ' used on reset

                    Case eObjectType.PipeMedium
                        Me._iWidth = PIPE_MEDIUM_WIDTH
                        Me._iHeight = PIPE_MEDIUM_HEIGHT
                        Me.oImage = Image.FromFile(sPath + "Objects\Pipe_Medium.png", False)
                        Me.eStartUpDrawType = eDrawType.StaticImage ' used on reset

                    Case eObjectType.PipeLarge
                        Me._iWidth = PIPE_LARGE_WIDTH
                        Me._iHeight = PIPE_LARGE_HEIGHT
                        Me.oImage = Image.FromFile(sPath + "Objects\Pipe_Large.png", False)
                        Me.eStartUpDrawType = eDrawType.StaticImage ' used on reset

                    Case eObjectType.Goal
                        Me._iWidth = GOAL_WIDTH
                        Me._iHeight = GOAL_HEIGHT
                        Me.oImage = Image.FromFile(sPath + "Objects\Goal.png", False)
                        Me.eStartUpDrawType = eDrawType.StaticImage ' used on reset
                        Me.OverPassable = True ' player can pass throught the object
                End Select

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Constructor for 'Floor' ObjectType
        ''' </summary>
        Public Sub New(ByVal eObjType As eObjectType, ByVal bVisible As Boolean, ByVal iPosX As Integer, ByVal iPosY As Integer, ByVal iWidth As Integer, ByVal iHeight As Integer)

            If eObjType <> eObjectType.Floor Then
                Throw New ApplicationException("This construtor received a " & eObjType.ToString() & " ObjectType and is valid only for 'Floor' ObjectType.")
            End If

            MyBase.PositionX = iPosX
            MyBase.PositionY = iPosY

            MyBase.iCurrentSprite = 0
            MyBase.MoveHorizDir = eMoveHorizDir.None

            Me._eObject = eObjectType.Floor
            Me._iWidth = iWidth
            Me._iHeight = iHeight

            Me.Visible = bVisible
            Me.eDraw = eDrawType.FillRectangle

            Me.iStartUpPosX = iPosX
            Me.iStartUpPosY = iPosY

        End Sub

        ''' <summary>
        ''' Reset static object status
        ''' </summary>
        Public Sub Reset()
            Try
                MyBase.PositionX = iStartUpPosX
                MyBase.PositionY = iStartUpPosY

                Me.eDraw = eStartUpDrawType
                Me.Visible = bStartUpVisible

                MyBase.iCurrentSprite = 0
                MyBase.MoveHorizDir = eMoveHorizDir.None

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

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
        ''' Object Type
        ''' </summary>
        Public ReadOnly Property ObjectType() As eObjectType
            Get
                Return _eObject
            End Get
        End Property

        ''' <summary>
        ''' Draw Type (StaticImage/Animation/FilledRectangle)
        ''' </summary>
        Public ReadOnly Property DrawType() As eDrawType
            Get
                Return Me.eDraw
            End Get
        End Property

        ''' <summary>
        ''' Get object's width
        ''' </summary>
        Public Overrides Function GetWidth() As Integer
            Return _iWidth
        End Function

        ''' <summary>
        ''' Get object's height
        ''' </summary>
        Public Overrides Function GetHeight() As Integer
            Return _iHeight
        End Function

        ''' <summary>
        ''' Returns a Rect struct with the current location of object
        ''' </summary>
        Public Function GetPositionRectangle() As Rectangle Implements iDrawable.GetPositionRectangle
            Return New Rectangle(PositionX, PositionY, GetWidth(), GetHeight())
        End Function

        ''' <summary>
        ''' Draw the Object's current sprite
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics) Implements iDrawable.Draw

            If Not _bVisible Then Return ' avoid draw invisible objects

            If eDraw = eDrawType.StaticImage Then ' image
                oGraphics.DrawImage(oImage, PositionX, PositionY, GetWidth(), GetHeight())

            ElseIf eDraw = eDrawType.Animation Then
                oGraphics.DrawImage(lstImages(iCurrentSprite), PositionX, PositionY, GetWidth(), GetHeight())
                CheckNextMoveSprite(TOTAL_ANIM_SPRITES, FRAMES_PER_STEP)

            ElseIf eDraw = eDrawType.FillRectangle Then ' rectangle
                oGraphics.FillRectangle(Brushes.Red, PositionX, PositionY, GetWidth(), GetHeight())
            End If

        End Sub

        ''' <summary>
        ''' Invoked on collision with another entity
        ''' </summary>
        ''' <param name="oCollision">Entity object agains the collision was produced</param>
        Public Overrides Sub CollisionedBy(ByVal oCollision As cCollisionBase)

            If oCollision.GetType.Equals(Type.GetType("SuperMarioNet.Entities.Entities.cPlayer")) Then ' hitted by the player
                Dim oPlayer As cPlayer = CType(oCollision, cPlayer)

                ' destroy StaticObject, remove from cCollisionSystem, add ScorePoints, make object invisible, etc
                Select Case ObjectType

                    Case eObjectType.NormalBrick
                        If oPlayer.LiveStatus = cPlayer.ePlayerLiveStatus.Small Then
                            cParticlesSystem.RegisterEfect(New cNormalBrickNoDestroyEffect(Me.PositionX, Me.PositionY))
                        Else
                            Visible = False ' make brick invisible
                            cGameControl.AddScorePoints(NORMAL_BRICK_POINTS) ' add points
                            'cCollisionsSystem.RemoveItemForCollision(Me) ' remove brick from collision detection
                            Me.ToRemoveFromCollision = True
                            cParticlesSystem.RegisterEfect(New cNormalBrickDestroyEffect(Me.PositionX, Me.PositionY)) ' display destroy effect
                        End If

                    Case eObjectType.CoinBrick
                        If eDraw = eDrawType.StaticImage Then Return
                        eDraw = eDrawType.StaticImage ' show destroyed image
                        cGameControl.AddScorePoints(COIN_BRICK_POINTS) ' add points
                        cGameControl.AddCoins(1) ' add one coin to counter
                        cParticlesSystem.RegisterEfect(New cCoinBrickDestroyEffect(Me.PositionX, Me.PositionY)) ' display destroy effect

                    Case eObjectType.CoinRewardBrick
                        If eDraw = eDrawType.StaticImage Then Return
                        eDraw = eDrawType.StaticImage ' show destroyed image
                        cGameControl.AddScorePoints(COIN_BRICK_POINTS) ' add points
                        cGameControl.AddCoins(1) ' add one coin to counter
                        RaiseEvent CreateReward(oPlayer, Me.PositionX, Me.PositionY - (BRICK_HEIGHT + 2)) ' create new good mushroom
                        If oPlayer.LiveStatus = cPlayer.ePlayerLiveStatus.Small Then
                            cParticlesSystem.RegisterEfect(New cGoodMushroomAppearEffect(Me.PositionX, Me.PositionY - BRICK_HEIGHT)) ' display mushroom appear effect
                        Else
                            cParticlesSystem.RegisterEfect(New cFlowerAppearEffect(Me.PositionX, Me.PositionY - BRICK_HEIGHT)) ' display flower appear effect
                        End If

                    Case eObjectType.Goal
                        RaiseEvent GoalReached() ' raise event
                        cParticlesSystem.RegisterEfect(New cGoalReachedEffect(Me.PositionX, Me.PositionY)) ' display effect 

                End Select
            End If

        End Sub

    End Class
End Namespace
