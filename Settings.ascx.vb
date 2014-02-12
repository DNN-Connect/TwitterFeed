' Copyright (c) 2014  Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
Imports DotNetNuke
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Entities.Modules


Public Class Settings
    Inherits ModuleSettingsBase

#Region "Base Method Implementations"

    Public Overrides Sub LoadSettings()
        Try
            If (Page.IsPostBack = False) Then

                LocalizeForm()
                BindTemplates()
                BindCount()
                BindIntervals()

                Try
                    If (Settings.Contains("Twitter_SelectedTemplate")) Then drpTemplate.SelectedValue = Settings("Twitter_SelectedTemplate").ToString()
                Catch
                End Try
                If (Settings.Contains("Twitter_DisplayMode")) Then drpDisplaymode.SelectedValue = Settings("Twitter_DisplayMode").ToString()
                If (Settings.Contains("Twitter_ConsumerKey")) Then txtConsumerKey.Text = Settings("Twitter_ConsumerKey").ToString()
                If (Settings.Contains("Twitter_ConsumerSecret")) Then txtConsumerSecret.Text = Settings("Twitter_ConsumerSecret").ToString()
                If (Settings.Contains("Twitter_TokenKey")) Then txtTokenKey.Text = Settings("Twitter_TokenKey").ToString()
                If (Settings.Contains("Twitter_TokenSecret")) Then txtTokenSecret.Text = Settings("Twitter_TokenSecret").ToString()
                If (Settings.Contains("Twitter_SearchFor")) Then txtSearchFor.Text = Settings("Twitter_SearchFor").ToString()
                If (Settings.Contains("Twitter_PostCount")) Then drpCount.SelectedValue = Settings("Twitter_PostCount").ToString()
                If (Settings.Contains("Twitter_RefreshInterval")) Then drpInterval.SelectedValue = Settings("Twitter_RefreshInterval").ToString()
                If (Settings.Contains("Twitter_RenderingMode")) Then drpRenderingMode.SelectedValue = Settings("Twitter_RenderingMode").ToString()

            End If
        Catch exc As Exception           'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Public Overrides Sub UpdateSettings()
        Try
            Dim objModules As New DotNetNuke.Entities.Modules.ModuleController

            objModules.UpdateModuleSetting(ModuleId, "Twitter_SelectedTemplate", drpTemplate.SelectedValue)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_DisplayMode", drpDisplaymode.SelectedValue)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_ConsumerKey", txtConsumerKey.Text)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_ConsumerSecret", txtConsumerSecret.Text)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_TokenKey", txtTokenKey.Text)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_TokenSecret", txtTokenSecret.Text)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_SearchFor", txtSearchFor.Text)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_PostCount", drpCount.SelectedValue)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_RefreshInterval", drpInterval.SelectedValue)
            objModules.UpdateModuleSetting(ModuleId, "Twitter_RenderingMode", drpRenderingMode.SelectedValue)

        Catch exc As Exception           'Module failed to load
            ProcessModuleLoadException(Me, exc)
        End Try
    End Sub

    Private Sub BindIntervals()

        drpInterval.Items.Clear()
        For i As Integer = 30 To 300 Step 30
            drpInterval.Items.Add(New ListItem(i.ToString, i.ToString))
        Next

    End Sub

    Private Sub BindCount()

        drpCount.Items.Clear()
        For i As Integer = 1 To 30
            drpCount.Items.Add(New ListItem(i.ToString, i.ToString))
        Next

    End Sub

    Private Sub BindTemplates()

        drpTemplate.Items.Clear()
        Dim basepath As String = Server.MapPath(Me.TemplateSourceDirectory & "/templates/")

        For Each folder As String In System.IO.Directory.GetDirectories(basepath)
            Dim foldername As String = folder.Substring(folder.LastIndexOf("\") + 1)
            Dim folderpath As String = Me.TemplateSourceDirectory & "/templates/" & foldername
            drpTemplate.Items.Add(New ListItem(foldername, folderpath))
        Next

    End Sub

    Private Sub LocalizeForm()

        For Each item As ListItem In drpDisplaymode.Items
            item.Text = LocalizeString(item.Text)
        Next

        For Each item As ListItem In drpRenderingMode.Items
            item.Text = LocalizeString(item.Text)
        Next

    End Sub

#End Region


End Class