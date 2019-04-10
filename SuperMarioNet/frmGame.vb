' 
'  Created by:
'      Juan Andres Garcia Alves de Borba
'  
'  Date & Version:
'      Nov-2010, version 1.0
' 
'  Contact:
'      andres_garcia_ao@hotmail.com
'      andres.garcia.alves@gmail.com
'  
'  Curse:
'      Programacion III
'      Tecnicatura Superior en Programación
'      Universidad Tecnológica Nacional (UTN) FRBA - Argentina
'
'  Licensing:
'      This software is Open Source and are available under de GNU LGPL license.
'      You can found a copy of the license at http://www.gnu.org/copyleft/lesser.html
'  
'  Enjoy playing!
'

Option Strict On
Imports System.IO
Imports System.Drawing.Drawing2D
Imports System.Configuration

Imports Entities.SuperMarioNet.Aux
Imports Entities.SuperMarioNet.Entities
Imports Entities.SuperMarioNet.Base
Imports MiscLibrary.SuperMarioNet.Miscelaneous

Public Class frmGame

    Dim bExit As Boolean = False
    Dim oGraphics As Graphics
    Dim oInput As cInput
    Dim oBackground As cBackground
    Dim oPlayer As cPlayer
    Dim oStaticObjects As cStaticObjects
    Dim oMonsters As cMonsters
    Dim oFPS As cFPS
    Dim oGameControl As cGameControl
    Dim oScoreBoard As cScoreBoard
    Dim oHighScores As cHighScores
    Dim oBlackScreen As cBlackScreen
    Dim oParticlesSystem As cParticlesSystem

    Public Sub New()
        InitializeComponent()
        InitializeGame(Nothing)
    End Sub

    Public Sub New(ByRef oMusic As cMusic)
        InitializeComponent()
        InitializeGame(oMusic)
    End Sub

    Private Sub InitializeGame(ByRef oMusic As cMusic)
        Try
            cLanguaje.Initialize() ' <-- remove on final release

            ' setup hot keys image
            Me.picKeyboard.BackgroundImage = Image.FromFile(ConfigurationManager.AppSettings("pathImages") & "MiniKeyboard.png")
            Me.picKeyboard.Visible = True

            ' graphics quality
            oGraphics = Me.CreateGraphics()
            oGraphics.SmoothingMode = SmoothingMode.HighQuality
            oGraphics.CompositingMode = CompositingMode.SourceCopy
            oGraphics.CompositingQuality = CompositingQuality.HighQuality

            ' world objects
            oInput = New cInput()
            oBackground = New cBackground(0)
            oPlayer = New cPlayer()
            oStaticObjects = New cStaticObjects(0)
            oMonsters = New cMonsters(0)
            oFPS = New cFPS()
            oGameControl = New cGameControl()
            oScoreBoard = New cScoreBoard()
            oHighScores = New cHighScores()
            oBlackScreen = New cBlackScreen()
            oParticlesSystem = New cParticlesSystem()

            ' get the input type (keyboard / joystick) from settings
            oInput.InputType = CType(IIf(ConfigurationManager.AppSettings("input") = "Keyboard", cInput.eInputType.Keyboard, cInput.eInputType.Joystick), cInput.eInputType)

            ' get the drawing game mode (normal / debug) from settings
            oGameControl.GameMode = CType(IIf(ConfigurationManager.AppSettings("mode") = "Normal", cGameControl.eGameMode.Normal, cGameControl.eGameMode.Debug), cGameControl.eGameMode)

            ' add the listeners for objects comunication
            AddHandler oPlayer.PlayerDie, AddressOf oGameControl.DecreasePlayerLive
            AddHandler oStaticObjects.GoalReached, AddressOf oGameControl.FinishLevel
            AddHandler oStaticObjects.CreateReward, AddressOf oMonsters.CreateReward

            ' start
            Me.tmrStart.Enabled = True

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    ''' <summary>
    ''' Game start
    ''' </summary>
    Private Sub tmrStart_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrStart.Tick

        Me.tmrStart.Enabled = False

        ' remove hot keys image
        Me.picKeyboard.BackgroundImage = Nothing
        Me.picKeyboard.Visible = False
        Me.picKeyboard.Dispose()

        MainBucle() ' game start

    End Sub

    ''' <summary>
    ''' Key hold down
    ''' </summary>
    Private Sub frmGame_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
        e.Handled = True

        Select Case e.KeyData
            Case Keys.Left : oInput.LeftKey = True
            Case Keys.Right : oInput.RightKey = True
            Case Keys.Up : oInput.UpKey = True
            Case Keys.Down : oInput.DownKey = True
            Case Keys.X : oInput.JumpKey = True
            Case Keys.Z : oInput.RunFireKey = True
            Case Keys.F10 : oInput.FpsKey = True
            Case Keys.Escape : oInput.EscKey = True
        End Select

    End Sub

    ''' <summary>
    ''' Key hold up
    ''' </summary>
    Private Sub frmGame_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyUp
        e.Handled = True

        Select Case e.KeyData
            Case Keys.Left : oInput.LeftKey = False
            Case Keys.Right : oInput.RightKey = False
            Case Keys.Up : oInput.UpKey = False
            Case Keys.Down : oInput.DownKey = False
            Case Keys.X : oInput.JumpKey = False
            Case Keys.Z : oInput.RunFireKey = False
            Case Keys.F10 : oInput.FpsKey = False
        End Select

    End Sub

    ''' <summary>
    ''' Redraw the game world
    ''' </summary>
    Private Sub frmGame_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint

        If (oGameControl.GameMode = cGameControl.eGameMode.Normal) Then ' normal mode
            oBackground.Draw(e.Graphics)
            oScoreBoard.Draw(e.Graphics, oGameControl.Score, oGameControl.Coins, oGameControl.World, oGameControl.Level, oGameControl.Time)
            oStaticObjects.Draw(e.Graphics)
            oMonsters.Draw(e.Graphics)
            oPlayer.Draw(e.Graphics)
            oPlayer.Shots.Draw(e.Graphics)
            oParticlesSystem.Draw(e.Graphics)
            oFPS.Draw(e.Graphics)
            oBlackScreen.Draw(e.Graphics, oGameControl.Score, oGameControl.Coins, oGameControl.World, oGameControl.Level, oGameControl.Lives)

        Else ' debug mode
            oStaticObjects.Draw(e.Graphics)
            oMonsters.Draw(e.Graphics)
            oPlayer.Draw(e.Graphics)
        End If

    End Sub

    ''' <summary>
    ''' Game main loop
    ''' </summary>
    Private Sub MainBucle()

        While Not bExit

            Application.DoEvents()
            oFPS.FrameStart()

            ' input control
            ProcessInput()

            ' jump & gravity movements
            oPlayer.CheckJumpMovement()
            oPlayer.CheckGravityMovement()

            ' monsters & misc
            oMonsters.Move(oPlayer)
            oMonsters.CheckGravityMovement()
            oPlayer.Shots.Move()

            ' time
            oGameControl.CheckTimeTick()

            ' general collision check
            cCollisionsSystem.CheckCollisions()
            cCollisionsSystem.RemovePendingsItemsFromCollision()

            ' lose live check
            If oGameControl.CheckLiveLose() Then
                GeneralReset()
                oBlackScreen.EnableBlackScreen()
            End If

            ' level finished check
            If oGameControl.CheckLevelComplete() Then
                GeneralReset()
                Dim sMsg As String = "This is a DEMO game. Only this level is available."
                MessageBox.Show(sMsg, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information)
                bExit = True
            End If

            ' game over check
            If oGameControl.CheckNoLivesRemaining() Then
                oHighScores.CheckNewHighScore(oGameControl.Score)
                bExit = True
            End If

            ' repaint the screen
            Me.Refresh()

            ' constant framerate
            oFPS.CalculateFPS()
            oFPS.FrameFinish()
        End While

        cCollisionsSystem.Clear()
        Me.Close()
    End Sub

    ''' <summary>
    ''' Process keyboard/joystick input
    ''' </summary>
    Private Sub ProcessInput()

        ' check Left movement
        If oInput.LeftKey AndAlso cCollisionsSystem.CheckValidMovement(oPlayer, False, cPlayer.PLAYER_HORIZ_WALK_MOV, cCollisionBase.eMoveHorizDir.Left) Then
            oPlayer.MoveLeft()
        End If

        ' check Right movement
        If oInput.RightKey AndAlso cCollisionsSystem.CheckValidMovement(oPlayer, False, cPlayer.PLAYER_HORIZ_WALK_MOV, cCollisionBase.eMoveHorizDir.Right) Then
            oPlayer.MoveRight()
            oPlayer.Shots.MoveRight(oPlayer)
            oBackground.MoveCameraRight(oPlayer)
            oStaticObjects.MoveRight(oPlayer)
            oMonsters.MoveRight(oPlayer)
            oParticlesSystem.MoveRight(oPlayer)
        End If

        ' check for StandBy mode
        If (Not oInput.LeftKey) And (Not oInput.RightKey) Then oPlayer.SetStandBy()

        ' check Jump
        If oInput.JumpKey Then oPlayer.ActivateJump()

        ' check Run/Fire
        If oInput.RunFireKey Then
            oPlayer.ActivateRunFire()
        Else
            oPlayer.DisableRunFire()
        End If

        ' check FPS
        If oInput.FpsKey Then oFPS.ShowHideFPS()

        ' check ESC
        If oInput.EscKey Then bExit = True

    End Sub

    ''' <summary>
    ''' Call reset method in game objects
    ''' </summary>
    Private Sub GeneralReset()
        oInput.Reset()
        oBackground.Reset()
        oPlayer.Reset()
        oStaticObjects.Reset()
        oMonsters.Reset()
        oParticlesSystem.Reset()
    End Sub

End Class