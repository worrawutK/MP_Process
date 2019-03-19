Public Class frmInputQR
    Public QRCode As String
    Public QROpNo As String = ""
    Public Caption As String
    Public intInputQty As Integer

    Private Sub tbRevQR_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tbRevQR.KeyPress
        If e.KeyChar = Chr(13) Then
            If tbRevQR.Text.Length = 252 AndAlso lbCaption.Text = "SCAN QR CODE" Then
                QRCode = ""
                QRCode = tbRevQR.Text
                lbCaption.Text = "SCAN OP No.."
            ElseIf tbRevQR.Text.Length = 6 AndAlso lbCaption.Text = "SCAN OP No.." Then
                QROpNo = ""
                QROpNo = tbRevQR.Text
                lbCaption.Text = "Input Qty"
                ProgressBar1.Visible = False
                tbInputQty.Visible = True
                Me.Width = 479
                Me.Height = 461
        
            Else
                ProgressBar1.Value = 0
                tbRevQR.Clear()
                MsgBox("QRCode ไม่ถูกต้องกรุณาสแกนใหม่")
                Exit Sub
            End If
            ProgressBar1.Value = 0
            tbRevQR.Clear()
        End If
        'If e.KeyChar = Chr(13) Then
        '    If tbRevQR.Text.Length = 252 AndAlso lbCaption.Text = "SCAN QR CODE" Then
        '        QRCode = ""
        '        QRCode = tbRevQR.Text
        '        ProgressBar1.Value = 0
        '        tbRevQR.Clear()
        '        lbCaption.Text = "SCAN OP No.."
        '    ElseIf tbRevQR.Text.Length = 6 AndAlso lbCaption.Text = "SCAN OP No.." Then
        '        QROpNo = ""
        '        QROpNo = tbRevQR.Text
        '        ProgressBar1.Value = 0
        '        tbRevQR.Clear()
        '        lbCaption.Text = "Input Qty"
        '        ProgressBar1.Visible = False
        '        tbInputQty.Visible = True
        '        Me.Width = 479
        '        Me.Height = 461
        '    ElseIf tbRevQR.Text.Length = 6 AndAlso IsNumeric(tbRevQR.Text) = True AndAlso lbCaption.Text = "SCAN OP No." Then
        '        QROpNo = ""
        '        QROpNo = tbRevQR.Text
        '        ProgressBar1.Value = 0
        '        tbRevQR.Clear()
        '        Me.DialogResult = Windows.Forms.DialogResult.Yes
        '    Else
        '        ProgressBar1.Value = 0
        '        tbRevQR.Clear()
        '        MsgBox("QRCode ไม่ถูกต้องกรุณาสแกนใหม่")
        '        Exit Sub
        '    End If

        'End If
    End Sub

    Private Sub frmInputQR_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        tbRevQR.Focus()
    End Sub


    Private Sub frmInputQR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbCaption.Text = Caption
        Me.Width = 479
        Me.Height = 137
        Timer1.Start()
        tbRevQR.Focus()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If lbCaption.Text = "SCAN QR CODE" Then
                ProgressBar1.Value = (tbRevQR.Text.Length / 252) * 100
            ElseIf lbCaption.Text = "SCAN OP No.." Then
                ProgressBar1.Value = (tbRevQR.Text.Length / 6) * 100
            ElseIf lbCaption.Text = "SCAN OP No." Then
                ProgressBar1.Value = (tbRevQR.Text.Length / 6) * 100
            ElseIf lbCaption.Text = "SCAN GL No." Then
                ProgressBar1.Value = (tbRevQR.Text.Length / 6) * 100
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click, Button9.Click, Button8.Click, Button7.Click, Button6.Click, Button5.Click, Button4.Click, Button3.Click, Button2.Click, Button11.Click, Button10.Click
        Dim bt As Button = CType(sender, Button)
        tbInputQty.Focus()
        If bt.Text <> "BS" Then
            SendKeys.Send(bt.Text)
        Else
            SendKeys.Send("{BS}")
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        If tbInputQty.Text = "" Or tbInputQty.Text = "0" Then
            MsgBox("กรุณากรอก InputQty")
            Exit Sub
        ElseIf IsNumeric(tbInputQty.Text) = False Then
            MsgBox("InputQty ไม่ถูกต้อง กรุณาตรวจสอบ")
            Exit Sub
        End If

        If CInt(tbInputQty.Text) >= My.Settings.SetInPut Then
            MsgBox("กรุณาตรวจสอบ Input")
            Exit Sub
        End If
        intInputQty = tbInputQty.Text

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class