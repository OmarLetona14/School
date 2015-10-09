Imports BusinessLogic.Helpers
Imports Modules.OnlineCourses.View
Imports BusinessLogic.Services.Interfaces
Imports BusinessLogic.Services.Implementations
Imports BusinessObjects.Helpers
Imports System.Collections.ObjectModel

Namespace Modules.OnlineCourses.ViewModel

    Public Class AddOnlineCourseViewModel
        Inherits ViewModelBase

        Private _newView As AddOnlineCourse
        Private _onlineCourse As New OnlineCourse
        Private onlineEdit As OnlineCourse
        Private _courses As ObservableCollection(Of Course)
        Private _selectedCourse As Course
        Private _url As String
        Private aceptCommand As ICommand
        Private cancelCommand As ICommand

        Public Property URL As String
            Get
                Return Me._url
            End Get
            Set(value As String)
                Me._url = value
                _onlineCourse.URL = _url
                OnPropertyChanged("URL")
            End Set
        End Property

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
                _onlineCourse.Course = _selectedCourse
                OnPropertyChanged("SelectedCourse")
            End Set
        End Property

        Public ReadOnly Property CreateOnlineCourseButtonCommand As ICommand
            Get
                If Me.aceptCommand Is Nothing Then
                    Me.aceptCommand = New RelayCommand(AddressOf CreateNewOnlineCourse)
                End If
                Return Me.aceptCommand
            End Get
        End Property

        Public ReadOnly Property ExitEditorButtonCommand As ICommand
            Get
                If Me.cancelCommand Is Nothing Then
                    Me.cancelCommand = New RelayCommand(AddressOf ExitEditor)
                End If
                Return Me.cancelCommand
            End Get
        End Property

        Sub New(ByRef newView As AddOnlineCourse)
            Me._newView = newView
            _newView.Height = 350
            _newView.Width = 350
            _courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _courses.Add(element)
            Next
        End Sub

        Sub New(ByRef newView As AddOnlineCourse, onlineCourse As OnlineCourse)
            Me._newView = newView
            _newView.Height = 350
            _newView.Width = 350
            onlineEdit = onlineCourse
            _courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _courses.Add(element)
            Next
            If onlineCourse Is Nothing Then
                Exit Sub
            End If
            _selectedCourse = onlineEdit.Course
            _url = onlineEdit.URL
        End Sub

        Sub CreateNewOnlineCourse()
            Try
                If onlineEdit Is Nothing Then
                    DataContext.DBEntities.OnlineCourses.Add(_onlineCourse)
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                Else
                    Dim Online As OnlineCourse = (From item In DataContext.DBEntities.OnlineCourses Where item.CourseID = onlineEdit.CourseID
                                                  Select item).FirstOrDefault()
                    Online.Course = _selectedCourse
                    Online.URL = _url
                    DataContext.DBEntities.SaveChanges()
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("No se puede agregar el curso en linea", MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Sub ExitEditor()
            Dim respuesta As String = MsgBox("¿Está seguro que desea salir?", MsgBoxStyle.YesNo)
            If respuesta = MsgBoxResult.Yes Then
                _newView.Close()
            Else
            End If
        End Sub

    End Class
End Namespace