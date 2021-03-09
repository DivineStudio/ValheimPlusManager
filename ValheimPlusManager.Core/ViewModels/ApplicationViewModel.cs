using System;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using ValheimPlusManager.Core.ViewModels;

namespace ValheimPlusManager.Core.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        #region Fields

        private IMvxAsyncCommand _navigateToInstallView;
        private IMvxAsyncCommand _navigateToUpdateView;
        private IMvxAsyncCommand _navigateToConfigView;
        private IMvxAsyncCommand _navigateToSettingsView;

        #endregion Fields

        #region Commands

        public IMvxAsyncCommand NavigateToInstallView;
        public IMvxAsyncCommand NavigateToUpdateView;
        public IMvxAsyncCommand NavigateToConfigView;
        public IMvxAsyncCommand NavigateToSettingsView;

        #endregion Commands

        #region Properties

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}