#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Diagnostics
Imports System.Runtime.Caching

Imports Sevansoft.CacheTest.BusinessTier
Imports Sevansoft.CacheTest.Caching
Imports Sevansoft.CacheTest.Interfaces

Namespace Sevansoft.CacheTest.ServiceTier
    <DebuggerStepThrough> _
    Public MustInherit Class ServiceBase
        Implements IServiceBase

        Public Sub ClearCache() Implements IServiceBase.ClearCache
            Cache.ClearCache()
        End Sub
    End Class
End Namespace