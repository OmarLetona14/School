Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.OnlineCourses.ViewModel
    Public Class OnlineCoursesViewModel
        Inherits ViewModelBase

        Private _onlinecourse As ObservableCollection(Of OnlineCourse)
        Private dataAccess As IOnlineCourseService

        Public Property OnlineCourses As ObservableCollection(Of OnlineCourse)
            Get
                Return Me._onlinecourse
            End Get
            Set(value As ObservableCollection(Of OnlineCourse))
                Me._onlinecourse = value
                OnPropertyChanged("OnlineCourses")
            End Set
        End Property

        Private Function GetAllOnlineService() As IQueryable(Of OnlineCourse)
            Return Me.dataAccess.GetAllOnlineCourses
        End Function

        Sub New()
            'Initialize property variable of onlinecourses
            Me._onlinecourse = New ObservableCollection(Of OnlineCourse)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOnlineCourseService)(New OnlineCourseService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOnlineCourseService)()
            ' Populate onlinecourse property variable 
            For Each element In Me.GetAllOnlineService
                Me._onlinecourse.Add(element)
            Next
        End Sub
    End Class
End Namespace