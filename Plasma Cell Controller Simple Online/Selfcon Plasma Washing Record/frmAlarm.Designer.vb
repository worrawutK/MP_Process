<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAlarm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Button1 = New System.Windows.Forms.Button
        Me.lbCaption1 = New System.Windows.Forms.Label
        Me.lbCaption2 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Button1.Location = New System.Drawing.Point(274, 170)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(106, 50)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'lbCaption1
        '
        Me.lbCaption1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbCaption1.Location = New System.Drawing.Point(12, 48)
        Me.lbCaption1.Name = "lbCaption1"
        Me.lbCaption1.Size = New System.Drawing.Size(618, 25)
        Me.lbCaption1.TabIndex = 1
        Me.lbCaption1.Text = "Alarm1"
        Me.lbCaption1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbCaption2
        '
        Me.lbCaption2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lbCaption2.Location = New System.Drawing.Point(17, 111)
        Me.lbCaption2.Name = "lbCaption2"
        Me.lbCaption2.Size = New System.Drawing.Size(613, 25)
        Me.lbCaption2.TabIndex = 2
        Me.lbCaption2.Text = "Alarm2"
        Me.lbCaption2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'frmAlarm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gold
        Me.ClientSize = New System.Drawing.Size(674, 232)
        Me.ControlBox = False
        Me.Controls.Add(Me.lbCaption2)
        Me.Controls.Add(Me.lbCaption1)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmAlarm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmAlarm"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lbCaption1 As System.Windows.Forms.Label
    Friend WithEvents lbCaption2 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
