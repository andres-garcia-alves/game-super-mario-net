Option Strict On
Imports System.Configuration
Imports System.Xml

Namespace SuperMarioNet.Miscellaneous

    Public NotInheritable Class cLanguaje

        Private Sub New()
        End Sub

        Shared ReadOnly languaje As String = ConfigurationManager.AppSettings("languaje")
        Shared ReadOnly languajesDir As String = ConfigurationManager.AppSettings("pathLanguajes")
        Shared xmlDocument As XmlDocument

        Public Shared Function Initialize() As Boolean
            Try
                xmlDocument = New XmlDocument()
                xmlDocument.Load(languajesDir & languaje & ".xml")
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetTextElement(ByVal elementName As String) As String
            Try

                Select Case elementName
                    Case "frmMenuBtnPlay" : Return xmlDocument.GetElementsByTagName("btnPlay").Item(0).InnerText
                    Case "frmMenuBtnHowTo" : Return xmlDocument.GetElementsByTagName("btnHowTo").Item(0).InnerText
                    Case "frmMenuBtnHighScores" : Return xmlDocument.GetElementsByTagName("btnHighScores").Item(0).InnerText
                    Case "frmMenuBtnOptions" : Return xmlDocument.GetElementsByTagName("btnOptions").Item(0).InnerText
                    Case "frmMenuBtnEditor" : Return xmlDocument.GetElementsByTagName("btnEditor").Item(0).InnerText
                    Case "frmMenuBtnCredits" : Return xmlDocument.GetElementsByTagName("btnCredits").Item(0).InnerText
                    Case "frmMenuBtnExits" : Return xmlDocument.GetElementsByTagName("btnExit").Item(0).InnerText
                    Case "frmGameLevelString" : Return xmlDocument.GetElementsByTagName("level").Item(0).InnerText
                    Case "frmGameTimeString" : Return xmlDocument.GetElementsByTagName("time").Item(0).InnerText
                    Case "frmGameScoreString" : Return xmlDocument.GetElementsByTagName("score").Item(0).InnerText
                    Case "frmHowToStep1" : Return xmlDocument.GetElementsByTagName("step1").Item(0).InnerText
                    Case "frmHowToStep2" : Return xmlDocument.GetElementsByTagName("step2").Item(0).InnerText
                    Case "frmHowToStep3" : Return xmlDocument.GetElementsByTagName("step3").Item(0).InnerText
                    Case "frmHowToStep4" : Return xmlDocument.GetElementsByTagName("step4").Item(0).InnerText
                    Case "frmHowToStep5" : Return xmlDocument.GetElementsByTagName("step5").Item(0).InnerText
                    Case "frmHowToLblLeyend" : Return xmlDocument.GetElementsByTagName("lblLeyend").Item(0).InnerText
                    Case "frmHowToBtnBack" : Return xmlDocument.GetElementsByTagName("btnBack").Item(0).InnerText
                    Case "frmHowToBtnRepeat" : Return xmlDocument.GetElementsByTagName("btnRepeat").Item(0).InnerText
                    Case "frmOptionsLblGameMode" : Return xmlDocument.GetElementsByTagName("lblGameMode").Item(0).InnerText
                    Case "frmOptionsLblLanguaje" : Return xmlDocument.GetElementsByTagName("lblLanguaje").Item(0).InnerText
                    Case "frmOptionsLblLives" : Return xmlDocument.GetElementsByTagName("lblLives").Item(0).InnerText
                    Case "frmOptionsLblMusic" : Return xmlDocument.GetElementsByTagName("lblMusic").Item(0).InnerText
                    Case "frmOptionsLblInput" : Return xmlDocument.GetElementsByTagName("lblInput").Item(0).InnerText
                    Case "frmOptionsLblMenssaje" : Return xmlDocument.GetElementsByTagName("lblMessaje").Item(0).InnerText
                    Case "frmOptionsBtnSave" : Return xmlDocument.GetElementsByTagName("btnOK").Item(0).InnerText
                    Case "frmOptionsBtnCancel" : Return xmlDocument.GetElementsByTagName("btnCancel").Item(0).InnerText
                    Case "frmOptionsBtnRestart" : Return xmlDocument.GetElementsByTagName("btnRestart").Item(0).InnerText
                    Case Else : Return String.Empty
                End Select

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetTextElement(ByVal elementName As String, ByVal index As Byte) As String
            Try

                Select Case elementName
                    Case "frmHighScoresHighScores" : Return xmlDocument.GetElementsByTagName("reset").Item(0).InnerText.Split("#"c)(index)
                    Case "frmGameLoseLive" : Return xmlDocument.GetElementsByTagName("loseLive").Item(0).InnerText.Split("#"c)(index)
                    Case "frmGameNoLive" : Return xmlDocument.GetElementsByTagName("noLive").Item(0).InnerText.Split("#"c)(index)
                    Case "frmGameLevelUp" : Return xmlDocument.GetElementsByTagName("levelUp").Item(0).InnerText.Replace("\n", vbLf).Split("#"c)(index)
                    Case "frmGameGameFinished" : Return xmlDocument.GetElementsByTagName("gameFinished").Item(0).InnerText.Split("#"c)(index)
                    Case "frmGameHighScore" : Return xmlDocument.GetElementsByTagName("highScore").Item(0).InnerText.Split("#"c)(index)
                    Case "frmCreditsTitles" : Return xmlDocument.GetElementsByTagName("titles").Item(0).InnerText.Split("#"c)(index)
                    Case Else : Return String.Empty
                End Select

            Catch ex As Exception
                Throw ex
            End Try
        End Function

    End Class
End Namespace
