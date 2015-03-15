#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Runtime.CompilerServices
Imports System.Xml

Imports Microsoft.VisualBasic


Namespace System.Xml
    <HideModuleName>
    Public Module XmlNodeExtensions
        <Extension>
        Public Function ReadAttribute(Of T As Class)(ByVal node As XmlNode, ByVal name As String) As T
            Dim result As Object = Nothing

            If node IsNot Nothing AndAlso Not String.IsNullOrEmpty(name) Then
                Dim attribute As XmlAttribute = node.Attributes(name)
                If attribute IsNot Nothing Then
                    result = attribute.Value
                End If
            End If

            Return CType(result, T)
        End Function

        <Extension>
        Public Function ReadElement(Of T As Class)(ByVal node As XmlNode) As T
            Dim result As Object = Nothing

            If node IsNot Nothing Then
                result = node.InnerText
            End If

            Return CType(result, T)
        End Function

    End Module
End Namespace