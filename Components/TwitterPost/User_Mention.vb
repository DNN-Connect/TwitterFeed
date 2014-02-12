Public Class user_mention

    Private m_name As String
    Private m_screen_name As String
    Private m_indices As String()

    Public Property name() As String
        Get
            Return m_name
        End Get
        Set(value As String)
            m_name = value
        End Set
    End Property

    Public Property screen_name() As String
        Get
            Return m_screen_name
        End Get
        Set(value As String)
            m_screen_name = value
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
