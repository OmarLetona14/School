Imports BusinessLogic.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Imports Modules.OfficeAssignament.View


Namespace Modules.OfficeAssignament.ViewModel

    Public Class AddOfficeAssignmentViewModel
        Inherits ViewModelBase


        Public _newWindow As New AddOfficeAssignment
        Private _idInstructor As Integer
        Private _location As String
        Private _timeStamp As Byte()
        Private _officeToEdit As OfficeAssignment
        Private dataAccess As IOfficeAssignamentService
        Private _icomButtonAddCommand As ICommand
        Private _icomButtonExitCommand As ICommand
        Private _officeAssignment As New OfficeAssignment
        Private _instructores As ObservableCollection(Of Person)
        Private _selectedRow As Person
        Private _icomButtonCreateNewCommand As ICommand


        Public Property IdInstructor As Integer
            Get
                Return Me._idInstructor
            End Get
            Set(value As Integer)
                Me._idInstructor = value
                _officeAssignment.InstructorID = _idInstructor
                OnPropertyChanged("IdInstructor")
            End Set
        End Property

        Public Property Location As String
            Get
                Return Me._location
            End Get
            Set(value As String)
                Me._location = value
                _officeAssignment.Location = _location
                OnPropertyChanged("Location")
            End Set
        End Property

        Public Property TimeStamp As Byte()
            Get
                Return Me._timeStamp
            End Get
            Set(value As Byte())
                Me._timeStamp = value
                _officeAssignment.Timestamp = _timeStamp
                OnPropertyChanged("TimeStamp")
            End Set
        End Property

        Public ReadOnly Property ButtonCreateOfficeAssignment()
            Get
                If Me._icomButtonAddCommand Is Nothing Then
                    Me._icomButtonAddCommand = New RelayCommand(AddressOf CreateOfficeAssignment)
                End If
                Return Me._icomButtonAddCommand
            End Get
        End Property

        Public Property Instructors As ObservableCollection(Of Person)
            Get
                Return _instructores
            End Get
            Set(value As ObservableCollection(Of Person))
                _instructores = value
                OnPropertyChanged("Instructors")
            End Set
        End Property

        Public Property SelectedInstructor As Person
            Get
                Return _selectedRow
            End Get
            Set(value As Global.Person)
                _officeAssignment.Person = value
                _selectedRow = value
                OnPropertyChanged("SelectInstructor")
            End Set
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

        Sub New(ByRef newView As AddOfficeAssignment)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _instructores = New ObservableCollection(Of Global.Person)
            Dim Person As IQueryable(Of Global.Person) = DataContext.DBEntities.People
            For Each element In Person
                _instructores.Add(element)
            Next
        End Sub

        Sub New(ByRef newView As AddOfficeAssignment, officeAssig As OfficeAssignment)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _officeToEdit = officeAssig
            _instructores = New ObservableCollection(Of Global.Person)
            Dim Person As IQueryable(Of Global.Person) = DataContext.DBEntities.People
            For Each element In Person
                _instructores.Add(element)
            Next
            If officeAssig Is Nothing Then
                Exit Sub
            End If
            _selectedRow = _officeToEdit.Person
            _location = _officeToEdit.Location
            _timeStamp = _officeToEdit.Timestamp
        End Sub
        Private Sub CreateOfficeAssignment()
            Try
                If _officeToEdit Is Nothing Then
                    DataContext.DBEntities.OfficeAssignments.Add(_officeAssignment)
                    DataContext.DBEntities.SaveChanges()
                    _newWindow.Close()
                Else
                    Dim Office As OfficeAssignment = (From item In DataContext.DBEntities.OfficeAssignments Where item.InstructorID = _officeToEdit.InstructorID
                                                      Select item).FirstOrDefault()
                    Office.Person = _selectedRow
                    Office.Location = _location
                    Office.Timestamp = _timeStamp
                    DataContext.DBEntities.SaveChanges()
                    _newWindow.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("No se puede agregar el office assignment")
            End Try
        End Sub

    End Class
End Namespace