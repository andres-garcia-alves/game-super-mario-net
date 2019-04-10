Option Strict On
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Imports System.Xml

Namespace SuperMarioNet.Miscelaneous

    Public NotInheritable Class cLanguaje

        Private Sub New()
        End Sub

        Shared sLanguaje As String = ConfigurationManager.AppSettings("languaje")
        Shared sLanguajesDir As String = ConfigurationManager.AppSettings("pathLanguajes")
        Shared oDoc As XmlDocument

        Public Shared Function Initialize() As Boolean
            Try
                oDoc = New XmlDocument()
                oDoc.Load(sLanguajesDir & sLanguaje & ".xml")
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetTextElement(ByVal sElementName As String) As String
            Try

                Select Case sElementName
                    Case "frmMenuBtnPlay" : Return oDoc.GetElementsByTagName("btnPlay").Item(0).InnerText
                    Case "frmMenuBtnHowTo" : Return oDoc.GetElementsByTagName("btnHowTo").Item(0).InnerText
                    Case "frmMenuBtnHighScores" : Return oDoc.GetElementsByTagName("btnHighScores").Item(0).InnerText
                    Case "frmMenuBtnOptions" : Return oDoc.GetElementsByTagName("btnOptions").Item(0).InnerText
                    Case "frmMenuBtnEditor" : Return oDoc.GetElementsByTagName("btnEditor").Item(0).InnerText
                    Case "frmMenuBtnCredits" : Return oDoc.GetElementsByTagName("btnCredits").Item(0).InnerText
                    Case "frmMenuBtnExits" : Return oDoc.GetElementsByTagName("btnExit").Item(0).InnerText
                    Case "frmGameLevelString" : Return oDoc.GetElementsByTagName("level").Item(0).InnerText
                    Case "frmGameTimeString" : Return oDoc.GetElementsByTagName("time").Item(0).InnerText
                    Case "frmGameScoreString" : Return oDoc.GetElementsByTagName("score").Item(0).InnerText
                    Case "frmHowToStep1" : Return oDoc.GetElementsByTagName("step1").Item(0).InnerText
                    Case "frmHowToStep2" : Return oDoc.GetElementsByTagName("step2").Item(0).InnerText
                    Case "frmHowToStep3" : Return oDoc.GetElementsByTagName("step3").Item(0).InnerText
                    Case "frmHowToStep4" : Return oDoc.GetElementsByTagName("step4").Item(0).InnerText
                    Case "frmHowToStep5" : Return oDoc.GetElementsByTagName("step5").Item(0).InnerText
                    Case "frmHowToLblLeyend" : Return oDoc.GetElementsByTagName("lblLeyend").Item(0).InnerText
                    Case "frmHowToBtnBack" : Return oDoc.GetElementsByTagName("btnBack").Item(0).InnerText
                    Case "frmHowToBtnRepeat" : Return oDoc.GetElementsByTagName("btnRepeat").Item(0).InnerText
                    Case "frmOptionsLblGameMode" : Return oDoc.GetElementsByTagName("lblGameMode").Item(0).InnerText
                    Case "frmOptionsLblLanguaje" : Return oDoc.GetElementsByTagName("lblLanguaje").Item(0).InnerText
                    Case "frmOptionsLblLives" : Return oDoc.GetElementsByTagName("lblLives").Item(0).InnerText
                    Case "frmOptionsLblMusic" : Return oDoc.GetElementsByTagName("lblMusic").Item(0).InnerText
                    Case "frmOptionsLblInput" : Return oDoc.GetElementsByTagName("lblInput").Item(0).InnerText
                    Case "frmOptionsLblMenssaje" : Return oDoc.GetElementsByTagName("lblMessaje").Item(0).InnerText
                    Case "frmOptionsBtnSave" : Return oDoc.GetElementsByTagName("btnOK").Item(0).InnerText
                    Case "frmOptionsBtnCancel" : Return oDoc.GetElementsByTagName("btnCancel").Item(0).InnerText
                    Case "frmOptionsBtnRestart" : Return oDoc.GetElementsByTagName("btnRestart").Item(0).InnerText
                    Case Else : Return String.Empty
                End Select

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetTextElement(ByVal sElementName As String, ByVal byIndex As Byte) As String
            Try

                Select Case sElementName
                    Case "frmHighScoresHighScores" : Return oDoc.GetElementsByTagName("reset").Item(0).InnerText.Split("#"c)(byIndex)
                    Case "frmGameLoseLive" : Return oDoc.GetElementsByTagName("loseLive").Item(0).InnerText.Split("#"c)(byIndex)
                    Case "frmGameNoLive" : Return oDoc.GetElementsByTagName("noLive").Item(0).InnerText.Split("#"c)(byIndex)
                    Case "frmGameLevelUp" : Return oDoc.GetElementsByTagName("levelUp").Item(0).InnerText.Replace("\n", vbLf).Split("#"c)(byIndex)
                    Case "frmGameGameFinished" : Return oDoc.GetElementsByTagName("gameFinished").Item(0).InnerText.Split("#"c)(byIndex)
                    Case "frmGameHighScore" : Return oDoc.GetElementsByTagName("highScore").Item(0).InnerText.Split("#"c)(byIndex)
                    Case "frmCreditsTitles" : Return oDoc.GetElementsByTagName("titles").Item(0).InnerText.Split("#"c)(byIndex)
                    Case Else : Return String.Empty
                End Select

            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace
