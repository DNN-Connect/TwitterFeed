Imports System
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports DotNetNuke.Web.Api
Imports DotNetNuke.Services.Localization
Imports System.IO

Namespace Services

    Public Class ClientFeedController
        Inherits DnnApiController

        Private _TokenKey As String = ""
        Private _TokenSecret As String = ""
        Private _ConsumerKey As String = ""
        Private _ConsumerSecret As String = ""
        Private _Displaymode As String = ""
        Private _Template As String = ""
        Private _SearchFor As String = ""
        Private _PostCount As String = "10"
        Private _RefreshInterval As String = "30"
        Private _settings As Hashtable

        <AllowAnonymous> <HttpGet> _
        Public Function TwitterSearchResult() As HttpResponseMessage

            _settings = ActiveModule.ModuleSettings()
            LoadSettings()

            Dim feed As New List(Of TwitterPost)
            Dim ctrl As New FeedController(_TokenKey, _TokenSecret, _ConsumerKey, _ConsumerSecret)

            Select Case _Displaymode.ToLower
                Case "u"
                    feed = ctrl.GetUserTimeLine(_SearchFor, _PostCount)
                Case "s"
                    feed = ctrl.SearchTweets(_SearchFor, _PostCount)
            End Select

            Dim strHtml As String = ""
            Dim strTemplatePath As String = _Template.Replace("/", "\")
            Dim strPath As String = HttpRuntime.AppDomainAppPath & strTemplatePath
            Dim strTemplate As String = ""

            strTemplate = strPath & "\Header.htm"
            Dim helper As New TemplateProcessor(_settings)
            helper.ProcessSurroundingTemplate(strHtml, Helpers.ReadTemplate(strTemplate))

            For i As Integer = 0 To feed.Count - 1

                If (i And 1) = 0 Then
                    strTemplate = strPath & "\Item.htm"
                Else
                    strTemplate = strPath & "\ItemAlternate.htm"
                End If
                helper.ProcessItemTemplate(strHtml, Helpers.ReadTemplate(strTemplate), feed(i))

            Next

            strTemplate = strPath & "\Footer.htm"
            helper.ProcessSurroundingTemplate(strHtml, Helpers.ReadTemplate(strTemplate))

            Return Request.CreateResponse(HttpStatusCode.OK, strHtml)


        End Function

        Private Sub LoadSettings()
            If (_settings.Contains("Twitter_SelectedTemplate")) Then _Template = _settings("Twitter_SelectedTemplate").ToString()
            If (_settings.Contains("Twitter_DisplayMode")) Then _Displaymode = _settings("Twitter_DisplayMode").ToString()
            If (_settings.Contains("Twitter_ConsumerKey")) Then _ConsumerKey = _settings("Twitter_ConsumerKey").ToString()
            If (_settings.Contains("Twitter_ConsumerSecret")) Then _ConsumerSecret = _settings("Twitter_ConsumerSecret").ToString()
            If (_settings.Contains("Twitter_TokenKey")) Then _TokenKey = _settings("Twitter_TokenKey").ToString()
            If (_settings.Contains("Twitter_TokenSecret")) Then _TokenSecret = _settings("Twitter_TokenSecret").ToString()
            If (_settings.Contains("Twitter_SearchFor")) Then _SearchFor = _settings("Twitter_SearchFor").ToString()
            If (_settings.Contains("Twitter_PostCount")) Then _PostCount = _settings("Twitter_PostCount").ToString()
            If (_settings.Contains("Twitter_RefreshInterval")) Then _RefreshInterval = _settings("Twitter_RefreshInterval").ToString()
        End Sub

    End Class

End Namespace

