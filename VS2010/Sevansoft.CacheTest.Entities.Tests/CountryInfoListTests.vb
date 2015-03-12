#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.IO
Imports System.Linq

Imports NUnit.Framework

Imports Sevansoft.CacheTest.Entities

Namespace Sevansoft.CacheTest.Entities.Tests
    <TestFixture>
    Public Class CountryInfoListTests
        <Test>
        Public Sub ShouldLoadFromValidFile()
            Dim testInstance As CountryInfoList = CountryInfoList.LoadList(New FileInfo("Data/CountryInfo.xml"))

            Assert.IsNotNull(testInstance, "testInstance is invalid")

            Assert.GreaterOrEqual(testInstance.Count, 1, "testInstance.Count is invalid")

            Dim countryInfo As CountryInfo = testInstance.Where(Function(c) String.Compare(c.Code, "GBR", True) = 0).FirstOrDefault()

            Assert.IsNotNull(countryInfo, "countryInfo is invalid")
            Assert.AreEqual("GBR", countryInfo.Code, "countryInfo.Code is invalid")
            Assert.AreEqual("United Kingdom", countryInfo.Name, "countryInfo.Name is invalid")

        End Sub
    End Class
End Namespace