using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;
using NLog;
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
            SetNLogConfigLocation();
            SetIoC();
            base.InitializeFirstChance();
        }

        private void SetNLogConfigLocation()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("NLog.debug.config");
            }
            else
            {
                NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("NLog.config");
            }
        }

        private void SetIoC()
        {
        }
    }
}
