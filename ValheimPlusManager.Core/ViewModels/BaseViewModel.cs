using System;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ValheimPlusManager.Core.ViewModels.ErrorHandling;

namespace ValheimPlusManager.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        #region Fields

        #endregion

        public BaseViewModel(IMvxNavigationService mvxNavigationService)
        {
            MvxNavigationService = mvxNavigationService;
        }

        #region Properties
        protected ErrorViewModel Errors { get; } = new Lazy<ErrorViewModel>().Value;

        protected IMvxNavigationService MvxNavigationService { get; private set; }

        #endregion

        #region Methods

        #endregion
    }
}
