
Imports DotNetNuke
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization
Imports System.IO
Imports DotNetNuke.Framework

Public Class View
    Inherits PortalModuleBase

#Region "Private Members"

    Private _TokenKey As String = ""
    Private _TokenSecret As String = ""
    Private _ConsumerKey As String = ""
    Private _ConsumerSecret As String = ""
    Private _Displaymode As String = ""
    Private _Template As String = Me.TemplateSourceDirectory & "\templates\_default.html"
    Private _SearchFor As String = ""
    Private _PostCount As String = "10"
    Private _RefreshInterval As String = "30"
    Private _RenderingMode As String = "S"
    Private _processor As TemplateProcessor = Nothing

#End Region

#Region "Event Handlers"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        Try
            ServicesFramework.Instance.RequestAjaxAntiForgerySupport()

            LoadSettings()

            If ValidateSettings() Then

                pnlSettingsIncomplete.Visible = False
                RegisterCss()

                If _RenderingMode = "C" Then

                    RegisterScripts()
                    rptFeed.Visible = False
                    pnlClientSideTweets.Visible = True

                Else

                    _processor = New TemplateProcessor(Settings)
                    rptFeed.Visible = True                    
                    BindData()
                    pnlClientSideTweets.Visible = False

                End If

            Else

                rptFeed.Visible = False
                pnlSettingsIncomplete.Visible = True
                lblSettingsIncomplete.Text = Localization.GetString("lblSettingsIncomplete", LocalResourceFile)

            End If

        Catch exc As Exception
            Exceptions.ProcessModuleLoadException(Me, exc)
        End Try

    End Sub

    Private Sub rptFeed_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptFeed.ItemDataBound

        Dim strTemplate As String = Server.MapPath(_Template)

        If (e.Item.ItemType = ListItemType.Header) Then

            strTemplate += "\Header.htm"
            strTemplate = strTemplate.Replace("\\", "\")

            _processor.ProcessSurroundingTemplate(e.Item.Controls, Helpers.ReadTemplate(strTemplate))

        End If

        If e.Item.ItemType = ListItemType.Item Then

            Dim objPost As TwitterPost = CType(e.Item.DataItem, TwitterPost)
            strTemplate += "\Item.htm"
            strTemplate = strTemplate.Replace("\\", "\")

            _processor.ProcessItemTemplate(e.Item.Controls, Helpers.ReadTemplate(strTemplate), objPost)

        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then

            Dim objPost As TwitterPost = CType(e.Item.DataItem, TwitterPost)
            strTemplate += "\ItemAlternate.htm"
            strTemplate = strTemplate.Replace("\\", "\")

            _processor.ProcessItemTemplate(e.Item.Controls, Helpers.ReadTemplate(strTemplate), objPost)

        End If

        If (e.Item.ItemType = ListItemType.Footer) Then

            strTemplate += "\Footer.htm"
            strTemplate = strTemplate.Replace("\\", "\")

            _processor.ProcessSurroundingTemplate(e.Item.Controls, Helpers.ReadTemplate(strTemplate))

        End If

    End Sub

#End Region

#Region "Private Methods"

    Private Sub RegisterCss()

        Dim strCssUrl As String = _Template & "/template.css"

        Dim blnAlreadyRegistered As Boolean = False
        For Each ctrl As Control In Me.Page.Header.Controls

            If TypeOf (ctrl) Is HtmlLink Then
                Dim ctrlCss As HtmlLink = CType(ctrl, HtmlLink)
                If ctrlCss.Href.ToLower = strCssUrl.ToLower Then
                    blnAlreadyRegistered = True
                    Exit For
                End If
            End If

        Next

        If Not blnAlreadyRegistered Then

            Dim ctrlLink As New HtmlLink
            ctrlLink.Href = strCssUrl
            ctrlLink.Attributes.Add("rel", "stylesheet")
            ctrlLink.Attributes.Add("type", "text/css")
            ctrlLink.Attributes.Add("media", "screen")

            Me.Page.Header.Controls.Add(ctrlLink)

        End If

    End Sub

    Private Sub RegisterScripts()

        Dim strScript As String = vbCrLf & "<script type=""text/javascript"">" & vbCrLf & vbCrLf

        strScript += "  (function ($, Sys) {" & vbCrLf
        strScript += "      $(document).ready(function () {" & vbCrLf
        strScript += "          setupTweets(" & ModuleId.ToString & ");" & vbCrLf
        strScript += "          setInterval(function() { setupTweets(" & ModuleId.ToString & ") }, " & (_RefreshInterval * 1000) & ");" & vbCrLf
        strScript += "          Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {" & vbCrLf
        strScript += "              setupTweets(" & ModuleId.ToString & ");" & vbCrLf
        strScript += "              setInterval(function() { setupTweets(" & ModuleId.ToString & ") }, " & (_RefreshInterval * 1000) & ");" & vbCrLf
        strScript += "          });" & vbCrLf
        strScript += "      });" & vbCrLf & vbCrLf

        strScript += "  }(jQuery, window.Sys));" & vbCrLf

        strScript += "</script>" & vbCrLf

        Me.Page.ClientScript.RegisterClientScriptInclude("ConnectTwitterFeed", ResolveClientUrl(Me.TemplateSourceDirectory & "/js/TwitterFeed.js"))
        Page.ClientScript.RegisterStartupScript(Me.[GetType](), "ConnectTwitterFeedScriptBlock", strScript)

    End Sub

    Private Sub BindData()

        Dim feed As New List(Of TwitterPost)
        Dim ctrl As New FeedController(_TokenKey, _TokenSecret, _ConsumerKey, _ConsumerSecret)

        Select Case _Displaymode.ToLower
            Case "u"
                feed = ctrl.GetUserTimeLine(_SearchFor, _PostCount)
            Case "s"
                feed = ctrl.SearchTweets(_SearchFor, _PostCount)
        End Select

        rptFeed.DataSource = feed
        rptFeed.DataBind()

    End Sub

    Private Sub LoadSettings()

        If (Settings.Contains("Twitter_SelectedTemplate")) Then _Template = Settings("Twitter_SelectedTemplate").ToString()
        If (Settings.Contains("Twitter_DisplayMode")) Then _Displaymode = Settings("Twitter_DisplayMode").ToString()
        If (Settings.Contains("Twitter_ConsumerKey")) Then _ConsumerKey = Settings("Twitter_ConsumerKey").ToString()
        If (Settings.Contains("Twitter_ConsumerSecret")) Then _ConsumerSecret = Settings("Twitter_ConsumerSecret").ToString()
        If (Settings.Contains("Twitter_TokenKey")) Then _TokenKey = Settings("Twitter_TokenKey").ToString()
        If (Settings.Contains("Twitter_TokenSecret")) Then _TokenSecret = Settings("Twitter_TokenSecret").ToString()
        If (Settings.Contains("Twitter_SearchFor")) Then _SearchFor = Settings("Twitter_SearchFor").ToString()
        If (Settings.Contains("Twitter_PostCount")) Then _PostCount = Settings("Twitter_PostCount").ToString()
        If (Settings.Contains("Twitter_RefreshInterval")) Then _RefreshInterval = Settings("Twitter_RefreshInterval").ToString()
        If (Settings.Contains("Twitter_RenderingMode")) Then _RenderingMode = Settings("Twitter_RenderingMode").ToString()

    End Sub

    Private Function ValidateSettings() As Boolean

        If String.IsNullOrEmpty(_TokenKey) Or String.IsNullOrEmpty(_TokenSecret) Or String.IsNullOrEmpty(_ConsumerKey) Or String.IsNullOrEmpty(_ConsumerSecret) Or String.IsNullOrEmpty(_SearchFor) Then
            Return False
        End If

        Return True

    End Function


#End Region



End Class