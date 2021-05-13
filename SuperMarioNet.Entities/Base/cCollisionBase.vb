Option Strict On

Namespace SuperMarioNet.Entities.Base

    <Serializable()>
    Public MustInherit Class cCollisionBase

        Public Const SCREEN_WIDTH As Integer = 640
        Public Const SCREEN_HEIGHT As Integer = 504

        Public Enum eMoveHorizDir As Byte
            None = 0
            Left = 1
            Right = 2
        End Enum

        Public Enum eMoveVertDir As Byte
            None = 0
            Up = 1
            Down = 2
        End Enum

        Private _iPositionX As Integer = 0
        Private _iPositionY As Integer = 0
        Private _bCollisionCheck As Boolean = True
        Private _bOverPassable As Boolean = False
        Private _eMoveHorizDir As eMoveHorizDir = eMoveHorizDir.None
        Private _eMoveVertDir As eMoveVertDir = eMoveVertDir.None
        Private _bToRemoveFromCollision As Boolean = False

        Protected iCurrentFrame As Integer = 0
        Protected iCurrentSprite As Integer = 0

        Public Property PositionX() As Integer
            Get
                Return _iPositionX
            End Get
            Set(ByVal value As Integer)
                _iPositionX = value
            End Set
        End Property

        Public Property PositionY() As Integer
            Get
                Return _iPositionY
            End Get
            Set(ByVal value As Integer)
                _iPositionY = value
            End Set
        End Property

        Public Property CollisionCheck() As Boolean
            Get
                Return _bCollisionCheck
            End Get
            Set(ByVal value As Boolean)
                _bCollisionCheck = value
            End Set
        End Property

        Public Property OverPassable() As Boolean
            Get
                Return _bOverPassable
            End Get
            Set(ByVal value As Boolean)
                _bOverPassable = value
            End Set
        End Property

        Public Property MoveHorizDir() As eMoveHorizDir
            Get
                Return _eMoveHorizDir
            End Get
            Set(ByVal value As eMoveHorizDir)
                _eMoveHorizDir = value
            End Set
        End Property

        Public Property MoveVertDir() As eMoveVertDir
            Get
                Return _eMoveVertDir
            End Get
            Set(ByVal value As eMoveVertDir)
                _eMoveVertDir = value
            End Set
        End Property

        Public Property ToRemoveFromCollision() As Boolean
            Get
                Return _bToRemoveFromCollision
            End Get
            Set(ByVal value As Boolean)
                _bToRemoveFromCollision = value
            End Set
        End Property

        Public MustOverride Function GetWidth() As Integer
        Public MustOverride Function GetHeight() As Integer
        Public MustOverride Sub CollisionedBy(ByVal oCollision As cCollisionBase)

        ''' <summary>
        ''' Check the change of the animation sprite
        ''' </summary>
        Protected Sub CheckNextMoveSprite(ByVal TotalAnimSteps As Integer, ByVal iFramesPerStep As Integer)

            iCurrentFrame += 1

            If (iCurrentFrame >= iFramesPerStep) Then
                iCurrentFrame = 0
                iCurrentSprite += 1
            End If

            If (iCurrentSprite >= TotalAnimSteps) Then iCurrentSprite = 0

        End Sub

    End Class
End Namespace