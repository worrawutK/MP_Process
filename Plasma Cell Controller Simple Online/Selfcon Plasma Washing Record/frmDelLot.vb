Public Class frmDelLot
    Public LotNo As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LotNo = cbLotNo.Text
        If LotNo = "" Then
            MsgBox("กรุณาเลือก LotNo")
            Exit Sub
        End If
        If MessageBox.Show("ต้องการลบข้อมูล LotNo " & LotNo, "", MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.OK Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class