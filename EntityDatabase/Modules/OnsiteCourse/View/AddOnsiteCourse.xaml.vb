Imports Modules.OnsiteCourse.ViewModel


Namespace Modules.OnsiteCourse.View
    Public Class AddOnsiteCourse
        Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Grid.DataContext = New AddOnsiteCourseViewModel(Me)
        End Sub

        Public Sub New(ByVal oncourse As Global.OnsiteCourse)
            InitializeComponent()
            Me.Grid.DataContext = New AddOnsiteCourseViewModel(Me, oncourse)
        End Sub
    End Class
End Namespace
