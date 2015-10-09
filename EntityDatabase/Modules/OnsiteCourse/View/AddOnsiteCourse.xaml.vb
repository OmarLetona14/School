Imports Modules.OnsiteCourse.ViewModel


Namespace Modules.OnsiteCourse.View
    Public Class AddOnsiteCourse
        Public Sub New()
            InitializeComponent()
            Me.DataContext = New AddOnsiteCourse()
        End Sub

        Public ReadOnly Property ViewModel As AddOnsiteCourse
            Get
                Return DirectCast(Me.DataContext, AddOnsiteCourse)
            End Get
        End Property
    End Class
End Namespace
