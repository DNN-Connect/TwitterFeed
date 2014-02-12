Public Class media

    Private m_media_url As String
    Private m_indices As String()

    Public Property media_url() As String
        Get
            Return m_media_url
        End Get
        Set(value As String)
            m_media_url = value
        End Set
    End Property

    Public Property indices() As String()
        Get
            Return m_indices
        End Get
        Set(value As String())
            m_indices = value
        End Set
    End Property

End Class
