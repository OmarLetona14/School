Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.OfficeAssignament.View

Namespace Modules.OfficeAssignament.ViewModel
    Public Class OfficeAssignamentViewModel

        Inherits ViewModelBase

        Private _officeassignaments As ObservableCollection(Of Global.OfficeAssignment)
        Private dataAccess As IOfficeAssignamentService
        Private _icomButtonExitCommand As ICommand
        Private _icomButtonNewWindowCommand As ICommand

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
        Public ReadOnly Property ButtonShowNewWindow()
            Get
                If Me._icomButtonNewWindowCommand Is Nothing Then
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf VentanaDiag)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property

        Private Sub VentanaDiag()
            Dim ventanaNueva As New AddOfficeAssignment
            ventanaNueva.Height = 350
            ventanaNueva.Width = 350
            ventanaNueva.ResizeMode = ResizeMode.NoResize
            ventanaNueva.VerticalAlignment = VerticalAlignment.Center
            ventanaNueva.HorizontalAlignment = HorizontalAlignment.Center

            ventanaNueva.ShowDialog()
        End Sub


    End Class
End Namespace