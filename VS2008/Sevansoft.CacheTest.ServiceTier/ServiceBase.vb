#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Diagnostics
Imports System.ServiceModel.Activation
Imports System.Web
Imports System.Web.Caching

Imports Sevansoft.CacheTest.BusinessTier

Namespace Sevansoft.CacheTest.ServiceTier
    <DebuggerStepThrough()> _
    Public MustInherit Class ServiceBase
        Protected Function GetItemFromCache(Of T)(ByVal Key As String) As T
            Dim result As T = Nothing

            Try
                result = CType(GetCache(Key), T)
            Catch ex As Exception

            End Try

            Return result
        End Function

        Protected Sub InsertToCache(ByVal Key As String, _
                                    ByVal value As Object)
            InsertToCache(Key, value, New TimeSpan(24, 0, 0), CacheItemPriority.Default)
        End Sub


        Protected Sub InsertToCache(ByVal Key As String, _
                                    ByVal value As Object, _
                                    ByVal slidingExpiration As TimeSpan, _
                                    ByVal priority As CacheItemPriority)
            InsertToCache(Key, value, Nothing, Cache.NoAbsoluteExpiration, slidingExpiration, priority, Nothing)
        End Sub

        Protected Sub InsertToCache(ByVal Key As String, _
                                    ByVal value As Object, _
                                    ByVal priority As CacheItemPriority)
            InsertToCache(Key, value, Nothing, Cache.NoAbsoluteExpiration, New TimeSpan(24, 0, 0), priority, Nothing)
        End Sub

        Protected Sub InsertToCache(ByVal Key As String, _
                                    ByVal value As Object, _
                                    ByVal dependencies As CacheDependency, _
                                    ByVal absoluteExpiration As Date, _
                                    ByVal slidingExpiration As TimeSpan, _
                                    ByVal priority As CacheItemPriority, _
                                    ByVal onRemoveCallback As CacheItemRemovedCallback)
            GetCache.Insert(Key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback)
        End Sub

        Protected Function GetProductBusinessTier() As ProductRepository
            Return New ProductRepository()
        End Function

        Protected Function GetCache() As Cache
            Return HttpContext.Current.Cache
        End Function
    End Class
End Namespace