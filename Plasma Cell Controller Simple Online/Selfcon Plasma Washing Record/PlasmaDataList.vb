<Serializable()> _
Public Class PlasmaDataList
    Private _ListPlasma As List(Of PlasmaData)
    Public Property ListPlasma() As List(Of PlasmaData)
        Get
            Return _ListPlasma
        End Get
        Set(ByVal value As List(Of PlasmaData))
            _ListPlasma = value
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


    Private _StatusLot As String
    Public Property StatusLot() As String
        Get
            Return _StatusLot
        End Get
        Set(ByVal value As String)
            _StatusLot = value
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

    Private _RecipeName As String
    Public Property RecipeName() As String
        Get
            Return _RecipeName
        End Get
        Set(ByVal value As String)
            _RecipeName = value
        End Set
    End Property
End Class
