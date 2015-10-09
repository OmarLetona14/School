Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.OnlineCourses.View

Namespace Modules.OnlineCourses.ViewModel
    Public Class OnlineCoursesViewModel
        Inherits ViewModelBase

        Private _onlinecourse As ObservableCollection(Of OnlineCourse)
        Private dataAccess As IOnlineCourseService
        Private _icomButtonExitCommand As ICommand
        Private _icomButtonNewWindowCommand As ICommand

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

        Public ReadOnly Property ButtonShowNewWindow()
            Get
                If Me._icomButtonNewWindowCommand Is Nothing Then
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf VentanaDiag)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property
        Private Sub VentanaDiag()
            Dim ventanaNueva As New AddOnlineCourse
            ventanaNueva.Height = 350
            ventanaNueva.Width = 350
            ventanaNueva.ResizeMode = ResizeMode.NoResize
            ventanaNueva.VerticalAlignment = VerticalAlignment.Center
            ventanaNueva.HorizontalAlignment = HorizontalAlignment.Center

            ventanaNueva.ShowDialog()
        End Sub

    End Class
End Namespace