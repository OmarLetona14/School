Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.OnlineCourses.View
Imports BusinessObjects.Helpers


Namespace Modules.OnlineCourses.ViewModel
    Public Class OnlineCoursesViewModel
        Inherits ViewModelBase

        Private _courses As ObservableCollection(Of OnlineCourse)
        Private _onlinecourse As ObservableCollection(Of OnlineCourse)
        Private dataAccess As IOnlineCourseService
        Private _selected As OnlineCourse
        Private _icomButtonExitCommand As ICommand
        Private _icomButtonNewWindowCommand As ICommand
        Private _icomButtonEditCommand As ICommand
        Private _icomButtonDeleteCommand As ICommand
        Private Shadows _newWindow As AddOnlineCourse

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

        Sub actualizarLista()
            Me._onlinecourse.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOnlineCourseService)(New OnlineCourseService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOnlineCourseService)()
            ' Populate onlinecourse property variable 
            For Each element In Me.GetAllOnlineService
                Me._onlinecourse.Add(element)
            Next
        End Sub

        Sub New()
            Me._onlinecourse = New ObservableCollection(Of OnlineCourse)
            actualizarLista()
        End Sub

        Public ReadOnly Property ButtonShowNewWindow()
            Get
                If Me._icomButtonNewWindowCommand Is Nothing Then
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf CreateOnlineCourseDB)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property

        Public ReadOnly Property EditButtonCommand As ICommand
            Get
                If Me._icomButtonEditCommand Is Nothing Then
                    Me._icomButtonEditCommand = New RelayCommand(AddressOf EditOnlineCourseDB)
                End If
                Return Me._icomButtonEditCommand
            End Get
        End Property

        Public ReadOnly Property DeleteButtonCommand As ICommand
            Get
                If Me._icomButtonDeleteCommand Is Nothing Then
                    Me._icomButtonDeleteCommand = New RelayCommand(AddressOf DeleteOnlineCourseDB)
                End If
                Return Me._icomButtonDeleteCommand
            End Get
        End Property
        Public Property Selected As OnlineCourse
            Get
                Return _selected
            End Get
            Set(value As OnlineCourse)
                _selected = value
            End Set
        End Property

        Sub CreateOnlineCourseDB()
            Using Context As New SchoolEntities
                _newWindow = New AddOnlineCourse
                _newWindow.ShowDialog()
                actualizarLista()
            End Using
        End Sub

        Sub EditOnlineCourseDB()
            If Selected IsNot Nothing Then
                Using Context As New SchoolEntities
                    _newWindow = New AddOnlineCourse(Selected)
                    _newWindow.ShowDialog()
                    actualizarLista()
                End Using
            Else
                MessageBox.Show("Por favor, seleccione un curso en linea")
            End If
        End Sub

        Sub DeleteOnlineCourseDB()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.OnlineCourses.Remove((From item In DataContext.DBEntities.OnlineCourses
                                                             Where item.CourseID = Selected.CourseID
                                                             Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                actualizarLista()
            Else
                MessageBox.Show("Por favor, seleccione un curso en linea")
            End If
        End Sub

    End Class
End Namespace