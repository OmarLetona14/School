Imports Modules.Departments.ViewModels
Imports Modules.Courses.ViewModel
Imports Modules.OfficeAssignament.ViewModel
Imports Modules.OnlineCourses.ViewModel
Imports Modules.Persons.ViewModels
Imports Modules.OnsiteCourse.ViewModel
Imports Modules.StudentGrade.ViewModel
Class MainWindow
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.CoursesUserControl.MainGrid.DataContext = New CoursesViewModel()
        Me.DepartmentsUserControl.MainGrid.DataContext = New DepartmentsViewModel()
        Me.OfficeAssignemntUserControl.MainGrid.DataContext = New OfficeAssignamentViewModel()
        Me.OnlineCoursesUserControl.MainGrid.DataContext = New OnlineCoursesViewModel()
        Me.PersonsUserControl.MainGrid.DataContext = New PersonsViewModel()
        Me.OnsiteCoursesUserControl.MainGrid.DataContext = New OnsiteCoursesViewModel()
        Me.StudentGradeUserControl.MainGrid.DataContext = New StudentGradeViewModel()
    End Sub
End Class
