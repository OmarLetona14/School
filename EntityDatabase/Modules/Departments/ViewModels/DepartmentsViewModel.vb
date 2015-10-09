Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.Departments.Views

Namespace Modules.Departments.ViewModels
    Public Class DepartmentsViewModel
        Inherits ViewModelBase

        Public DepartmentWindow As New DepartmentsList
        Private _departments As ObservableCollection(Of Department)
        Private dataAccess As IDepartmentService
        Private _icomButtonExitCommand As ICommand
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

        ' Function to get all departments from service
        Private Function GetAllDepartments() As IQueryable(Of Department)
            Return Me.dataAccess.GetAllDepartments
        End Function

        Sub New()
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
                ' DepartmentWindow.Close()
            Else
            End If
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
            Dim ventanaNueva As New AddDepartment
            ventanaNueva.Height = 350
            ventanaNueva.Width = 350
            ventanaNueva.ResizeMode = ResizeMode.NoResize
            ventanaNueva.VerticalAlignment = VerticalAlignment.Center
            ventanaNueva.HorizontalAlignment = HorizontalAlignment.Center

            ventanaNueva.ShowDialog()
        End Sub
    End Class
End Namespace

