Imports System.Runtime.Serialization.Formatters.Soap                      'XML Format
Imports System.IO

Module Module1
    Public para As New ClassPara

    Public _00 As Boolean
    Public _01 As Boolean
    Public _02 As Boolean
    Public _03 As Boolean
    Public _04 As Boolean
    Public _05 As Boolean
    Public _06 As Boolean
    Public _70 As Boolean
    Public _71 As Boolean
    Public _72 As Boolean
    Public _73 As Boolean
    Public _99 As Boolean

    Public _RunMode As Integer

    Enum MCMode
        Online = 0
        Offline = 1
    End Enum
    'Public SelfConData As PlasmaData = New PlasmaData

    Friend ReadOnly _ipServer As String = "172.16.0.100"                      'ZION Server
    Friend ReadOnly _ipDbxUser As String = "172.16.0.102"                     'DBX,APCS  Server
    Public strFileCommLog As String = "Comm.log"
    Public ConSocket As String
    Public ConSysConfig As String
    Public ConModMaster As String
    Public ConAlarmMaster As String
    Public ConSetModule As String
    Public ConSetTool As String
    Public ConDataItem As String
    Public ConMainInfo As String
    Public Const MAIN_DIR_IPM_LOG = "C:\RohmSystem\rCellcon\LOG\"
    Public _ConnectMCOnline As Boolean = False            ' 
    Public tmpPlasmaData As New PlasmaDataList


    Friend Sub WrXml(ByVal pathfile As String, ByVal TarObj As Object)
        Dim fs As New IO.FileStream(pathfile, IO.FileMode.Create)
        Dim bs As New SoapFormatter
        bs.Serialize(fs, TarObj)
        fs.Close()
    End Sub
    Friend Function RdXml(ByVal pathfile As String) As Object
        Dim TarObj As New Object
        Dim fs As New IO.FileStream(pathfile, IO.FileMode.Open)
        Dim bs As New SoapFormatter
        TarObj = bs.Deserialize(fs)
        fs.Close()
        Return TarObj
    End Function


    Public Sub SaveData(ByVal strFileName As String, ByVal strSaveData As String, Optional ByVal bAppend As Boolean = True)

        If Frmmain.lbStateMC.BackColor <> Color.Lime Then
            Exit Sub
        End If
        Dim strDate As String = Format(Now, "yyyyMMdd")
        Dim FolderLog As String = "C:\LogPlasma\"

        If Not Directory.Exists(FolderLog) Then
            Directory.CreateDirectory(FolderLog)
        End If

        Dim LogFileCount() As String = Directory.GetFiles(FolderLog)
        If LogFileCount.Length > 50 Then
            For Each strData As String In LogFileCount
                File.Delete(strData)
            Next
        End If


        My.Computer.FileSystem.WriteAllText(FolderLog & strDate & strFileName, strSaveData, bAppend)


    End Sub

    Public Sub SaveErrorDataLog(ByVal strFileName As String, Optional ByVal bAppend As Boolean = True)
        Dim strDate As String = Format(Now, "yyyy/MM/dd HH:mm:ss") & " : "
        Dim FolderLog As String = "C:\LogPlasma\Error\"
        If Not Directory.Exists(FolderLog) Then
            Directory.CreateDirectory(FolderLog)
        End If
        My.Computer.FileSystem.WriteAllText(FolderLog & "Error.log", strDate & strFileName, bAppend)
    End Sub


#Region "===AuthenticationUser"

    'Dim ETC2 As String                          'From QR Code ,Check ETC2 = BDXX-M/BJ/C is auto motive
    'Dim strNextOperatorNo As String              'OP No.
    'Dim GetUserAuthenGroupByMCType As String       'M/C Type ( Refer with DBx.Group)
    'Dim GL_Group As String                         'GL Gruop ( Refer with DBx.Group)
    'Dim Process As String                        'Process Ex. "FL"
    'Dim MCNo As String                           'MC No Ex "FL-V-01"
    Public ErrMesETG As String
    Public _OperatorAlarm As String
    Public Function PermiisionCheck(ByVal ETC2 As String, ByVal strNextOperatorNo As String, ByVal GetUserAuthenGroupByMCType As String, ByVal GL_Group As String, ByVal Procees As String, ByVal MCNo As String) As Boolean
        Dim permission As New AuthenticationUser.AuthenUser
        Dim AuthenPass As Boolean

        If permission.CheckAutomotiveLot(ETC2) Then
            'This lot is Automotive
            If Not permission.CheckMachineAutomotive(Procees, MCNo) Then
                ErrMesETG = "MC No.นี้ไม่สามารถรัน Lot Automotive ได้ "
                _OperatorAlarm = "Machine cannot run the automotive lot,Please contact ETG"
                'MsgBox("MC No.นี้ไม่สามารถรัน Lot Automotive ได้  กรุณาติดต่อ ETG/SYSTEM")
                Exit Function
            End If

            AuthenPass = permission.AuthenUser(strNextOperatorNo, GetUserAuthenGroupByMCType) And permission.AuthenUser(strNextOperatorNo, "AUTOMOTIVE")
            If AuthenPass = False Then AuthenPass = permission.AuthenUser(strNextOperatorNo, GL_Group) 'GL Can run every condition
            If AuthenPass = False Then
                ErrMesETG = "OP No.นี้ไม่สามารถรัน Lot Automotive ได้" 'MsgBox("OP No.นี้ไม่สามารถรัน Lot Automotive ได้  กรุณาติดต่อ ETG/SYSTEM")
                _OperatorAlarm = "OP No cannot run the automotive lot ,Please contact ETG"
            End If

        Else
            'This lot isn't Automotive
            AuthenPass = permission.AuthenUser(strNextOperatorNo, GetUserAuthenGroupByMCType)
            If AuthenPass = False Then AuthenPass = permission.AuthenUser(strNextOperatorNo, GL_Group)
            If AuthenPass = False Then
                ErrMesETG = "OP No.นี้ไม่สามารถรันได้  " 'MsgBox("OP No.นี้ไม่สามารถรันได้  กรุณาติดต่อ ETG/SYSTEM")
                _OperatorAlarm = "OP No cannot run ,Please contact ETG"
            End If

        End If

        Return AuthenPass

    End Function

#End Region


End Module
