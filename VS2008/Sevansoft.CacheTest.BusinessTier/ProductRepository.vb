#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Threading

Imports Sevansoft.CacheTest.Entities
Imports Sevansoft.CacheTest.Interfaces

Namespace Sevansoft.CacheTest.BusinessTier
    Public Class ProductRepository
        Implements IProduct

        Public Function GetProduct(ByVal code As String) As Product Implements IProduct.GetProduct
            Return New Product() With {.Code = code, .Name = String.Format("This is the name of {0}", code)}
        End Function

        Public Function GetProducts() As ProductList Implements IProduct.GetProducts
            Dim result As ProductList = New ProductList()

            For counter As Int32 = 1 To 1000
                result.Add(New Product() With {.Code = String.Format("Ref{0}", counter), .Name = String.Format("This is the name of Ref{0}", counter)})
            Next counter

            'simulate slowness
            Thread.Sleep(2000)

            Return result
        End Function

        Public Sub ClearCache() Implements IProduct.ClearCache
            Throw New NotImplementedException
        End Sub
    End Class
End Namespace