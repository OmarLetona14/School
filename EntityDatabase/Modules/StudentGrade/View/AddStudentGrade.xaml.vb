Imports Modules.StudentGrade.ViewModel
Namespace Modules.StudentGrade.View
    Public Class AddStudentGrade
        Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Grid.DataContext = New AddStudentGradeViewModel(Me)
        End Sub

        Public Sub New(ByVal studentGrade As Global.StudentGrade)
            InitializeComponent()
            Me.Grid.DataContext = New AddStudentGradeViewModel(Me, studentGrade)
        End Sub
    End Class
End Namespace