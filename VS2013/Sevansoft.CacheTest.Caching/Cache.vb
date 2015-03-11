
#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.Caching


Namespace Sevansoft.CacheTest.Caching
    <DebuggerStepThrough>
    Public NotInheritable Class Cache

        Private Sub New()

        End Sub

        Public Shared Sub ClearCache()
            Dim cacheKeys As IEnumerable(Of String) = InternalGetCache().Select(Function(kvp) kvp.Key)

            For Each cacheKey As String In cacheKeys
                InternalGetCache().Remove(cacheKey)
            Next cacheKey
        End Sub

        Public Shared Function [Get](Of T As Class)(ByVal cacheKey As String,
                                                    ByVal GetItemCallBack As Func(Of T)) As T
            Dim result As T = TryCast(InternalGetCache().Get(cacheKey), T)

            If result Is Nothing Then
                result = GetItemCallBack()
                If result IsNot Nothing Then
                    InternalInsertToCache(cacheKey, result)
                End If

            End If

            Return result
        End Function


        Private Shared Sub InternalInsertToCache(ByVal Key As String, _
                                                 ByVal value As Object)
            InternalInsertToCache(Key, value, New TimeSpan(24, 0, 0), CacheItemPriority.Default)
        End Sub


        Private Shared Sub InternalInsertToCache(ByVal Key As String, _
                                                ByVal value As Object, _
                                                ByVal slidingExpiration As TimeSpan, _
                                                ByVal priority As CacheItemPriority)
            InternalInsertToCache(Key, value, ObjectCache.InfiniteAbsoluteExpiration, slidingExpiration, priority, Nothing)
        End Sub

        Private Shared Sub InternalInsertToCache(ByVal Key As String, _
                                                 ByVal value As Object, _
                                                 ByVal priority As CacheItemPriority)
            InternalInsertToCache(Key, value, ObjectCache.InfiniteAbsoluteExpiration, New TimeSpan(24, 0, 0), priority, Nothing)
        End Sub

        Private Shared Sub InternalInsertToCache(ByVal Key As String, _
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

            InternalGetCache.Add(Key, value, cacheItemPolicy)
        End Sub

        Private Shared Function InternalGetCache() As ObjectCache
            Return MemoryCache.Default
        End Function
    End Class
End Namespace