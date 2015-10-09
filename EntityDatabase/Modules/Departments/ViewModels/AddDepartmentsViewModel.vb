Imports BusinessLogic.Helpers
Imports System.Collections.ObjectModel
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports BusinessObjects.Helpers
Imports Modules.Departments.Views

Namespace Modules.Departments.ViewModels

    Public Class AddDepartmentsViewModel
        Inherits ViewModelBase
        Private _Department As New Department
        Private _newView As AddDepartment
        Private _idDepartment As String
        Private _Name As String
        Private _Budget As Decimal
        Private _date As Date = Today
        Private _Admin As Integer = 1
        Private departEdit As Department
        Private _acceptCommand As ICommand
        Private _cancelCommand As ICommand


        Public Property idDepartment As Integer
            Get
                Return Me._idDepartment
            End Get
            Set(value As Integer)
                Me._idDepartment = value
                _Department.DepartmentID = _idDepartment
                OnPropertyChanged("idDepartment")
            End Set
        End Property
        Public Property Name As String
            Get
                Return Me._Name
            End Get
            Set(value As String)
                Me._Name = value
                _Department.Name = _Name
                OnPropertyChanged("Name")
            End Set
        End Property

        Public Property Budgets As Decimal
            Get
                Return Me._Budget
            End Get
            Set(value As Decimal)
                Me._Budget = value
                _Department.Budget = _Budget
                OnPropertyChanged("Budgets")
            End Set
        End Property

        Public Property Dates As Date
            Get
                Return Me._date
            End Get
            Set(value As Date)
                Me._date = value
                _Department.StartDate = _date
                OnPropertyChanged("Dates")
            End Set
        End Property

        Public Property Administrator As Integer
            Get
                Return Me._Admin
            End Get
            Set(value As Integer)
                Me._Admin = value
                _Department.Administrator = _Admin
                OnPropertyChanged("Administrator")
            End Set
        End Property
        Public ReadOnly Property ButtonCreateDepartment As ICommand
            Get
                If Me._acceptCommand Is Nothing Then
                    Me._acceptCommand = New RelayCommand(AddressOf AcceptExecute)
                End If
                Return Me._acceptCommand
            End Get
        End Property

        Public ReadOnly Property CancelButton As ICommand
            Get
                If Me._cancelCommand Is Nothing Then
                    Me._cancelCommand = New RelayCommand(AddressOf ExitEditor)
                End If
                Return Me._cancelCommand
            End Get
        End Property

        Public Sub ExitEditor()
            Dim respuesta As String = MsgBox("¿Está seguro que desea salir ?", MsgBoxStyle.YesNo)
            If respuesta = MsgBoxResult.Yes Then
                _newView.Close()
            Else
            End If
        End Sub

        Sub New(ByRef newView As AddDepartment)
            Me._newView = newView
        End Sub

        Sub New(ByRef newView As AddDepartment, Department As Department)
            Me._newView = newView
            departEdit = Department
            If Department Is Nothing Then
                Exit Sub
            End If
            _idDepartment = departEdit.DepartmentID
            _Name = departEdit.Name
            _Budget = departEdit.Budget
            _date = departEdit.StartDate
            _Admin = departEdit.Administrator
        End Sub

        Sub AcceptExecute()
            Try
                If departEdit Is Nothing Then
                    DataContext.DBEntities.Departments.Add(_Department)
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("El nuevo departamento se ha agregado exitosamente", MsgBoxStyle.Information)
                    _newView.Close()
                Else
                    Dim Department As Department = (From item In DataContext.DBEntities.Departments Where item.DepartmentID = _Department.DepartmentID
                                                    Select item).FirstOrDefault()
                    Department.DepartmentID = _idDepartment
                    Department.Name = _Name
                    Department.Budget = _Budget
                    Department.StartDate = _Date
                    Department.Administrator = _Admin
                    DataContext.DBEntities.SaveChanges()
                    MessageBox.Show("El nuevo departamento se ha editado exitosamente", MsgBoxStyle.Information)
                    _newView.Close()
                End If
            Catch ex As Exception
                MessageBox.Show("No se puede agregar el departamento")
            End Try
        End Sub

        Sub CancelExecute()
            _newView.Close()
        End Sub
    End Class
End Namespace



