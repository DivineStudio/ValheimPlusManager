using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;
using Octokit;
using Octokit.Reactive;
using Serilog;
using Serilog.Events;
using ValheimPlusManager.Core.Factories;
using ValheimPlusManager.Core.Repositories;
using ValheimPlusManager.Core.Services;

namespace ValheimPlusManager.Core
{
    public class Setup : MvxWpfSetup
    {
        protected override IMvxApplication CreateApp()
        {
            return null;
        }

        protected override void InitializeFirstChance()
        {
            SetSerilogConfig();
            SetIoC();
            base.InitializeFirstChance();
        }

        private void SetSerilogConfig()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var logFolder = "logs";
            var logsDirectory = Path.Combine(currentDirectory, logFolder);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                    .Enrich.FromLogContext()
                    .WriteTo.File(Path.Combine(logsDirectory, "debugLog"))
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .Enrich.FromLogContext()
                    .WriteTo.File(Path.Combine(logsDirectory, "log"))
                    .CreateLogger();
            }
        }

        private void SetIoC()
        {
            Mvx.IoCProvider.RegisterSingleton<IGitHubClient>(GitHubClientFactory.Create());

            Mvx.IoCProvider.RegisterSingleton<IFileInformationRepository>(RepositoryFactory.Create<IFileInformationRepository>());
            Mvx.IoCProvider.RegisterSingleton<IGitHubRepository>(RepositoryFactory.Create<IGitHubRepository>());

            Mvx.IoCProvider.RegisterSingleton<IFileInformationService>(ServiceFactory.Create<IFileInformationService>());
            Mvx.IoCProvider.RegisterSingleton<IGitHubService>(ServiceFactory.Create<IGitHubService>());
        }
    }
}
