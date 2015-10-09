Imports Modules.OfficeAssignament.ViewModel

Namespace Modules.OfficeAssignament.View
    Public Class OfficeAssignmentsList
        Public Sub New()
            InitializeComponent()
            Me.DataContext = New AddOfficeAssignmentViewModel()
        End Sub

        Public ReadOnly Property ViewModel As AddOfficeAssignmentViewModel
            Get
                Return DirectCast(Me.DataContext, AddOfficeAssignmentViewModel)
            End Get
        End Property
    End Class
End Namespace