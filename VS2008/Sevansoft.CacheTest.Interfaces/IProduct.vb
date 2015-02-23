#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.ServiceModel

Imports Sevansoft.CacheTest.Entities

Namespace Sevansoft.CacheTest.Interfaces
    <ServiceContract()> _
    Public Interface IProduct
        <OperationContract()> _
        Sub ClearCache()

        <OperationContract()> _
        Function GetProduct(ByVal code As String) As Product

        <OperationContract()> _
        Function GetProducts() As ProductList
    End Interface
End Namespace