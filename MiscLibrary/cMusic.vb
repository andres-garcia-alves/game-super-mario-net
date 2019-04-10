Option Strict On
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Timers
Imports System.Configuration
Imports System.Runtime.InteropServices

Namespace SuperMarioNet.Miscelaneous

    Public Class cMusic

        ' P/Invoke
        Public Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" (ByVal lpstrCommand As String, ByVal lpstrReturnString As String, ByVal uReturnLength As Long, ByVal hwndCallback As Long) As Long
        Private lRet As Long = 0
        Private lCB As Long = 0
        Private sRetString As String = String.Empty

        Public Enum eMusicTheme As Byte
            None = 0
            CastleTheme = 1
            Death = 2
            FinishMap = 3
            FinishWorld = 4
            Go = 5
            LiveUp = 6
            MarioTheme = 7
            MarioThemeHurry = 8
            MarioThemeReggae = 9
            MarioThemeTechno = 10
            TimeWarnning = 11
            UnderworldTheme = 12
            WaterTheme = 13
        End Enum

        Private Const PATH_CASTLE_THEME As String = "CastleTheme.mid"
        Private Const LENGHT_CASTLE_THEME As Integer = 80000
        Private Const SEQUENCER_CASTLE_THEME As String = "MIDI01"

        Private Const PATH_DEATH As String = "Death.mid"
        Private Const LENGHT_DEATH As Integer = 3000
        Private Const SEQUENCER_DEATH As String = "MIDI02"

        Private Const PATH_FINISH_MAP As String = "FinishMap.mid"
        Private Const LENGHT_FINISH_MAP As Integer = 5000
        Private Const SEQUENCER_FINISH_MAP As String = "MIDI03"

        Private Const PATH_FINISH_WORLD As String = "FinishWorld.mid"
        Private Const LENGHT_FINISH_WORLD As Integer = 8000
        Private Const SEQUENCER_FINISH_WORLD As String = "MIDI04"

        Private Const PATH_GO As String = "Go.mid"
        Private Const LENGHT_GO As Integer = 5000
        Private Const SEQUENCER_GO As String = "MIDI05"

        Private Const PATH_LIVE_UP As String = "LiveUp.mid"
        Private Const LENGHT_LIVE_UP As Integer = 1000
        Private Const SEQUENCER_LIVE_UP As String = "MIDI06"

        Private Const PATH_MARIO_THEME As String = "MarioTheme.mid"
        Private Const LENGHT_MARIO_THEME As Integer = 86000
        Private Const SEQUENCER_MARIO_THEME As String = "MIDI07"

        Private Const PATH_MARIO_THEME_HURRY As String = "MarioTheme_Hurry.mid"
        Private Const LENGHT_MARIO_THEME_HURRY As Integer = 65000
        Private Const SEQUENCER_MARIO_THEME_HURRY As String = "MIDI08"

        Private Const PATH_MARIO_THEME_REGGAE As String = "MarioTheme_Reggae.mid"
        Private Const LENGHT_MARIO_THEME_REGGAE As Integer = 262000
        Private Const SEQUENCER_MARIO_THEME_REGGAE As String = "MIDI09"

        Private Const PATH_MARIO_THEME_TECHNO As String = "MarioTheme_Techno.mid"
        Private Const LENGHT_MARIO_THEME_TECHNO As Integer = 62000
        Private Const SEQUENCER_MARIO_THEME_TECHNO As String = "MIDI10"

        Private Const PATH_TIME_WARNNING As String = "TimeWarning.mid"
        Private Const LENGHT_TIME_WARNNING As Integer = 3000
        Private Const SEQUENCER_TIME_WARNNING As String = "MIDI11"

        Private Const PATH_UNDERWORLD_THEME As String = "UnderworldTheme.mid"
        Private Const LENGHT_UNDERWORLD_THEME As Integer = 25000
        Private Const SEQUENCER_UNDERWORLD_THEME As String = "MIDI12"

        Private Const PATH_WATER_THEME As String = "WaterTheme.mid"
        Private Const LENGHT_WATER_THEME As Integer = 52000
        Private Const SEQUENCER_WATER_THEME As String = "MIDI13"

        Private Const SAFETY_DELAY As Integer = 1000

        Private bMusicOn As Boolean = False
        Private sBasePath As String = String.Empty
        Private eCurrentTheme As eMusicTheme = eMusicTheme.None

        Private WithEvents oTimer As Timer

        Public Sub New()
            Try
                ' get if music is On/Off
                bMusicOn = CType(IIf(ConfigurationManager.AppSettings("music") = "ON", True, False), Boolean)

                ' don't load resources if music is Off
                If Not bMusicOn Then Return

                sBasePath = ConfigurationManager.AppSettings("pathSounds")
                eCurrentTheme = eMusicTheme.None
                oTimer = New Timer()

                InitSequencer(eMusicTheme.Death)
                InitSequencer(eMusicTheme.FinishMap)
                InitSequencer(eMusicTheme.Go)
                InitSequencer(eMusicTheme.LiveUp)
                InitSequencer(eMusicTheme.MarioTheme)
                InitSequencer(eMusicTheme.MarioThemeHurry)
                InitSequencer(eMusicTheme.MarioThemeReggae)
                InitSequencer(eMusicTheme.MarioThemeTechno)
                InitSequencer(eMusicTheme.TimeWarnning)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Play infinitely the requested theme
        ''' </summary>
        ''' <param name="eTheme">Theme to play</param>
        Public Sub PlayMusic(ByVal eTheme As eMusicTheme)
            If Not bMusicOn Then Return

            PlayMusic(eTheme, True)
        End Sub

        ''' <summary>
        ''' Play requested theme
        ''' </summary>
        ''' <param name="eTheme">Theme to play</param>
        ''' <param name="bRepeat">Indicate if theme plays one time or infinitely</param>
        Public Sub PlayMusic(ByVal eTheme As eMusicTheme, ByVal bRepeat As Boolean)
            If Not bMusicOn Then Return

            oTimer.Stop()

            lRet = mciSendString("stop " & GetThemeSeq(eCurrentTheme), sRetString, 128, lCB) ' stop current sequencer (if exist)
            lRet = mciSendString("seek " & GetThemeSeq(eTheme) & " to 0", sRetString, 128, lCB) ' back to start of secuence
            lRet = mciSendString("play " & GetThemeSeq(eTheme), sRetString, 128, lCB) ' play new sequencer
            eCurrentTheme = eTheme ' save new sequencer for next time

            If bRepeat Then ' play infinitely?
                oTimer.Interval = GetThemeLenght(eTheme) + SAFETY_DELAY
                oTimer.Start()
            End If
        End Sub

        ''' <summary>
        ''' Stop current theme
        ''' </summary>
        Public Sub StopMusic()
            If Not bMusicOn Then Return

            oTimer.Stop()
            lRet = mciSendString("stop " & GetThemeSeq(eCurrentTheme), sRetString, 128, lCB)
        End Sub

        ' <summary>
        ' Repeat the current MIDI
        ' </summary>
        Private Sub TimeElapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles oTimer.Elapsed
            lRet = mciSendString("stop " & GetThemeSeq(eCurrentTheme), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eCurrentTheme), sRetString, 128, lCB)
            lRet = mciSendString("open " & GetThemePath(eCurrentTheme) & " Type Sequencer Alias " & GetThemeSeq(eCurrentTheme), sRetString, 128, lCB)
            lRet = mciSendString("play " & GetThemeSeq(eCurrentTheme), sRetString, 128, lCB)
        End Sub

        ''' <summary>
        ''' Asociate a Theme to their Sequencer
        ''' </summary>
        ''' <param name="eTheme">Theme to asociate</param>
        Private Sub InitSequencer(ByVal eTheme As eMusicTheme)
            lRet = mciSendString("stop " & GetThemeSeq(eTheme), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eTheme), sRetString, 128, lCB)
            lRet = mciSendString("open " & GetThemePath(eTheme) & " Type Sequencer Alias " & GetThemeSeq(eTheme), sRetString, 128, lCB)
        End Sub

        ''' <summary>
        ''' Destructor
        ''' </summary>
        Protected Overrides Sub Finalize()
            If Not bMusicOn Then Return

            oTimer.Stop()
            oTimer.Dispose()

            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.Death), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.Death), sRetString, 128, lCB)
            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.FinishMap), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.FinishMap), sRetString, 128, lCB)
            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.Go), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.Go), sRetString, 128, lCB)
            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.LiveUp), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.LiveUp), sRetString, 128, lCB)
            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.MarioTheme), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.MarioTheme), sRetString, 128, lCB)
            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.MarioThemeHurry), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.MarioThemeHurry), sRetString, 128, lCB)
            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.MarioThemeReggae), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.MarioThemeReggae), sRetString, 128, lCB)
            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.MarioThemeTechno), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.MarioThemeTechno), sRetString, 128, lCB)
            lRet = mciSendString("stop " & GetThemeSeq(eMusicTheme.TimeWarnning), sRetString, 128, lCB)
            lRet = mciSendString("close " & GetThemeSeq(eMusicTheme.TimeWarnning), sRetString, 128, lCB)
        End Sub

        ''' <summary>
        ''' Retrieve the filepath of the selected theme
        ''' </summary>
        ''' <param name="eTheme">Theme to retrieve</param>
        Private Function GetThemePath(ByVal eTheme As eMusicTheme) As String
            Select Case eTheme
                Case eMusicTheme.CastleTheme : Return sBasePath & PATH_CASTLE_THEME
                Case eMusicTheme.Death : Return sBasePath & PATH_DEATH
                Case eMusicTheme.FinishMap : Return sBasePath & PATH_FINISH_MAP
                Case eMusicTheme.FinishWorld : Return sBasePath & PATH_FINISH_WORLD
                Case eMusicTheme.Go : Return sBasePath & PATH_GO
                Case eMusicTheme.LiveUp : Return sBasePath & PATH_LIVE_UP
                Case eMusicTheme.MarioTheme : Return sBasePath & PATH_MARIO_THEME
                Case eMusicTheme.MarioThemeHurry : Return sBasePath & PATH_MARIO_THEME_HURRY
                Case eMusicTheme.MarioThemeReggae : Return sBasePath & PATH_MARIO_THEME_REGGAE
                Case eMusicTheme.MarioThemeTechno : Return sBasePath & PATH_MARIO_THEME_TECHNO
                Case eMusicTheme.TimeWarnning : Return sBasePath & PATH_TIME_WARNNING
                Case eMusicTheme.UnderworldTheme : Return sBasePath & PATH_UNDERWORLD_THEME
                Case eMusicTheme.WaterTheme : Return sBasePath & PATH_WATER_THEME
                Case Else : Return String.Empty
            End Select
        End Function

        ''' <summary>
        ''' Retrieve the duration of the selected theme (in miliseconds)
        ''' </summary>
        ''' <param name="eTheme">Theme to retrieve</param>
        Private Function GetThemeLenght(ByVal eTheme As eMusicTheme) As Integer
            Select Case eTheme
                Case eMusicTheme.CastleTheme : Return LENGHT_CASTLE_THEME
                Case eMusicTheme.Death : Return LENGHT_DEATH
                Case eMusicTheme.FinishMap : Return LENGHT_FINISH_MAP
                Case eMusicTheme.FinishWorld : Return LENGHT_FINISH_WORLD
                Case eMusicTheme.Go : Return LENGHT_GO
                Case eMusicTheme.LiveUp : Return LENGHT_LIVE_UP
                Case eMusicTheme.MarioTheme : Return LENGHT_MARIO_THEME
                Case eMusicTheme.MarioThemeHurry : Return LENGHT_MARIO_THEME_HURRY
                Case eMusicTheme.MarioThemeReggae : Return LENGHT_MARIO_THEME_REGGAE
                Case eMusicTheme.MarioThemeTechno : Return LENGHT_MARIO_THEME_TECHNO
                Case eMusicTheme.TimeWarnning : Return LENGHT_TIME_WARNNING
                Case eMusicTheme.UnderworldTheme : Return LENGHT_UNDERWORLD_THEME
                Case eMusicTheme.WaterTheme : Return LENGHT_WATER_THEME
                Case Else : Return 0
            End Select
        End Function

        ''' <summary>
        ''' Retrieve the sequencer asociated to selected theme
        ''' </summary>
        ''' <param name="eTheme">Theme to retrieve</param>
        Private Function GetThemeSeq(ByVal eTheme As eMusicTheme) As String
            Select Case eTheme
                Case eMusicTheme.CastleTheme : Return SEQUENCER_CASTLE_THEME
                Case eMusicTheme.Death : Return SEQUENCER_DEATH
                Case eMusicTheme.FinishMap : Return SEQUENCER_FINISH_MAP
                Case eMusicTheme.FinishWorld : Return SEQUENCER_FINISH_WORLD
                Case eMusicTheme.Go : Return SEQUENCER_GO
                Case eMusicTheme.LiveUp : Return SEQUENCER_LIVE_UP
                Case eMusicTheme.MarioTheme : Return SEQUENCER_MARIO_THEME
                Case eMusicTheme.MarioThemeHurry : Return SEQUENCER_MARIO_THEME_HURRY
                Case eMusicTheme.MarioThemeReggae : Return SEQUENCER_MARIO_THEME_REGGAE
                Case eMusicTheme.MarioThemeTechno : Return SEQUENCER_MARIO_THEME_TECHNO
                Case eMusicTheme.TimeWarnning : Return SEQUENCER_TIME_WARNNING
                Case eMusicTheme.UnderworldTheme : Return SEQUENCER_UNDERWORLD_THEME
                Case eMusicTheme.WaterTheme : Return SEQUENCER_WATER_THEME
                Case Else : Return String.Empty
            End Select
        End Function

    End Class
End Namespace
