Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Prism
Imports Microsoft.Practices.Prism.Logging
Imports Microsoft.Practices.ServiceLocation
Imports Microsoft.Practices.Prism.UnityExtensions
Imports Microsoft.Practices.Prism.Modularity

Imports Modules
Imports Modules.Courses
Public Class Bootstrapper
    Inherits UnityBootstrapper

    Protected Overrides Function CreateShell() As DependencyObject
        Dim shell = Me.Container.Resolve(Of MainWindow)()
        shell.Show()
        Return shell

    End Function

    Protected Overrides Sub InitializeModules()
        Dim userLoginModule As IModule = Me.Container.Resolve(Of CourseModule)()
        userLoginModule.Initialize()
    End Sub

End Class
