Public Class frmAlarm
    Public Label1 As String
    Public Label2 As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmAlarmLot_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Timer1.Enabled = False
    End Sub

    Private Sub frmAlarmLot_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Me.BackColor = Color.Gold Then
            Me.BackColor = Color.Red
        Else
            Me.BackColor = Color.Gold
        End If
    End Sub
End Class