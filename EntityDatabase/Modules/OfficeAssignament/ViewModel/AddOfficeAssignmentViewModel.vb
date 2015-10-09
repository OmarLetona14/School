Imports BusinessLogic.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Imports Modules.OfficeAssignament.View


Namespace Modules.OfficeAssignament.ViewModel

    Public Class AddOfficeAssignmentViewModel
        Inherits ViewModelBase


        Public addOfficeWindow As New AddOfficeAssignment
        Private _idInstructor As Integer
        Private _location As String
        Private _timeStamp As Byte
        Private dataAccess As IOfficeAssignamentService
        Private _icomButtonAddCommand As ICommand
        Private _icomButtonExitCommand As ICommand


        Public Property IdInstructor As Integer
            Get
                Return Me._idInstructor
            End Get
            Set(value As Integer)
                Me._idInstructor = value
                OnPropertyChanged("IdInstructor")
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

        Public Property TimeStamp As Byte
            Get
                Return Me._timeStamp
            End Get
            Set(value As Byte)
                Me._timeStamp = value
                OnPropertyChanged("TimeStamp")
            End Set
        End Property

        Public ReadOnly Property ButtonCreateOfficeAssignment()
            Get
                If Me._icomButtonAddCommand Is Nothing Then
                    Me._icomButtonAddCommand = New RelayCommand(AddressOf CreateNewOfficeAssignment)
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
                addOfficeWindow.Close()
            Else
            End If
        End Sub

        Public Sub CreateNewOfficeAssignment()
            CreateOfficeAssignment()
        End Sub

        'Sub New()
        '    ServiceLocator.RegisterService(Of IAddOfficeAssignmentService)(New AddOfficeAssignment)
        '    Me.dataAccess = ServiceLocator.GetService(Of IAddOfficeAssignmentService)()

        'End Sub

        Private Sub CreateOfficeAssignment()
            If IdInstructor <> "" And Location <> "" And TimeStamp <> "" Then
                Dim officeAssignment As New OfficeAssignment
                officeAssignment.InstructorID = IdInstructor
                officeAssignment.Location = Location
                '  officeAssignment.Timestamp = TimeStamp
                AddOfficeAssignment(officeAssignment)

                Me.IdInstructor = ""
                Me.Location = ""
                Me.TimeStamp = ""
            Else
                MsgBox("Ingrese los datos correctamente", MsgBoxStyle.Critical, "School System")
            End If
        End Sub

        Sub AddOfficeAssignment(ByVal office As OfficeAssignment)
            Using context As New SchoolEntities

                Try
                    'agregar a la base de datos
                    context.OfficeAssignments.Add(office)
                    MsgBox("Se agregó correctamente")
                    'eliminar de la base de datos   
                Catch ex As Exception
                    MsgBox("No se puede agregar a la base de datos", MsgBoxStyle.Critical)
                End Try

            End Using
        End Sub

    End Class
End Namespace