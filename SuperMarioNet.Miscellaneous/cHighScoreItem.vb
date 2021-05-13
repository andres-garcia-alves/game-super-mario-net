Option Strict On

Namespace SuperMarioNet.Miscellaneous

    <Serializable()>
    Public Class cHighScoreItem

        Private _iPoints As Integer = 0
        Private _sName As String = "Empty"

        Public Sub New(ByVal iPoints As Integer, ByVal sName As String)
            Me._iPoints = iPoints
            Me._sName = sName
        End Sub

        Public Property Points() As Integer
            Get
                Return Me._iPoints
            End Get
            Set(ByVal value As Integer)
                Me._iPoints = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return Me._sName
            End Get
            Set(ByVal value As String)
                Me._sName = value
            End Set
        End Property
    End Class

End Namespace
