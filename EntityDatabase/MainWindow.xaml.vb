Imports Modules.Departments.ViewModels
Imports Modules.Courses.ViewModel
Imports Modules.OfficeAssignament.ViewModels
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
        'Me.DepartamentsUserControl.MainGrid.DataContext = New DepartmentsViewModel()
        'Me.OfficeAssignamentUserControl.MainGrid.DataContext = New OfficeAssignamentViewModel()
        'Me.OnlineCoursesUserControl.MainGrid.DataContext = New OnlineCoursesViewModel()
        Me.PersonsUserControl.MainGrid.DataContext = New CoursesViewModel()
        'Me.OnsiteCoursesUserControl.MainGrid.DataContext = New CoursesViewModel()
        'Me.StudentGradeUserControl.MainGrid.DataContext = New CoursesViewModel()
    End Sub
End Class
