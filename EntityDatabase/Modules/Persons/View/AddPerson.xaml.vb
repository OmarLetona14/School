Imports Modules.Persons.ViewModel


Namespace Modules.Persons.View
    Public Class AddPerson
        Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            Me.Grid.DataContext = New AddPersonViewModel(Me)
        End Sub

        Public Sub New(ByVal person As Person)
            InitializeComponent()
            Me.Grid.DataContext = New AddPersonViewModel(Me, person)
        End Sub
    End Class
End Namespace
