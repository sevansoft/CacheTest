#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace Sevansoft.CacheTest.Entities
    <Serializable>
    <CollectionDataContract>
    Public Class ProductList
        Inherits List(Of Product)
    End Class
End Namespace