#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.ServiceModel

Imports Sevansoft.CacheTest.Entities

Namespace Sevansoft.CacheTest.Interfaces
    <ServiceContract>
    Public Interface IServiceBase
        <OperationContract>
        Sub ClearCache()
    End Interface
End Namespace