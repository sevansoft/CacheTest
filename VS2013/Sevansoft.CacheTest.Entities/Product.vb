#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Diagnostics
Imports System.Runtime.Serialization
Imports System.ServiceModel

Namespace Sevansoft.CacheTest.Entities
    <DebuggerStepThrough>
    <DataContract>
    Public Class Product
        Public Sub New()
            [Date] = DateTime.Now
        End Sub

        <DataMember>
        Public Property Code() As String

        <DataMember>
        Public Property Name() As String

        <DataMember>
        Public Property [Date] As DateTime
    End Class
End Namespace