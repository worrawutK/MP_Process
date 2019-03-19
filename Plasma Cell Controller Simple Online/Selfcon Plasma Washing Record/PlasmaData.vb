<Serializable()> _
Public Class PlasmaData
  


    Private _QR As String
    Public Property QR() As String
        Get
            Return _QR
        End Get
        Set(ByVal value As String)
            _QR = value
        End Set
    End Property

    Private _mcNum As String
    Public Property McNo() As String
        Get
            Return _mcNum
        End Get
        Set(ByVal value As String)
            _mcNum = value
        End Set
    End Property

    Private _MCtype As String
    Public Property MCtype() As String
        Get
            Return _MCtype
        End Get
        Set(ByVal value As String)
            _MCtype = value
        End Set
    End Property

    Private _OpNum As String
    Public Property OpNo() As String
        Get
            Return _OpNum
        End Get
        Set(ByVal value As String)
            _OpNum = value
        End Set
    End Property

    Private _lotNum As String
    Public Property LotNo() As String
        Get
            Return _lotNum
        End Get
        Set(ByVal value As String)
            _lotNum = value
        End Set
    End Property

    Private _Package As String
    Public Property Package() As String
        Get
            Return _Package
        End Get
        Set(ByVal value As String)
            _Package = value
        End Set
    End Property
    Private _Device As String
    Public Property Device() As String
        Get
            Return _Device
        End Get
        Set(ByVal value As String)
            _Device = value
        End Set
    End Property
    Private _InputQty As String
    Public Property InputQty() As String
        Get
            Return _InputQty
        End Get
        Set(ByVal value As String)
            _InputQty = value
        End Set
    End Property


    Private _InlotTime As String
    Public Property LotStartTime() As String
        Get
            Return _InlotTime
        End Get
        Set(ByVal value As String)
            _InlotTime = value
        End Set
    End Property


    Private _NGQty As String
    Public Property NGQty() As String
        Get
            Return _NGQty
        End Get
        Set(ByVal value As String)
            _NGQty = value
        End Set
    End Property


    Private _GoodQty As String
    Public Property GoodQty() As String
        Get
            Return _GoodQty
        End Get
        Set(ByVal value As String)
            _GoodQty = value
        End Set
    End Property

    Private _DefectMode As String
    Public Property DefectMode() As String
        Get
            Return _DefectMode
        End Get
        Set(ByVal value As String)
            _DefectMode = value
        End Set
    End Property


    Private _Remark As String
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property


    Private _McNg As String
    Public Property McNg() As String
        Get
            Return _McNg
        End Get
        Set(ByVal value As String)
            _McNg = value
        End Set
    End Property


    Private _FinishTime As String
    Public Property LotEndTime() As String
        Get
            Return _FinishTime
        End Get
        Set(ByVal value As String)
            _FinishTime = value
        End Set
    End Property

    Private _CountMC As Integer
    Public Property CountMC() As Integer
        Get
            Return _CountMC
        End Get
        Set(ByVal value As Integer)
            _CountMC = value
        End Set
    End Property

    Private _StatusLot As String
    Public Property StatusLot() As String
        Get
            Return _StatusLot
        End Get
        Set(ByVal value As String)
            _StatusLot = value
        End Set
    End Property


    Private _RecipeName As String
    Public Property RecipeName() As String
        Get
            Return _RecipeName
        End Get
        Set(ByVal value As String)
            _RecipeName = value
        End Set
    End Property

    Private _CanRun As String
    Public Property CanRun() As String
        Get
            Return _CanRun
        End Get
        Set(ByVal value As String)
            _CanRun = value
        End Set
    End Property

    Private _PermitUser As Boolean
    Public Property PermitUser() As Boolean
        Get
            Return _PermitUser
        End Get
        Set(ByVal value As Boolean)
            _PermitUser = value
        End Set
    End Property

    Private _ModeOnline As Boolean
    Public Property ModeOnline() As Boolean
        Get
            Return _ModeOnline
        End Get
        Set(ByVal value As Boolean)
            _ModeOnline = value
        End Set
    End Property

    Private _FrameMap As List(Of String)
    Public Property FrameMap() As List(Of String)
        Get
            Return _FrameMap
        End Get
        Set(ByVal value As List(Of String))
            _FrameMap = value
        End Set
    End Property

    'Sub clear()
    '    _QR = ""
    '    _OpNum = ""
    '    _lotNum = ""
    '    _Package = ""
    '    _Device = ""
    '    _LotQty = ""
    '    _InlotTime = ""

    '    _NGQty = ""
    '    _GoodQty = ""
    '    _DefectMode = ""
    '    _Remark = ""
    '    _DefectMode = ""
    '    _Remark = ""
    '    _McNg = ""
    '    _FinishTime = ""

    '    _CanRun = False
    'End Sub
End Class
