#Region "copyright"


#End Region


Imports System.Data
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Tokens


<Serializable> _
Public Class RenderedPost

    Private m_AuthorScreenName As String
    Private m_AuthorFullname As String
    Private m_AuthorUrl As String
    Private m_AuthorProfilePic As String
    Private m_GUID As String
    Private m_Image As String
    Private m_Link As String
    Private m_Pubdate As DateTime
    Private m_Content As String
    Private m_Title As String

    Public Property AuthorScreenName() As String
        Get
            Return m_AuthorScreenName
        End Get
        Set(value As String)
            m_AuthorScreenName = value
        End Set
    End Property

    Public Property AuthorFullName() As String
        Get
            Return m_AuthorFullname
        End Get
        Set(value As String)
            m_AuthorFullname = value
        End Set
    End Property

    Public Property AuthorUrl() As String
        Get
            Return m_AuthorUrl
        End Get
        Set(value As String)
            m_AuthorUrl = value
        End Set
    End Property

    Public Property AuthorProfilePic() As String
        Get
            Return m_AuthorProfilePic
        End Get
        Set(value As String)
            m_AuthorProfilePic = value
        End Set
    End Property

    Public Property GUID() As String
        Get
            Return m_GUID
        End Get
        Set(value As String)
            m_GUID = value
        End Set
    End Property

    Public Property Image() As String
        Get
            Return m_Image
        End Get
        Set(value As String)
            m_Image = value
        End Set
    End Property

    Public Property Link() As String
        Get
            Return m_Link
        End Get
        Set(value As String)
            m_Link = value
        End Set
    End Property

    Public Property Pubdate() As DateTime
        Get
            Return m_Pubdate
        End Get
        Set(value As DateTime)
            m_Pubdate = value
        End Set
    End Property

    Public Property Content() As String
        Get
            Return m_Content
        End Get
        Set(value As String)
            m_Content = value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return m_Title
        End Get
        Set(value As String)
            m_Title = value
        End Set
    End Property

    Public Sub New()
    End Sub

End Class



