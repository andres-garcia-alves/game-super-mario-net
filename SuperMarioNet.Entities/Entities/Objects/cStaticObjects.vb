Option Strict On
Imports System.Configuration
Imports System.Drawing
Imports System.Xml

Imports SuperMarioNet.Entities.Base

Namespace SuperMarioNet.Entities.Entities
    Public Class cStaticObjects

        Public Event GoalReached()
        Public Event CreateReward(ByRef oPlayer As cPlayer, ByVal iPosX As Integer, ByVal iPosY As Integer)

        Private ReadOnly lstStaticObjects As New List(Of cStaticObject)

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
                Dim oStaticObject As New cStaticObject()

                Dim oXmlDocument As New XmlDocument()
                oXmlDocument.Load(sPath)

                ' list of objects
                For Each oXmlNode As XmlNode In oXmlDocument.DocumentElement.ChildNodes

                    If oXmlNode.Attributes Is Nothing Then Continue For ' avoid coments in xml

                    sType = oXmlNode.Attributes("type").Value
                    bVisible = Convert.ToBoolean(oXmlNode.Attributes("visible").Value)
                    iPosX = Convert.ToInt32(oXmlNode.Attributes("pos_x").Value) * cBackground.BLOCK_WIDTH
                    iPosY = Convert.ToInt32(oXmlNode.Attributes("pos_y").Value) * cBackground.BLOCK_HEIGHT
                    iWidth = Convert.ToInt32(oXmlNode.Attributes("width").Value)
                    iHeight = Convert.ToInt32(oXmlNode.Attributes("height").Value)

                    Select Case sType
                        Case "Floor"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.Floor, bVisible, iPosX, iPosY, iWidth, iHeight)
                        Case "NormalBrick"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.NormalBrick, bVisible, iPosX, iPosY)
                        Case "NormalRewardBrick"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.NormalRewardBrick, bVisible, iPosX, iPosY)
                        Case "CoinBrick"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.CoinBrick, bVisible, iPosX, iPosY)
                        Case "CoinRewardBrick"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.CoinRewardBrick, bVisible, iPosX, iPosY)
                        Case "SolidBlock"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.SolidBlock, bVisible, iPosX, iPosY)
                        Case "PipeSmall"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.PipeSmall, bVisible, iPosX, iPosY)
                        Case "PipeMedium"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.PipeMedium, bVisible, iPosX, iPosY)
                        Case "PipeLarge"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.PipeLarge, bVisible, iPosX, iPosY)
                        Case "Goal"
                            oStaticObject = New cStaticObject(cStaticObject.eObjectType.Goal, bVisible, iPosX, iPosY)
                        Case Else
                            Continue For
                    End Select

                    lstStaticObjects.Add(oStaticObject) ' add to objects list
                    cCollisionsSystem.RegisterItemForCollision(CType(oStaticObject, cCollisionBase)) ' register for collision
                    AddHandler oStaticObject.GoalReached, AddressOf GoalReach ' throw to upper its events
                    AddHandler oStaticObject.CreateReward, AddressOf NewReward ' throw to upper its events
                Next

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Add received Object to list
        ''' </summary>
        Public Sub AddObject(ByVal oStaticObject As cStaticObject)
            lstStaticObjects.Add(oStaticObject)
        End Sub

        ''' <summary>
        ''' Remove received Object from list
        ''' </summary>
        Public Sub RemoveObject(ByVal oStaticObject As cStaticObject)
            lstStaticObjects.Remove(oStaticObject)
        End Sub

        ''' <summary>
        ''' remove all objects from collision detection
        ''' </summary>
        Public Sub Clear()

            For Each oStaticObject As cStaticObject In lstStaticObjects
                cCollisionsSystem.RemoveItemForCollision(CType(oStaticObject, cCollisionBase))
            Next
            lstStaticObjects.Clear()

        End Sub

        ''' <summary>
        ''' Reset objects status
        ''' </summary>
        Public Sub Reset()

            For Each oStaticObject As cStaticObject In lstStaticObjects
                oStaticObject.Reset()
                cCollisionsSystem.RemoveItemForCollision(CType(oStaticObject, cCollisionBase)) ' remove for collision
                cCollisionsSystem.RegisterItemForCollision(CType(oStaticObject, cCollisionBase)) ' register for collision
            Next

        End Sub

        ''' <summary>
        ''' Move to left
        ''' </summary>
        ''' <param name="oPlayer">cPlayer object</param>
        ''' <param name="iCameraPos">cBackground.CameraPositionX property</param>
        Public Sub MoveLeft(ByRef oPlayer As cPlayer, ByVal iCameraPos As Integer)

            ' don't move object position, until player PositionX reach minimium/maximium posible value on the screen
            If oPlayer.ScreenGapReached = False Then Return
            If iCameraPos <= 0 Then Return

            For Each oStaticObject As cStaticObject In lstStaticObjects
                oStaticObject.PositionX += CType(IIf(oPlayer.MoveStatus = cPlayer.ePlayerMoveStatus.Running, cPlayer.PLAYER_HORIZ_RUN_MOV, cPlayer.PLAYER_HORIZ_WALK_MOV), Int32)
            Next

        End Sub

        ''' <summary>
        ''' Move to right
        ''' </summary>
        ''' <param name="oPlayer">cPlayer object</param>
        Public Sub MoveRight(ByRef oPlayer As cPlayer)

            ' don't move object position, until player PositionX reach minimium/maximium posible value on the screen
            If Not oPlayer.ScreenGapReached Then Return

            For Each oStaticObject As cStaticObject In lstStaticObjects
                oStaticObject.PositionX -= CType(IIf(oPlayer.MoveStatus = cPlayer.ePlayerMoveStatus.Running, cPlayer.PLAYER_HORIZ_RUN_MOV, cPlayer.PLAYER_HORIZ_WALK_MOV), Int32)
            Next

        End Sub

        ''' <summary>
        ''' Draw static objects. Only those that are inside camera view, of the entire map
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics)

            For Each oStaticObject As cStaticObject In lstStaticObjects
                oStaticObject.Draw(oGraphics)
            Next

        End Sub

        ''' <summary>
        ''' Throw a GoalReached event
        ''' </summary>
        Private Sub GoalReach()
            RaiseEvent GoalReached()
        End Sub

        ''' <summary>
        ''' Throw a CreateReward event
        ''' </summary>
        Private Sub NewReward(ByRef oPlayer As cPlayer, ByVal iPosX As Integer, ByVal iPosY As Integer)
            RaiseEvent CreateReward(oPlayer, iPosX, iPosY)
        End Sub

    End Class
End Namespace