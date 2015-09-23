Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.OfficeAssignament.ViewModel
    Public Class OfficeAssignamentViewModel

        Inherits ViewModelBase

        Private _officeassignaments As ObservableCollection(Of Global.OfficeAssignment)
        Private dataAccess As IOfficeAssignamentService

        'Implements Singlenton 
        Public Property OfficeAssignament As ObservableCollection(Of Global.OfficeAssignment)
            Get
                Return Me._officeassignaments
            End Get
            Set(value As ObservableCollection(Of Global.OfficeAssignment))
                Me._officeassignaments = value
                OnPropertyChanged("OfficeAssignament")
            End Set
        End Property

        ' Function to get all persons from service
        Private Function GetAllOfficeAssignaments() As IQueryable(Of Global.OfficeAssignment)
            Return Me.dataAccess.GetAllOfficeAssignaments
        End Function

        Sub New()
            'Initialize property variable of persons
            Me._officeassignaments = New ObservableCollection(Of Global.OfficeAssignment)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOfficeAssignamentService)(New OfficeAssignmentService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOfficeAssignamentService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllOfficeAssignaments
                Me._officeassignaments.Add(element)
            Next
        End Sub
    End Class
End Namespace