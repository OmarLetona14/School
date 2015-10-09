Imports Modules.OnlineCourses.ViewModel

Namespace Modules.OnlineCourses.View

    Public Class AddOnlineCourse
        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.Grid.DataContext = New AddOnlineCourseViewModel(Me)
        End Sub

        Sub New(ByVal Online As OnlineCourse)
            InitializeComponent()
            Me.Grid.DataContext = New AddOnlineCourseViewModel(Me, Online)
        End Sub
    End Class
End Namespace