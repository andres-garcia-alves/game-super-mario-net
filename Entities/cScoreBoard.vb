Option Strict On
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Configuration
Imports System.IO

Imports Entities.SuperMarioNet.Base
Imports Entities.SuperMarioNet.Interfaces
Imports MiscLibrary.SuperMarioNet.Miscelaneous

Namespace SuperMarioNet.Aux

    Public Class cScoreBoard
        Implements Interfaces.iDrawable

        Private Const FRAMES_PER_STEP As Integer = 8
        Private Const TOTAL_SPRITES As Integer = 4

        Private iCurrentFrame As Integer = 0
        Private iCurrentSprite As Integer = 3

        Private _bVisible As Boolean = True

        Private sScore, sLevel, sTime As String

        Private lstSprites As New List(Of Image)

        Private oFontFamily As FontFamily
        Private oNormalFont, oSmallFont As Font

        Public Sub New()
            Try
                Dim sPathFonts As String = ConfigurationManager.AppSettings("pathFonts")
                Dim sPathImages As String = ConfigurationManager.AppSettings("pathImages")

                ' coin image
                lstSprites.Add(Image.FromFile(sPathImages & "Misc/Coin_Small_01.png", False))
                lstSprites.Add(Image.FromFile(sPathImages & "Misc/Coin_Small_02.png", False))
                lstSprites.Add(Image.FromFile(sPathImages & "Misc/Coin_Small_03.png", False))

                ' base font
                Dim oPrivateFontCollection As New PrivateFontCollection()
                oPrivateFontCollection.AddFontFile(sPathFonts & "emulogic.ttf")
                oFontFamily = oPrivateFontCollection.Families(0)

                oSmallFont = New Font(oFontFamily, 7.0, FontStyle.Regular, GraphicsUnit.Point, 0)
                oNormalFont = New Font(oFontFamily, 12.0, FontStyle.Regular, GraphicsUnit.Point, 0)

                ' base labels
                sScore = cLanguaje.GetTextElement("frmGameScoreString")
                sLevel = cLanguaje.GetTextElement("frmGameLevelString")
                sTime = cLanguaje.GetTextElement("frmGameTimeString")

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

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
        ''' Not Implemented. Don't use it.
        ''' </summary>
        Public Sub Draw(ByRef oGraphics As Graphics) Implements iDrawable.Draw
            Throw New NotSupportedException("Invalid method.")
        End Sub

        ''' <summary>
        ''' Draw the ScoreBoard
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics, ByVal iScore As Integer, ByVal byCoins As Byte, ByVal byWorld As Byte, ByVal byLevel As Byte, ByVal iTime As Integer)

            If Not _bVisible Then Return ' avoid draw if scoreboard is invisible

            ' score text
            oGraphics.DrawString(sScore & vbNewLine & iScore.ToString().PadLeft(6, "0"c), oNormalFont, Brushes.White, New RectangleF(90, 20, 112, 40))

            ' coins text
            oGraphics.DrawString("x", oSmallFont, Brushes.White, New RectangleF(255, 35, 100, 30))
            oGraphics.DrawString(byCoins.ToString().PadLeft(2, "0"c), oNormalFont, Brushes.White, New RectangleF(267, 30, 100, 30))

            ' world/level text
            oGraphics.DrawString(sLevel & vbNewLine & (" " & byWorld.ToString() & "-" & byLevel.ToString()), oNormalFont, Brushes.White, New RectangleF(350, 20, 100, 40))

            ' time left text
            oGraphics.DrawString(sTime & vbNewLine & " " & iTime.ToString().PadLeft(3, "0"c), oNormalFont, Brushes.White, New RectangleF(480, 20, 100, 40))

            ' coins sprite
            Select Case iCurrentSprite
                Case 1 : oGraphics.DrawImage(lstSprites(0), New Point(235, 28))
                Case 2 : oGraphics.DrawImage(lstSprites(1), New Point(235, 28))
                Case 3 : oGraphics.DrawImage(lstSprites(2), New Point(235, 28))
                Case 4 : oGraphics.DrawImage(lstSprites(1), New Point(235, 28))
            End Select

            CheckNextSprite()

        End Sub

        ''' <summary>
        ''' Check the change of the animation sprite
        ''' </summary>
        Private Sub CheckNextSprite()

            iCurrentFrame += 1

            If iCurrentFrame = FRAMES_PER_STEP Then
                iCurrentFrame = 0
                iCurrentSprite += 1
            End If

            If iCurrentSprite > TOTAL_SPRITES Then iCurrentSprite = 1

        End Sub

        Public Function GetPositionRectangle() As Rectangle Implements iDrawable.GetPositionRectangle
            Throw New NotSupportedException("Invalid method.")
        End Function

    End Class
End Namespace
