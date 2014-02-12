Public Class PostEntities

    Private m_urls As List(Of Url)
    Private m_media As List(Of media)
    Private m_user_mentions As List(Of user_mention)
    Private m_hashtags As List(Of hashtag)

    Public Property urls() As List(Of Url)
        Get
            Return m_urls
        End Get
        Set(value As List(Of Url))
            m_urls = value
        End Set
    End Property

    Public Property media() As List(Of Media)
        Get
            Return m_media
        End Get
        Set(value As List(Of Media))
            m_media = value
        End Set
    End Property

    Public Property hashtags() As List(Of hashtag)
        Get
            Return m_hashtags
        End Get
        Set(value As List(Of hashtag))
            m_hashtags = value
        End Set
    End Property

    Public Property user_mentions() As List(Of user_mention)
        Get
            Return m_user_mentions
        End Get
        Set(value As List(Of user_mention))
            m_user_mentions = value
        End Set
    End Property
End Class
