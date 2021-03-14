using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;
using NLog;

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
    }
}
