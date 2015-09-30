Class Application
    Protected Overrides Sub OnStartup(ByVal e As System.Windows.StartupEventArgs)
        MyBase.OnStartup(e)
        Dim boostrapper As Bootstrapper = New Bootstrapper
        boostrapper.Run()
    End Sub
End Class
