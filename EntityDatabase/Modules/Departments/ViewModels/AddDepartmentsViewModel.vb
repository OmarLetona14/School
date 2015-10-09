Imports BusinessLogic.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Imports Modules.Departments.Views

Namespace Modules.Departments.ViewModels

    Public Class AddDepartmentsViewModel
        Inherits ViewModelBase

        Public addDepartmentWindow As AddDepartment
        Private _idDepartment As Integer
        Private _idAdministrator As Integer
        Private _selectedAdministrator As String
        Private _name As String
        Private newWindowDepartment As AddDepartment
        Private _budget As String
        Private _department As New Department
        Private _startDate As Date
        Private dataAccess As IDepartmentService
        Private _icomButtonAddCommand As ICommand
        Private _icomButtonExitCommand As ICommand

        Public Property IdDepartments As Integer
            Get
                Return Me._idDepartment
            End Get
            Set(value As Integer)
                Me._idDepartment = value
                OnPropertyChanged("IdDepartments")
            End Set
        End Property

        Public Property IdAdministrators As Integer
            Get
                Return Me._idAdministrator
            End Get
            Set(value As Integer)
                Me._idAdministrator = value
                OnPropertyChanged("IdAdministrators")
            End Set
        End Property

        Public Property SelectedAdministrator As String
            Get
                Return Me._selectedAdministrator
            End Get
            Set(value As String)
                Me._selectedAdministrator = value
                OnPropertyChanged("SelectedAdministrator")
            End Set
        End Property

        Public Property Name As String
            Get
                Return Me._name
            End Get
            Set(value As String)
                Me._name = value
                OnPropertyChanged("Name")
            End Set
        End Property

        Public Property Budgets As String
            Get
                Return Me._budget
            End Get
            Set(value As String)
                Me._budget = value
                OnPropertyChanged("Budgets")
            End Set
        End Property

        Public Property StartDate As Date
            Get
                Return Me._startDate
            End Get
            Set(value As Date)
                Me._startDate = value
                OnPropertyChanged("StartDate")
            End Set
        End Property

        Public ReadOnly Property ButtonCreateDepartment()
            Get
                If Me._icomButtonAddCommand Is Nothing Then
                    Me._icomButtonAddCommand = New RelayCommand(AddressOf CreateNewDepartment)
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
                addDepartmentWindow.Close()
            Else
            End If
        End Sub

        Public Sub CreateNewDepartment()
            CreateDepartment()
        End Sub

        Private Sub CreateDepartment()
            Try
                Dim departments As IQueryable(Of Department) = DataContext.DBEntities.Departments
                _department.StartDate = Date.Today
                For Each element In departments
                    _department.DepartmentID = Integer.Parse(element.DepartmentID.ToString) + 1
                Next
                DataContext.DBEntities.Departments.Add(_department)
                DataContext.DBEntities.SaveChanges()
                newWindowDepartment.Close()
            Catch ex As Exception
                MessageBox.Show("No se ha podido ingresar el departamentp", MsgBoxStyle.Critical)
            End Try
        End Sub
        Sub New(ByRef view As AddDepartment)
            Me.newWindowDepartment = view
            newWindowDepartment.Height = 350
            newWindowDepartment.Width = 350
        End Sub

    End Class
End Namespace



