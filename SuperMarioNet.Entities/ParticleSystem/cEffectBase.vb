Imports System.Drawing

Namespace SuperMarioNet.Entities.ParticleSystem
    Public MustInherit Class cEffectBase

        Protected iPositionX As Integer
        Protected iPositionY As Integer

        Protected iCurrentFrame As Integer
        Protected iCurrentStep As Integer

        Protected bVisible As Boolean = True

        Public Sub New()
            Me.iPositionX = 0
            Me.iPositionY = 0

            Me.iCurrentFrame = 0
            Me.iCurrentStep = 0
        End Sub

        Public Property PositionX() As Integer
            Get
                Return Me.iPositionX
            End Get
            Set(ByVal value As Integer)
                Me.iPositionX = value
            End Set
        End Property

        Public Overridable Property Visible() As Boolean
            Get
                Return Me.bVisible
            End Get
            Set(ByVal value As Boolean)
                Me.bVisible = value
            End Set
        End Property

        Public MustOverride Sub Draw(ByRef oGraphics As Graphics)

    End Class
End Namespace
