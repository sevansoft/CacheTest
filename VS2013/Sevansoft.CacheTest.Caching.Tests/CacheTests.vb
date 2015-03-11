#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Threading

Imports NUnit.Framework

Imports Sevansoft.CacheTest.Caching

Namespace Sevansoft.CacheTest.Caching.Tests
    <TestFixture>
    Public Class CacheTests
        <Test>
        Public Sub ShouldCacheItem()
            Dim value As DateTime = DateTime.Now
            Thread.Sleep(1000)

            Dim firstItem As CachedItem = Cache.Get(Of CachedItem)("cacheKey", Function() GetTestCacheItem(value))

            Thread.Sleep(1000)
            Dim secondItem As CachedItem = Cache.Get(Of CachedItem)("cacheKey", Function() GetTestCacheItem(DateTime.Now))

            Assert.AreEqual(value, firstItem.Value, "firstItem.Value is invalid")

            Assert.AreEqual(firstItem.Text, secondItem.Text, "secondItem.Text is invalid")
        End Sub

        Private Function GetTestCacheItem() As CachedItem
            Return GetTestCacheItem(DateTime.Now)
        End Function

        Private Function GetTestCacheItem(ByVal value As DateTime) As CachedItem
            Return New CachedItem() With {.Text = Guid.NewGuid.ToString(), .Value = value}
        End Function

        Private Class CachedItem
            Public Value As DateTime
            Public Text As String
        End Class
    End Class
End Namespace
