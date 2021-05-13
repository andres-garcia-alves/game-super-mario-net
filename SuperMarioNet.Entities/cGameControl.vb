Option Strict On
Imports System.Configuration
Imports System.Windows.Forms

Imports SuperMarioNet.Miscellaneous

Namespace SuperMarioNet.Entities

    Public Class cGameControl

        Public Enum eGameMode
            Normal = 0
            Debug = 1
        End Enum

        Public Enum eDieReason
            MonsterHit = 0
            FallDown = 1
            TimeUp = 2
        End Enum

        Private Const MAX_LEVEL As Integer = 10
        Private Const CANT_LIVES As Integer = 3
        Private Const TIME_PER_LEVEL As Integer = 400
        Private Const CICLES_PER_TICK As Integer = 13

        Private _iWorld As Byte = 1
        Private _iLevel As Byte = 1
        Private _iLives As Integer = CANT_LIVES
        Private _iTime As Integer = TIME_PER_LEVEL
        Private ReadOnly _iMaxLevels As Integer = MAX_LEVEL
        Private _eType As eGameMode = eGameMode.Normal

        Private Shared _iCoins As Byte = 0
        Private Shared _iScore As Integer = 0

        Private iCicles As Integer = 0
        Private bPlayerDead As Boolean = False
        Private bLevelCompleted As Boolean = False

        Public Sub New()
            Try
                Dim iAux As Integer

                iAux = Convert.ToInt32(ConfigurationManager.AppSettings("lives"))
                If (iAux >= 1 And iAux <= 5) Then
                    _iLives = iAux
                Else
                    Throw New ApplicationException("Lives value in 'App.config' file must be between 1 to 5. Using default value: " & CANT_LIVES & " lives.")
                End If

                iAux = Convert.ToInt32(ConfigurationManager.AppSettings("levels"))
                If (iAux > 0) Then
                    _iMaxLevels = iAux
                Else
                    Throw New ApplicationException("Levels value in 'App.config' file must be greather then 0. Using default value: " & MAX_LEVEL & " levels.")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End Sub

        Public Property GameMode() As eGameMode
            Get
                Return _eType
            End Get
            Set(ByVal value As eGameMode)
                _eType = value
            End Set
        End Property

        Public Property Lives() As Integer
            Get
                Return _iLives
            End Get
            Set(ByVal value As Integer)
                _iLives = value
            End Set
        End Property

        Public Property World() As Byte
            Get
                Return _iWorld
            End Get
            Set(ByVal value As Byte)
                _iWorld = value
            End Set
        End Property

        Public Property Level() As Byte
            Get
                Return _iLevel
            End Get
            Set(ByVal value As Byte)
                _iLevel = value
            End Set
        End Property

        Public Property Score() As Integer
            Get
                Return _iScore
            End Get
            Set(ByVal value As Integer)
                _iScore = value
            End Set
        End Property

        Public Property Coins() As Byte
            Get
                Return _iCoins
            End Get
            Set(ByVal value As Byte)
                _iCoins = value
            End Set
        End Property

        Public ReadOnly Property Time() As Integer
            Get
                Return _iTime
            End Get
        End Property

        Public ReadOnly Property MaxLevels() As Integer
            Get
                Return _iMaxLevels
            End Get
        End Property

        Public Shared Sub AddScorePoints(ByVal iPoints As Integer)
            _iScore += iPoints
        End Sub

        Public Shared Sub AddCoins(ByVal iCoins As Byte)
            _iCoins += iCoins
        End Sub

        Public Sub ResetScore()
            _iScore = 0
        End Sub

        ''' <summary>
        ''' Check if is necesary to reduce game time left
        ''' </summary>
        Public Sub CheckTimeTick()

            iCicles += 1

            If iCicles = CICLES_PER_TICK Then
                iCicles = 0
                _iTime -= 1 ' decreace time remaining
            End If

            If _iTime < 0 Then
                DecreasePlayerLive() ' eDieReason.TimeUp
                ResetTime()
            End If

        End Sub

        ''' <summary>
        ''' Reset map time left
        ''' </summary>
        Public Sub ResetTime()
            _iTime = 400
        End Sub

        ''' <summary>
        ''' Reduce player's lives
        ''' </summary>
        Public Sub DecreasePlayerLive() ' ByVal eReason As eDieReason

            _iLives -= 1
            bPlayerDead = True

            'Select Case eReason
            '    Case eDieReason.TimeUp
            'MessageBox.Show("Time Up. " & _iLives.ToString() & " lives remaining.", "Mario is dead", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Case eDieReason.FallDown
            'MessageBox.Show("Falling Down from Screen. " & _iLives.ToString() & " lives remaining.", "Mario is dead", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Case eDieReason.MonsterHit
            'MessageBox.Show("Killed by a Monster. " & _iLives.ToString() & " lives remaining.", "Mario is dead", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'End Select

        End Sub

        ''' <summary>
        ''' Check if player have lose a live
        ''' </summary>
        Public Function CheckLiveLose() As Boolean

            If bPlayerDead = True Then
                bPlayerDead = False
                Return True
            Else
                Return False
            End If

        End Function

        ''' <summary>
        ''' Check if no lives remaing
        ''' </summary>
        Public Function CheckNoLivesRemaining() As Boolean

            If _iLives <= 0 Then
                Dim sTitle As String = cLanguaje.GetTextElement("frmGameNoLive", 0)
                Dim sMsg As String = cLanguaje.GetTextElement("frmGameNoLive", 1)
                MessageBox.Show(sMsg, sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return True
            Else
                Return False
            End If

        End Function

        ''' <summary>
        ''' Mark level as finished
        ''' </summary>
        Public Sub FinishLevel()

            bLevelCompleted = True
            MessageBox.Show("Level completed.", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Sub

        ''' <summary>
        ''' Check if level is completed
        ''' </summary>
        Public Function CheckLevelComplete() As Boolean

            If bLevelCompleted = True Then
                bLevelCompleted = False
                Return True
            Else
                Return False
            End If

        End Function

    End Class
End Namespace
