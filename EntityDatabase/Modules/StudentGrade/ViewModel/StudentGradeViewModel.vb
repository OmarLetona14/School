Imports BusinessLogic.Helpers
Imports BusinessLogic.Services.Implementations
Imports BusinessLogic.Services.Interfaces
Imports System.Collections.ObjectModel

Namespace Modules.StudentGrade.ViewModel
    Public Class StudentGradeViewModel
        Inherits ViewModelBase

        Private _studentgrade As ObservableCollection(Of StudentGrade)
        Private dataAccess As IStudentGradeService

        'Implements Singlenton 
        Public Property StudentGrade As ObservableCollection(Of StudentGrade)
            Get
                Return Me._studentgrade
            End Get
            Set(value As ObservableCollection(Of StudentGrade))
                Me._studentgrade = value
                OnPropertyChanged("StudentGrade")
            End Set
        End Property

        ' Function to get all persons from service
        Private Function GetAllStudentGrades() As IQueryable(Of StudentGrade)
            Return Me.dataAccess.GetAllStudentGrades
        End Function

        Sub New()
            'Initialize property variable of persons
            Me._studentgrade = New ObservableCollection(Of StudentGrade)
            ' Register service with ServiceLocator
            ServiceLocator.RegisterService(Of IStudentGradeService)(New StudentGradeService)
            ' Initialize dataAccess from service
            Me.dataAccess = GetService(Of IStudentGradeService)()
            ' Populate departments property variable 
            For Each element In Me.GetAllStudentGrades
                Me._studentgrade.Add(element)
            Next
        End Sub
    End Class
End Namespace