using MvvmCross.Navigation;
using MvvmCross.ViewModels;

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

        protected IMvxNavigationService MvxNavigationService { get; private set; }

        #endregion

        #region Methods

        #endregion
    }
}
