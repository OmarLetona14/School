Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Namespace BusinessLogic.Services.Implementations
    Public Class OnlineCourseService
        Implements IOnlineCourseService

        Public Function GetAllOnlineCourses() As IQueryable(Of OnlineCourse) Implements IOnlineCourseService.GetAllOnlineCourses
            Return DataContext.DBEntities.OnlineCourses
        End Function
    End Class
End Namespace