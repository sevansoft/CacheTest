#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.ServiceModel

Imports Sevansoft.CacheTest.Entities

Namespace Sevansoft.CacheTest.Interfaces
    <ServiceContract>
    Public Interface ICountryInformation
        Inherits IServiceBase

        <OperationContract>
        Function GetCountryInfos() As CountryInfoList

        <OperationContract>
        Function GetStateInfos(ByVal countryCode As String) As StateInfoList
    End Interface
End Namespace