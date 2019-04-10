Option Strict On
Imports System.Text
Imports System.IO
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Runtime.Serialization.Formatters.Binary

Namespace SuperMarioNet.Miscelaneous

    Public Class cHighScores

        Const CANT_RANKING As Integer = 10
        Const NAME_MAX_LENGHT As Integer = 10

        Private lstHighScores As List(Of cHighScoreItem)

        Public Sub New()
            Try
                lstHighScores = New List(Of cHighScoreItem)()

                For i As Integer = 0 To CANT_RANKING - 1
                    lstHighScores.Add(New cHighScoreItem(0, "Empty"))
                Next

                Dim sPath As String = Application.StartupPath & "\HighScores.dat"

                Dim fs As New FileStream(sPath, FileMode.Open, FileAccess.Read)
                Dim bf As New BinaryFormatter()
                lstHighScores = DirectCast(bf.Deserialize(fs), List(Of cHighScoreItem))
                fs.Close()

            Catch ex1 As FileNotFoundException
                MessageBox.Show("File 'HightScore.dat' not found!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Catch ex2 As Exception
                Throw ex2
            End Try
        End Sub

        Public Function GetHighScores() As List(Of cHighScoreItem)
            Return lstHighScores
        End Function

        ''' <summary>
        ''' Check if a value is a new high score
        ''' </summary>
        ''' <param name="iTestValue">Value to check</param>
        Public Sub CheckNewHighScore(ByVal iTestValue As Integer)
            Try
                For i As Integer = 0 To lstHighScores.Count - 1
                    If iTestValue > lstHighScores(i).Points Then
                        Dim iPosX As Integer = (Screen.PrimaryScreen.Bounds.Width - 300) \ 2
                        Dim iPosY As Integer = (Screen.PrimaryScreen.Bounds.Height - 100) \ 2

                        Dim sTitle As String = cLanguaje.GetTextElement("frmGameHighScore", 0)
                        Dim sMsg1 As String = cLanguaje.GetTextElement("frmGameHighScore", 1)
                        Dim sMsg2 As String = cLanguaje.GetTextElement("frmGameHighScore", 2)

                        Dim sName As String = InputBox(sMsg1 & NAME_MAX_LENGHT & sMsg2, sTitle, "Empty", iPosX, iPosY)
                        If sName.Length > NAME_MAX_LENGHT Then
                            sName = sName.Substring(0, NAME_MAX_LENGHT)
                        End If

                        lstHighScores.Insert(i, New cHighScoreItem(iTestValue, sName))

                        If lstHighScores.Count > CANT_RANKING Then
                            lstHighScores.RemoveAt(CANT_RANKING)
                        End If

                        PersistHighScores()
                        Exit For
                    End If
                Next

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub PersistHighScores()
            Try
                Dim sPath As String = Application.StartupPath & "\HighScores.dat"

                Dim fs As New FileStream(sPath, FileMode.Create, FileAccess.Write)
                Dim bf As New BinaryFormatter()
                bf.Serialize(fs, lstHighScores)
                fs.Close()

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub ResetHightScores()

            lstHighScores = New List(Of cHighScoreItem)()

            For i As Integer = 0 To CANT_RANKING - 1
                lstHighScores.Add(New cHighScoreItem(0, "Empty"))
            Next

            PersistHighScores()

        End Sub

    End Class
End Namespace
