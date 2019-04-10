Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing

Imports Entities.SuperMarioNet.Interfaces
Imports Entities.SuperMarioNet.Entities

Namespace SuperMarioNet.Aux
    Public Class cParticlesSystem

        Private i As Integer = 0
        Private _bVisible As Boolean = True

        Shared lstEffects As New List(Of cEffectBase)

        ''' <summary>
        ''' Add a new effect to playing list
        ''' </summary>
        Public Shared Sub RegisterEfect(ByVal oEffect As cEffectBase)
            lstEffects.Add(oEffect)
        End Sub

        ''' <summary>
        ''' Remove the received effect from list
        ''' </summary>
        Public Shared Sub RemoveEfect(ByVal oEffect As cEffectBase)
            lstEffects.Remove(oEffect)
        End Sub

        ''' <summary>
        ''' Clear all effects in list
        ''' </summary>
        Public Sub Reset()
            lstEffects.Clear()
        End Sub

        ''' <summary>
        ''' Move to right
        ''' </summary>
        ''' <param name="oPlayer">cPlayer object</param>
        Public Sub MoveRight(ByRef oPlayer As cPlayer)

            ' don't move monster position, until player PositionX reach minimium/maximium posible value on the screen
            If Not oPlayer.ScreenGapReached Then Return

            For Each oEffectBase As cEffectBase In lstEffects
                oEffectBase.PositionX -= CType(IIf(oPlayer.MoveStatus = cPlayer.ePlayerMoveStatus.Running, cPlayer.PLAYER_HORIZ_RUN_MOV, cPlayer.PLAYER_HORIZ_WALK_MOV), Int32)
            Next

        End Sub

        ''' <summary>
        ''' Draw current step for all effects in the list
        ''' </summary>
        Public Sub Draw(ByRef oGraphics As Graphics)

            i = 0
            Do While i < lstEffects.Count
                lstEffects(i).Draw(oGraphics)
                i += 1
            Loop

        End Sub

    End Class
End Namespace
