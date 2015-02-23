#Region "Header Block"
Option Explicit On
Option Strict On
#End Region

Imports System
Imports System.Diagnostics
Imports System.Reflection
Imports System.ServiceModel

Imports Sevansoft.CacheTest.ServiceTier

Namespace Sevansoft.CacheTest.Client.ConsoleApp
    Public NotInheritable Class App
        Private Sub New()

        End Sub

        Public Shared Sub Main(ByVal args As String())

            DumpAssemblyInfo()

            Try
                Test01()
            Catch ex As Exception
                DumpException(ex)
            End Try

            Console.WriteLine()
            Console.WriteLine("Press Any Key to continue...")
            Console.ReadKey(True)
        End Sub

        Private Shared Sub Test01()
            Dim service As ProductClient = New ProductClient()
            For counter As Integer = 1 To 10
                Console.WriteLine("Starting test cycle {0}", counter)
                Dim stopWatch As Stopwatch = New Stopwatch()

                stopWatch = stopWatch.StartNew()
                service = New ProductClient()
                stopWatch.Stop()
                Console.WriteLine("New ProductClient() took {0} milliseconds", stopWatch.ElapsedMilliseconds)

                If counter = 5 Then
                    stopWatch = stopWatch.StartNew()
                    service.ClearCache()
                    stopWatch.Stop()
                    Console.WriteLine("ClearCache {0} milliseconds", stopWatch.ElapsedMilliseconds)
                End If

                stopWatch = stopWatch.StartNew()
                Dim productList As ProductList = service.GetProducts()
                stopWatch.Stop()
                Console.WriteLine("service.GetProducts() took {0} milliseconds", stopWatch.ElapsedMilliseconds)

                stopWatch = stopWatch.StartNew()
                productList = service.GetProducts()
                stopWatch.Stop()
                Console.WriteLine("service.GetProducts() took {0} milliseconds", stopWatch.ElapsedMilliseconds)

                service.CloseProxy()
                Console.WriteLine("Finished test cycle {0}", counter)
                Console.WriteLine()
            Next counter

        End Sub

        <DebuggerStepThrough()> _
        Private Shared Sub DumpAssemblyInfo()
            Dim assembly As Assembly = assembly.GetExecutingAssembly()
            Dim version As Version = assembly.GetName().Version
            'NAnt 0.85 (Build 0.85.2478.0; release; 14/10/2006)
            'Copyright (C) 2001-2006 Gerry Shaw
            Console.WriteLine("{0} {1} (Build {2};)", _
                                assembly.GetName().Name, _
                                version.ToString(2), _
                                version)

            Console.WriteLine(GetCopyright(assembly))

            Console.WriteLine("http://www.sevansoft.co.uk")
            Console.WriteLine()
            Return
        End Sub

        <DebuggerStepThrough()> _
        Private Shared Sub DumpException(ByVal exception As Exception)
            Dim localException As Exception = exception
            Dim originalColor As ConsoleColor = Console.ForegroundColor
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine()
            Console.WriteLine("***** Exception ***** Exception ***** Exception ***** Exception ***** Exception *****")
            Console.WriteLine()
            While localException IsNot Nothing
                Console.WriteLine("(Exception {0})", localException.[GetType]().FullName)
                Console.WriteLine(" {0}", localException.Message.Replace(Environment.NewLine, " "))
                Console.WriteLine()
                localException = localException.InnerException
            End While
            Console.WriteLine("(Stack Trace)")
            Console.WriteLine(exception.StackTrace)
            Console.WriteLine()
            Console.WriteLine(exception.ToString())
            Console.WriteLine("***** Exception ***** Exception ***** Exception ***** Exception ***** Exception *****")
            Console.WriteLine()
            Console.ForegroundColor = originalColor
            Return
        End Sub

        Private Shared ReadOnly Property GetCopyright(ByVal assembly As Assembly) As String
            <DebuggerStepThrough()> _
            Get
                Return CType(assembly.GetCustomAttributes(GetType(AssemblyCopyrightAttribute), False)(0), AssemblyCopyrightAttribute).Copyright
            End Get
        End Property
    End Class
End Namespace