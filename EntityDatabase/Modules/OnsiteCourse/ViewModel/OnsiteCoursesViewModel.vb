Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.OnsiteCourse.View
Imports BusinessObjects.Helpers

Namespace Modules.OnsiteCourse.ViewModel

    Public Class OnsiteCoursesViewModel
        Inherits ViewModelBase

        Private _onsitecourse As ObservableCollection(Of Global.OnsiteCourse)
        Private _selectedRow As Global.OnsiteCourse
        Public Shadows _newWindow As AddOnsiteCourse
        Private dataAccess As IOnsiteCourseService
        Private _icomButtonNewWindowCommand As ICommand
        Private _icomButtonDeleteCommand As ICommand
        Private _icomButtonEditCommand As ICommand


        Public Property OnsiteCourses As ObservableCollection(Of Global.OnsiteCourse)
            Get
                Return Me._onsitecourse
            End Get
            Set(value As ObservableCollection(Of Global.OnsiteCourse))
                Me._onsitecourse = value
                OnPropertyChanged("OnsiteCourses")
            End Set
        End Property

        Public Property DeleteButton As ICommand
            Get
                If _icomButtonDeleteCommand Is Nothing Then
                    _icomButtonDeleteCommand = New RelayCommand(AddressOf Delete)
                End If
                Return _icomButtonDeleteCommand
            End Get
            Set(value As ICommand)
                _icomButtonDeleteCommand = value
            End Set
        End Property

        Public ReadOnly Property EditButtonCommand As ICommand
            Get
                If Me._icomButtonEditCommand Is Nothing Then
                    Me._icomButtonEditCommand = New RelayCommand(AddressOf EditCourse)
                End If
                Return Me._icomButtonEditCommand
            End Get
        End Property

        Sub EditCourse()
            If SelectedCourse IsNot Nothing Then
                Using context As New SchoolEntities
                    _newWindow = New AddOnsiteCourse(SelectedCourse)
                    _newWindow.ShowDialog()
                    actualizarLista()
                End Using
            Else
                MessageBox.Show("Por favor, seleccione un curso")
            End If
        End Sub
        Sub Delete()
            Try
                If SelectedCourse IsNot Nothing Then
                    DataContext.DBEntities.OnsiteCourses.Remove((From c In DataContext.DBEntities.OnsiteCourses
                                                                 Where c.CourseID = SelectedCourse.CourseID
                                                                 Select c).FirstOrDefault)
                    DataContext.DBEntities.SaveChanges()
                    actualizarLista()
                Else
                    MessageBox.Show("Debe seleccionar un curso")
                End If
            Catch ex As Exception

            End Try

        End Sub
        Sub New()
            Me._onsitecourse = New ObservableCollection(Of Global.OnsiteCourse)
            actualizarLista()
        End Sub


        ' Function to get all onsitecourses from service
        Private Function GetAllOnsiteCourses() As IQueryable(Of Global.OnsiteCourse)
            Return Me.dataAccess.GetAllOnsiteCourses
        End Function

        Sub actualizarLista()
            _onsitecourse.Clear()
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
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf AddOnsiteCourseToDB)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property
        Public Property SelectedCourse As Global.OnsiteCourse
            Get
                Return _selectedRow
            End Get
            Set(value As Global.OnsiteCourse)
                _selectedRow = value
                OnPropertyChanged("SelectedCourse")
            End Set
        End Property
        Sub AddOnsiteCourseToDB()
            Using school As New SchoolEntities
                _newWindow = New AddOnsiteCourse
                _newWindow.ShowDialog()
                actualizarLista()
            End Using
        End Sub
    End Class
End Namespace
