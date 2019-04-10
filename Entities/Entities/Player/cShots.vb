Option Strict On
Imports System.Drawing
Imports System.Timers
Imports System.Configuration

Imports Entities.SuperMarioNet.Base
Imports Entities.SuperMarioNet.Interfaces
Imports Entities.SuperMarioNet.Aux

Namespace SuperMarioNet.Entities

    Public Class cShots

        Private Const MAX_SIMULTANEOUS_SHOTS As Integer = 2

        Private i As Integer
        Private oShot As cShot
        Private tmrAllowCreateShot As Timer

        Private bAllowCreateShot As Boolean = True
        Private _lstShots As New List(Of cShot)

        Public Sub New()
        End Sub

        ''' <summary>
        ''' Add received Shot to list
        ''' </summary>
        Public Function CreateNewShot(ByVal iPosX As Integer, ByVal iPosY As Integer, ByVal eHorizDir As cCollisionBase.eMoveHorizDir) As Boolean

            If bAllowCreateShot AndAlso Count < MAX_SIMULTANEOUS_SHOTS Then

                ' create shot
                oShot = New cShot(iPosX, iPosY, eHorizDir)
                _lstShots.Add(oShot) ' add to shots list
                AddHandler oShot.ShotDisapear, AddressOf Remove ' register event handler
                cCollisionsSystem.RegisterItemForCollision(CType(oShot, cCollisionBase)) ' register for collision

                ' enable create a new shot only after a wait period
                bAllowCreateShot = False
                tmrAllowCreateShot = New Timer(200)
                AddHandler tmrAllowCreateShot.Elapsed, AddressOf AllowCreateShot
                Me.tmrAllowCreateShot.Start()

            End If

        End Function

        ''' <summary>
        ''' Remove received Shot from list
        ''' </summary>
        Public Sub Remove(ByRef oShot As cShot)

            _lstShots.Remove(oShot)
            RemoveHandler oShot.ShotDisapear, AddressOf Remove
            cCollisionsSystem.RemoveItemForCollision(CType(oShot, cCollisionBase))

        End Sub

        ''' <summary>
        ''' Remove all monsters from collision detection
        ''' </summary>
        Public Sub Clear()

            For Each oShot As cShot In _lstShots
                cCollisionsSystem.RemoveItemForCollision(CType(oShot, cCollisionBase))
            Next
            _lstShots.Clear()

        End Sub

        ''' <summary>
        ''' Count of Shots in list
        ''' </summary>
        Public ReadOnly Property Count() As Integer
            Get
                Return _lstShots.Count
            End Get
        End Property

        ''' <summary>
        ''' Get a Shot by index
        ''' </summary>
        ''' <param name="index">Shot index to retrieve</param>
        Default Public ReadOnly Property Item(ByVal index As Integer) As Object
            Get
                Return _lstShots(index)
            End Get
        End Property

        ''' <summary>
        ''' Move to right
        ''' </summary>
        ''' <param name="oPlayer">cPlayer object</param>
        Public Sub MoveRight(ByRef oPlayer As cPlayer)

            ' don't move monster position, until player PositionX reach minimium/maximium posible value on the screen
            If Not oPlayer.ScreenGapReached Then Return

            i = 0
            Do While i < _lstShots.Count
                _lstShots(i).PositionX -= CType(IIf(oPlayer.MoveStatus = cPlayer.ePlayerMoveStatus.Running, cPlayer.PLAYER_HORIZ_RUN_MOV, cPlayer.PLAYER_HORIZ_WALK_MOV), Int32)
                i += 1
            Loop

        End Sub

        ''' <summary>
        ''' Move shots
        ''' </summary>
        Public Function Move() As Integer

            i = 0
            Do While i < _lstShots.Count
                _lstShots(i).Move()
                i += 1
            Loop

        End Function

        ''' <summary>
        ''' Draw shots
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics)
            For Each oShot As cShot In _lstShots
                oShot.Draw(oGraphics)
            Next
        End Sub

        ''' <summary>
        ''' Allow to create a new cShot
        ''' </summary>
        Private Sub AllowCreateShot(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            Me.tmrAllowCreateShot.Stop()
            Me.tmrAllowCreateShot.Close()
            RemoveHandler tmrAllowCreateShot.Elapsed, AddressOf AllowCreateShot

            bAllowCreateShot = True

        End Sub

    End Class
End Namespace
