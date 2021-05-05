using System;
using MvvmCross;
using Octokit;
using Serilog;
using ValheimPlusManager.Core.Repositories;

namespace ValheimPlusManager.Core.Factories
{
    /// <summary>
    /// Factory for returning concrete repository objects.
    /// </summary>
    public static class RepositoryFactory
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

            if (typeof(TInterface) == typeof(IFileInformationRepository))
            {
                service = new FileInformationRepository() as TInterface;
            }
            else if (typeof(TInterface) == typeof(IGitHubRepository))
            {
                service = new GitHubRepository(Mvx.IoCProvider.Resolve<IGitHubClient>()) as TInterface;
            }
            else
            {
                throw new NotImplementedException();
            }

            return service;
        }
    }
}
