Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers
Imports Modules.AddCourses.View

Namespace Modules.Courses.ViewModel

    Public Class CoursesViewModel
        Inherits ViewModelBase


        Private _courses As ObservableCollection(Of Course)
        Private dataAccess As ICourseService
        Private _delete As ICommand
        Private _insert As ICommand
        Private _selected As Course
        Private _selectedRow As Course
        Private _icomButtonExitCommand As ICommand
        Public Shadows newWindow As AddCourse
        Private _icomButtonNewWindowCommand As ICommand
        Private _icomButtonEditCommand As ICommand

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return Me._courses
            End Get
            Set(value As ObservableCollection(Of Course))
                Me._courses = value
                OnPropertyChanged("Course")
            End Set
        End Property
        Public Property DeleteCommand As ICommand
            Get
                If _delete Is Nothing Then
                    _delete = New RelayCommand(AddressOf DeleteDB)
                End If
                Return _delete
            End Get
            Set(value As ICommand)
                _delete = value
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
                    newWindow = New AddCourse(SelectedCourse)
                    newWindow.ShowDialog()
                    refresh()
                End Using
            Else
                MessageBox.Show("Por favor, seleccione un curso")
            End If
        End Sub
        Sub DeleteDB()
            Try
                If SelectedCourse IsNot Nothing Then
                    DataContext.DBEntities.Courses.Remove((From c In DataContext.DBEntities.Courses
                                                           Where c.CourseID = SelectedCourse.CourseID
                                                           Select c).FirstOrDefault)
                    DataContext.DBEntities.SaveChanges()
                    refresh()
                Else
                    MessageBox.Show("Debe seleccionar un curso")
                End If
            Catch ex As Exception

            End Try

        End Sub

        Sub refresh()
            Me._courses.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of ICourseService)(New CourseService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of ICourseService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllCourses
                Me._courses.Add(element)
            Next
        End Sub
        Sub New()
            Me.Courses = New ObservableCollection(Of Course)
            refresh()
        End Sub

        Private Function GetAllCourses() As IQueryable(Of Course)
            Return Me.dataAccess.GetAllCourses
        End Function

        Public Property ShowWindowButtonCommand As ICommand
            Get
                If Me._insert Is Nothing Then
                    Me._insert = New RelayCommand(AddressOf AddCourseToDB)
                End If
                Return Me._insert
            End Get
            Set(value As ICommand)
                _insert = value
            End Set
        End Property

        Public Property SelectedCourse As Course
            Get
                Return _selectedRow
            End Get
            Set(value As Course)
                _selectedRow = value
                OnPropertyChanged("SelectedCourse")
            End Set
        End Property
        Sub AddCourseToDB()
            Using school As New SchoolEntities
                newWindow = New AddCourse
                newWindow.ShowDialog()
                refresh()
            End Using
        End Sub
    End Class
End Namespace