Public Class frmDateTime
    Public strDateTime As String
    Dim HrNow As String = Format(Now, "HH")
    Dim MinNow As String = Format(Now, "mm")
    Dim SecNow As String = Format(Now, "ss")

    Private Sub frmDateTime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cbxHR.Text = HrNow
        cbxMin.Text = MinNow
        strDateTime = ""
        DateTimePicker1.Value = CDate(Format(Now, "yyyy/MM/dd"))
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOK.Click
        strDateTime = DateTimePicker1.Value & " " & cbxHR.Text & ":" & cbxMin.Text & ":" & SecNow
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class