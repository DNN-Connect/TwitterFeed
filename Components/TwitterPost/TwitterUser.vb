Public Class TwitterUser

    Private m_screen_name As String
    Private m_name As String
    Private m_url As String
    Private m_profile_image_url As String

    Public Sub New()
    End Sub

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

    Public Property profile_image_url() As String
        Get
            Return m_profile_image_url
        End Get
        Set(value As String)
            m_profile_image_url = value
        End Set
    End Property

    Public Property url() As String
        Get
            Return m_url
        End Get
        Set(value As String)
            m_url = value
        End Set
    End Property

End Class
