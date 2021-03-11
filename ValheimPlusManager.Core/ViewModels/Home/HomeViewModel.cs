using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using MvvmCross.Navigation;
using ValheimPlusManager.Core.ViewModels;
using ValheimPlusManager.Core.ViewModels.Config;
using ValheimPlusManager.Core.ViewModels.Install;

namespace ValheimPlusManager.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        #region Fields

        private Version _valheimVersion;
        private Version _valheimPlusVersion;
        private string _valheimGameFolderPath;

        #endregion Fields

        public HomeViewModel(IMvxNavigationService mvxNavigationService)
            : base(mvxNavigationService)
        {
        }

        #region Commands

        public IMvxAsyncCommand NavigateToInstallViewCommand => new MvxAsyncCommand(() => MvxNavigationService.Navigate(typeof(InstallViewModel)));
        public IMvxAsyncCommand NavigateToConfigViewCommand => new MvxAsyncCommand(() => MvxNavigationService.Navigate(typeof(ConfigViewModel)));
        public IMvxAsyncCommand SaveCommand => throw new NotImplementedException();

        #endregion Commands

        #region Properties

        public Version ValheimVersion
        {
            get => _valheimVersion;
            set => SetProperty(ref _valheimVersion, value);
        }
        public Version ValheimPlusVersion
        {
            get => _valheimPlusVersion;
            set => SetProperty(ref _valheimPlusVersion, value);
        }

        public string ValheimGameFolderPath
        {
            get => _valheimGameFolderPath;
            set => SetProperty(ref _valheimGameFolderPath, value);
        }

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}
