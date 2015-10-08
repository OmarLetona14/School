Imports Modules.Persons.View
Imports BusinessLogic.Helpers
Imports BusinessObjects.Helpers

Imports System.Collections.ObjectModel

Namespace Modules.Persons.ViewModel
    Public Class AddPersonViewModel
        Inherits ViewModelBase


        Private _persons As ObservableCollection(Of Person)
        Private _idPerson As Integer
        Private _lastName As String
        Private _firstName As String
        Private _hireDate As Date = DateString
        Private _person As Person
        Private _enrollment As Date = DateString
        Public _newWindow As AddPerson
        Private _personToEdit As Person
        Private _icomButtonAddCommand As ICommand
        Private _icomButtonExitCommand As ICommand


        Public Property idPerson As Integer
            Get
                Return _idPerson
            End Get
            Set(value As Integer)
                Me._idPerson = value
                _person.PersonID = _idPerson
                OnPropertyChanged("idPerson")
            End Set
        End Property
        Public Property FirstName As String
            Get
                Return _firstName
            End Get
            Set(value As String)
                Me._firstName = value
                _person.FirstName = _firstName
                OnPropertyChanged("FirstName")
            End Set
        End Property

        Public Property HireDate As Date
            Get
                Return _hireDate
            End Get
            Set(value As Date)
                Me._hireDate = value
                _person.HireDate = _hireDate
                OnPropertyChanged("HireDate")
            End Set
        End Property

        Public Property LastName As String
            Get
                Return _lastName
            End Get
            Set(value As String)
                Me._lastName = value
                _person.LastName = _lastName
                OnPropertyChanged("LastName")
            End Set
        End Property

        Public Property EnrollmentDate As Date
            Get
                Return _enrollment
            End Get
            Set(value As Date)
                Me._enrollment = value
                _person.EnrollmentDate = _enrollment
                OnPropertyChanged("EnrollmentDate")
            End Set
        End Property

        Public ReadOnly Property ButtonCreateNewPerson()
            Get
                If Me._icomButtonAddCommand Is Nothing Then
                    Me._icomButtonAddCommand = New RelayCommand(AddressOf CreatePerson)
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


        Private Sub CreatePerson()
            Try
                If _personToEdit Is Nothing Then
                    DataContext.DBEntities.People.Add(_person)
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("La nueva persona se ha agregado exitosamente", MsgBoxStyle.Information)
                    _newWindow.Close()
                Else
                    Dim person As Person = (From item In DataContext.DBEntities.People
                                            Where item.PersonID = _personToEdit.PersonID
                                            Select item).FirstOrDefault()

                    person.PersonID = _idPerson
                    person.FirstName = _firstName
                    person.LastName = _lastName
                    If _personToEdit.HireDate IsNot Nothing Then
                        _hireDate = _personToEdit.HireDate
                    End If
                    If _personToEdit.EnrollmentDate IsNot Nothing Then
                        _enrollment = _personToEdit.EnrollmentDate
                    End If
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("La persona se ha editado exitosamente", MsgBoxStyle.Information)
                    _newWindow.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("No se puede agregar la persona. Por favor asegúrese de que estén llenos todos los campos.", MsgBoxStyle.Critical)
            End Try
        End Sub

        Sub New(ByRef newView As AddPerson)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350

        End Sub

        Sub New(ByRef newView As AddPerson, u As Person)
            Me._newWindow = newView
            _newWindow.Height = 350
            _newWindow.Width = 350
            _personToEdit = u
            If u Is Nothing Then
                Exit Sub
            End If
            _idPerson = _personToEdit.PersonID
            _firstName = _personToEdit.FirstName
            _lastName = _personToEdit.LastName
            If _personToEdit.HireDate IsNot Nothing Then
                _hireDate = _personToEdit.HireDate
            End If
            If _personToEdit.EnrollmentDate IsNot Nothing Then
                _enrollment = _personToEdit.EnrollmentDate
            End If
        End Sub





    End Class
End Namespace
