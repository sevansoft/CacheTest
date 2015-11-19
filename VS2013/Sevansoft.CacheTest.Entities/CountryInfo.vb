#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Diagnostics
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.Xml

Namespace Sevansoft.CacheTest.Entities
    <DebuggerStepThrough>
    <DataContract>
    Public Class CountryInfo
        Public Sub New(ByVal node As XmlNode)
            Code = node.ReadAttribute(Of String)("value")
            Name = node.ReadElement(Of String)()
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