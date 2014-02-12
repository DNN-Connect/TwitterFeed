Public Class hashtag

    Private m_text As String
    Private m_indices As String()

    Public Property text() As String
        Get
            Return m_text
        End Get
        Set(value As String)
            m_text = value
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
