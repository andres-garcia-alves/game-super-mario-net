Option Strict On
Imports System.Drawing
Imports System.Configuration
Imports System.IO

Imports SuperMarioNet.Entities.Interfaces

Namespace SuperMarioNet.Entities.Entities

    Public Class cBackground
        Implements iDrawable

        Public Const SCREEN_LEFT_GAP As Integer = 30
        Public Const SCREEN_RIGHT_GAP As Integer = 300

        Public Const BLOCK_WIDTH As Integer = 44
        Public Const BLOCK_HEIGHT As Integer = 36

        Private Const MAP_HORIZ_BLOCKS As Byte = 212
        Private Const MAP_VERT_BLOCKS As Byte = 14

        Private Const SCREEN_HORIZ_BLOCKS As Byte = 15
        Private Const SCREEN_VERT_BLOCKS As Byte = 14

        Private _bVisible As Boolean = True
        Private _iCameraPosition As Integer = 0

        Private ReadOnly arrMatrix(0 To MAP_HORIZ_BLOCKS - 1, 0 To MAP_VERT_BLOCKS - 1) As Byte
        Private ReadOnly lstTiles As New List(Of Image)

        Public Sub New(ByVal byLevel As Byte)
            Try
                Dim sPathImages As String = ConfigurationManager.AppSettings("pathImages")
                Dim sPathLevels As String = ConfigurationManager.AppSettings("pathLevels")

                ' load tiles
                For i As Integer = 0 To 36
                    Me.lstTiles.Add(Image.FromFile(sPathImages & "Tiles/" & i.ToString.PadLeft(2, "0"c) & ".png", False))
                Next i

                ' load level background
                Dim sLevelNum As String = byLevel.ToString().PadLeft(2, "0"c)
                LoadBackgroundFromFile(sPathLevels & sLevelNum & ".bground")

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Load backgound from file
        ''' </summary>
        ''' <param name="sPath">Filepath</param>
        Private Sub LoadBackgroundFromFile(ByVal sPath As String)
            Try
                Dim sLine As String
                Dim sArray() As String
                Dim oReader As StreamReader = File.OpenText(sPath)

                For i As Integer = 0 To MAP_VERT_BLOCKS - 1
                    sLine = oReader.ReadLine()
                    sArray = sLine.Split(New Char() {","c}, StringSplitOptions.None)

                    For j As Integer = 0 To sArray.Length - 1
                        arrMatrix(j, i) = Convert.ToByte(sArray(j))
                    Next j
                Next i

                oReader.Close()

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Reset background status
        ''' </summary>
        Public Sub Reset()
            Try
                CameraPositionX = 0

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ''' <summary>
        ''' Left side of camera position. Used to draw the background
        ''' </summary>
        Public Property CameraPositionX() As Integer
            Get
                Return _iCameraPosition
            End Get
            Set(ByVal value As Integer)
                ' control that camera position is between allowed values
                If value >= 0 AndAlso value <= (MAP_HORIZ_BLOCKS * BLOCK_WIDTH) - (BLOCK_WIDTH * SCREEN_HORIZ_BLOCKS) Then _iCameraPosition = value
            End Set
        End Property

        ''' <summary>
        ''' Visibility
        ''' </summary>
        Public Property Visible() As Boolean Implements iDrawable.Visible
            Get
                Return _bVisible
            End Get
            Set(ByVal value As Boolean)
                _bVisible = value
            End Set
        End Property

        ''' <summary>
        ''' Returns a Rect struct with the current location of player
        ''' </summary>
        Public Function GetPositionRectangle() As Rectangle Implements iDrawable.GetPositionRectangle
            Throw New NotSupportedException("Invalid method.")
        End Function

        ''' <summary>
        ''' Move to left
        ''' </summary>
        ''' <param name="oPlayer">cPlayer object</param>
        Public Sub MoveCameraLeft(ByRef oPlayer As cPlayer)

            ' don't move camera position, until player PositionX reach minimium/maximium posible value on the screen
            If Not oPlayer.ScreenGapReached Then Return

            CameraPositionX -= CType(IIf(oPlayer.MoveStatus = cPlayer.ePlayerMoveStatus.Running, cPlayer.PLAYER_HORIZ_RUN_MOV, cPlayer.PLAYER_HORIZ_WALK_MOV), Int32)

        End Sub

        ''' <summary>
        ''' Move to right
        ''' </summary>
        ''' <param name="oPlayer">cPlayer object</param>
        Public Sub MoveCameraRight(ByRef oPlayer As cPlayer)

            ' don't move camera position, until player PositionX reach minimium/maximium posible value on the screen
            If Not oPlayer.ScreenGapReached Then Return

            CameraPositionX += CType(IIf(oPlayer.MoveStatus = cPlayer.ePlayerMoveStatus.Running, cPlayer.PLAYER_HORIZ_RUN_MOV, cPlayer.PLAYER_HORIZ_WALK_MOV), Int32)

        End Sub

        ''' <summary>
        ''' Draw the background
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics) Implements iDrawable.Draw
            Dim iIndex As Byte

            ' Horizontal Starting Block
            Dim iHorizStartBlock As Integer = CameraPositionX \ BLOCK_WIDTH ' Eg: Position=100 -> (100\44) -> 2
            Dim iShiftToLeft As Integer = CameraPositionX Mod BLOCK_WIDTH ' Eg: Position=100 -> (100 Mod 44) -> 12

            For i As Integer = 0 To SCREEN_VERT_BLOCKS - 1
                For j As Integer = iHorizStartBlock To iHorizStartBlock + SCREEN_HORIZ_BLOCKS ' (SCREEN_HORIZ_BLOCKS - 1)
                    iIndex = arrMatrix(j, i) ' get the TileId asociated to that position
                    oGraphics.DrawImage(lstTiles(iIndex), ((j - iHorizStartBlock) * BLOCK_WIDTH) - iShiftToLeft, (i * BLOCK_HEIGHT), BLOCK_WIDTH, BLOCK_HEIGHT)
                Next j
            Next i
        End Sub

    End Class
End Namespace
