Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel
Imports Modules.StudentGrade.View

Namespace Modules.StudentGrade.ViewModel
    Public Class StudentGradeViewModel
        Inherits ViewModelBase

        Private _studentgrade As ObservableCollection(Of Global.StudentGrade)
        Private dataAccess As IStudentGradeService
        Private _icomButtonNewWindowCommand As ICommand

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

        ' Function to get all persons from service
        Private Function GetAllStudentGrades() As IQueryable(Of Global.StudentGrade)
            Return Me.dataAccess.GetAllStudentGrades
        End Function

        Sub New()
            'Initialize property variable of persons
            Me._studentgrade = New ObservableCollection(Of Global.StudentGrade)

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
                    Me._icomButtonNewWindowCommand = New RelayCommand(AddressOf VentanaDiag)
                End If
                Return Me._icomButtonNewWindowCommand
            End Get
        End Property

        Private Sub VentanaDiag()
            Dim ventanaNueva As New AddStudentGrade
            ventanaNueva.Height = 350
            ventanaNueva.Width = 350
            ventanaNueva.ResizeMode = ResizeMode.NoResize
            ventanaNueva.VerticalAlignment = VerticalAlignment.Center
            ventanaNueva.HorizontalAlignment = HorizontalAlignment.Center

            ventanaNueva.ShowDialog()
        End Sub
    End Class
End Namespace