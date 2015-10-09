Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.Departments.Views
Imports BusinessObjects.Helpers

Namespace Modules.Departments.ViewModels
    Public Class DepartmentsViewModel
        Inherits ViewModelBase

        Public DepartmentWindow As New DepartmentsList
        Private _select As Department
        Private _departments As ObservableCollection(Of Department)
        Private dataAccess As IDepartmentService
        Private _icomButtonExitCommand As ICommand
        Private _icomButtonEditCommand As ICommand
        Private _icomButtonDeleteCommand As ICommand
        Private Shadows _newWindow As AddDepartment
        Private _icomButtonNewWindowCommand As ICommand

        Public Property Departments As ObservableCollection(Of Department)
            Get
                Return Me._departments
            End Get
            Set(value As ObservableCollection(Of Department))
                Me._departments = value
                OnPropertyChanged("Departments")
            End Set
        End Property

        Public Property Selected As Department
            Get
                Return _select
            End Get
            Set(value As Department)
                _select = value
            End Set
        End Property

        Public ReadOnly Property ShowNewWindowButtonCommand As ICommand
            Get
                If Me._icomButtonNewWindowCommand Is Nothing Then
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf CreateDepartmentDB)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property

        Public ReadOnly Property EditButtonCommand As ICommand
            Get
                If Me._icomButtonEditCommand Is Nothing Then
                    Me._icomButtonEditCommand = New RelayCommand(AddressOf EditDepartmentDB)
                End If
                Return Me._icomButtonEditCommand
            End Get
        End Property

        Public ReadOnly Property DeleteButtonCommand As ICommand
            Get
                If Me._icomButtonDeleteCommand Is Nothing Then
                    Me._icomButtonDeleteCommand = New RelayCommand(AddressOf DeleteDepartmentDB)
                End If
                Return Me._icomButtonDeleteCommand
            End Get
        End Property

        Sub New()
            'Initialize property variable of departments
            Me._departments = New ObservableCollection(Of Department)
            actualizarLista()
        End Sub

        Sub CreateDepartmentDB()
            Using Contentx As New SchoolEntities
                _newWindow = New AddDepartment
                _newWindow.ShowDialog()

                actualizarLista()
            End Using
        End Sub

        Sub EditDepartmentDB()
            If Selected IsNot Nothing Then
                Using Context As New SchoolEntities
                    _newWindow = New AddDepartment(Selected)
                    _newWindow.ShowDialog()
                    actualizarLista()
                End Using
            Else
                MessageBox.Show("Por favor, seleccione un departamento")
            End If
        End Sub

        Sub DeleteDepartmentDB()
            Try
                If Selected IsNot Nothing Then
                    DataContext.DBEntities.Departments.Remove((From item In DataContext.DBEntities.Departments
                                                               Where item.DepartmentID = Selected.DepartmentID
                                                               Select item).FirstOrDefault)
                    DataContext.DBEntities.SaveChanges()
                    actualizarLista()

                Else
                    MessageBox.Show("Por favor, seleccione un departamento")
                End If
            Catch ex As Exception
                MessageBox.Show("No se puede eliminar este departamento", MsgBoxStyle.Critical)
            End Try
        End Sub

        ' Function to get all departments from service
        Private Function GetAllDepartments() As IQueryable(Of Department)
            Return Me.dataAccess.GetAllDepartments
        End Function

        Sub actualizarLista()
            'Initialize property variable of departments
            Me._departments = New ObservableCollection(Of Department)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IDepartmentService)(New DepartmentService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IDepartmentService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllDepartments
                Me._departments.Add(element)
            Next
        End Sub


    End Class
End Namespace

