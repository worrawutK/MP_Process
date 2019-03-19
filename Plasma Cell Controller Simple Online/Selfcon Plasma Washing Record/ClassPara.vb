<Serializable()> _
Public Class ClassPara
    Public Status As String
    Public QRcode252 As String
    Public Package As String
    Public Device As String
    Public LotNo As String
    Public InputQty As String
    Public OPNo As String
    Public EndTime As String
    Public StartTime As String

    Public NGQty As String
    Public GoodQty As String
    Public DefectMode As String
    Public Remark As String
    Public RequestMode As String

    Private _MCLock As Integer
    Public Property MCLock() As Integer
        Get
            Return _MCLock
        End Get
        Set(ByVal value As Integer)
            _MCLock = value
        End Set
    End Property
    Private LotInforData As String
    Public Property LotInfor() As String
        Get
            Return LotInforData
        End Get
        Set(ByVal value As String)
            LotInforData = value
        End Set
    End Property
End Class
