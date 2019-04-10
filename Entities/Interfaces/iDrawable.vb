Option Strict On
Imports System.Drawing

Namespace SuperMarioNet.Interfaces

    Public Interface iDrawable

        Property Visible() As Boolean

        Sub Draw(ByRef oGraphics As Graphics)

        Function GetPositionRectangle() As Rectangle

    End Interface

End Namespace

