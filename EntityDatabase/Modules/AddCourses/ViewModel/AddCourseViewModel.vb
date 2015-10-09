Imports BusinessLogic.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Imports Modules.AddCourses.View


Namespace Modules.AddCourses.ViewModel

    Public Class AddCourseViewModel
        Inherits ViewModelBase

        Private _course As New Course
        Public _newWindow As AddCourse
        Private _title As String
        Private _departments As ObservableCollection(Of Department)
        Private _selectedDepartment As Department
        Private _credits As Integer
        Private _aceptCommand As ICommand
        Private _ExitEditorCommand As ICommand
        Private _courseEdit As Course
        Private _IdCourse As Integer
        Private _cancelCommand As ICommand
        Private _isEnabled As Boolean

#Region "Propieties"
        Public Property Titles As String
            Get
                Return Me._title
            End Get
            Set(value As String)
                Me._title = value
                _course.Title = _title
                OnPropertyChanged("Titles")
            End Set
        End Property

        Public Property IdCourse As Integer
            Get
                Return Me._IdCourse
            End Get
            Set(value As Integer)
                Me._IdCourse = value
                _Course.CourseID = _IdCourse
                OnPropertyChanged("IdCourse")
            End Set
        End Property

        Public ReadOnly Property CreateButtonCommand As ICommand
            Get
                If Me._aceptCommand Is Nothing Then
                    Me._aceptCommand = New RelayCommand(AddressOf CreateNewCourse)
                End If
                Return Me._aceptCommand
            End Get
        End Property

        Public Property Credits As Integer
            Get
                Return Me._credits
            End Get
            Set(value As Integer)
                Me._credits = value
                _course.Credits = _credits
                OnPropertyChanged("Credits")
            End Set
        End Property

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return _departments
            End Get
            Set(value As ObservableCollection(Of Department))
                _departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        Public Property Department As Department
            Get
                Return _selectedDepartment
            End Get
            Set(value As Department)
                _Course.Department = value
                _selectedDepartment = value
                OnPropertyChanged("Department")
            End Set
        End Property
        Public Property Enabled As Boolean
            Get
                Return Me._isEnabled
            End Get
            Set(value As Boolean)
                _isEnabled = value
            End Set
        End Property
        Public ReadOnly Property ButtonExit()
            Get
                If Me._ExitEditorCommand Is Nothing Then
                    Me._ExitEditorCommand = New RelayCommand(AddressOf ExitEditor)
                End If
                Return Me._ExitEditorCommand
            End Get
        End Property
#End Region
#Region "Subs"
        Sub CreateNewCourse()
            Try
                If _courseEdit Is Nothing Then
                    DataContext.DBEntities.Courses.Add(_course)
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("El nuevo curso se ha agregado exitosamente", MsgBoxStyle.Information)
                    _newWindow.Close()
                Else
                    Dim course As Course = (From item In DataContext.DBEntities.Courses
                                            Where item.CourseID = _courseEdit.CourseID
                                            Select item).FirstOrDefault()

                    course.CourseID = _IdCourse
                    course.Title = _title
                    course.Credits = _credits
                    course.Department = _selectedDepartment
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("El curso se ha editado exitosamente", MsgBoxStyle.Information)
                    _newWindow.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("No se puede agregar el curso. Por favor asegúrese de que estén llenos todos los campos.", MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Sub ExitEditor()
            Dim respuesta As String = MsgBox("¿Está seguro que desea salir ?", MsgBoxStyle.YesNo)
            If respuesta = MsgBoxResult.Yes Then
                _newWindow.Close()
            Else
            End If
        End Sub

        Sub New(ByRef newView As AddCourse)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _departments = New ObservableCollection(Of Department)
            Dim departments As IQueryable(Of Department) = DataContext.DBEntities.Departments
            For Each element In departments
                _departments.Add(element)
            Next
            _isEnabled = True
        End Sub

        Sub New(ByRef newView As AddCourse, u As Course)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _courseEdit = u
            _departments = New ObservableCollection(Of Department)
            Dim departments As IQueryable(Of Department) = DataContext.DBEntities.Departments
            For Each element In departments
                _departments.Add(element)
            Next
            If u Is Nothing Then
                Exit Sub
            End If
            _IdCourse = _courseEdit.CourseID
            _title = _courseEdit.Title
            _credits = _courseEdit.Credits
            _selectedDepartment = _courseEdit.Department
            _isEnabled = False
        End Sub
#End Region
    End Class
End Namespace
