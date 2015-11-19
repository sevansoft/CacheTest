#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.IO
Imports System.Threading

Imports Sevansoft.CacheTest.Entities
Imports Sevansoft.CacheTest.Interfaces

Namespace Sevansoft.CacheTest.BusinessTier
    Public Class CountryInformationRepository
        Implements ICountryInformation

        Public Function GetCountryInfos() As CountryInfoList Implements ICountryInformation.GetCountryInfos
            Dim filePath As String = Path.Combine(DataPath, "CountryInfo.xml")
            'artifical delay to behave like a slow process
            Thread.Sleep(2000)
            Return CountryInfoList.LoadList(New FileInfo(filePath))
        End Function

        Public Function GetStateInfos(countryCode As String) As StateInfoList Implements ICountryInformation.GetStateInfos
            Dim filePath As String = Path.Combine(DataPath, "StateInfo.xml")

            Return StateInfoList.LoadList(New FileInfo(filePath), countryCode)
        End Function

        Private Function DataPath() As String
            Return "~/Data"
        End Function

        Public Sub ClearCache() Implements IServiceBase.ClearCache
            Throw New NotImplementedException
        End Sub

        Public Function HostName() As String Implements IServiceBase.HostName
            Throw New NotImplementedException
        End Function
    End Class
End Namespace