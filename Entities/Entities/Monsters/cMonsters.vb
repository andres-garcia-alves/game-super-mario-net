Option Strict On
Imports System.IO
Imports System.Xml
Imports System.Drawing
Imports System.Configuration

Imports Entities.SuperMarioNet.Base
Imports Entities.SuperMarioNet.Interfaces
Imports Entities.SuperMarioNet.Entities
Imports Entities.SuperMarioNet.Aux

Namespace SuperMarioNet.Entities
    Public Class cMonsters

        Public Event MonsterDie()

        Private lstMonsters As New List(Of cMonster)

        Public Sub New(ByVal byLevel As Byte)
            Try
                Dim sPathLevels As String = ConfigurationManager.AppSettings("pathLevels")
                Dim sLevelNum As String = byLevel.ToString().PadLeft(2, "0"c)

                ' load level objects
                LoadLevelFromFile(sPathLevels & sLevelNum & ".level")

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Load objects of a level from file
        ''' </summary>
        ''' <param name="sPath">Filepath</param>
        Private Sub LoadLevelFromFile(ByVal sPath As String)
            Try
                Dim sType As String = String.Empty
                Dim bVisible As Boolean = True
                Dim iPosX, iPosY, iWidth, iHeight As Integer
                Dim oMonster As New cMonster()

                Dim oXmlDocument As New XmlDocument()
                oXmlDocument.Load(sPath)

                ' list of objects
                For Each oXmlNode As XmlNode In oXmlDocument.DocumentElement.ChildNodes

                    If oXmlNode.Attributes Is Nothing Then Continue For ' avoid coments in xml

                    sType = oXmlNode.Attributes("type").Value
                    bVisible = Convert.ToBoolean(oXmlNode.Attributes("visible").Value)
                    iPosX = Convert.ToInt32(oXmlNode.Attributes("pos_x").Value) '* cBackground.BLOCK_WIDTH
                    iPosY = Convert.ToInt32(oXmlNode.Attributes("pos_y").Value) '* cBackground.BLOCK_HEIGHT
                    iWidth = Convert.ToInt32(oXmlNode.Attributes("width").Value)
                    iHeight = Convert.ToInt32(oXmlNode.Attributes("height").Value)

                    Select Case sType
                        Case "Mushroom"
                            oMonster = New cMonster(cMonster.eMonsterType.BadMushroom, bVisible, iPosX, iPosY)
                        Case "Turtle"
                            oMonster = New cMonster(cMonster.eMonsterType.Turtle, bVisible, iPosX, iPosY)
                        Case Else
                            Continue For
                    End Select

                    lstMonsters.Add(oMonster) ' add to objects list
                    cCollisionsSystem.RegisterItemForCollision(CType(oMonster, cCollisionBase)) ' register for collision
                Next

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Add received Monster to list
        ''' </summary>
        Public Sub AddMonster(ByVal oMonster As cMonster)
            lstMonsters.Add(oMonster)
        End Sub

        ''' <summary>
        ''' Remove received Monster from list
        ''' </summary>
        Public Sub RemoveMonster(ByVal oMonster As cMonster)
            lstMonsters.Remove(oMonster)
        End Sub

        ''' <summary>
        ''' Remove all monsters from collision detection
        ''' </summary>
        Public Sub Clear()

            For Each oMonster As cMonster In lstMonsters
                cCollisionsSystem.RemoveItemForCollision(CType(oMonster, cCollisionBase))
            Next
            lstMonsters.Clear()

        End Sub

        ''' <summary>
        ''' Reset monsters status
        ''' </summary>
        Public Sub Reset()

            For Each oMonster As cMonster In lstMonsters
                cCollisionsSystem.RemoveItemForCollision(CType(oMonster, cCollisionBase)) ' remove for collision
                oMonster.Reset()
                cCollisionsSystem.RegisterItemForCollision(CType(oMonster, cCollisionBase)) ' register for collision
            Next

        End Sub

        ''' <summary>
        ''' Create a new 'GoodMushroom' and append it to list
        ''' </summary>
        Public Sub CreateReward(ByRef oPlayer As cPlayer, ByVal iPosX As Integer, ByVal iPosY As Integer)
            Dim oMonster As cMonster

            If oPlayer.LiveStatus = cPlayer.ePlayerLiveStatus.Small Then
                oMonster = New cMonster(cMonster.eMonsterType.GoodMushroom, False, iPosX, iPosY)
                oMonster.ProgramVisible(750) ' show good mushroom after a time period
                oMonster.ProgramEnable(750) ' activate good mushroom after a time period
            Else
                oMonster = New cMonster(cMonster.eMonsterType.Flower, False, iPosX, iPosY)
                oMonster.ProgramVisible(750) ' show flower after a time period
            End If

            lstMonsters.Add(oMonster)  ' add to objects list
            cCollisionsSystem.RegisterItemForCollision(CType(oMonster, cCollisionBase)) ' register for collision

        End Sub

        ''' <summary>
        ''' Move to right
        ''' </summary>
        ''' <param name="oPlayer">cPlayer object</param>
        Public Sub MoveRight(ByRef oPlayer As cPlayer)

            ' don't move monster position, until player PositionX reach minimium/maximium posible value on the screen
            If Not oPlayer.ScreenGapReached Then Return

            For Each oMonster As cMonster In lstMonsters
                oMonster.PositionX -= CType(IIf(oPlayer.MoveStatus = cPlayer.ePlayerMoveStatus.Running, cPlayer.PLAYER_HORIZ_RUN_MOV, cPlayer.PLAYER_HORIZ_WALK_MOV), Int32)
            Next

        End Sub

        ''' <summary>
        ''' Move monsters to next position. Move only if monster is near to player
        ''' </summary>
        ''' <param name="oPlayer">cPlayer object</param>
        Public Sub Move(ByRef oPlayer As cPlayer)

            For Each oMonster As cMonster In lstMonsters
                If (oMonster.PositionX - oPlayer.PositionX) < 500 Then oMonster.Move()
            Next

        End Sub

        ''' <summary>
        ''' Draw monsters. Only those that are inside camera view, of the entire map
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics)

            For Each oMonster As cMonster In lstMonsters
                oMonster.Draw(oGraphics)
            Next

        End Sub

        ''' <summary>
        ''' Adjust monsters position acording to gravity (if necesary)
        ''' </summary>
        Public Sub CheckGravityMovement()

            For Each oMonster As cMonster In lstMonsters
                oMonster.CheckGravityMovement()
            Next

        End Sub

        Public Function GetPositionRectangle() As Rectangle
            Throw New NotSupportedException("Invalid method.")
        End Function

    End Class
End Namespace
