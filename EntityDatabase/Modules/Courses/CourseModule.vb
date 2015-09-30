Imports Microsoft.Practices.Prism.Modularity
Imports Microsoft.Practices.Prism.Regions
Imports Microsoft.Practices.Unity
Imports Infrastructure
Imports Modules.Courses.View


Public Class CourseModule
    Implements IModule

    Private _container As IUnityContainer
    Private _regionManager As IRegionManager

    Sub New(ByVal container As IUnityContainer, ByVal regionManager As IRegionManager)

        _container = New UnityContainer
        _container = container

        _regionManager = New RegionManager
        _regionManager = regionManager
    End Sub

    Public Sub Initialize() Implements Microsoft.Practices.Prism.Modularity.IModule.Initialize
        RegisterViewsWithRegions()
    End Sub

    Protected Overridable Sub RegisterViewsWithRegions()
        _regionManager.RegisterViewWithRegion(RegionNames.MenuRegion, GetType())
        _regionManager.RegisterViewWithRegion(RegionNames.MainRegion, GetType(CoursesList))
    End Sub
End Class
End Class
