using Octokit;
using ValheimPlusManager.Core.Models;

namespace ValheimPlusManager.Core.Extensions
{
    public static class ReleaseExtensions
    {
        public static ReleaseInfo ToReleaseInfo(this Release release)
        {
            return new ReleaseInfo(release);
        }
    }
}
