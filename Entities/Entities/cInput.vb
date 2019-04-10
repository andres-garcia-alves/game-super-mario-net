Option Strict On

Namespace SuperMarioNet.Aux

    Public Class cInput

        Public Enum eInputType
            Keyboard = 0
            Joystick = 1
        End Enum

        Private _eType As eInputType = eInputType.Keyboard

        Private _bLeft As Boolean = False
        Private _bUp As Boolean = False
        Private _bRight As Boolean = False
        Private _bDown As Boolean = False
        Private _bJump As Boolean = False
        Private _bRunFire As Boolean = False
        Private _bFPS As Boolean = False
        Private _bEsc As Boolean = False

        Public Property InputType() As eInputType
            Get
                Return _eType
            End Get
            Set(ByVal value As eInputType)
                _eType = value
            End Set
        End Property

        Public Property LeftKey() As Boolean
            Get
                Return _bLeft
            End Get
            Set(ByVal value As Boolean)
                _bLeft = value
            End Set
        End Property

        Public Property UpKey() As Boolean
            Get
                Return _bUp
            End Get
            Set(ByVal value As Boolean)
                _bUp = value
            End Set
        End Property

        Public Property RightKey() As Boolean
            Get
                Return _bRight
            End Get
            Set(ByVal value As Boolean)
                _bRight = value
            End Set
        End Property

        Public Property DownKey() As Boolean
            Get
                Return _bDown
            End Get
            Set(ByVal value As Boolean)
                _bDown = value
            End Set
        End Property

        Public Property JumpKey() As Boolean
            Get
                Return _bJump
            End Get
            Set(ByVal value As Boolean)
                _bJump = value
            End Set
        End Property

        Public Property RunFireKey() As Boolean
            Get
                Return _bRunFire
            End Get
            Set(ByVal value As Boolean)
                _bRunFire = value
            End Set
        End Property

        Public Property FpsKey() As Boolean
            Get
                Return _bFPS
            End Get
            Set(ByVal value As Boolean)
                _bFPS = value
            End Set
        End Property

        Public Property EscKey() As Boolean
            Get
                Return _bEsc
            End Get
            Set(ByVal value As Boolean)
                _bEsc = value
            End Set
        End Property

        Public Function Reset() As Boolean
            Me._bLeft = False
            Me._bUp = False
            Me._bRight = False
            Me._bDown = False
            Me._bJump = False
            Me._bRunFire = False
            Me._bFPS = False
            Me._bEsc = False
        End Function

    End Class
End Namespace
