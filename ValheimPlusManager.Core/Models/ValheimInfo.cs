using System;

namespace ValheimPlusManager.Core.Models
{
    public class ValheimInfo
    {
        public ValheimInfo()
        {
            ValheimPlusInfo = new ValheimPlusInfo();
        }

        public ValheimPlusInfo ValheimPlusInfo { get; set; }
        public Version Version { get; set; }
        public Uri InstallLocation { get; set; }
    }
}
