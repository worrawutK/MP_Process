<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFinalInsp
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
        Me.tbNGQty = New System.Windows.Forms.TextBox
        Me.tbMcNg = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.tbDefectMode = New System.Windows.Forms.TextBox
        Me.cbDefectMode = New System.Windows.Forms.ComboBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.lbGoodQty = New System.Windows.Forms.Label
        Me.tbRemark = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.lbStartTime = New System.Windows.Forms.Label
        Me.lbLotNo = New System.Windows.Forms.Label
        Me.btEnd = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'tbNGQty
        '
        Me.tbNGQty.BackColor = System.Drawing.Color.White
        Me.tbNGQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.tbNGQty.Location = New System.Drawing.Point(161, 133)
        Me.tbNGQty.Name = "tbNGQty"
        Me.tbNGQty.Size = New System.Drawing.Size(107, 26)
        Me.tbNGQty.TabIndex = 286
        Me.tbNGQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbMcNg
        '
        Me.tbMcNg.BackColor = System.Drawing.Color.White
        Me.tbMcNg.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.tbMcNg.Location = New System.Drawing.Point(149, 276)
        Me.tbMcNg.Name = "tbMcNg"
        Me.tbMcNg.Size = New System.Drawing.Size(202, 23)
        Me.tbMcNg.TabIndex = 295
        Me.tbMcNg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(44, 276)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 18)
        Me.Label2.TabIndex = 294
        Me.Label2.Text = "M/C NG."
        '
        'tbDefectMode
        '
        Me.tbDefectMode.BackColor = System.Drawing.Color.White
        Me.tbDefectMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.tbDefectMode.Location = New System.Drawing.Point(149, 207)
        Me.tbDefectMode.Name = "tbDefectMode"
        Me.tbDefectMode.Size = New System.Drawing.Size(202, 23)
        Me.tbDefectMode.TabIndex = 293
        Me.tbDefectMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbDefectMode
        '
        Me.cbDefectMode.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.cbDefectMode.FormattingEnabled = True
        Me.cbDefectMode.Items.AddRange(New Object() {"-", "1. FRAM BENT", "2. QUALITY NG.", "3. M/C ABNORMAL"})
        Me.cbDefectMode.Location = New System.Drawing.Point(149, 175)
        Me.cbDefectMode.Name = "cbDefectMode"
        Me.cbDefectMode.Size = New System.Drawing.Size(202, 26)
        Me.cbDefectMode.TabIndex = 292
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label28.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label28.Location = New System.Drawing.Point(44, 180)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(99, 17)
        Me.Label28.TabIndex = 291
        Me.Label28.Text = "Defect Mode"
        '
        'lbGoodQty
        '
        Me.lbGoodQty.BackColor = System.Drawing.Color.White
        Me.lbGoodQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbGoodQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lbGoodQty.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbGoodQty.Location = New System.Drawing.Point(439, 133)
        Me.lbGoodQty.Name = "lbGoodQty"
        Me.lbGoodQty.Size = New System.Drawing.Size(76, 23)
        Me.lbGoodQty.TabIndex = 289
        Me.lbGoodQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbRemark
        '
        Me.tbRemark.BackColor = System.Drawing.Color.White
        Me.tbRemark.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.tbRemark.Location = New System.Drawing.Point(149, 237)
        Me.tbRemark.Name = "tbRemark"
        Me.tbRemark.Size = New System.Drawing.Size(202, 23)
        Me.tbRemark.TabIndex = 266
        Me.tbRemark.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Chartreuse
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label24.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label24.Location = New System.Drawing.Point(290, 136)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(143, 17)
        Me.Label24.TabIndex = 285
        Me.Label24.Text = "GOOD Q'TY (PCS)"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Red
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Label25.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label25.Location = New System.Drawing.Point(19, 139)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(124, 17)
        Me.Label25.TabIndex = 284
        Me.Label25.Text = "NG. Q'TY (PCS)"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.Color.Transparent
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label26.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label26.Location = New System.Drawing.Point(44, 237)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(67, 18)
        Me.Label26.TabIndex = 251
        Me.Label26.Text = "Remark"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(55, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 25)
        Me.Label1.TabIndex = 298
        Me.Label1.Text = "LotNo :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 25)
        Me.Label3.TabIndex = 298
        Me.Label3.Text = "Start Time :"
        '
        'lbStartTime
        '
        Me.lbStartTime.AutoSize = True
        Me.lbStartTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbStartTime.Location = New System.Drawing.Point(136, 72)
        Me.lbStartTime.Name = "lbStartTime"
        Me.lbStartTime.Size = New System.Drawing.Size(113, 25)
        Me.lbStartTime.TabIndex = 298
        Me.lbStartTime.Text = "Start Time :"
        '
        'lbLotNo
        '
        Me.lbLotNo.AutoSize = True
        Me.lbLotNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lbLotNo.Location = New System.Drawing.Point(136, 22)
        Me.lbLotNo.Name = "lbLotNo"
        Me.lbLotNo.Size = New System.Drawing.Size(113, 25)
        Me.lbLotNo.TabIndex = 298
        Me.lbLotNo.Text = "Start Time :"
        '
        'btEnd
        '
        Me.btEnd.BackColor = System.Drawing.Color.DarkOrange
        Me.btEnd.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold)
        Me.btEnd.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btEnd.Location = New System.Drawing.Point(384, 224)
        Me.btEnd.Name = "btEnd"
        Me.btEnd.Size = New System.Drawing.Size(99, 70)
        Me.btEnd.TabIndex = 299
        Me.btEnd.Text = "END"
        Me.btEnd.UseVisualStyleBackColor = False
        '
        'frmFinalInsp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 325)
        Me.Controls.Add(Me.btEnd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbMcNg)
        Me.Controls.Add(Me.tbNGQty)
        Me.Controls.Add(Me.tbRemark)
        Me.Controls.Add(Me.tbDefectMode)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.lbLotNo)
        Me.Controls.Add(Me.lbStartTime)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbDefectMode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.lbGoodQty)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.Label24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFinalInsp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmFinalInsp"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tbNGQty As System.Windows.Forms.TextBox
    Friend WithEvents tbMcNg As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbDefectMode As System.Windows.Forms.TextBox
    Friend WithEvents cbDefectMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lbGoodQty As System.Windows.Forms.Label
    Friend WithEvents tbRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbStartTime As System.Windows.Forms.Label
    Friend WithEvents lbLotNo As System.Windows.Forms.Label
    Friend WithEvents btEnd As System.Windows.Forms.Button
End Class
