using OTLog.Core.Constants;
using OTLog.Home.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace OTLog.Home
{
    public class HomeModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>()
                             .RegisterViewWithRegion(RegionNames.MainRegion, typeof(StartPage));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<StartPage>();
            containerRegistry.RegisterForNavigation<HomePage>();
        }
    }
}