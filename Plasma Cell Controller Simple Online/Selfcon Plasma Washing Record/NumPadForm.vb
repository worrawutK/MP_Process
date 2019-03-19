Public Class NumPadForm

    Private m_TargetTextBox As TextBox
    Public Property TargetTextBox() As TextBox
        Get
            Return m_TargetTextBox
        End Get
        Set(ByVal value As TextBox)
            m_TargetTextBox = value
        End Set
    End Property

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click, Button8.Click, Button7.Click, Button6.Click, Button5.Click, Button4.Click, Button3.Click, Button2.Click, Button10.Click, Button1.Click
        If m_TargetTextBox Is Nothing Then
            Exit Sub
        End If
        Dim btn As Button = CType(sender, Button)
        If IsNumeric(btn.Text) Then

            ' m_TargetTextBox.AppendText(btn.Text)
            m_TargetTextBox.Text = CInt(m_TargetTextBox.Text).ToString()
        End If
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Hide()
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        If m_TargetTextBox Is Nothing Then
            Exit Sub
        End If
        m_TargetTextBox.Focus()
        SendKeys.Send("{TAB}")
        m_TargetTextBox = Nothing
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Try
            m_TargetTextBox.Focus()
            SendKeys.Send("{BS}")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NumPadForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim X As Integer = m_TargetTextBox.Location.X - 30
        'Dim Y As Integer = m_TargetTextBox.Location.Y - 20
        'Me.Location = New Point(X, Y)
    End Sub


End Class