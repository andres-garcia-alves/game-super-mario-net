Option Strict On
Imports System.Configuration
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms

Namespace SuperMarioNet.Entities

    Public Class cFPS

        ' FPS control
        Private dtPrevious As DateTime
        Private ReadOnly iMinFrameDuration As Integer = 30
        Private tsInteval As TimeSpan

        ' FPS calculation
        Private bInitialized As Boolean = False
        Private dtPrevTick As DateTime
        Private dtCurrentTick As DateTime
        Private iCountFPS As Integer = 0
        Private iAcumFreeTime As Integer = 0

        ' FPS display
        Private bShowFPS As Boolean = False
        Private iCurrentFPS As Integer = 0
        Private iCurrentFreeTime As Integer = 0
        Private sMessage As String = String.Empty

        Public Sub New()
            Try
                Dim iAux As Integer

                iAux = Convert.ToInt32(ConfigurationManager.AppSettings("fps"))
                If (iAux >= 10 And iAux <= 100) Then
                    iMinFrameDuration = Convert.ToInt32(1000 / iAux)
                Else
                    iMinFrameDuration = Convert.ToInt32(1000 / 30) ' 1000/30 = 33 FPS
                    Throw New ApplicationException("FPS value in 'App.config' file must be between 10 to 100. Using default value: 33 FPS.")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try

        End Sub

        ''' <summary>
        ''' Mark the start of a new cicle
        ''' </summary>
        Public Sub FrameStart()
            dtPrevious = DateTime.Now
        End Sub

        ''' <summary>
        ''' Generate a Delay to control the FPS rate
        ''' </summary>
        Public Sub FrameFinish()

            tsInteval = DateTime.Now.Subtract(dtPrevious)

            If (tsInteval.Milliseconds < iMinFrameDuration) Then
                iAcumFreeTime += (iMinFrameDuration - tsInteval.Milliseconds) ' freetime acumulation

                ' sleep to complete at least iMinFrameDuration per frame
                Thread.Sleep(iMinFrameDuration - tsInteval.Milliseconds)
            End If

        End Sub

        ''' <summary>
        ''' Acum the number of invocations to this method over a second
        ''' </summary>
        Public Sub CalculateFPS()

            If (bShowFPS) Then
                If (Not bInitialized) Then ' 1st time: set init tick
                    bInitialized = True
                    dtPrevTick = DateTime.Now
                End If

                iCountFPS += 1
                dtCurrentTick = DateTime.Now

                ' If 1 second passed from dtPrevTick, recalculate current framerate
                If (dtCurrentTick.Subtract(dtPrevTick).TotalMilliseconds > 1000) Then

                    dtPrevTick = dtPrevTick.AddMilliseconds(1000) ' next second
                    iCurrentFPS = iCountFPS ' current FPS
                    iCurrentFreeTime = Convert.ToInt32(iAcumFreeTime / 10)

                    iCountFPS = 0 ' reset counter
                    iAcumFreeTime = 0 ' reset acum
                End If
            End If

        End Sub

        Public Sub ShowHideFPS()
            bShowFPS = Not bShowFPS
        End Sub

        Public Sub Draw(ByRef oGraphics As Graphics)
            If (bShowFPS) Then
                sMessage = "FPS: " & iCurrentFPS.ToString().PadLeft(2, "0"c) & "  FreeTime: " & iCurrentFreeTime.ToString().PadLeft(2, "0"c) & "%"
                oGraphics.DrawString(sMessage, New Font("Verdana", 8), Brushes.White, 10, 460)
            End If
        End Sub

    End Class
End Namespace

