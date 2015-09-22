Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class OfficeAssignmentService
        Implements IOfficeAssignamentService
        Public Function GetAllOfficeAssignaments() As IQueryable(Of OfficeAssignment) Implements IOfficeAssignamentService.GetAllOfficeAssignaments
            Return DataContext.DBEntities.OfficeAssignments
        End Function
    End Class
End Namespace


