Public Class TwitterPost

    Private m_created_at As String
    Private m_id_str As String
    Private m_text As String
    Private m_source As String
    Private m_user As TwitterUser
    Private m_entities As PostEntities

    Public Sub New()
    End Sub

    Public Property created_at() As String
        Get
            Return m_created_at
        End Get
        Set(value As String)
            m_created_at = value
        End Set
    End Property

    Public Property id_str() As String
        Get
            Return m_id_str
        End Get
        Set(value As String)
            m_id_str = value
        End Set
    End Property

    Public Property source() As String
        Get
            Return m_source
        End Get
        Set(value As String)
            m_source = value
        End Set
    End Property

    Public Property text() As String
        Get
            Return m_text
        End Get
        Set(value As String)
            m_text = value
        End Set
    End Property

    Public Property user() As TwitterUser
        Get
            Return m_user
        End Get
        Set(value As TwitterUser)
            m_user = value
        End Set
    End Property

    Public Property entities() As PostEntities
        Get
            Return m_entities
        End Get
        Set(value As PostEntities)
            m_entities = value
        End Set
    End Property




End Class
