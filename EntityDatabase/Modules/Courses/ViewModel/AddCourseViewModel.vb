Imports BusinessLogic.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Namespace Modules.Courses.ViewModel

    Public Class AddCourseViewModel
        Inherits ViewModelBase

        Private _courses As ObservableCollection(Of Course)
        Private _title As String
        Private _credits As Integer
        Private _departments As ObservableCollection(Of Department)
        Private dataAccess As ICourseService
        Private DepartmentdataAccess As IDepartmentService

        Public Property Courses As ObservableCollection(Of Course)
            Get
                Return Me._courses
            End Get
            Set(value As ObservableCollection(Of Course))
                Me._courses = value
                OnPropertyChanged("Course")
            End Set
        End Property

        Public Property Titles As String
            Get
                Return Me._title
            End Get
            Set(value As String)
                Me._title = value
                OnPropertyChanged("Titles")
            End Set
        End Property

        Public Property Credits As Integer
            Get
                Return Me._credits
            End Get
            Set(value As Integer)
                Me._credits = value
                OnPropertyChanged("Credits")
            End Set
        End Property

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return Me._departments
            End Get
            Set(value As ObservableCollection(Of Department))
                Me._departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        Private Function GetAllCourses() As IQueryable(Of Course)
            Return Me.dataAccess.GetAllCourses
        End Function
        Private Function GetAllDepartments() As IQueryable(Of Department)
            Return Me.DepartmentdataAccess.GetAllDepartments
        End Function

        Sub New()
            'Initialize property variable of courses
            Me._courses = New ObservableCollection(Of Course)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of ICourseService)(New CourseService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of ICourseService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllCourses
                Me._courses.Add(element)
            Next

            'Initialize property variable of courses
            Me._departments = New ObservableCollection(Of Department)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            ' Initialize dataAccess from service
            Me.DepartmentdataAccess = GetService(Of IDepartmentService)()
            ' Populate departments property variable 

            For Each departmentElement In Me.GetAllDepartments
                Me._departments.Add(departmentElement)
            Next

        End Sub


        Public Function autenticatingUser() As Boolean

            Using context As New HotelEntities

                Dim query =
                    From u In DataContext.DBEntities.Usuario
                    Where u.userName = _userName And u.userPassword = _password
                    Select New With {
                       .idUser = u.userId,
                       .username = u.userName,
                       .password = u.userPassword,
                       .rolId = u.idRol
                    }

                For Each usuario In query
                    Console.WriteLine("user ID: {0} ", usuario.idUser)

                    If usuario.rolId = 1 Then
                        MsgBox("es administrador")
                        _loginSuccess = True
                        LoadView()
                        Return True
                    ElseIf usuario.rolId = 2 Then
                        MsgBox("es recepcionista")
                        Return True
                    Else
                        Return False
                    End If
                Next
            End Using
            Return Nothing
        End Function

    End Class
End Namespace
