Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.OfficeAssignament.View
Imports BusinessObjects.Helpers


Namespace Modules.OfficeAssignament.ViewModel
    Public Class OfficeAssignamentViewModel

        Inherits ViewModelBase

        Private _officeassignaments As ObservableCollection(Of Global.OfficeAssignment)
        Private dataAccess As IOfficeAssignamentService
        Private _selected As Global.OfficeAssignment
        Private Shadows _windowOfficeAssig As AddOfficeAssignment
        Private _icomButtonExitCommand As ICommand
        Private _icomButtonNewWindowCommand As ICommand
        Private _icomButtonEditCommand As ICommand
        Private _icomButtonDeleteCommand As ICommand


        Public Property OfficeAssignament As ObservableCollection(Of Global.OfficeAssignment)
            Get
                Return Me._officeassignaments
            End Get
            Set(value As ObservableCollection(Of Global.OfficeAssignment))
                Me._officeassignaments = value
                OnPropertyChanged("OfficeAssignament")
            End Set
        End Property

        Public Property Selected As OfficeAssignment
            Get
                Return _selected
            End Get
            Set(value As OfficeAssignment)
                _selected = value
            End Set
        End Property

        Public ReadOnly Property EditButtonCommand As ICommand
            Get
                If Me._icomButtonEditCommand Is Nothing Then
                    Me._icomButtonEditCommand = New RelayCommand(AddressOf Edit)
                End If
                Return Me._icomButtonEditCommand
            End Get
        End Property

        Public ReadOnly Property DeleteButtonCommand As ICommand
            Get
                If Me._icomButtonDeleteCommand Is Nothing Then
                    Me._icomButtonDeleteCommand = New RelayCommand(AddressOf Delete)
                End If
                Return Me._icomButtonDeleteCommand
            End Get
        End Property

        Private Function GetAllOfficeAssignments() As IQueryable(Of OfficeAssignment)
            Return Me.dataAccess.GetAllOfficeAssignaments
        End Function

        Sub New()
            Me._officeassignaments = New ObservableCollection(Of OfficeAssignment)
            actualizarLista()
        End Sub

        Sub actualizarLista()
            Me._officeassignaments.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IOfficeAssignamentService)(New OfficeAssignmentService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IOfficeAssignamentService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllOfficeAssignments
                Me._officeassignaments.Add(element)
            Next
        End Sub
        Public ReadOnly Property ButtonShowNewWindow()
            Get
                If Me._icomButtonNewWindowCommand Is Nothing Then
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf ShowWindow)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property

        Sub ShowWindow()
            Using Context As New SchoolEntities
                _windowOfficeAssig = New AddOfficeAssignment
                _windowOfficeAssig.ShowDialog()
                actualizarLista()
            End Using
        End Sub

        Sub Edit()
            If Selected IsNot Nothing Then
                Using Context As New SchoolEntities
                    _windowOfficeAssig = New AddOfficeAssignment(Selected)
                    _windowOfficeAssig.ShowDialog()
                    actualizarLista()
                End Using
            Else
                MessageBox.Show("Por favor, seleccione un office assignment")
            End If
        End Sub

        Sub Delete()
            If Selected IsNot Nothing Then
                DataContext.DBEntities.OfficeAssignments.Remove((From item In DataContext.DBEntities.OfficeAssignments
                                                                 Where item.InstructorID = Selected.InstructorID
                                                                 Select item).FirstOrDefault)
                DataContext.DBEntities.SaveChanges()
                actualizarLista()
            Else
                MessageBox.Show("Por favor, seleccione un office assignment")
            End If
        End Sub


    End Class
End Namespace