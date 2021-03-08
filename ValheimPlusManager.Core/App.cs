using MvvmCross.IoC;
using MvvmCross.ViewModels;
using ValheimPlusManager.Core.ViewModels;

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

            RegisterAppStart<ApplicationViewModel>();
        }
    }
}
