Imports Modules.Departments.ViewModels
Namespace Modules.Departments.Views
    Public Class AddDepartment
        Sub New()

            ' Esta llamada es exigida por el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.Grid.DataContext = New AddDepartmentsViewModel(Me)
        End Sub
        Sub New(ByVal Department As Department)

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.Grid.DataContext = New AddDepartmentsViewModel(Me, Department)
        End Sub
    End Class
End Namespace