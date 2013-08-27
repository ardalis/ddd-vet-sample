Imports HibernatingRhinos.Profiler.Appender.EntityFramework

<assembly: WebActivator.PreApplicationStartMethod(GetType(Global.$rootnamespace$.App_Start.EntityFrameworkProfilerBootstrapper), "PreStart")>
Namespace App_Start
	Public Class EntityFrameworkProfilerBootstrapper
        Public Shared Sub PreStart()
            ' Initialize the profiler
			EntityFrameworkProfiler.Initialize()

            ' You can also use the profiler in an offline manner.
            ' This will generate a file with a snapshot of all the EntityFramework activity in the application,
            ' which you can use for later analysis by loading the file into the profiler.
            ' Dim FileName as String = @"c:\profiler-log";
            ' EntityFrameworkProfiler.InitializeOfflineProfiling(FileName)
        End Sub
    End Class
End Namespace

