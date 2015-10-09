Imports Modules.OfficeAssignament.ViewModel
Namespace Modules.OfficeAssignament.View

    Public Class AddOfficeAssignment
        Sub New()

            ' Llamada necesaria para el diseñador.
            InitializeComponent()

            ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
            Me.Grid.DataContext = New AddOfficeAssignmentViewModel(Me)
        End Sub

        Sub New(ByVal Office As OfficeAssignment)
            InitializeComponent()
            Me.Grid.DataContext = New AddOfficeAssignmentViewModel(Me, Office)
        End Sub

    End Class
End Namespace