Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.Courses.ViewModel

    Public Class CoursesViewModel
        Inherits ViewModelBase

        Private _courses As ObservableCollection(Of Course)
        Private dataAccess As ICourseService

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return Me._courses
            End Get
            Set(value As ObservableCollection(Of Course))
                Me._courses = value
                OnPropertyChanged("Course")
            End Set
        End Property

        ' Function to get all courses from service
        Private Function GetAllCourses() As IQueryable(Of Course)
            Return Me.dataAccess.GetAllCourses
        End Function

        Sub New()
            'Initialize property variable of departments
            Me._courses = New ObservableCollection(Of Course)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of ICourseService)(New CourseService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of ICourseService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllCourses
                Me._courses.Add(element)
            Next
        End Sub
    End Class
End Namespace