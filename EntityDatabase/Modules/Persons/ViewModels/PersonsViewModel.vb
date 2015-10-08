Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports BusinessObjects.Helpers

Imports Modules.Persons.View

Namespace Modules.Persons.ViewModels
    Public Class PersonsViewModel
        Inherits ViewModelBase

        Private _persons As ObservableCollection(Of Person)
        Private dataAccess As IPersonService
        Private _selectedRow As Person
        Private _icomButtonNewWindowCommand As ICommand
        Public Shadows _newWindow As AddPerson
        Private _icomButtonDeleteCommand As ICommand
        Private _icomButtonEditCommand As ICommand

        'Implements Singlenton 
        Public Property Person As ObservableCollection(Of Person)
            Get
                Return Me._persons
            End Get
            Set(value As ObservableCollection(Of Person))
                Me._persons = value
                OnPropertyChanged("Person")
            End Set
        End Property


        Public Property DeleteButton As ICommand
            Get
                If _icomButtonDeleteCommand Is Nothing Then
                    _icomButtonDeleteCommand = New RelayCommand(AddressOf Delete)
                End If
                Return _icomButtonDeleteCommand
            End Get
            Set(value As ICommand)
                _icomButtonDeleteCommand = value
            End Set
        End Property

        Public ReadOnly Property EditButtonCommand As ICommand
            Get
                If Me._icomButtonEditCommand Is Nothing Then
                    Me._icomButtonEditCommand = New RelayCommand(AddressOf EditPerson)
                End If
                Return Me._icomButtonEditCommand
            End Get
        End Property

        Sub EditPerson()
            If SelectedPerson IsNot Nothing Then
                Using context As New SchoolEntities
                    _newWindow = New AddPerson(SelectedPerson)
                    _newWindow.ShowDialog()
                    actualizarLista()
                End Using
            Else
                MessageBox.Show("Por favor, seleccione una persona")
            End If
        End Sub
        Sub Delete()
            Try
                If SelectedPerson IsNot Nothing Then
                    DataContext.DBEntities.People.Remove((From p In DataContext.DBEntities.People
                                                          Where p.PersonID = SelectedPerson.PersonID
                                                          Select p).FirstOrDefault)
                    DataContext.DBEntities.SaveChanges()
                    actualizarLista()
                Else
                    MessageBox.Show("Debe seleccionar una persona")
                End If
            Catch ex As Exception

            End Try

        End Sub
        Sub New()
            Me._persons = New ObservableCollection(Of Person)
            actualizarLista()
        End Sub

        ' Function to get all persons from service
        Private Function GetAllPersons() As IQueryable(Of Person)
            Return Me.dataAccess.GetAllPersons
        End Function

        Sub actualizarLista()
            _persons.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IPersonService)(New PersonService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IPersonService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllPersons
                Me._persons.Add(element)
            Next
        End Sub
        Public ReadOnly Property ButtonShowNewWindow()
            Get
                If Me._icomButtonNewWindowCommand Is Nothing Then
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf AddOPersonToDB)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property
        Public Property SelectedPerson As Person
            Get
                Return _selectedRow
            End Get
            Set(value As Person)
                _selectedRow = value
                OnPropertyChanged("SelectedPerson")
            End Set
        End Property
        Sub AddOPersonToDB()
            Using school As New SchoolEntities
                _newWindow = New AddPerson
                _newWindow.ShowDialog()
                actualizarLista()
            End Using
        End Sub

    End Class
End Namespace

