Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.Persons.ViewModels
    Public Class PersonsViewModel
        Inherits ViewModelBase

        Private _persons As ObservableCollection(Of Person)
        Private dataAccess As IPersonService

        'Implements Singlenton 
        Public Property Persons As ObservableCollection(Of Person)
            Get
                Return Me._persons
            End Get
            Set(value As ObservableCollection(Of Person))
                Me._persons = value
                OnPropertyChanged("Persons")
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
            Me.dataAccess = GetService(Of IDepartmentService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllPersons
                Me._persons.Add(element)
            Next
        End Sub
    End Class
End Namespace

