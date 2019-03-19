Imports System.IO
Imports System.Runtime.Serialization.Formatters.Soap
Imports System.Threading
Imports System.Xml.Serialization
Imports Rohm.Apcs.Tdc
Imports Rohm.Ems
Imports Selfcon_Plasma_Washing_Record.RohmService

'เขียนไล่จากปุ่มด้านบนสุด จนไปถึงแถบเมนู จึงเริ่มเขียนโค๊ด จังหวะโหลดโปรแกรม
Public Class Frmmain
    Public m_EmsClient As EmsServiceClient = New EmsServiceClient("MP", "http://webserv.thematrix.net:7777/EmsService")
    ' ประกาศตัวแปร
    Private m_TdcService As TdcService
    Dim m_Locker As New Object
    Private m_LotSetQueue As Queue(Of String) = New Queue(Of String)
    Private m_LotEndQueue As Queue(Of String) = New Queue(Of String)
    Private m_LotReqQueue As String
    Dim li As LotInfo
    Dim m_dlg As TdcAlarmMessageForm
    Dim m_LotReqMes As String

    Dim QRCode252 As String
    ' Private m_TdcService As TdcService
    Dim m_Traget As TextBox
    Dim NewLot As Boolean
    Enum PlasmaStatus
        LotSet = 1
        LotStart = 2
        LotEnd = 3
        LotClear = 4
    End Enum
    Sub AAAA(ByVal aaaa As PlasmaStatus)
        aaaa.ToString()
        '  Return ""
    End Sub

    Private Sub Frmmain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        m_TdcService = TdcService.GetInstance()
        m_TdcService.ConnectionString = My.Settings.APCSDBConnectionString

        Try
            Dim reg As EmsMachineRegisterInfo = New EmsMachineRegisterInfo(My.Settings.MCNo, "MP-" & My.Settings.MCNo, "MP", My.Settings.MCType, "", 0, 0, 0, 0, 0)
            m_EmsClient.Register(reg)
        Catch ex As Exception
            '  SaveLog(Message.Cellcon, "EmsMachineRegisterInfo :" & ex.ToString)
        End Try

        If My.Settings.RunOffline = MCMode.Online Then
            lbStateMC.Text = MCMode.Online.ToString
        Else
            lbStateMC.Text = MCMode.Offline.ToString
        End If

        '   lbMCNoShow.Text = My.Settings.MCNo
        lbMachineNo.Text = My.Settings.MCNo
        Label21.Text = My.Settings.MCType
        LoadXml()
        If para.Status = PlasmaStatus.LotSet.ToString Then
            LoadQR()
        ElseIf para.Status = PlasmaStatus.LotStart.ToString Then
            LoadQR()
            Start()
            PanelEnd.Visible = True
            btEnd.Visible = True


            'BtStart.Visible = False
            btEnd.Visible = True
            PanelEnd.Enabled = True
            Start()
        ElseIf para.Status = PlasmaStatus.LotEnd.ToString Then
            LoadQR()
            Start()
            EndLot()
        End If
        ' AAAA(PlasmaStatus.LotClear)





    End Sub








    Private Sub MinimizeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MinimizeButton.Click
        Me.WindowState = FormWindowState.Minimized  'ย่อยจอไปเก็บที่ Taskbar
    End Sub

    Private Sub BtExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtExit.Click
        Dim e1 As MsgBoxResult = MsgBox("Do You Want Exit", MsgBoxStyle.YesNo, "Exit") ' ถามว่าจะออกจากโปรแกรมหรือไม
        If e1 = MsgBoxResult.Yes Then

            '    SaveDataTableXml() 'Save Data ก่อนปิดโปรแกรม
            Me.Close()
        End If
    End Sub

    Private Sub lbAndon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbAndon.Click
        'กดปุ่ม Andon แล้วเข้า Internet Explorer
        Try
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv/andontmn", AppWinStyle.NormalFocus) 'Web andon for manual M/C
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub lbWkRecd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbWkRecd.Click
        'กดปุ่ม WorkRecord แล้วเข้า Internet Explorer
        Try
            Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv/ERECORD/", AppWinStyle.NormalFocus)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lbHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbHelp.Click
        Process.Start(My.Application.Info.DirectoryPath & "\PlasmaManual.pdf")
    End Sub

    Private Sub lbBMRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbBMRequest.Click
        'กดปุ่ม BM Request แล้วเข้า Internet Explorer
        Dim tmpStr As String
        tmpStr = "MCNo=" & My.Settings.MCNo
        tmpStr = tmpStr & "&LotNo=" & para.LotNo
        If tmpPlasmaData.StatusLot <> PlasmaStatus.LotEnd AndAlso tmpPlasmaData.StatusLot <> PlasmaStatus.LotClear Then
            tmpStr = tmpStr & "&MCStatus=Running"
        Else
            tmpStr = tmpStr & "&MCStatus=Stop"
        End If

        Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv.thematrix.net/LsiPETE/LSI_Prog/Maintenance/MainloginPD.asp?" & tmpStr, vbNormalFocus)
    End Sub

    Private Sub lbPMRepairing_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbPMRepairing.Click
        ''กดปุ่ม PM Repairing แล้วเข้า Internet Explorer

        Call Shell("C:\Program Files\Internet Explorer\iexplore.exe http://webserv.thematrix.net/LsiPETE/LSI_Prog/Maintenance/MainPMlogin.asp?" & "MCNo=" & My.Settings.MCNo, vbNormalFocus)
    End Sub

    Private Sub LoadQR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScanQR.Click

        If para.Status = PlasmaStatus.LotStart.ToString Then
            MsgBox("กรุณาจบ Lot ก่อน")
            Exit Sub
        End If
        lbLotInfo.Text = ""
        Dim frmQR As frmInputQR = New frmInputQR
        frmQR.Caption = "SCAN QR CODE"
        If frmQR.ShowDialog = Windows.Forms.DialogResult.OK Then
            para.QRcode252 = frmQR.QRCode
            para.Package = Trim(para.QRcode252.Substring(0, 10)).ToUpper
            para.Device = Trim(para.QRcode252.Substring(10, 20)).ToUpper
            para.LotNo = Trim(para.QRcode252.Substring(30, 10)).ToUpper
            para.InputQty = CStr(frmQR.intInputQty)
            para.OPNo = frmQR.QROpNo



            ' _RunMode = MCMode.Online
            '  _RunMode = MCMode.Offline

            ' _01 = False

            If My.Settings.RunOffline = MCMode.Online Then ' Online
                BtStart.Enabled = False
                If LotRequestTDC(para.LotNo, RunModeType.Normal, My.Settings.MCNo) = False Then

                    Exit Sub
                End If
                '  BtStart.Visible = True
                BtStart.Enabled = True
                'm_LotReqQueue = My.Settings.MCNo & "," & para.LotNo & "," & "0"
                ' bgTDCLotReq.RunWorkerAsync()

            Else
                para.MCLock = MCLock.Unlock
                para.LotInfor = "Run Offline"
                BtStart.Enabled = True
            End If

            LoadQR()
            ' para.Status = "LoadQR"
            para.Status = PlasmaStatus.LotSet.ToString
            SeveparaXml()

            'SavePlasmaDataListXml()
            SaveCatchLog("LoadQR_Click >>" & para.Status & ">>OPNo=" & para.OPNo & ">>InputQty=" & para.InputQty, para.QRcode252)
        End If
        NewLot = True
    End Sub
    Function LotRequestTDC(ByVal LotNo As String, ByVal rm As RunModeType, ByVal MCNo As String) As Boolean
        ' Dim mc As String = "MAP-" & MCNo
        Dim strMess As String = ""
        Dim res As TdcLotRequestResponse = m_TdcService.LotRequest(MCNo, LotNo, rm)

        If res.HasError Then

            Using svError As ApcsWebServiceSoapClient = New ApcsWebServiceSoapClient
                If svError.LotRptIgnoreError(MCNo, res.ErrorCode) = False Then
                    Dim li As LotInfo = Nothing
                    li = m_TdcService.GetLotInfo(LotNo, MCNo)
                    Using dlg As TdcAlarmMessageForm = New TdcAlarmMessageForm(res.ErrorCode, res.ErrorMessage, LotNo, li)
                        dlg.TopMost = True
                        dlg.ShowDialog()

                        Return False
                    End Using
                End If
            End Using
            strMess = res.ErrorCode & " : " & res.ErrorMessage
            Return True
        Else
            strMess = "00 : Run Normal"
            Return True
        End If
    End Function
    Private Sub LoadQR()
        QRCode252 = para.QRcode252
        lbPackage.Text = para.Package
        lbDevice.Text = para.Device
        lbLotNo.Text = para.LotNo
        lbInputQty.Text = para.InputQty
        lbOPNo.Text = para.OPNo
        lbEndTime.Text = ""
        lbStartTime.Text = ""
        tbNGQty.Text = ""
        lbGoodQty.Text = ""
        cbDefectMode.Text = ""
        ' tbDefectMode.Text = ""
        tbRemark.Text = ""
        tbRequest.Text = ""
    End Sub

    Private Sub Start_Click(sender As System.Object, e As System.EventArgs) Handles BtStart.Click
        '   SaveCatchLog("Start_Click", "Start_Click()")
        If para.Status = PlasmaStatus.LotEnd.ToString Then
            MsgBox("จบ Lot แล้ว")
            Exit Sub
        End If
        If QRCode252 = "" Then
            MsgBox("กรุณา Scan QR Code")
            Exit Sub
        End If
        para.Status = PlasmaStatus.LotStart.ToString
        ' If NewLot = True Then
        para.StartTime = Format(Now, "yyyy/MM/dd HH:mm:ss")
        NewLot = False
        Dim insertdata As DBxDataSet.MPPlasmaDataRow = DBxDataSet.MPPlasmaData.NewMPPlasmaDataRow
        insertdata.MCNo = My.Settings.MCNo
        insertdata.MCType = My.Settings.MCType
        insertdata.LotNo = lbLotNo.Text
        insertdata.LotStartTime = CDate(para.StartTime)
        insertdata.OpNo = lbOPNo.Text
        insertdata.InputQty = lbInputQty.Text

        DBxDataSet.MPPlasmaData.Rows.Add(insertdata)
        MpPlasmaDataTableAdapter1.Update(DBxDataSet.MPPlasmaData)
        'End If

        lbGoodQty.Text = lbInputQty.Text
        tbNGQty.Text = "0"
        PanelEnd.Visible = True
        btEnd.Visible = True
        ''insert
        'Dim insertdata As DBxDataSet.PlasmaDataRow = DBxDataSet.PlasmaData.NewPlasmaDataRow
        'insertdata.MCNo = My.Settings.MCNo
        'insertdata.MCType = My.Settings.MCType
        'insertdata.LotNo = lbLotNo.Text
        'insertdata.LotStartTime = CDate(para.StartTime)
        'insertdata.OpNo = lbOPNo.Text
        'insertdata.InputQty = lbInputQty.Text

        'DBxDataSet.PlasmaData.Rows.Add(insertdata)
        'PlasmaDataTableAdapter.Update(DBxDataSet.PlasmaData)

        'insert

        '  MpPlasmaDataTableAdapter1.Update()

        BtStart.Enabled = False
        btEnd.Visible = True
        PanelEnd.Enabled = True
        Start()
        SeveparaXml()
        SaveCatchLog("Start_Click >>" & para.Status & "Start=" & para.StartTime & ">>OPNo=" & para.OPNo & ">>InputQty=" & para.InputQty, para.QRcode252)

        Dim resSet As TdcResponse = m_TdcService.LotSet(My.Settings.MCNo, para.LotNo, para.StartTime, para.OPNo, RunModeType.Normal)
        'EMS monitor
        Try
            m_EmsClient.SetCurrentLot(My.Settings.MCNo, para.LotNo, 0)
            m_EmsClient.SetActivity(My.Settings.MCNo, "Running", TmeCategory.NetOperationTime)
        Catch ex As Exception
            ' SaveLog(Message.Cellcon, "SetActivity Running:" & ex.ToString)
        End Try
        'Send TDC
        'SyncLock m_Locker
        '    Dim strLotSetData As String = My.Settings.MCNo & "," & para.LotNo & "," & para.StartTime & "," & para.OPNo & "," & "0"
        '    m_LotSetQueue.Enqueue(strLotSetData)

        'End SyncLock

        'If bgTDC.IsBusy = False Then
        '    ' lbLotSetEnd.BackColor = Color.Lime
        '    bgTDC.RunWorkerAsync()
        'End If
    End Sub
    Private Sub Start()

        lbStartTime.Text = para.StartTime



    End Sub


    Private Sub KeyNum_Click(sender As System.Object, e As System.EventArgs) Handles Button15.Click, Button9.Click, Button8.Click, Button7.Click, Button6.Click, Button4.Click, Button3.Click, Button14.Click, Button13.Click, Button12.Click, Button10.Click
        Dim tb As Button = CType(sender, Button)
        m_Traget.Focus()
        If tb.Text <> "BS" Then
            SendKeys.Send(tb.Text)
        Else
            SendKeys.Send("{BS}")
        End If
    End Sub

    Private Sub tbDefectMode_Click(sender As System.Object, e As System.EventArgs) Handles tbNGQty.Click, tbRemark.Click, cbDefectMode.Click, tbRequest.Click ', tbDefectMode.Click
        Dim val As String = sender.name
        If val = "tbNGQty" Then
            GroupBoxKeyNum.Visible = True
            PanelKey.Visible = False
        ElseIf val = "cbDefectMode" Or val = "tbMcNg" Then
            GroupBoxKeyNum.Visible = False
            PanelKey.Visible = False
        Else
            GroupBoxKeyNum.Visible = False
            PanelKey.Visible = True
        End If
        PanelMaster.Visible = False
        Try
            m_Traget = CType(sender, TextBox)
        Catch ex As Exception

        End Try

    End Sub



    Private Sub BtSpacebar_Click(sender As System.Object, e As System.EventArgs) Handles BtZ.Click, BtY.Click, BtX.Click, BtW.Click, BtV.Click, BtU.Click, Btt.Click, BtSpacebar.Click, BtS.Click, BtR.Click, BtQ.Click, BtP.Click, BtO.Click, BtN.Click, BtMinus.Click, BtM.Click, BtL.Click, BtK.Click, BtJ.Click, BtI.Click, BtH.Click, BtG.Click, BtF.Click, BtE.Click, BtD.Click, BtClear.Click, BtC.Click, BtBS.Click, BtB.Click, BtA.Click, Bt9.Click, Bt8.Click, Bt7.Click, Bt6.Click, Bt5.Click, Bt4.Click, Bt3.Click, Bt2.Click, Bt1.Click, Bt0.Click
        Dim tb As Button = CType(sender, Button)
        m_Traget.Focus()
        If tb.Text = "BACKSPACE" Then
            SendKeys.Send("{BS}")
        ElseIf tb.Text = "SPACEBAR" Then
            SendKeys.Send(" ")
        ElseIf tb.Text = "CLEAR" Then
            m_Traget.Text = ""
        Else
            SendKeys.Send(tb.Text)
        End If
    End Sub

    Private Sub tbNGQty_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbNGQty.TextChanged
        If lbInputQty.Text = "" Then
            lbInputQty.Text = "0"
        End If
        If tbNGQty.Text = "" Then
            tbNGQty.Text = "0"
        End If

        Dim sum As Integer = CInt(lbInputQty.Text)
        Dim del As Integer = CInt(tbNGQty.Text)
        If del > sum Then
            tbNGQty.Text = ""
            MsgBox("กรุณาตรวจสอบจำนวน Input Qty")
            Exit Sub
        End If
        lbGoodQty.Text = (sum - del).ToString

    End Sub
    Dim cbDefectModeINdex As String
    Dim cbRequestModeINdex As String

    Private Sub btEnd_Click(sender As System.Object, e As System.EventArgs) Handles btEnd.Click
        If cbDefectMode.Text = "" Or tbRemark.Text = "" Or tbRequest.Text = "" Then
            MsgBox("กรุณากรอกข้อมูลให้ครบ")
            Exit Sub
        End If
        GroupBoxKeyNum.Visible = False
        PanelKey.Visible = False
        PanelMaster.Visible = True
        '  BtStart.Visible = True
        btEnd.Visible = False
        PanelEnd.Enabled = False

        ' QRCode252 = "End"
        para.EndTime = Format(Now, "yyyy/MM/dd HH:mm:ss")
        para.Status = PlasmaStatus.LotEnd.ToString
        para.NGQty = tbNGQty.Text
        para.GoodQty = lbGoodQty.Text
        para.DefectMode = cbDefectModeINdex 'tbDefectMode.Text
        para.Remark = tbRemark.Text
        para.RequestMode = cbRequestModeINdex ' tbRequest.Text

        EndLot()
        SeveparaXml()
        'update
        MpPlasmaDataTableAdapter1.FillUpdate(DBxDataSet.MPPlasmaData, My.Settings.MCNo, lbLotNo.Text)
        For Each Data As DBxDataSet.MPPlasmaDataRow In DBxDataSet.MPPlasmaData
            If Data.LotStartTime = para.StartTime Then
                Data.LotEndTime = Format(Now, "yyyy/MM/dd HH:mm:ss")
                Data.GoodQty = para.GoodQty
                Data.DefectMode = para.DefectMode
                Data.NGQty = para.NGQty
                Data.Remark = para.Remark
                Data.RequestMode = para.RequestMode
            End If

        Next
        MpPlasmaDataTableAdapter1.Update(DBxDataSet.MPPlasmaData)
        ''update
        'PlasmaDataTableAdapter.FillUpdate(DBxDataSet.PlasmaData, My.Settings.MCNo, lbLotNo.Text)
        'For Each Data As DBxDataSet.PlasmaDataRow In DBxDataSet.PlasmaData
        '    If Data.LotStartTime = para.StartTime Then
        '        Data.LotEndTime = Format(Now, "yyyy/MM/dd HH:mm:ss")
        '        Data.GoodQty = para.GoodQty
        '        Data.DefectMode = para.DefectMode
        '        Data.NGQty = para.NGQty
        '        Data.Remark = para.Remark
        '        Data.MCNG = para.McNg
        '    End If

        'Next
        'PlasmaDataTableAdapter.Update(DBxDataSet.PlasmaData)

        SaveCatchLog("btEnd_Click >>" & para.Status & "End=" & para.EndTime & ">>OPNo=" & para.OPNo & ">>InputQty=" & para.InputQty & ">>NGQty=" & para.NGQty & ">>GoodQty=" & para.GoodQty & ">>DefectMode=" & para.DefectMode & ">>Remark=" & para.Remark & ">>RequestMode=" & para.RequestMode, para.QRcode252)

        Dim tmpData As String = My.Settings.MCNo & "," & para.LotNo & "," & CDate(para.EndTime) & "," & CInt(para.GoodQty) & "," & CInt(para.NGQty) & "," & para.OPNo & "," & "1"

        Dim resEnd As TdcResponse = m_TdcService.LotEnd(My.Settings.MCNo, lbLotNo.Text, CDate(para.EndTime), CInt(para.GoodQty), CInt(para.NGQty), EndModeType.Normal, para.OPNo)
        'SaveLog(Message.Cellcon, lbLotNo.Text & ":" & lbOp.Text & ">>" & resEnd.ErrorCode & ":" & resEnd.ErrorMessage)
        'EMS end My.Settings.MCNo, para.LotNo, para.StartTime, para.OPNo, RunModeType.Normal
        Try
            m_EmsClient.SetOutput(My.Settings.MCNo, CInt(para.GoodQty), CInt(para.NGQty))
            m_EmsClient.SetLotEnd(My.Settings.MCNo) 'LA-01
            m_EmsClient.SetActivity(My.Settings.MCNo, "Stop", TmeCategory.StopLoss)
        Catch ex As Exception
            '  SaveLog(Message.Cellcon, "SetActivity Stop :" & ex.ToString)
        End Try
        'SyncLock m_Locker
        '    m_LotEndQueue.Enqueue(tmpData)
        'End SyncLock


        'If bgTDC.IsBusy = False Then
        '    '  lbLotSetEnd.BackColor = Color.Lime
        '    bgTDC.RunWorkerAsync()
        'End If
    End Sub
    Private Sub EndLot()
        tbNGQty.Text = para.NGQty
        lbGoodQty.Text = para.GoodQty
        If para.DefectMode = "-" Then
            cbDefectMode.SelectedIndex = 0 ' tbDefectMode.Text = para.DefectMode
        Else
            cbDefectMode.SelectedIndex = para.DefectMode  ' tbDefectMode.Text = para.DefectMode
        End If
        If para.RequestMode = "-" Then
            tbRequest.SelectedIndex = 0 ' tbDefectMode.Text = para.DefectMode
        Else
            tbRequest.SelectedIndex = para.RequestMode
        End If

        tbRemark.Text = para.Remark

        lbEndTime.Text = para.EndTime
    End Sub

    Private Sub cbDefectMode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbDefectMode.SelectedIndexChanged, tbRequest.SelectedIndexChanged
        Dim txt As String = cbDefectMode.Text
        '    Dim asss As String = cbDefectMode.SelectedIndex
        If txt = "-" Then
            tbRemark.Text = txt
            '   tbRequest.Text = txt
        Else
            tbRemark.Text = ""
            ' tbRequest.Text = ""
        End If
        If cbDefectMode.SelectedIndex = 0 Then
            cbDefectModeINdex = "-"
        Else
            cbDefectModeINdex = cbDefectMode.SelectedIndex
        End If
        If tbRequest.SelectedIndex = 0 Then
            cbRequestModeINdex = "-"
        Else
            cbRequestModeINdex = tbRequest.SelectedIndex
        End If

        '  tbDefectMode.Text = txt
    End Sub

    Private Sub SeveparaXml()
        'Dim Address As String = My.Application.Info.DirectoryPath & "\xmlfilePara.xml"
        'Dim fs As New IO.FileStream(Address, IO.FileMode.Create)
        'Dim bs As New SoapFormatter
        'bs.Serialize(fs, para)
        'fs.Close()
        Dim xfile As String = Application.StartupPath & "\SaveParameter.xml"
        Using fs As New IO.FileStream(xfile, IO.FileMode.Create)
            Dim bs As New XmlSerializer(GetType(ClassPara))
            bs.Serialize(fs, para)
        End Using
    End Sub

    Private Sub LoadXml()
        'Dim Address As String = My.Application.Info.DirectoryPath & "\xmlfilePara.xml"
        'Dim fs As New IO.FileStream(Address, IO.FileMode.Open)
        'Dim bs As New SoapFormatter

        'para = CType(bs.Deserialize(fs), ClassPara)
        'fs.Close()
        Dim xfile As String = Application.StartupPath & "\SaveParameter.xml"
        If File.Exists(xfile) = False Then
            Exit Sub
        End If

        Using fs As New IO.FileStream(xfile, IO.FileMode.Open)
            Dim bs As New XmlSerializer(GetType(ClassPara))
            para = CType(bs.Deserialize(fs), ClassPara)
        End Using
    End Sub
    Public DIR_LOG As String = My.Application.Info.DirectoryPath & "\LOG"
    Public Sub SaveCatchLog(ByVal fnName As String, ByVal message As String)
        'If Directory.Exists(DIR_LOG & "\BackUp") = False Then
        '    Directory.CreateDirectory(DIR_LOG & "\BackUp")
        'End If
        'Dim arr As String() = Directory.GetFiles(DIR_LOG)
        'If arr.Length >= 50 Then
        '    Dim arr1 As String() = Directory.GetFiles(DIR_LOG & "\BackUp")
        '    For Each strData1 As String In arr1
        '        File.Delete(strData1)
        '    Next
        '    For Each strData As String In arr
        '        Dim pathSource As String = strData
        '        Dim pathdes As String = strData.Replace(DIR_LOG, DIR_LOG & "\BackUp")
        '        File.Move(pathSource, pathdes)
        '    Next

        '    Directory.CreateDirectory(DIR_LOG & "\BackUp")
        '    '    File.Move(arr., DIR_LOG & "\BackUp")
        '  End If
        'Using sw As StreamReader = New StreamReader(Path.Combine(DIR_LOG, "Catch_" & Now.ToString("yyyyMMdd") & ".log"), True)
        '    sw.WriteLine(Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & " " & fnName & ">" & message)
        'End Using

        Using sw As StreamWriter = New StreamWriter(Path.Combine(DIR_LOG, "Catch_" & Now.ToString("yyyyMMdd") & ".log"), True)
            sw.WriteLine(Now.ToString("yyyy/MM/dd HH:mm:ss.fff") & " " & fnName & ">" & message)
        End Using
    End Sub
    Public Sub addlogfile(ByVal m As String)
        Dim logfile As String = My.Application.Info.DirectoryPath & "\Log\Errlog.log"
        Using outfile As IO.StreamWriter = New StreamWriter(logfile, True)
            outfile.WriteLine(Format(Now, "yyyy/MM/dd HH:mm:ss") & " : " & m)
        End Using

        'Using sr As StreamReader = File.OpenText(logfile)
        '    If sr.BaseStream.Length > 900000 Then
        '        sr.Close()
        '        File.Delete(logfile)
        '    End If
        'End Using
    End Sub
    Private Sub bgTDC_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgTDC.DoWork
        Dim TmpData As String = ""
        Dim ArrayData As String()
        'LotSet
        Dim MCNo As String
        Dim LotNo As String
        Dim StartTime As String
        Dim OPNo As String
        Dim LotSetMode As String = ""

        'LotEnd
        Dim EndTime As String
        Dim GoodQty As Integer
        Dim NGQTy As Integer
        Dim CountErr03 As Integer = 0
        Dim LotEndMode As String = ""

LBL_QUEUE_LOTSET_CHECK:
        Dim RepeatCountLotSet As Integer = 0
        SyncLock m_Locker
            If m_LotSetQueue.Count > 0 Then 'ทำ LotSet จนกว่าจะหมด Qeue
                TmpData = m_LotSetQueue.Dequeue
                ArrayData = TmpData.Split(CChar(","))
                MCNo = ArrayData(0)
                LotNo = ArrayData(1)
                StartTime = ArrayData(2)
                OPNo = ArrayData(3)
                LotSetMode = ArrayData(4)
                RepeatCountLotSet = 0
            Else 'ทำ LotEnd จนกว่าจะหมด Qeue
                GoTo LBL_QUEUE_LOTEND_CHECK
            End If
        End SyncLock

LBL_QUEUE_LotSet_Err:

        Dim resSet As TdcResponse = m_TdcService.LotSet(MCNo, LotNo, CDate(StartTime), OPNo, RunModeType.Normal)
        If resSet.HasError Then
            If RepeatCountLotSet > 5 Then 'กรณีที่ Err 70,71,72 วนรันซ้ำมากกว่า 5 ครั้ง เป็น Err Log แล้วรัน Lot ต่อไป
                addlogfile("LotSet : " & TmpData)
                GoTo LBL_QUEUE_LOTSET_CHECK
            End If

            Select Case resSet.ErrorCode
                Case "04"
                    RepeatCountLotSet += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotSet_Err
                Case "70"
                    RepeatCountLotSet += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotSet_Err
                Case "71"
                    RepeatCountLotSet += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotSet_Err
                Case "72"
                    RepeatCountLotSet += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotSet_Err
                Case "99"
                    RepeatCountLotSet += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotSet_Err
            End Select
        End If
        GoTo LBL_QUEUE_LOTSET_CHECK


LBL_QUEUE_LOTEND_CHECK:
        Dim RepeatCountLotEnd As Integer = 0
        SyncLock m_Locker

            If m_LotEndQueue.Count > 0 Then 'ทำ LotEnd จนกว่าจะหมด Qeue
                TmpData = m_LotEndQueue.Dequeue
                ArrayData = TmpData.Split(CChar(","))
                MCNo = ArrayData(0)
                LotNo = ArrayData(1)
                EndTime = ArrayData(2)
                GoodQty = CInt(ArrayData(3))

                If ArrayData(4) = "" Then
                    NGQTy = 0
                Else
                    NGQTy = CInt(ArrayData(4))
                End If
                OPNo = ArrayData(5)
                LotEndMode = ArrayData(6)
                RepeatCountLotEnd = 0
            Else 'ทำ LotEnd จนกว่าจะหมด Qeue
                Exit Sub
            End If
        End SyncLock

LBL_QUEUE_LotEnd_Err:

        Dim resEnd As TdcResponse = m_TdcService.LotEnd(MCNo, LotNo, CDate(EndTime), CInt(GoodQty), CInt(NGQTy), CType(LotEndMode, EndModeType), OPNo)
        If resEnd.HasError Then
            If RepeatCountLotEnd > 5 Then 'กรณีที่ Err 70,71,72 วนรันซ้ำมากกว่า 5 ครั้ง เป็น Err Log แล้วรัน Lot ต่อไป
                addlogfile("LotEnd : " & TmpData)
                GoTo LBL_QUEUE_LOTEND_CHECK
            End If
            Select Case resEnd.ErrorCode
                Case "03" 'Lot was not started or ended 150921
                    CountErr03 += 1 '                                  150923
                    If CountErr03 > 10 Then '                          150923
                        addlogfile("LotErr03 : " & TmpData) '          150923
                        CountErr03 = 0 '                               150923
                        GoTo LBL_QUEUE_LOTEND_CHECK '                  150923
                    End If '150923
                    m_LotEndQueue.Enqueue(TmpData)
                    GoTo LBL_QUEUE_LOTSET_CHECK
                Case "04" 'MC not found
                    RepeatCountLotEnd += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotEnd_Err
                Case "70"
                    RepeatCountLotEnd += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotEnd_Err
                Case "71"
                    RepeatCountLotEnd += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotEnd_Err
                Case "72"
                    RepeatCountLotEnd += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotEnd_Err
                Case "99"
                    RepeatCountLotEnd += 1
                    Thread.Sleep(3000)
                    GoTo LBL_QUEUE_LotEnd_Err
            End Select
        End If
        CountErr03 = 0
        GoTo LBL_QUEUE_LOTEND_CHECK
    End Sub

    Private Sub bgTDCLotReq_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgTDCLotReq.DoWork
        Dim TmpData As String
        Dim ArrayData() As String
        Dim MCNo As String
        Dim LotNo As String
        Dim LotSetMode As String


        'McNo,LotNo,LotStartMode
        TmpData = m_LotReqQueue
        ArrayData = TmpData.Split(CChar(","))
        MCNo = ArrayData(0)
        LotNo = ArrayData(1)
        LotSetMode = ArrayData(2)


        li = Nothing

        Dim strMess As String = ""
        Dim res As TdcResponse = m_TdcService.LotRequest(MCNo, LotNo, CType(CInt(LotSetMode), RunModeType))
        If res.HasError Then
            'My.Settings.NotFound = cb01.SelectedIndex
            'My.Settings.Running = cb02.SelectedIndex
            'My.Settings.NotRun = cb03.SelectedIndex
            'My.Settings.MachineNotFound = cb04.SelectedIndex
            'My.Settings.ErrorLotStatus = cb05.SelectedIndex
            'My.Settings.ErrorFlow = cb06.SelectedIndex
            'My.Settings.ErrorConnectDatabase = cb70.SelectedIndex
            'My.Settings.ErrorReadDatabase = cb71.SelectedIndex
            'My.Settings.ErrorWriteDatabase = cb72.SelectedIndex
            'My.Settings.ErrorOther = cb99.SelectedIndex
            'My.Settings.Save()
            If res.ErrorCode = "01" Then
                strMess = "01 : Not found"
                If My.Settings.NotFound = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "02" Then
                strMess = "02 : Running" 'Lotset ซ้ำ
                If My.Settings.Running = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "03" Then
                strMess = "03 : Not run" 'End ที่ยังไม่ lotset
                If My.Settings.NotRun = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "04" Then
                strMess = "04 : Machine not found" 'Machine ยังไม่ลงทะเบียน
                If My.Settings.MachineNotFound = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "05" Then
                strMess = "05 : Error lot status"
                If My.Settings.ErrorLotStatus = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "06" Then
                strMess = "06 : " & res.ErrorMessage 'run ผิด flow
                If My.Settings.ErrorFlow = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "70" Then
                strMess = "70 : Error connect database"
                If My.Settings.ErrorConnectDatabase = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "71" Then
                strMess = "71 : Error read database"
                If My.Settings.ErrorReadDatabase = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "72" Then
                strMess = "72 : Error write database"
                If My.Settings.ErrorWriteDatabase = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            ElseIf res.ErrorCode = "99" Then
                strMess = "99 : " & res.ErrorMessage 'Other
                If My.Settings.ErrorOther = MCLock.Unlock Then
                    para.MCLock = MCLock.Unlock
                Else
                    para.MCLock = MCLock.Lock
                End If
            End If
        Else
            strMess = "00 : Run Normal"
            para.MCLock = MCLock.Unlock
        End If
        'If res.HasError Then
        '    If res.ErrorCode = "01" Then
        '        strMess = "01 : Not found"
        '        If _01 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "02" Then
        '        strMess = "02 : Running" 'Lotset ซ้ำ
        '        If _02 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "03" Then
        '        strMess = "03 : Not run" 'End ที่ยังไม่ lotset
        '        If _03 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "04" Then
        '        strMess = "04 : Machine not found" 'Machine ยังไม่ลงทะเบียน
        '        If _04 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "05" Then
        '        strMess = "05 : Error lot status"
        '        If _05 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "06" Then
        '        strMess = res.ErrorMessage 'run ผิด flow
        '        If _06 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "70" Then
        '        strMess = "70 : Error connect database"
        '        If _70 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "71" Then
        '        strMess = "71 : Error read database"
        '        If _71 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "72" Then
        '        strMess = "72 : Error write database"
        '        If _72 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    ElseIf res.ErrorCode = "99" Then
        '        strMess = "99 : " & res.ErrorMessage 'Other
        '        If _99 = True Then
        '            para.MCLock = False
        '        Else
        '            para.MCLock = True
        '        End If
        '    End If
        'Else
        '    strMess = "00 : Run Normal"
        '    para.MCLock = False
        'End If




        If para.MCLock = MCLock.Lock Then
            li = m_TdcService.GetLotInfo(para.LotNo, My.Settings.MCNo)
            m_dlg = New TdcAlarmMessageForm(res.ErrorCode, res.ErrorMessage, para.LotNo, li)
        End If

        m_LotReqMes = strMess

    End Sub

    Private Sub bgTDCLotReq_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgTDCLotReq.RunWorkerCompleted

        para.LotInfor = m_LotReqMes
        lbLotInfo.Text = para.LotInfor

        SeveparaXml()

        If para.MCLock = MCLock.Lock Then
            m_dlg.ShowDialog()
        Else
            BtStart.Enabled = True
        End If


    End Sub
    Enum MCLock
        Unlock
        Lock

    End Enum
    Private Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        My.Settings.NotFound = cb01.SelectedIndex
        My.Settings.Running = cb02.SelectedIndex
        My.Settings.NotRun = cb03.SelectedIndex
        My.Settings.MachineNotFound = cb04.SelectedIndex
        My.Settings.ErrorLotStatus = cb05.SelectedIndex
        My.Settings.ErrorFlow = cb06.SelectedIndex
        My.Settings.ErrorConnectDatabase = cb70.SelectedIndex
        My.Settings.ErrorReadDatabase = cb71.SelectedIndex
        My.Settings.ErrorWriteDatabase = cb72.SelectedIndex
        My.Settings.ErrorOther = cb99.SelectedIndex
        My.Settings.RunOffline = cbRunOffline.SelectedIndex
        My.Settings.Save()
        If My.Settings.RunOffline = MCMode.Online Then
            lbStateMC.Text = MCMode.Online.ToString
        Else
            lbStateMC.Text = MCMode.Offline.ToString
        End If
        MsgBox("บันทึกเรียบร้อย")
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        cb01.SelectedIndex = My.Settings.NotFound
        cb02.SelectedIndex = My.Settings.Running
        cb03.SelectedIndex = My.Settings.NotRun
        cb04.SelectedIndex = My.Settings.MachineNotFound
        cb05.SelectedIndex = My.Settings.ErrorLotStatus
        cb06.SelectedIndex = My.Settings.ErrorFlow
        cb70.SelectedIndex = My.Settings.ErrorConnectDatabase
        cb71.SelectedIndex = My.Settings.ErrorReadDatabase
        cb72.SelectedIndex = My.Settings.ErrorWriteDatabase
        cb99.SelectedIndex = My.Settings.ErrorOther
        cbRunOffline.SelectedIndex = My.Settings.RunOffline
    End Sub


End Class
