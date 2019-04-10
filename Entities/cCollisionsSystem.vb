Option Strict On
Imports System.Collections

Imports Entities.SuperMarioNet.Base
Imports Entities.SuperMarioNet.Entities

Namespace SuperMarioNet.Aux

    Public Class cCollisionsSystem

        ' list of collisionables objects
        Private Shared lstCollisionObjects As New List(Of cCollisionBase)

        ''' <summary>
        ''' add a item at the end of collisionable objects list
        ''' </summary>
        Public Shared Sub RegisterItemForCollision(ByRef oCollision As cCollisionBase)
            lstCollisionObjects.Add(oCollision)
        End Sub

        ''' <summary>
        ''' add a item at a specific position of collisionable objects list
        ''' </summary>
        Public Shared Sub RegisterItemForCollision(ByRef oCollision As cCollisionBase, ByVal iIndex As Integer)
            lstCollisionObjects.Insert(iIndex, oCollision)
        End Sub

        ''' <summary>
        ''' add a list of objects to the collisionable objects list
        ''' </summary>
        Public Shared Sub RegisterListForCollision(ByRef lstObjects As List(Of cCollisionBase))
            For Each o As cCollisionBase In lstObjects
                lstCollisionObjects.Add(o)
            Next
        End Sub

        ''' <summary>
        ''' remove a object from the list
        ''' </summary>
        Public Shared Sub RemoveItemForCollision(ByRef oCollision As cCollisionBase)
            lstCollisionObjects.Remove(oCollision)
        End Sub

        ''' <summary>
        ''' Remove from list items marked with property ToRemoveFromCollision = True
        ''' </summary>
        Public Shared Sub RemovePendingsItemsFromCollision()

            Dim i As Integer = 0
            Do While i < lstCollisionObjects.Count
                If lstCollisionObjects(i).ToRemoveFromCollision = True Then
                    lstCollisionObjects.RemoveAt(i)
                Else
                    i += 1
                End If
            Loop

        End Sub

        ''' <summary>
        ''' Removes all items in the collision list
        ''' </summary>
        Public Shared Sub Clear()
            lstCollisionObjects.Clear()
        End Sub

        ''' <summary>
        ''' Check if received player/monster collide with any of the other registered items in the list.
        ''' Overload method. Send default value in Vertical movement
        ''' </summary>
        ''' <param name="oTest">Player/monster to be compared against the other registered items for collision</param>
        ''' <param name="NotifyIfWillCollide">Notify (or not) to collisioned object in the list</param>
        ''' <param name="iMovement">Pixels to move the player/monster before comparision</param>
        ''' <param name="MoveHorizDir">Direction to move the player/monster before comparision</param>
        Public Shared Function CheckValidMovement(ByVal oTest As cCollisionBase, ByVal NotifyIfWillCollide As Boolean, _
        ByVal iMovement As Integer, ByVal MoveHorizDir As cCollisionBase.eMoveHorizDir) As Boolean

            Return CheckValidMovement(oTest, NotifyIfWillCollide, iMovement, MoveHorizDir, cCollisionBase.eMoveVertDir.None)

        End Function

        ''' <summary>
        ''' Check if received player/monster collide with any of the other registered items in the list.
        ''' Overload method. Send default value in Horizontal movement
        ''' </summary>
        ''' <param name="oTest">Player/monster to be compared against the other registered items for collision</param>
        ''' <param name="NotifyIfWillCollide">Notify (or not) to collisioned object in the list</param>
        ''' <param name="iMovement">Pixels to move the player/monster before comparision</param>
        ''' <param name="MoveVertDir">Direction to move the player/monster before comparision</param>
        Public Shared Function CheckValidMovement(ByVal oTest As cCollisionBase, ByVal NotifyIfWillCollide As Boolean, _
        ByVal iMovement As Integer, ByVal MoveVertDir As cCollisionBase.eMoveVertDir) As Boolean

            Return CheckValidMovement(oTest, NotifyIfWillCollide, iMovement, cCollisionBase.eMoveHorizDir.None, MoveVertDir)

        End Function

        ''' <summary>
        ''' Check if received player/monster collide with any of the other registered items in the list
        ''' </summary>
        ''' <param name="oTest">Player/monster to be compared against the other registered items for collision</param>
        ''' <param name="NotifyIfWillCollide">Notify (or not) to collisioned object in the list</param>
        ''' <param name="iMovement">Pixels to move the player/monster before comparision</param>
        ''' <param name="MoveHorizDir">Horizontal direction to move the player/monster before comparision</param>
        ''' <param name="MoveVertDir">Vertical direction to move the player/monster before comparision</param>
        Public Shared Function CheckValidMovement(ByVal oTest As cCollisionBase, ByVal NotifyIfWillCollide As Boolean, _
        ByVal iMovement As Integer, ByVal MoveHorizDir As cCollisionBase.eMoveHorizDir, ByVal MoveVertDir As cCollisionBase.eMoveVertDir) As Boolean

            Dim CheckPositionX, CheckPositionY As Integer

            ' setup calculated positions for player, previous to check collisions
            If MoveHorizDir = cCollisionBase.eMoveHorizDir.Left Then
                CheckPositionX = oTest.PositionX - iMovement
                CheckPositionY = oTest.PositionY

            ElseIf MoveHorizDir = cCollisionBase.eMoveHorizDir.Right Then
                CheckPositionX = oTest.PositionX + iMovement
                CheckPositionY = oTest.PositionY

            ElseIf MoveVertDir = cCollisionBase.eMoveVertDir.Up Then
                CheckPositionX = oTest.PositionX
                CheckPositionY = oTest.PositionY - iMovement

            ElseIf MoveVertDir = cCollisionBase.eMoveVertDir.Down Then
                CheckPositionX = oTest.PositionX
                CheckPositionY = oTest.PositionY + iMovement

            Else ' both are eMoveX.None
                Return False
            End If

            ' check if the copy of player collide against any of the items in list
            For i As Integer = 0 To lstCollisionObjects.Count - 1

                ' exclude collision check agains himself
                ' exclude those are marked as OverPassable
                ' include only those are marked as CollisionCheck = true
                If (oTest.GetHashCode() <> lstCollisionObjects(i).GetHashCode() AndAlso _
                    Not lstCollisionObjects(i).OverPassable AndAlso lstCollisionObjects(i).CollisionCheck) Then

                    ' checks:
                    ' oPlayer at left of object
                    ' oPlayer at right of object
                    ' oPlayer at top of object
                    ' oPlayer at bottom of object
                    If Not (CheckPositionX + oTest.GetWidth() < lstCollisionObjects(i).PositionX) And _
                       Not (CheckPositionX > lstCollisionObjects(i).PositionX + lstCollisionObjects(i).GetWidth()) And _
                       Not (CheckPositionY + oTest.GetHeight() < lstCollisionObjects(i).PositionY) And _
                       Not (CheckPositionY > lstCollisionObjects(i).PositionY + lstCollisionObjects(i).GetHeight()) Then

                        ' if reach until here, there is collision
                        If NotifyIfWillCollide Then
                            lstCollisionObjects(i).CollisionedBy(oTest)
                            Return False
                        Else
                            Return False
                        End If

                    End If
                End If
            Next

            ' if reach until here, there is NO superposition
            Return True

        End Function

        ''' <summary>
        ''' Check for collisions, all against all
        ''' </summary>
        Public Shared Sub CheckCollisions()

            For i As Integer = 0 To lstCollisionObjects.Count - 1
                For j As Integer = 0 To lstCollisionObjects.Count - 1

                    ' check all against all, exclude collision check agains himself
                    If (i < lstCollisionObjects.Count AndAlso j < lstCollisionObjects.Count AndAlso lstCollisionObjects(i).CollisionCheck AndAlso _
                        lstCollisionObjects(j).CollisionCheck AndAlso lstCollisionObjects(i).GetHashCode() <> lstCollisionObjects(j).GetHashCode()) Then

                        ' checks:
                        ' object A at left of object B
                        ' object A at right of object B
                        ' object A at top of object B
                        ' object A at bottom of object B
                        If Not (lstCollisionObjects(i).PositionX + lstCollisionObjects(i).GetWidth() < lstCollisionObjects(j).PositionX) And _
                           Not (lstCollisionObjects(i).PositionX > lstCollisionObjects(j).PositionX + lstCollisionObjects(j).GetWidth()) And _
                           Not (lstCollisionObjects(i).PositionY + lstCollisionObjects(i).GetHeight() < lstCollisionObjects(j).PositionY) And _
                           Not (lstCollisionObjects(i).PositionY > lstCollisionObjects(j).PositionY + lstCollisionObjects(j).GetHeight()) Then

                            ' if reach until here, there is superposition
                            lstCollisionObjects(i).CollisionedBy(lstCollisionObjects(j))
                        End If
                    End If
                Next
            Next
        End Sub

    End Class
End Namespace
