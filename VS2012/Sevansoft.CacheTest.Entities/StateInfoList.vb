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
    Public Class StateInfoList
        Inherits List(Of StateInfo)

        Public Shared Function LoadList(ByVal path As FileSystemInfo, ByVal countryCode As String) As StateInfoList
            Dim result As StateInfoList = New StateInfoList()

            If path IsNot Nothing AndAlso path.Exists Then
                Dim document As XmlDocument = New XmlDocument()

                document.Load(path.FullName)

                Dim iterator As XPathNodeIterator = document.CreateNavigator.Select(String.Format("data/options[@countryCode='{0}']/option", countryCode))

                Do While iterator.MoveNext
                    result.Add(New StateInfo(DirectCast(iterator.Current, IHasXmlNode).GetNode))
                Loop
            End If

            Return result
        End Function
    End Class
End Namespace