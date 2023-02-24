﻿'
' News Articles for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2008
' by Ventrian ( sales@ventrian.com ) ( http://www.ventrian.com )
'

Imports System.IO
Imports System.Xml
Imports DotNetNuke.Application

Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Host
Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Services.Cache
Imports DotNetNuke.Services.Localization

Namespace Ventrian.NewsArticles.Components.Utility

    Public Class LocalizationUtil

#Region " Private Members "

        Private Shared _strUseLanguageInUrlDefault As String = Null.NullString

#End Region

#Region " Public Methods "

        'Private Shared Function GetHostSettingAsBoolean(ByVal key As String, ByVal defaultValue As Boolean) As Boolean
        '    Dim retValue As Boolean = defaultValue
        '    Try
        '        Dim setting As String = DotNetNuke.Entities.Host.HostSettings.GetHostSetting(key)
        '        If String.IsNullOrEmpty(setting) = False Then
        '            retValue = (setting.ToUpperInvariant().StartsWith("Y") OrElse setting.ToUpperInvariant = "TRUE")
        '        End If
        '    Catch ex As Exception
        '        'we just want to trap the error as we may not be installed so there will be no Settings
        '    End Try
        '    Return retValue
        'End Function

        'Private Shared Function GetPortalSettingAsBoolean(ByVal portalID As Integer, ByVal key As String, ByVal defaultValue As Boolean) As Boolean
        '    Dim retValue As Boolean = defaultValue
        '    Try
        '        Dim setting As String = DotNetNuke.Entities.Portals.PortalSettings.GetSiteSetting(portalID, key)
        '        If String.IsNullOrEmpty(setting) = False Then
        '            retValue = (setting.ToUpperInvariant().StartsWith("Y") OrElse setting.ToUpperInvariant = "TRUE")
        '        End If
        '    Catch ex As Exception
        '        'we just want to trap the error as we may not be installed so there will be no Settings
        '    End Try
        '    Return retValue
        'End Function

        Public Shared Function UseLanguageInUrl() As Boolean

            If (Host.EnableUrlLanguage) Then
                Return Host.EnableUrlLanguage
            End If

            If (PortalSettings.Current.EnableUrlLanguage) Then
                Return PortalSettings.Current.EnableUrlLanguage
            End If

            If (File.Exists(HttpContext.Current.Server.MapPath(Localization.ApplicationResourceDirectory + "/Locales.xml")) = False) Then
                Return Host.EnableUrlLanguage
            End If

            Dim cacheKey As String = ""
            Dim objPortalSettings As PortalSettings = PortalController.Instance.GetCurrentPortalSettings()
            Dim useLanguage As Boolean = False

            ' check default host setting
            If String.IsNullOrEmpty(_strUseLanguageInUrlDefault) Then
                Dim xmldoc As New XmlDocument
                Dim languageInUrl As XmlNode

                xmldoc.Load(HttpContext.Current.Server.MapPath(Localization.ApplicationResourceDirectory + "/Locales.xml"))
                languageInUrl = xmldoc.SelectSingleNode("//root/languageInUrl")
                If Not languageInUrl Is Nothing Then
                    _strUseLanguageInUrlDefault = languageInUrl.Attributes("enabled").InnerText
                Else
                    Try
                        Dim version As Integer = Convert.ToInt32(DotNetNukeContext.Current.Application.Version.ToString().Replace(".", ""))
                        If (version >= 490) Then
                            _strUseLanguageInUrlDefault = "true"
                        Else
                            _strUseLanguageInUrlDefault = "false"
                        End If
                    Catch
                        _strUseLanguageInUrlDefault = "false"
                    End Try
                End If
            End If
            useLanguage = Boolean.Parse(_strUseLanguageInUrlDefault)

            ' check current portal setting
            Dim FilePath As String = HttpContext.Current.Server.MapPath(Localization.ApplicationResourceDirectory + "/Locales.Portal-" + objPortalSettings.PortalId.ToString + ".xml")
            If File.Exists(FilePath) Then
                cacheKey = "dotnetnuke-uselanguageinurl" & objPortalSettings.PortalId.ToString
                Try
                    Dim o As Object = DataCache.GetCache(cacheKey)
                    If o Is Nothing Then
                        Dim xmlLocales As New XmlDocument
                        Dim bXmlLoaded As Boolean = False

                        xmlLocales.Load(FilePath)
                        bXmlLoaded = True

                        Dim d As New XmlDocument
                        d.Load(FilePath)

                        If bXmlLoaded AndAlso Not xmlLocales.SelectSingleNode("//locales/languageInUrl") Is Nothing Then
                            useLanguage = Boolean.Parse(xmlLocales.SelectSingleNode("//locales/languageInUrl").Attributes("enabled").InnerText)
                        End If
                        If Host.PerformanceSetting <> Globals.PerformanceSettings.NoCaching Then
                            Dim dp As New DNNCacheDependency(FilePath)
                            DataCache.SetCache(cacheKey, useLanguage, dp)
                        End If
                    Else
                        useLanguage = CType(o, Boolean)
                    End If
                Catch ex As Exception
                End Try

                Return useLanguage
            Else
                Return useLanguage
            End If

        End Function

#End Region

    End Class

End Namespace


