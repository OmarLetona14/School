Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.OfficeAssignament.ViewModels
    Public Class OfficeAssignamentViewModel

        Inherits ViewModelBase

        Private _officeassignaments As ObservableCollection(Of OfficeAssignment)
        Private dataAccess As IOfficeAssignamentService

        'Implements Singlenton 
        Public Property OfficeAssignament As ObservableCollection(Of OfficeAssignment)
            Get
                Return Me._officeassignaments
            End Get
            Set(value As ObservableCollection(Of OfficeAssignment))
                Me._officeassignaments = value
                OnPropertyChanged("OfficeAssignament")
            End Set
        End Property

        ' Function to get all persons from service
        Private Function GetAllOfficeAssignaments() As IQueryable(Of OfficeAssignment)
            Return Me.dataAccess.GetAllOfficeAssignaments
        End Function

        Sub New()
            'Initialize property variable of persons
            Me._officeassignaments = New ObservableCollection(Of OfficeAssignment)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOfficeAssignamentService)(New OfficeAssignmentService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IDepartmentService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllOfficeAssignaments
                Me._officeassignaments.Add(element)
            Next
        End Sub
    End Class
End Namespace