#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Runtime.CompilerServices
Imports System.ServiceModel

Imports Microsoft.VisualBasic

Namespace System.ServiceModel
    <HideModuleName()> _
    Public Module ServiceClientExtensions
        <Extension()> _
        Public Sub CloseProxy(ByVal instance As ICommunicationObject)
            If instance IsNot Nothing Then
                Try
                    If instance.State = CommunicationState.Opened Then
                        ' may throw exception while closing
                        instance.Close()
                    Else
                        instance.Abort()
                    End If
                Catch generatedExceptionName As CommunicationException
                    instance.Abort()
                    Throw

                End Try
            End If
        End Sub
    End Module
End Namespace