using System;
using MvvmCross;
using ValheimPlusManager.Core.Repositories;
using ValheimPlusManager.Core.Services;

namespace ValheimPlusManager.Core.Factories
{
    /// <summary>
    /// Factory for returning concrete service objects.
    /// </summary>
    public static class ServiceFactory
    {
        /// <summary>
        /// Create a concrete implementation from the provided <see cref="interface"/> type. 
        /// </summary>
        /// <typeparam name="TInterface">Assign type as a service <see cref="interface"/>.</typeparam>
        /// <returns>Concrete class inheriting from type <see cref="TInterface"/>.</returns>
        public static TInterface Create<TInterface>()
            where TInterface : class
        {
            TInterface service;

            if (typeof(TInterface) == typeof(IFileInformationService))
            {
                service = new FileInformationService(Mvx.IoCProvider.Resolve<IFileInformationRepository>()) as TInterface;
            }
            else
            {
                throw new NotImplementedException();
            }

            return service;
        }
    }
}
