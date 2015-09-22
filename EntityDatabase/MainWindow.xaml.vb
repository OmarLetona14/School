Imports Modules.Departments.ViewModels
Imports Modules.Courses.ViewModel
Class MainWindow
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.CoursesUserControl.MainGrid.DataContext = New CoursesViewModel()
    End Sub
End Class
