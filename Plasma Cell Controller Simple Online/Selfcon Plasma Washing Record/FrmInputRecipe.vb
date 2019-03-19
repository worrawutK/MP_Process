Public Class FrmInputRecipe

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If tbRecipeName.Text = "" Then
            MsgBox("กรุณากรอก Recipe Name")
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK

    End Sub
End Class