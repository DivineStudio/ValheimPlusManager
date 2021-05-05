using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross;
using Octokit;
using Octokit.Reactive;
using ValheimPlusManager.Core.Repositories;

namespace ValheimPlusManager.Core.Factories
{
    /// <summary>
    /// Factory for returning concrete GitHub client objects.
    /// </summary>
    public static class GitHubClientFactory
    {
        /// <summary>
        /// Valheim Plus GitHub base address.
        /// </summary>
        private static readonly Uri ValheimPlusGitHubBaseAddress = new Uri("https://github.com/valheimPlus/ValheimPlus");

        /// <summary>
        /// Valheim Plus Manager product header value.
        /// </summary>
        private static readonly ProductHeaderValue ProductHeaderValue = new ProductHeaderValue("ValheimPlusManager");

        /// <summary>
        /// Create a concrete implementation <see cref="IGitHubClient"/>.
        /// </summary>
        /// <returns>GitHub client.</returns>
        public static IGitHubClient Create()
        {
            return new GitHubClient(ProductHeaderValue, ValheimPlusGitHubBaseAddress);
        }

        /// <summary>
        /// Create a concrete implementation <see cref="IObservableGitHubClient"/>. 
        /// </summary>
        /// <returns>Observable GitHub client.</returns>
        public static IObservableGitHubClient CreateObservable()
        {
            return new ObservableGitHubClient(ProductHeaderValue, ValheimPlusGitHubBaseAddress);
        }
    }
}
