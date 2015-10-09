

Imports Modules.AddCourses.ViewModel
Namespace Modules.AddCourses.View

    Public Class AddCourse

        Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Grid.DataContext = New AddCourseViewModel(Me)
        End Sub

        Public Sub New(ByVal course As Course)
            InitializeComponent()
            Me.Grid.DataContext = New AddCourseViewModel(Me, course)
        End Sub
    End Class
End Namespace