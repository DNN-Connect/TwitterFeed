Imports DotNetNuke.Services.Localization

Public Class TemplateProcessor

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
    Private _LocalResourcefile As String = "~/Desktopmodules/Connect/TwitterFeed/App_LocalResources/View.ascx"

    Public Sub New(Settings As Hashtable)
        _settings = Settings
        LoadSettings()
    End Sub

    Public Sub ProcessSurroundingTemplate(ByRef controls As System.Web.UI.ControlCollection, ByVal Template As String)
        ProcessSurroundingTemplate(Nothing, controls, Template)
    End Sub

    Public Sub ProcessSurroundingTemplate(ByRef strHtml As String, ByVal Template As String)
        ProcessSurroundingTemplate(strHtml, Nothing, Template)
    End Sub

    Private Sub ProcessSurroundingTemplate(ByRef strHtml As String, ByRef controls As System.Web.UI.ControlCollection, ByVal Template As String)

        Dim literal As New Literal
        Dim delimStr As String = "[]"
        Dim delimiter As Char() = delimStr.ToCharArray()

        Dim templateArray As String()
        templateArray = Template.Split(delimiter)

        For iPtr As Integer = 0 To templateArray.Length - 1 Step 2

            If Not controls Is Nothing Then
                controls.Add(New LiteralControl(templateArray(iPtr).ToString()))
            End If

            If Not strHtml Is Nothing Then
                strHtml += templateArray(iPtr).ToString()
            End If


            If iPtr < templateArray.Length - 1 Then
                Select Case templateArray(iPtr + 1)

                    Case "TWITTERURL"

                        Dim strUrl As String = ""
                        Select Case _Displaymode.ToLower
                            Case "u"
                                strUrl = "http://twitter.com/" & _SearchFor
                            Case "s"
                                strUrl = "http://twitter.com/search?q=" & _SearchFor
                        End Select

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = strUrl
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += strUrl
                        End If

                    Case Else

                        If (templateArray(iPtr + 1).ToUpper().StartsWith("RESX:")) Then

                            Dim key As String = templateArray(iPtr + 1).Substring(5, templateArray(iPtr + 1).Length - 5)
                            Dim strText As String = Localization.GetString(key, _LocalResourcefile)

                            If Not controls Is Nothing Then
                                Dim objLiteral As New Literal
                                objLiteral.Text = strText
                                objLiteral.EnableViewState = False
                                controls.Add(objLiteral)
                            End If

                            If Not strHtml Is Nothing Then
                                strHtml += strText
                            End If

                        End If

                End Select
            End If
        Next

    End Sub

    Public Sub ProcessItemTemplate(ByRef strHtml As String, ByVal Template As String, ByVal objPost As TwitterPost)
        ProcessItemTemplate(strHtml, Nothing, Template, objPost)
    End Sub

    Public Sub ProcessItemTemplate(ByRef controls As System.Web.UI.ControlCollection, ByVal Template As String, ByVal objPost As TwitterPost)
        ProcessItemTemplate(Nothing, controls, Template, objPost)
    End Sub

    Private Sub ProcessItemTemplate(ByRef strHtml As String, ByRef controls As System.Web.UI.ControlCollection, ByVal Template As String, ByVal objPost As TwitterPost)

        Dim literal As New Literal
        Dim delimStr As String = "[]"
        Dim delimiter As Char() = delimStr.ToCharArray()

        Dim templateArray As String()
        templateArray = Template.Split(delimiter)

        For iPtr As Integer = 0 To templateArray.Length - 1 Step 2

            If Not controls Is Nothing Then
                controls.Add(New LiteralControl(templateArray(iPtr).ToString()))
            End If

            If Not strHtml Is Nothing Then
                strHtml += templateArray(iPtr).ToString()
            End If

            If iPtr < templateArray.Length - 1 Then
                Select Case templateArray(iPtr + 1)
                    Case "CONTENT"

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = objPost.text
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += objPost.text
                        End If

                    Case "HTMLCONTENT"

                        Dim strContent As String = Helpers.GetTweetBodyAsRichText(objPost)

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = strContent
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += strContent
                        End If


                    Case "SOURCE"

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = objPost.source
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += objPost.source
                        End If

                    Case "HASEMBEDDEDIMAGE"
                        If objPost.entities.media Is Nothing OrElse objPost.entities.media.Count = 0 Then
                            While (iPtr < templateArray.Length - 1)
                                If (templateArray(iPtr + 1) = "/HASEMBEDDEDIMAGE") Then
                                    Exit While
                                End If
                                iPtr = iPtr + 1
                            End While
                        End If
                    Case "EMBEDDEDIMAGEURL"
                        If Not objPost.entities.media Is Nothing Then
                            If objPost.entities.media.Count > 0 Then

                                If Not controls Is Nothing Then
                                    Dim objLiteral As New Literal
                                    objLiteral.Text = objPost.entities.media(0).media_url
                                    objLiteral.EnableViewState = False
                                    controls.Add(objLiteral)
                                End If

                                If Not strHtml Is Nothing Then
                                    strHtml += objPost.entities.media(0).media_url
                                End If


                            End If
                        End If
                    Case "HASEMBEDDELINK"
                        If objPost.entities.urls Is Nothing OrElse objPost.entities.urls.Count = 0 Then
                            While (iPtr < templateArray.Length - 1)
                                If (templateArray(iPtr + 1) = "/HASEMBEDDELINK") Then
                                    Exit While
                                End If
                                iPtr = iPtr + 1
                            End While
                        End If
                    Case "EMBEDDEDLINKURL"
                        If Not objPost.entities.urls Is Nothing Then
                            If objPost.entities.urls.Count > 0 Then

                                If Not controls Is Nothing Then
                                    Dim objLiteral As New Literal
                                    objLiteral.Text = objPost.entities.urls(0).url
                                    objLiteral.EnableViewState = False
                                    controls.Add(objLiteral)
                                End If

                                If Not strHtml Is Nothing Then
                                    strHtml += objPost.entities.urls(0).url
                                End If


                            End If
                        End If
                    Case "PUBLISHDATE"

                        Dim Publishdate As DateTime = Helpers.ParseDateTime(objPost.created_at)
                        Dim strDate As String = Publishdate.ToShortDateString

                        If Publishdate.Date = Date.Now.Date Then
                            strDate = Localization.GetString("Today", _LocalResourcefile)
                        End If

                        If Publishdate.Date.AddDays(1) = Date.Now.Date Then
                            strDate = Localization.GetString("Yesterday", _LocalResourcefile)
                        End If


                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = strDate
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += strDate
                        End If


                    Case "PUBLISHTIME"

                        Dim Publishdate As DateTime = Helpers.ParseDateTime(objPost.created_at)
                        Dim strTime As String = String.Format(Localization.GetString("Time", _LocalResourcefile), Publishdate.ToShortTimeString)

                        If Publishdate.Date = Date.Now.Date Then
                            'today
                            Dim diff As Integer = DateDiff(DateInterval.Minute, Publishdate, Date.Now)
                            If diff < 60 Then
                                'within last hour
                                strTime = String.Format(Localization.GetString("MinutesAgo", _LocalResourcefile), diff.ToString)
                            End If

                        End If

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = strTime
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += strTime
                        End If


                    Case "AUTHORSCREENNAME"

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = objPost.user.screen_name
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += objPost.user.screen_name
                        End If


                    Case "AUTHORFULLNAME"

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = objPost.user.name
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += objPost.user.name
                        End If

                    Case "AUTHORURL"

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = objPost.user.url
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += objPost.user.url
                        End If

                    Case "AUTHORIMAGEURL"

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = objPost.user.profile_image_url
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += objPost.user.profile_image_url
                        End If

                    Case "AUTHORIMAGEURLBIGGER"

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = objPost.user.profile_image_url.Replace("_normal", "_bigger")
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += objPost.user.profile_image_url.Replace("_normal", "_bigger")
                        End If

                    Case "AUTHORIMAGEURLORIGINAL"

                        If Not controls Is Nothing Then
                            Dim objLiteral As New Literal
                            objLiteral.Text = objPost.user.profile_image_url.Replace("_normal", "")
                            objLiteral.EnableViewState = False
                            controls.Add(objLiteral)
                        End If

                        If Not strHtml Is Nothing Then
                            strHtml += objPost.user.profile_image_url.Replace("_normal", "")
                        End If

                End Select
            End If
        Next


    End Sub

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
