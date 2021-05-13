Option Strict On
Imports System.Configuration
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Timers

Imports SuperMarioNet.Entities.Interfaces
Imports SuperMarioNet.Miscellaneous

Namespace SuperMarioNet.Entities

    Public Class cBlackScreen
        Implements IDrawable

        Private _bVisible As Boolean = False

        Private ReadOnly sScore As String
        Private ReadOnly sLevel As String
        Private ReadOnly sTime As String
        Private ReadOnly oImageCoin As Image
        Private ReadOnly oImageMario As Image

        Private ReadOnly oFontFamily As FontFamily
        Private ReadOnly oNormalFont As Font
        Private ReadOnly oSmallFont As Font
        Private tmrDisplay As Timer

        Public Sub New()
            Try
                Dim sPathFonts As String = ConfigurationManager.AppSettings("pathFonts")
                Dim sPathImages As String = ConfigurationManager.AppSettings("pathImages")

                ' load images
                oImageCoin = Image.FromFile(sPathImages & "Misc/Coin_Small_03.png", False)
                oImageMario = Image.FromFile(sPathImages & "Misc/Mario.png", False)

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
        Public Property Visible() As Boolean Implements IDrawable.Visible
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
        Public Sub Draw(ByRef oGraphics As Graphics) Implements IDrawable.Draw
            Throw New NotSupportedException("Invalid method.")
        End Sub

        ''' <summary>
        ''' Draw the BlackScreen
        ''' </summary>
        ''' <param name="oGraphics">Graphics object where to draw</param>
        Public Sub Draw(ByRef oGraphics As Graphics, ByVal iScore As Integer, ByVal byCoins As Byte, ByVal byWorld As Byte, ByVal byLevel As Byte, ByVal iLives As Integer)

            If Not Me.Visible Then Return ' avoid draw if is invisible

            ' clear background
            oGraphics.Clear(Color.Black)

            ' score text
            oGraphics.DrawString(sScore & vbNewLine & iScore.ToString().PadLeft(6, "0"c), oNormalFont, Brushes.White, New RectangleF(90, 50, 112, 40))

            ' coin
            oGraphics.DrawImage(oImageCoin, New Point(235, 58))
            oGraphics.DrawString("x", oSmallFont, Brushes.White, New RectangleF(255, 65, 100, 30))
            oGraphics.DrawString(byCoins.ToString().PadLeft(2, "0"c), oNormalFont, Brushes.White, New RectangleF(267, 60, 100, 30))

            ' world/level text
            oGraphics.DrawString(sLevel & vbNewLine & (" " & byWorld.ToString() & "-" & byLevel.ToString()), oNormalFont, Brushes.White, New RectangleF(350, 50, 100, 40))

            ' time text
            oGraphics.DrawString(sTime, oNormalFont, Brushes.White, New RectangleF(480, 50, 100, 40))

            ' mario + lives
            oGraphics.DrawString(sLevel & " " & byWorld.ToString() & "-" & byLevel.ToString(), oNormalFont, Brushes.White, New RectangleF(232, 200, 160, 40))
            oGraphics.DrawImage(oImageMario, New Point(240, 250))
            oGraphics.DrawString("x", oSmallFont, Brushes.White, New RectangleF(300, 270, 100, 30))
            oGraphics.DrawString(iLives.ToString(), oNormalFont, Brushes.White, New RectangleF(350, 265, 100, 80))

        End Sub

        ''' <summary>
        ''' Display cBlackScreen for a few seconds
        ''' </summary>
        Public Sub EnableBlackScreen()

            tmrDisplay = New Timer(3000)
            AddHandler tmrDisplay.Elapsed, AddressOf DisableBlackScreen
            tmrDisplay.Start()

            Me.Visible = True

        End Sub

        ''' <summary>
        ''' Hide cBlackScreen
        ''' </summary>
        Private Sub DisableBlackScreen(ByVal sender As Object, ByVal e As ElapsedEventArgs)

            tmrDisplay.Stop()
            tmrDisplay.Close()
            RemoveHandler tmrDisplay.Elapsed, AddressOf DisableBlackScreen

            Me.Visible = False

        End Sub

        ''' <summary>
        ''' Returns a Rect struct with the current location of player
        ''' </summary>
        Public Function GetPositionRectangle() As Rectangle Implements IDrawable.GetPositionRectangle
            Throw New NotSupportedException("Invalid method.")
        End Function

    End Class
End Namespace
