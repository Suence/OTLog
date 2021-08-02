using Prism.Mvvm;
using Prism.Regions;

namespace OTLog.Home.ViewModels
{
    public class SettingsViewModel : BindableBase, IRegionMemberLifetime
    {
        public SettingsViewModel()
        {

        }

        public bool KeepAlive => false;
    }
}
