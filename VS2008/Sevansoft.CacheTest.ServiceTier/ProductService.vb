#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Collections
Imports System.ServiceModel.Activation

Imports Sevansoft.CacheTest.Entities
Imports Sevansoft.CacheTest.Interfaces

Namespace Sevansoft.CacheTest.ServiceTier
    <AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Required)> _
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

        Public Sub ClearCache() Implements Interfaces.IProduct.ClearCache
            Dim enumerator As IDictionaryEnumerator = GetCache().GetEnumerator()

            While enumerator.MoveNext
                GetCache().Remove(enumerator.Key.ToString())
            End While
        End Sub
    End Class
End Namespace