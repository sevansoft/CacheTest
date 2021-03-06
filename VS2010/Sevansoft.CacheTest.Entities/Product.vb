﻿#Region "Header Block"
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

        <DataMember()> _
        Public Property Code() As String

        <DataMember()> _
        Public Property Name() As String
    End Class
End Namespace