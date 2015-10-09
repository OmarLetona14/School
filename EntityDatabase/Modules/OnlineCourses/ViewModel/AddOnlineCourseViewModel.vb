Imports BusinessLogic.Helpers
Imports Modules.OnlineCourses.View
Imports BusinessLogic.Services.Interfaces
Imports BusinessLogic.Services.Implementations

Namespace Modules.OnlineCourses.ViewModel

    Public Class AddOnlineCourseViewModel
        Inherits ViewModelBase

        Public addOnlineCourseWindow As New AddOnlineCourse
        Private _idCourse As Integer
        Private _nameCourse As String
        Private _url As String
        Private dataAccess As IOnlineCourseService
        Private _icomButtonAddCommand As ICommand
        Private _icomButtonExitCommand As ICommand

        Public Property IdCourse As Integer
            Get
                Return Me._idCourse
            End Get
            Set(value As Integer)
                Me._idCourse = value
                OnPropertyChanged("IdCourse")
            End Set
        End Property

        Public Property NameCourse As String
            Get
                Return Me._nameCourse
            End Get
            Set(value As String)
                Me._nameCourse = value
                OnPropertyChanged("NameCourse")
            End Set
        End Property

        Public Property URL As String
            Get
                Return Me._url
            End Get
            Set(value As String)
                Me._url = value
                OnPropertyChanged("URL")
            End Set
        End Property

        Public ReadOnly Property ButtonCreateOnlineCourse()
            Get
                If Me._icomButtonAddCommand Is Nothing Then
                    Me._icomButtonAddCommand = New RelayCommand(AddressOf CreateNewOnlineCourse)
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
                addOnlineCourseWindow.Close()
            Else
            End If
        End Sub

        Public Sub CreateNewOnlineCourse()
            CreateOnlineCourse()
        End Sub

        Sub New()


        End Sub

        Private Sub CreateOnlineCourse()
            If NameCourse <> "" And URL <> "" Then
                Dim onlinecourse As New OnlineCourse
                onlinecourse.CourseID = IdCourse
                onlinecourse.URL = URL
                AddOnlineCourse(onlinecourse)

                Me.IdCourse = ""
                Me.URL = ""
            Else
                MsgBox("Ingrese los datos correctamente", MsgBoxStyle.Critical, "School System")
            End If
        End Sub

        Sub AddOnlineCourse(ByVal onlinecourse As OnlineCourse)
            Using context As New SchoolEntities

                Try
                    'agregar a la base de datos
                    context.OnlineCourses.Add(onlinecourse)
                    MsgBox("Se agregó correctamente")
                    'eliminar de la base de datos   
                Catch ex As Exception
                    MsgBox("No se puede agregar a la base de datos", MsgBoxStyle.Critical)
                End Try

            End Using
        End Sub


    End Class
End Namespace