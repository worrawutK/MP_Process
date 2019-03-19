Imports System.Math
Public Class frmFinalInsp
    Dim m_Numpad As New NumPadForm
    Dim m_KeyBoard As New FrmKeyBoard

    Public _LotNo As String
    Public _StartTime As String
    Public _DefectMode As String
    Public _Remark As String
    Public _McNg As String
    Public _InputQty As Integer
    Public _GoodQty As Integer
    Public _NgQty As Integer


    Private Sub tbDefectMode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbDefectMode.TextChanged
        If tbDefectMode.Text = "-" Then
            tbRemark.Text = "-"
            tbMcNg.Text = "-"
        Else
            tbRemark.Text = ""
            tbMcNg.Text = ""
        End If
    End Sub

    Private Sub btEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btEnd.Click
        FrmKeyBoard.Visible = False
        m_Numpad.Visible = False

        If tbNGQty.Text = "" Then
            MsgBox("กรุณาป้อนค่า NG.Qty ")
            Exit Sub
        ElseIf IsNumeric(tbNGQty.Text) = False Then
            MsgBox("NG.Qty ไม่ถูกต้อง")
            Exit Sub
        ElseIf CInt(lbGoodQty.Text) < 0 Then
            MsgBox("NG.Qty ไม่ถูกต้อง")
            Exit Sub
        ElseIf lbGoodQty.Text = "" Then
            MsgBox("กรุณาป้อนค่า Good Qty ")
            Exit Sub
        ElseIf tbDefectMode.Text = "" Then
            MsgBox("กรุณาป้อนค่า Defect Mode")
            Exit Sub
        ElseIf tbRemark.Text = "" Then
            MsgBox("กรุณาป้อนค่า Remark")
            Exit Sub
        ElseIf tbMcNg.Text = "" Then
            MsgBox("กรุณาป้อนค่า MC NG.")
            Exit Sub
        End If

        _GoodQty = lbGoodQty.Text
        _NgQty = tbNGQty.Text
        _DefectMode = tbDefectMode.Text
        _Remark = tbRemark.Text
        _McNg = tbMcNg.Text
        'If SelfConData.ModeOnline = True Then
        '    m_TdcService.LotEnd(SelfConData.McNo, SelfConData.LotNo, SelfConData.LotEndTime, CInt(SelfConData.GoodQty), CInt(SelfConData.NGQty), EndModeType.Normal, SelfConData.OpNo)
        'End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub tbNGQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbNGQty.TextChanged
        If IsNumeric(tbNGQty.Text) = True Then
            Dim InputQty As Integer = _InputQty
            Dim totalNG As Integer = CInt(tbNGQty.Text)
            lbGoodQty.Text = InputQty - totalNG
        End If
    End Sub

    Private Sub frmFinalInsp_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        m_KeyBoard.Visible = False
        m_Numpad.Visible = False
    End Sub


    Private Sub tbNGQty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbNGQty.Click
        If m_KeyBoard.Visible = True Then
            m_KeyBoard.Visible = False
        End If
        m_Numpad.TargetTextBox = CType(sender, TextBox)
        m_Numpad.Show()

    End Sub

    Private Sub cbDefectMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDefectMode.SelectedIndexChanged
        tbDefectMode.Text = cbDefectMode.Text
    End Sub

    Private Sub tbDefectMode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbDefectMode.Click, tbRemark.Click, tbMcNg.Click
        If m_Numpad.Visible = True Then
            m_Numpad.Visible = False
        End If
        Dim tb As TextBox = CType(sender, TextBox)
        m_KeyBoard.TargetTextBox = tb
        m_KeyBoard.Show()
    End Sub

    Private Sub frmFinalInsp_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbLotNo.Text = _LotNo
        lbStartTime.Text = _StartTime

    End Sub
End Class