Option Strict On
Imports System.Drawing

Namespace SuperMarioNet.Entities.Interfaces

    Public Interface IDrawable

        Property Visible() As Boolean

        Sub Draw(ByRef oGraphics As Graphics)

        Function GetPositionRectangle() As Rectangle

    End Interface

End Namespace

