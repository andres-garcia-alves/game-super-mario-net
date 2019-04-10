Option Strict On

Namespace SuperMarioNet.Miscelaneous

    <Serializable()> _
    Public Class cHighScoreItem

        Private m_iPoints As Integer = 0
        Private m_sName As String = "Empty"

        Public Sub New(ByVal iPoints As Integer, ByVal sName As String)
            Me.m_iPoints = iPoints
            Me.m_sName = sName
        End Sub

        Public Property Points() As Integer
            Get
                Return Me.m_iPoints
            End Get
            Set(ByVal value As Integer)
                Me.m_iPoints = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return Me.m_sName
            End Get
            Set(ByVal value As String)
                Me.m_sName = value
            End Set
        End Property
    End Class

End Namespace
