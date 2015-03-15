#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ServiceModel.Activation

Imports Sevansoft.CacheTest.BusinessTier
Imports Sevansoft.CacheTest.Caching
Imports Sevansoft.CacheTest.Entities
Imports Sevansoft.CacheTest.Interfaces

Namespace Sevansoft.CacheTest.ServiceTier
    Public Class CountryInformationService
        Inherits ServiceBase
        Implements ICountryInformation

        Public Function GetCountryInfos() As CountryInfoList Implements ICountryInformation.GetCountryInfos
            Const CACHE_KEY As String = "countryList"

            Return Cache.Get(Of CountryInfoList)(CACHE_KEY, Function() GetCountryInformationBusinessTier().GetCountryInfos())

        End Function

        Public Function GetStateInfos(countryCode As String) As StateInfoList Implements ICountryInformation.GetStateInfos
            Const CACHE_KEY_PREFIX As String = "stateList:{0}"

            Return Cache.Get(Of StateInfoList)(String.Format(CACHE_KEY_PREFIX, countryCode), Function() GetCountryInformationBusinessTier().GetStateInfos(countryCode))
        End Function

        Protected Function GetCountryInformationBusinessTier() As CountryInformationRepository
            Return New CountryInformationRepository()
        End Function
    End Class
End Namespace