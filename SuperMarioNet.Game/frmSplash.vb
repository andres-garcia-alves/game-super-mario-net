Option Strict On
Imports System.Configuration
Imports System.Drawing.Text

Public NotInheritable Class frmSplash

    Private oFontFamily As FontFamily

    ''' <summary>
    ''' Set up the dialog text at runtime according to the application's assembly information
    ''' </summary>
    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Dim sPathFonts As String = ConfigurationManager.AppSettings("pathFonts")
            Dim sPathImages As String = ConfigurationManager.AppSettings("pathImages")

            ' base font
            Dim oPrivateFontCollection As New PrivateFontCollection()
            oPrivateFontCollection.AddFontFile(sPathFonts & "emulogic.ttf")
            oFontFamily = oPrivateFontCollection.Families(0)

            lblApplicationTitle.Font = New Font(oFontFamily, 11.0, FontStyle.Regular, GraphicsUnit.Point, 0)
            lblVersion.Font = New Font(oFontFamily, 6.0, FontStyle.Regular, GraphicsUnit.Point, 0)

            lblApplicationTitle.Text = My.Application.Info.Title
            lblVersion.Text = "Version " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
