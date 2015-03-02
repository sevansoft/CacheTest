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
    Public Class StateInfoLists
        <Test>
        Public Sub ShouldLoadFromValidFile()
            Dim testInstance As StateInfoList = StateInfoList.LoadList(New FileInfo("Data/StateInfo.xml"), "USA")

            Assert.IsNotNull(testInstance, "testInstance is invalid")

            Assert.GreaterOrEqual(testInstance.Count, 1, "testInstance.Count is invalid")

            Dim stateInfo As StateInfo = testInstance.Where(Function(c) String.Compare(c.Code, "WY", True) = 0).FirstOrDefault()

            Assert.IsNotNull(stateInfo, "stateInfo is invalid")
            Assert.AreEqual("WY", stateInfo.Code, "stateInfo.Code is invalid")
            Assert.AreEqual("WY", stateInfo.Name, "stateInfo.Name is invalid")

        End Sub
    End Class
End Namespace