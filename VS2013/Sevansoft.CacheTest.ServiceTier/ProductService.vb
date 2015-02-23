#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.ServiceModel.Activation

Imports Sevansoft.CacheTest.Entities
Imports Sevansoft.CacheTest.Interfaces

Namespace Sevansoft.CacheTest.ServiceTier
    Public Class ProductService
        Inherits ServiceBase
        Implements IProduct

        Public Function GetProduct(ByVal code As String) As Product Implements IProduct.GetProduct
            Return GetProductBusinessTier().GetProduct(code)
        End Function

        Public Function GetProducts() As ProductList Implements IProduct.GetProducts
            Const CACHE_KEY As String = "productList"
            Dim result As ProductList = MyBase.GetItemFromCache(Of ProductList)(CACHE_KEY)

            If result Is Nothing Then
                result = GetProductBusinessTier().GetProducts()
                MyBase.InsertToCache(CACHE_KEY, result)
            End If

            Return result
        End Function

        Public Sub ClearCache() Implements IProduct.ClearCache
            Dim cacheKeys As IEnumerable(Of String) = GetCache().Select(Function(kvp) kvp.Key)

            For Each cacheKey As String In cacheKeys
                GetCache().Remove(cacheKey)
            Next cacheKey
        End Sub
    End Class
End Namespace