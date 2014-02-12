Public Class url

    Private m_url As String
    Private m_indices As String()

    Public Property url() As String
        Get
            Return m_url
        End Get
        Set(value As String)
            m_url = value
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
