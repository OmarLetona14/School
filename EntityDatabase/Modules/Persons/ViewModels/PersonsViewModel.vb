Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.Persons.View

Namespace Modules.Persons.ViewModels
    Public Class PersonsViewModel
        Inherits ViewModelBase

        Private _persons As ObservableCollection(Of Person)
        Private dataAccess As IPersonService
        Private _icomButtonNewWindowCommand As ICommand

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

        ' Function to get all persons from service
        Private Function GetAllPersons() As IQueryable(Of Person)
            Return Me.dataAccess.GetAllPersons
        End Function

        Sub New()
            'Initialize property variable of persons
            Me._persons = New ObservableCollection(Of Person)
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
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf VentanaDiag)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property

        Private Sub VentanaDiag()
            Dim ventanaNueva As New AddPerson
            ventanaNueva.Height = 350
            ventanaNueva.Width = 350
            ventanaNueva.ResizeMode = ResizeMode.NoResize
            ventanaNueva.VerticalAlignment = VerticalAlignment.Center
            ventanaNueva.HorizontalAlignment = HorizontalAlignment.Center

            ventanaNueva.ShowDialog()
        End Sub

    End Class
End Namespace

