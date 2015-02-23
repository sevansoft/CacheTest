#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Diagnostics
Imports System.Runtime.Caching

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
            InsertToCache(Key, value, ObjectCache.InfiniteAbsoluteExpiration, slidingExpiration, priority, Nothing)
        End Sub

        Protected Sub InsertToCache(ByVal Key As String, _
                                    ByVal value As Object, _
                                    ByVal priority As CacheItemPriority)
            InsertToCache(Key, value, ObjectCache.InfiniteAbsoluteExpiration, New TimeSpan(24, 0, 0), priority, Nothing)
        End Sub

        Protected Sub InsertToCache(ByVal Key As String, _
                                    ByVal value As Object, _
                                    ByVal absoluteExpiration As DateTimeOffset, _
                                    ByVal slidingExpiration As TimeSpan, _
                                    ByVal priority As CacheItemPriority, _
                                    ByVal onRemoveCallback As CacheEntryRemovedCallback)

            Dim cacheItemPolicy As CacheItemPolicy = New CacheItemPolicy()


            cacheItemPolicy.AbsoluteExpiration = absoluteExpiration
            cacheItemPolicy.Priority = priority
            cacheItemPolicy.SlidingExpiration = slidingExpiration
            cacheItemPolicy.RemovedCallback = onRemoveCallback

            GetCache.Add(Key, value, cacheItemPolicy)
        End Sub

        Protected Function GetProductBusinessTier() As ProductRepository
            Return New ProductRepository()
        End Function

        Protected Function GetCache() As ObjectCache
            Return MemoryCache.Default
        End Function
    End Class
End Namespace