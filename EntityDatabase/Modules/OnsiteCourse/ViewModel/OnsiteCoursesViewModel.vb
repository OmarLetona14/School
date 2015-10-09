Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.OnsiteCourse.View

Namespace Modules.OnsiteCourse.ViewModel

    Public Class OnsiteCoursesViewModel
        Inherits ViewModelBase

        Private _onsitecourse As ObservableCollection(Of Global.OnsiteCourse)
        Private dataAccess As IOnsiteCourseService
        Private _icomButtonNewWindowCommand As ICommand


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

        Public ReadOnly Property ButtonShowNewWindow()
            Get
                If Me._icomButtonNewWindowCommand Is Nothing Then
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf VentanaDiag)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property

        Private Sub VentanaDiag()
            Dim ventanaNueva As New AddOnsiteCourse
            ventanaNueva.Height = 350
            ventanaNueva.Width = 350
            ventanaNueva.ResizeMode = ResizeMode.NoResize
            ventanaNueva.VerticalAlignment = VerticalAlignment.Center
            ventanaNueva.HorizontalAlignment = HorizontalAlignment.Center

            ventanaNueva.ShowDialog()
        End Sub

    End Class
End Namespace
