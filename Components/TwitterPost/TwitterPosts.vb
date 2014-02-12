Public Class TwitterPosts

    Private m_posts As List(Of TwitterPost)

    Public Property statuses() As List(Of TwitterPost)
        Get
            Return m_posts
        End Get
        Set(value As List(Of TwitterPost))
            m_posts = value
        End Set
    End Property

End Class
