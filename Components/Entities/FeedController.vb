#Region "copyright"

' bitboxx - http://www.bitboxx.net
' Copyright (c) 2014 
' by bitboxx solutions Torsten Weggen
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.

#End Region


Imports System.Collections.Generic
Imports System.Linq
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Xml.Linq
Imports System.Net
Imports System.IO


Public Class FeedController

    Private m_TokenKey As String
    Private m_TokenSecret As String
    Private m_ConsumerKey As String
    Private m_ConsumerSecret As String

    Public Property TokenKey() As String
        Get
            Return m_TokenKey
        End Get
        Set(value As String)
            m_TokenKey = value
        End Set
    End Property

    Public Property TokenSecret() As String
        Get
            Return m_TokenSecret
        End Get
        Set(value As String)
            m_TokenSecret = value
        End Set
    End Property

    Public Property ConsumerKey() As String
        Get
            Return m_ConsumerKey
        End Get
        Set(value As String)
            m_ConsumerKey = value
        End Set
    End Property

    Public Property ConsumerSecret() As String
        Get
            Return m_ConsumerSecret
        End Get
        Set(value As String)
            m_ConsumerSecret = value
        End Set
    End Property

    Public Sub New(tokenKey As String, tokenSecret As String, consumerKey As String, consumerSecret As String)
        Me.TokenKey = tokenKey
        Me.TokenSecret = tokenSecret
        Me.ConsumerKey = consumerKey
        Me.ConsumerSecret = consumerSecret
    End Sub

    Public Function GetUserTimeLine(twitterUsername As String, tweetsCount As Integer) As List(Of TwitterPost)
        ' Other OAuth connection/authentication variables
        Dim oAuthVersion As String = "1.0"
        Dim oAuthSignatureMethod As String = "HMAC-SHA1"
        Dim oAuthNonce As String = Convert.ToBase64String(New ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()))
        Dim timeSpan As TimeSpan = DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0, _
            0, DateTimeKind.Utc)
        Dim oAuthTimestamp As String = Convert.ToInt64(timeSpan.TotalSeconds).ToString()
        Dim resourceUrl As String = "https://api.twitter.com/1.1/statuses/user_timeline.json"

        ' Generate OAuth signature. Note that Twitter is very particular about the format of this string. Even reordering the variables
        ' will cause authentication errors.

        Dim baseFormat = "count={7}&oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" & "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&screen_name={6}"

        Dim baseString = String.Format(baseFormat, ConsumerKey, oAuthNonce, oAuthSignatureMethod, oAuthTimestamp, TokenKey, _
            oAuthVersion, Uri.EscapeDataString(twitterUsername), Uri.EscapeDataString(tweetsCount.ToString()))

        baseString = String.Concat("GET&", Uri.EscapeDataString(resourceUrl), "&", Uri.EscapeDataString(baseString))

        ' Generate an OAuth signature using the baseString
        Dim compositeKey = String.Concat(Uri.EscapeDataString(ConsumerSecret), "&", Uri.EscapeDataString(TokenSecret))
        Dim oAuthSignature As String
        Using hasher As New HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey))
            oAuthSignature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)))
        End Using

        ' Now build the Authentication header. Again, Twitter is very particular about the format. Do not reorder variables.
        Dim headerFormat = "OAuth oauth_nonce=""{0}"", oauth_signature_method=""{1}"", " & "oauth_timestamp=""{2}"", oauth_consumer_key=""{3}"", " & "oauth_token=""{4}"", oauth_signature=""{5}"", " & "oauth_version=""{6}"""

        Dim authHeader = String.Format(headerFormat, Uri.EscapeDataString(oAuthNonce), Uri.EscapeDataString(oAuthSignatureMethod), Uri.EscapeDataString(oAuthTimestamp), Uri.EscapeDataString(ConsumerKey), Uri.EscapeDataString(TokenKey), _
            Uri.EscapeDataString(oAuthSignature), Uri.EscapeDataString(oAuthVersion))

        ' Now build the actual request

        ServicePointManager.Expect100Continue = False
        Dim postBody = String.Format("screen_name={0}&count={1}", Uri.EscapeDataString(twitterUsername), Uri.EscapeDataString(tweetsCount.ToString()))
        resourceUrl += "?" & postBody
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(resourceUrl), HttpWebRequest)
        request.Headers.Add("Authorization", authHeader)
        request.Method = "GET"
        request.ContentType = "application/x-www-form-urlencoded"

        ' Retrieve the response data and deserialize the JSON data to a list of Tweet objects
        Dim response As WebResponse = request.GetResponse()
        Dim responseData As String = New StreamReader(response.GetResponseStream()).ReadToEnd()
        Dim userTweets As List(Of TwitterPost) = New JavaScriptSerializer().Deserialize(Of List(Of TwitterPost))(responseData)

        Return userTweets
        'Return TweetsToNews(userTweets)

    End Function

    Public Function SearchTweets(searchTerm As String, tweetsCount As Integer) As List(Of TwitterPost)
        'Dim TokenKey As String = "55684465-gvmJ81DMpzs7QxjY4BlQM2GzRWE9sUZIVTWMrX9nN"
        'Dim TokenSecret As String = "nO07e9Vyj4MVJtAYTmYaW2RrEtuJSineAIesWtTTXA"
        'Dim ConsumerKey As String = "HEc2tiosBZ0U62SLmsYaOA"
        'Dim ConsumerSecret As String = "i3OGf4Rm3bA5MSV55X2froEjo4TCJtA49msBJ1dW9Fc"

        ' Other OAuth connection/authentication variables
        Dim oAuthVersion As String = "1.0"
        Dim oAuthSignatureMethod As String = "HMAC-SHA1"
        Dim oAuthNonce As String = Convert.ToBase64String(New ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()))
        Dim timeSpan As TimeSpan = DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0, _
            0, DateTimeKind.Utc)
        Dim oAuthTimestamp As String = Convert.ToInt64(timeSpan.TotalSeconds).ToString()
        Dim resourceUrl As String = "https://api.twitter.com/1.1/search/tweets.json"

        ' Generate OAuth signature. Note that Twitter is very particular about the format of this string. Even reordering the variables
        ' will cause authentication errors.

        Dim baseFormat = "count={7}&oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" & "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&q={6}"

        Dim baseString = String.Format(baseFormat, ConsumerKey, oAuthNonce, oAuthSignatureMethod, oAuthTimestamp, TokenKey, _
            oAuthVersion, Uri.EscapeDataString(searchTerm), Uri.EscapeDataString(tweetsCount.ToString()))

        baseString = String.Concat("GET&", Uri.EscapeDataString(resourceUrl), "&", Uri.EscapeDataString(baseString))

        ' Generate an OAuth signature using the baseString
        Dim compositeKey = String.Concat(Uri.EscapeDataString(ConsumerSecret), "&", Uri.EscapeDataString(TokenSecret))
        Dim oAuthSignature As String
        Using hasher As New HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey))
            oAuthSignature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)))
        End Using

        ' Now build the Authentication header. Again, Twitter is very particular about the format. Do not reorder variables.
        Dim headerFormat = "OAuth oauth_nonce=""{0}"", oauth_signature_method=""{1}"", " & "oauth_timestamp=""{2}"", oauth_consumer_key=""{3}"", " & "oauth_token=""{4}"", oauth_signature=""{5}"", " & "oauth_version=""{6}"""

        Dim authHeader = String.Format(headerFormat, Uri.EscapeDataString(oAuthNonce), Uri.EscapeDataString(oAuthSignatureMethod), Uri.EscapeDataString(oAuthTimestamp), Uri.EscapeDataString(ConsumerKey), Uri.EscapeDataString(TokenKey), _
            Uri.EscapeDataString(oAuthSignature), Uri.EscapeDataString(oAuthVersion))

        ' Now build the actual request

        ServicePointManager.Expect100Continue = False
        Dim postBody = String.Format("q={0}&count={1}", Uri.EscapeDataString(searchTerm), Uri.EscapeDataString(tweetsCount.ToString()))
        resourceUrl += "?" & postBody
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(resourceUrl), HttpWebRequest)
        request.Headers.Add("Authorization", authHeader)
        request.Method = "GET"
        request.ContentType = "application/x-www-form-urlencoded"

        ' Retrieve the response data and deserialize the JSON data to a list of Tweet objects
        Dim response As WebResponse = request.GetResponse()
        Dim responseData As String = New StreamReader(response.GetResponseStream()).ReadToEnd()
        'Dim userTweets As TwitterPosts = New JavaScriptSerializer().Deserialize(Of TwitterPosts)(responseData)
        Dim posts As TwitterPosts = New JavaScriptSerializer().Deserialize(Of TwitterPosts)(responseData)
        Return posts.statuses
        'Return TweetsToNews(userTweets.posts)

    End Function

    Private Function TweetsToNews(tweets As List(Of TwitterPost)) As List(Of RenderedPost)

        Dim Posts As New List(Of RenderedPost)()

        For Each userTweet As TwitterPost In tweets
            Dim tweet As New RenderedPost()
            tweet.AuthorFullName = userTweet.user.name
            tweet.AuthorScreenName = userTweet.user.screen_name
            tweet.AuthorUrl = userTweet.user.url
            tweet.AuthorUrl = userTweet.user.url
            tweet.AuthorProfilePic = userTweet.user.profile_image_url

            tweet.GUID = userTweet.id_str
            tweet.Image = userTweet.user.profile_image_url
            tweet.Link = "https://twitter.com/" & userTweet.user.screen_name
            tweet.Pubdate = Helpers.ParseDateTime(userTweet.created_at)
            tweet.Content = HttpUtility.HtmlDecode(userTweet.text)
            tweet.Title = HttpUtility.HtmlDecode(userTweet.text)

            Dim pos As Integer = tweet.Title.IndexOf("http://t.co")
            If pos > -1 Then
                Dim start As String = tweet.Title.Substring(0, pos - 1)
                Dim rest As String = tweet.Title.Substring(pos)
                Dim posEnde As Integer = rest.IndexOf(" ")

                If posEnde > -1 Then
                    tweet.Link = rest.Substring(0, posEnde - 1)
                    rest = rest.Substring(posEnde)
                    tweet.Content = ((start & " <a href=""") + tweet.Link & """>") + tweet.Link & "</a> " & rest
                    tweet.Title = (start & " ") + tweet.Link & rest
                Else
                    tweet.Link = rest
                    tweet.Content = ((start & " <a href=""") + tweet.Link & """>") + tweet.Link & "</a> "
                    tweet.Title = (start & " ") + tweet.Link
                End If
            End If
            pos = tweet.Title.IndexOf("https://t.co")
            If pos > -1 Then
                Dim start As String = tweet.Title.Substring(0, pos - 1)
                Dim rest As String = tweet.Title.Substring(pos)
                Dim posEnde As Integer = rest.IndexOf(" ")

                If posEnde > -1 Then
                    tweet.Link = rest.Substring(0, posEnde - 1)
                    rest = rest.Substring(posEnde)
                    tweet.Content = ((start & " <a href=""") + tweet.Link & """>") + tweet.Link & "</a> " & rest
                    tweet.Title = (start & " ") + tweet.Link & rest
                Else
                    tweet.Link = rest
                    tweet.Content = ((start & " <a href=""") + tweet.Link & """>") + tweet.Link & "</a> "
                    tweet.Title = (start & " ") + tweet.Link
                End If
            End If
            Posts.Add(tweet)
        Next

        Return (From l In Posts Where l.Content <> [String].Empty Order By l.Pubdate Descending).ToList()

    End Function


End Class

