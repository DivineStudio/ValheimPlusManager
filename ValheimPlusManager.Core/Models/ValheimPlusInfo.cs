using System;

namespace ValheimPlusManager.Core.Models
{
    public  class ValheimPlusInfo
    {
        public Version Version { get; set; }
        public Uri DllLocation { get; set; }
        public Uri ConfigLocation { get; set; }
    }
}
