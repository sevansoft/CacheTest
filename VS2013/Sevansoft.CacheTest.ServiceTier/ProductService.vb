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
    Public Class ProductService
        Inherits ServiceBase
        Implements IProduct

        Public Function GetProduct(ByVal code As String) As Product Implements IProduct.GetProduct
            Return GetProductBusinessTier().GetProduct(code)
        End Function

        Public Function GetProducts() As ProductList Implements IProduct.GetProducts
            Const CACHE_KEY As String = "productList"

            Return Cache.Get(Of ProductList)(CACHE_KEY, Function() GetProductBusinessTier().GetProducts())
        End Function

        Protected Function GetProductBusinessTier() As ProductRepository
            Return New ProductRepository()
        End Function
    End Class
End Namespace