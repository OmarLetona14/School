Imports BusinessLogic.Helpers
Imports Modules.OnsiteCourse.View
Imports BusinessLogic.Services.Interfaces
Imports BusinessLogic.Services.Implementations
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel

Namespace Modules.OnsiteCourse.ViewModel
    Public Class AddOnsiteCourseViewModel
        Inherits ViewModelBase

        Public _newWindow As AddOnsiteCourse
        Private _onsiteCourse As New Global.OnsiteCourse
        Private _idCourse As Integer
        Private _selectedCourse As Course
        Private _location As String
        Private _days As String
        Private _time As Date = DateString
        Private _onsiteCourseEdit As Global.OnsiteCourse
        Private dataAccess As IOnsiteCourseService
        Private _courses As ObservableCollection(Of Course)
        Private _icomButtonAddCommand As ICommand
        Private _icomButtonExitCommand As ICommand

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return _courses
            End Get
            Set(value As ObservableCollection(Of Course))
                _courses = value
                OnPropertyChanged("Courses")
            End Set
        End Property

        Public Property SelectedCourse As Course
            Get
                Return _selectedCourse
            End Get
            Set(value As Course)
                Me._selectedCourse = value
                _onsiteCourse.Course = _selectedCourse
                OnPropertyChanged("SelectedCourse")
            End Set
        End Property

        Public Property Location As String
            Get
                Return Me._location
            End Get
            Set(value As String)
                Me._location = value
                _onsiteCourse.Location = _location
                OnPropertyChanged("Location")
            End Set
        End Property

        Public Property Days As String
            Get
                Return Me._days
            End Get
            Set(value As String)
                Me._days = value
                _onsiteCourse.Days = _days
                OnPropertyChanged("Days")
            End Set
        End Property

        Public Property Time As Date
            Get
                Return Me._time
            End Get
            Set(value As Date)
                Me._time = value
                _onsiteCourse.Time = _time
                OnPropertyChanged("Time")
            End Set
        End Property
        Public ReadOnly Property ButtonCreateNewOnsiteCouse()
            Get
                If Me._icomButtonAddCommand Is Nothing Then
                    Me._icomButtonAddCommand = New RelayCommand(AddressOf CreateOnsiteCourse)
                End If
                Return Me._icomButtonAddCommand
            End Get
        End Property

        Public ReadOnly Property ButtonExit()
            Get
                If Me._icomButtonExitCommand Is Nothing Then
                    Me._icomButtonExitCommand = New RelayCommand(AddressOf ExitEditor)
                End If
                Return Me._icomButtonExitCommand
            End Get
        End Property

        Public Sub ExitEditor()
            Dim respuesta As String = MsgBox("¿Está seguro que desea salir ?", MsgBoxStyle.YesNo)
            If respuesta = MsgBoxResult.Yes Then
                _newWindow.Close()
            Else
            End If
        End Sub


        Private Sub CreateOnsiteCourse()
            Try
                If _onsiteCourseEdit Is Nothing Then
                    DataContext.DBEntities.OnsiteCourses.Add(_onsiteCourse)
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("El nuevo curso se ha agregado exitosamente", MsgBoxStyle.Information)
                    _newWindow.Close()
                Else
                    Dim onCourse As Global.OnsiteCourse = (From item In DataContext.DBEntities.OnsiteCourses
                                                           Where item.CourseID = _onsiteCourseEdit.CourseID
                                                           Select item).FirstOrDefault()

                    onCourse.Location = _location
                    onCourse.Days = _days
                    onCourse.Time = _time
                    onCourse.Course = _selectedCourse
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("El curso se ha editado exitosamente", MsgBoxStyle.Information)
                    _newWindow.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("No se puede agregar el curso. Por favor asegúrese de que estén llenos todos los campos.", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub New(ByRef newView As AddOnsiteCourse)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _courses.Add(element)
            Next
        End Sub

        Sub New(ByRef newView As AddOnsiteCourse, u As Global.OnsiteCourse)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _onsiteCourseEdit = u
            _courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _courses.Add(element)
            Next
            If u Is Nothing Then
                Exit Sub
            End If
            _location = _onsiteCourseEdit.Location
            _days = _onsiteCourseEdit.Days
            _time = _onsiteCourseEdit.Time
            _selectedCourse = _onsiteCourseEdit.Course
        End Sub


    End Class
End Namespace
