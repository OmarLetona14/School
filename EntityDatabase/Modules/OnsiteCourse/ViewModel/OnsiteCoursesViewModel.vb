Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.OnsiteCourse.ViewModel

    Public Class OnsiteCoursesViewModel
        Inherits ViewModelBase

        Private _onsitecourse As ObservableCollection(Of Global.OnsiteCourse)
        Private dataAccess As IOnsiteCourseService


        Public Property OnsiteCourses As ObservableCollection(Of Global.OnsiteCourse)
            Get
                Return Me._onsitecourse
            End Get
            Set(value As ObservableCollection(Of Global.OnsiteCourse))
                Me._onsitecourse = value
                OnPropertyChanged("OnsiteCourses")
            End Set
        End Property

        ' Function to get all onsitecourses from service
        Private Function GetAllOnsiteCourses() As IQueryable(Of Global.OnsiteCourse)
            Return Me.dataAccess.GetAllOnsiteCourses
        End Function

        Sub New()
            'Initialize property variable of departments
            Me._onsitecourse = New ObservableCollection(Of Global.OnsiteCourse)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOnsiteCourseService)(New OnsiteCourseService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOnsiteCourseService)()
            ' Populate onsitecourses property variable 
            For Each element In Me.GetAllOnsiteCourses
                Me._onsitecourse.Add(element)
            Next
        End Sub
    End Class
End Namespace
