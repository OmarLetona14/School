Namespace BusinessLogic.Services.Interfaces
    Public Interface IOnlineCourseService
        Function GetAllOnlineCourses() As IQueryable(Of OnlineCourse)
    End Interface
End Namespace