using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ValheimPlusManager.Core.ViewModels;
using ValheimPlusManager.Core.ViewModels.Home;

namespace ValheimPlusManager.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Repository")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            // There needs to be a determination of which ViewModel is to be displayed first.
            RegisterAppStart<HomeViewModel>();
        }
    }
}
