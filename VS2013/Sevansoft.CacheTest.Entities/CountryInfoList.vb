#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Xml.XPath

Namespace Sevansoft.CacheTest.Entities
    <Serializable>
    <CollectionDataContract>
    Public Class CountryInfoList
        Inherits List(Of CountryInfo)

        Public Sub New()
            Me.Date = DateTime.Now
        End Sub

        Public Shared Function LoadList(ByVal path As FileSystemInfo) As CountryInfoList
            Dim result As CountryInfoList = New CountryInfoList()

            If path IsNot Nothing AndAlso path.Exists Then
                Dim document As XmlDocument = New XmlDocument()

                document.Load(path.FullName)

                Dim iterator As XPathNodeIterator = document.CreateNavigator.Select("options/option")

                Do While iterator.MoveNext
                    result.Add(New CountryInfo(DirectCast(iterator.Current, IHasXmlNode).GetNode))
                Loop
            End If

            Return result
        End Function

        <DataMember>
        Public Property [Date] As DateTime

    End Class

End Namespace