Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.StudentGrade.View
Imports BusinessObjects.Helpers


Namespace Modules.StudentGrade.ViewModel
    Public Class StudentGradeViewModel
        Inherits ViewModelBase

        Private _studentgrade As ObservableCollection(Of Global.StudentGrade)
        Private dataAccess As IStudentGradeService
        Private _selectedRow As Global.StudentGrade
        Private _icomButtonNewWindowCommand As ICommand
        Public Shadows _newWindow As AddStudentGrade
        Private _icomButtonDeleteCommand As ICommand
        Private _icomButtonEditCommand As ICommand

        'Implements Singlenton 
        Public Property StudentGrade As ObservableCollection(Of Global.StudentGrade)
            Get
                Return Me._studentgrade
            End Get
            Set(value As ObservableCollection(Of Global.StudentGrade))
                Me._studentgrade = value
                OnPropertyChanged("StudentGrade")
            End Set
        End Property

        Public Property DeleteButton As ICommand
            Get
                If _icomButtonDeleteCommand Is Nothing Then
                    _icomButtonDeleteCommand = New RelayCommand(AddressOf Delete)
                End If
                Return _icomButtonDeleteCommand
            End Get
            Set(value As ICommand)
                _icomButtonDeleteCommand = value
            End Set
        End Property

        Public ReadOnly Property EditButtonCommand As ICommand
            Get
                If Me._icomButtonEditCommand Is Nothing Then
                    Me._icomButtonEditCommand = New RelayCommand(AddressOf EditStudentGrade)
                End If
                Return Me._icomButtonEditCommand
            End Get
        End Property

        Sub EditStudentGrade()
            If SelectedStudentGrade IsNot Nothing Then
                Using context As New SchoolEntities
                    _newWindow = New AddStudentGrade(SelectedStudentGrade)
                    _newWindow.ShowDialog()
                    actualizarLista()
                End Using
            Else
                MessageBox.Show("Por favor, seleccione un student grade")
            End If
        End Sub
        Sub Delete()
            Try
                If SelectedStudentGrade IsNot Nothing Then
                    DataContext.DBEntities.StudentGrades.Remove((From sG In DataContext.DBEntities.StudentGrades
                                                                 Where sG.EnrollmentID = SelectedStudentGrade.EnrollmentID
                                                                 Select sG).FirstOrDefault)
                    DataContext.DBEntities.SaveChanges()
                    actualizarLista()
                Else
                    MessageBox.Show("Debe seleccionar un student grade")
                End If
            Catch ex As Exception

            End Try

        End Sub
        Sub New()
            Me._studentgrade = New ObservableCollection(Of Global.StudentGrade)
            actualizarLista()
        End Sub

        ' Function to get all persons from service
        Private Function GetAllStudentGrades() As IQueryable(Of Global.StudentGrade)
            Return Me.dataAccess.GetAllStudentGrades
        End Function

        Sub actualizarLista()
            _studentgrade.Clear()
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IStudentGradeService)(New StudentGradeService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IStudentGradeService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllStudentGrades
                Me._studentgrade.Add(element)
            Next
        End Sub

        Public ReadOnly Property ButtonShowNewWindow()
            Get
                If Me._icomButtonNewWindowCommand Is Nothing Then
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf AddStudentGradeDB)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property
        Public Property SelectedStudentGrade As Global.StudentGrade
            Get
                Return _selectedRow
            End Get
            Set(value As Global.StudentGrade)
                _selectedRow = value
                OnPropertyChanged("SelectedStudentGrade")
            End Set
        End Property
        Sub AddStudentGradeDB()
            Using school As New SchoolEntities
                _newWindow = New AddStudentGrade
                _newWindow.ShowDialog()
                actualizarLista()
            End Using
        End Sub
    End Class
End Namespace