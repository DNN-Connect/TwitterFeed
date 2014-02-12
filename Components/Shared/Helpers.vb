Imports System.IO

Public Class Helpers

    Public Shared Function ParseDateTime([date] As String) As DateTime


        Try

            Dim format As String = "ddd MMM dd HH:mm:ss zzzz yyyy"
            Dim dt As DateTime = DateTime.ParseExact([date], format, System.Globalization.CultureInfo.InvariantCulture)
            Return dt

        Catch ex As Exception


            Dim dayOfWeek As String = [date].Substring(0, 3).Trim()
            Dim month As String = [date].Substring(4, 3).Trim()
            Dim dayInMonth As String = [date].Substring(8, 2).Trim()
            Dim time As String = [date].Substring(11, 9).Trim()
            Dim offset As String = [date].Substring(20, 5).Trim()
            Dim year As String = [date].Substring(25, 5).Trim()
            Dim dateTime__1 As String = String.Format("{0}-{1}-{2} {3}", dayInMonth, month, year, time)
            Dim ret As DateTime = DateTime.Parse(dateTime__1)
            Return ret

        End Try

    End Function

    Public Shared Function GetTweetBodyAsRichText(ByVal objTweet As TwitterPost) As String

        Dim strHTML As String = objTweet.text

        If Not objTweet.entities Is Nothing Then

            If Not objTweet.entities.urls Is Nothing Then
                For Each objUrl As url In objTweet.entities.urls
                    Dim urlstring As String = objUrl.url
                    Dim url As String = objUrl.url
                    strHTML = strHTML.Replace(urlstring, String.Format("<a href=""{0}"" target=""_blank"">{1}</a>", url, urlstring))
                Next
            End If

            If Not objTweet.entities.user_mentions Is Nothing Then
                For Each objMention As user_mention In objTweet.entities.user_mentions
                    Dim usernamestring As String = "@" & objMention.screen_name
                    Dim userurl As String = "http://twitter.com/" & objMention.screen_name
                    strHTML = strHTML.Replace(usernamestring, String.Format("<a href=""{0}"" target=""_blank"">{1}</a>", userurl, usernamestring))
                Next
            End If

            If Not objTweet.entities.hashtags Is Nothing Then
                For Each objHashtag As hashtag In objTweet.entities.hashtags
                    Dim hashtagstring As String = "#" & objHashtag.text
                    Dim hashtagurl As String = "http://twitter.com?q=" & objHashtag.text
                    strHTML = strHTML.Replace(hashtagstring, String.Format("<a href=""{0}"" target=""_blank"">{1}</a>", hashtagurl, hashtagstring))
                Next

            End If
        End If

        Return strHTML

    End Function

    Public Shared Function ReadTemplate(strTemplatePath As String) As String

        If File.Exists(strTemplatePath) Then
            Dim sr As New StreamReader(strTemplatePath)
            Dim strContent As String = sr.ReadToEnd
            sr.Close()
            sr.Dispose()
            Return strContent
        End If

        Return ""

    End Function

End Class
