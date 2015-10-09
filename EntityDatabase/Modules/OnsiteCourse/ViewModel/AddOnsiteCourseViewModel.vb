Imports BusinessLogic.Helpers
Imports Modules.OnsiteCourse.View
Imports BusinessLogic.Services.Interfaces
Imports BusinessLogic.Services.Implementations

Namespace Modules.OnsiteCourse.ViewModel
    Public Class AddOnsiteCourseViewModel
        Inherits ViewModelBase

        Public addOnsiteCourseWindow As New AddOnsiteCourse
        Private _idCourse As Integer
        Private _selectedCourse As String
        Private _location As String
        Private _days As Date
        Private _time As Date
        Private dataAccess As IOnsiteCourseService
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

        Public Property SelectedCourse As String
            Get
                Return Me._selectedCourse
            End Get
            Set(value As String)
                Me._selectedCourse = value
                OnPropertyChanged("SelectedCourse")
            End Set
        End Property

        Public Property Location As String
            Get
                Return Me._location
            End Get
            Set(value As String)
                Me._location = value
                OnPropertyChanged("Location")
            End Set
        End Property

        Public Property Days As Date
            Get
                Return Me._days
            End Get
            Set(value As Date)
                Me._days = value
                OnPropertyChanged("Days")
            End Set
        End Property

        Public Property Time As Date
            Get
                Return Me._time
            End Get
            Set(value As Date)
                Me._time = value
                OnPropertyChanged("Time")
            End Set
        End Property

        Public ReadOnly Property ButtonCreateNewOnsiteCouse()
            Get
                If Me._icomButtonAddCommand Is Nothing Then
                    Me._icomButtonAddCommand = New RelayCommand(AddressOf CreateNewOnsiteCourse)
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
                addOnsiteCourseWindow.Close()
            Else
            End If
        End Sub

        Public Sub CreateNewOnsiteCourse()
            CreateOnsiteCourse()
        End Sub

        Sub New()

        End Sub

        Private Sub CreateOnsiteCourse()
            If IdCourse <> "" And SelectedCourse <> Nothing And Location <> "" And Days <> "" And Time <> "" Then
                Dim onsitecourse As New Global.OnsiteCourse
                onsitecourse.CourseID = IdCourse
                onsitecourse.Location = Location
                onsitecourse.Course.Title = SelectedCourse
                onsitecourse.Days = Days
                onsitecourse.Time = Time
                AddOnsiteCourse(onsitecourse)

                Me.IdCourse = ""
                Me.Location = ""
                Me.SelectedCourse = Nothing
                Me.Days = Nothing
                Me.Time = Nothing
            Else
                MsgBox("Ingrese los datos correctamente", MsgBoxStyle.Critical, "School System")
            End If
        End Sub

        Sub AddOnsiteCourse(ByVal onsitecourse As Global.OnsiteCourse)
            Using context As New SchoolEntities

                Try
                    'agregar a la base de datos
                    context.OnsiteCourses.Add(onsitecourse)
                    MsgBox("Se agregó correctamente")
                    'eliminar de la base de datos   
                Catch ex As Exception
                    MsgBox("No se puede agregar a la base de datos", MsgBoxStyle.Critical)
                End Try

            End Using
        End Sub



    End Class
End Namespace
