#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Diagnostics
Imports System.Runtime.Serialization
Imports System.ServiceModel

Namespace Sevansoft.CacheTest.Entities
    <DebuggerStepThrough()> _
    <DataContract()> _
    Public Class Product

        Private _code As String
        <DataMember()> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property

        Private _Name As String
        <DataMember()> _
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property
    End Class
End Namespace