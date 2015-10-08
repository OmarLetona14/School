Imports System.Collections.ObjectModel
Imports BusinessLogic.Helpers
Imports Modules.StudentGrade.View
Imports BusinessObjects.Helpers


Namespace Modules.StudentGrade.ViewModel

    Public Class AddStudentGradeViewModel
        Inherits ViewModelBase

        Private _courses As ObservableCollection(Of Course)
        Private _persons As ObservableCollection(Of Person)
        Private _studenteGrade As New Global.StudentGrade
        Private _student As Person
        Private _idEnrollment As Integer
        Private _grade As String
        Private _selectedCourse As Course
        Public _newWindow As AddStudentGrade
        Private _studentGradeToEdit As Global.StudentGrade
        Private _icomButtonAddCommand As ICommand
        Private _icomButtonExitCommand As ICommand

        Public Property idEnrollment As Integer
            Get
                Return _idEnrollment
            End Get
            Set(value As Integer)
                Me._idEnrollment = value
                _studenteGrade.Course = _selectedCourse
                OnPropertyChanged("idEnrollment")
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
                _studenteGrade.Course = _selectedCourse
                OnPropertyChanged("SelectedCourse")
            End Set
        End Property


        Public Property Persons As ObservableCollection(Of Person)
            Get
                Return _persons
            End Get
            Set(value As ObservableCollection(Of Person))
                _persons = value
                OnPropertyChanged("Persons")
            End Set
        End Property

        Public Property SelectedStudent As Person
            Get
                Return Me._student
            End Get
            Set(value As Person)
                Me._student = value
                _studenteGrade.Person = _student
                OnPropertyChanged("SelectedStudent")
            End Set
        End Property
        Public Property Grade As Decimal
            Get
                Return Me._grade
            End Get
            Set(value As Decimal)
                Me._grade = value
                _studenteGrade.Grade = _grade
                OnPropertyChanged("Grade")
            End Set
        End Property

        Public ReadOnly Property ButtonCreateNewStudentGrade()
            Get
                If Me._icomButtonAddCommand Is Nothing Then
                    Me._icomButtonAddCommand = New RelayCommand(AddressOf CreateStudentGrade)
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


        Private Sub CreateStudentGrade()
            Try
                If _studentGradeToEdit Is Nothing Then
                    DataContext.DBEntities.StudentGrades.Add(_studenteGrade)
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("El nuevo student grade se ha agregado exitosamente", MsgBoxStyle.Information)
                    _newWindow.Close()
                Else
                    Dim studentGrade As Global.StudentGrade = (From item In DataContext.DBEntities.StudentGrades
                                                               Where item.StudentID = _studentGradeToEdit.StudentID
                                                               Select item).FirstOrDefault()

                    studentGrade.EnrollmentID = _idEnrollment
                    studentGrade.Course = _selectedCourse
                    studentGrade.Person = _student
                    studentGrade.Grade = _grade

                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("El student grade se ha editado exitosamente", MsgBoxStyle.Information)
                    _newWindow.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("No se puede agregar el student grade. Por favor asegúrese de que estén llenos todos los campos.", MsgBoxStyle.Critical)
            End Try
        End Sub
        Sub New(ByRef newView As AddStudentGrade)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _courses.Add(element)
            Next
            _persons = New ObservableCollection(Of Person)
            Dim Persons As IQueryable(Of Person) = DataContext.DBEntities.People
            For Each element In Persons
                _persons.Add(element)
            Next
        End Sub
        Sub New(ByRef newView As AddStudentGrade, u As Global.StudentGrade)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _studentGradeToEdit = u
            _courses = New ObservableCollection(Of Course)
            Dim Courses As IQueryable(Of Course) = DataContext.DBEntities.Courses
            For Each element In Courses
                _courses.Add(element)
            Next
            _persons = New ObservableCollection(Of Person)
            Dim Persons As IQueryable(Of Person) = DataContext.DBEntities.People
            For Each element In Persons
                _persons.Add(element)
            Next
            If u Is Nothing Then
                Exit Sub
            End If
            _idEnrollment = _studentGradeToEdit.EnrollmentID
            _selectedCourse = _studentGradeToEdit.Course
            _student = _studentGradeToEdit.Person
            _grade = _studentGradeToEdit.Grade
        End Sub









    End Class
End Namespace
