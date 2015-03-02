
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
            LogEvent("Clearing cache")
            For Each cacheKey As String In cacheKeys
                InternalGetCache().Remove(cacheKey)
            Next cacheKey
            LogEvent("Cleared cache")
        End Sub

        Public Shared Function [Get](Of T As Class)(ByVal cacheKey As String,
                                                    ByVal GetItemCallBack As Func(Of T)) As T
            Return [Get](cacheKey, GetItemCallBack, New TimeSpan(24, 0, 0))
        End Function

        Public Shared Function [Get](Of T As Class)(ByVal cacheKey As String,
                                                    ByVal GetItemCallBack As Func(Of T),
                                                    ByVal slidingExpiration As TimeSpan) As T
            Dim result As T = TryCast(InternalGetCache().Get(cacheKey), T)

            If result Is Nothing Then
                LogEvent("Item key {0} is not cached", cacheKey)
                result = GetItemCallBack()
                If result IsNot Nothing Then
                    InternalInsertToCache(cacheKey, result, slidingExpiration)
                End If
            Else
                LogEvent("Item key {0} is cached", cacheKey)
            End If

            Return result
        End Function

        Private Shared Sub InternalInsertToCache(ByVal Key As String,
                                                 ByVal value As Object)
            InternalInsertToCache(Key, value, New TimeSpan(24, 0, 0))
        End Sub

        Private Shared Sub InternalInsertToCache(ByVal Key As String,
                                                 ByVal value As Object,
                                                 ByVal slidingExpiration As TimeSpan)
            InternalInsertToCache(Key, value, slidingExpiration, CacheItemPriority.Default)
        End Sub

        Private Shared Sub InternalInsertToCache(ByVal Key As String,
                                                 ByVal value As Object,
                                                 ByVal slidingExpiration As TimeSpan,
                                                 ByVal priority As CacheItemPriority)
            InternalInsertToCache(Key, value, ObjectCache.InfiniteAbsoluteExpiration, slidingExpiration, priority, Nothing)
        End Sub

        Private Shared Sub InternalInsertToCache(ByVal Key As String,
                                                 ByVal value As Object,
                                                 ByVal absoluteExpiration As DateTimeOffset,
                                                 ByVal slidingExpiration As TimeSpan,
                                                 ByVal priority As CacheItemPriority,
                                                 ByVal onRemoveCallback As CacheEntryRemovedCallback)

            Dim cacheItemPolicy As CacheItemPolicy = New CacheItemPolicy()

            cacheItemPolicy.AbsoluteExpiration = absoluteExpiration
            cacheItemPolicy.Priority = priority
            cacheItemPolicy.SlidingExpiration = slidingExpiration
            cacheItemPolicy.RemovedCallback = onRemoveCallback

            InternalGetCache.Add(Key, value, cacheItemPolicy)
            LogEvent("Add item key {0} to cache", Key)
        End Sub

        Private Shared Function InternalGetCache() As ObjectCache
            Return MemoryCache.Default
        End Function

        <Conditional("DEBUG")>
        Private Shared Sub LogEvent(ByVal format As String, ByVal ParamArray args As Object())
            If Not EventLog.SourceExists("Sevansoft.CacheTest.Caching") Then
                EventLog.CreateEventSource("Sevansoft.CacheTest.Caching", "Application")
            End If
            Dim log As EventLog = New EventLog("Application", Environment.MachineName, "Sevansoft.CacheTest.Caching")
            Dim eventMessage As String = format
            If args IsNot Nothing Then
                eventMessage = String.Format(format, args)
            End If

            log.WriteEntry(eventMessage, EventLogEntryType.Information)
            Console.WriteLine(eventMessage)
        End Sub
    End Class
End Namespace