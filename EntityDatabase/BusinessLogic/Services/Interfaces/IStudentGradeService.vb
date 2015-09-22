Namespace BusinessLogic.Services.Interfaces
    Public Interface IStudentGradeService
        Function GetAllStudentGrades() As IQueryable(Of StudentGrade)
    End Interface
End Namespace