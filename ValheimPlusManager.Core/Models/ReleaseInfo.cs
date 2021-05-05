using System;
using Octokit;

namespace ValheimPlusManager.Core.Models
{
    /// <summary>
    /// Response model that maps needed properties from Release.
    /// </summary>
    /// <exception cref="ArgumentException"/>
    public class ReleaseInfo
    {
        private Version _tag;
        private Uri _url;

        public ReleaseInfo()
        {
        }

        /// <summary>
        /// Builds ReleaseResponse object and maps needed Release properties.
        /// </summary>
        /// <param name="release">Release object from GitHub Octokit API.</param>
        public ReleaseInfo(Release release)
        {
            SetProperties(release);
        }

        /// <summary>
        /// Mapped from <see cref="Release.TagName"/>.
        /// </summary>
        public virtual Version Tag => _tag;
        /// <summary>
        /// Mapped from <see cref="Release.HtmlUrl"/>.
        /// </summary>
        public virtual Uri Url => _url;

        /// <summary>
        /// Sets the properties of this <see cref="ReleaseRepsonse"/> object by mapping the needed properties from the provided <see cref="Release"/> object.
        /// </summary>
        /// <param name="release"></param>
        private void SetProperties(Release release)
        {
            bool areSetSuccessfully;

            areSetSuccessfully = Version.TryParse(release.TagName, out _tag);
            // Need to do this using a the Uri.TryCreate()'s throw new UriFormatException
            try
            {
                areSetSuccessfully &= Uri.TryCreate(release.HtmlUrl, UriKind.Absolute, out _url);
            }
            catch (UriFormatException)
            {
            }

            if (!areSetSuccessfully)
            {
                throw new ArgumentException($"Release argument \"release\" properties are invalid. release.TagName={release.TagName} release.HtmlUrl={release.HtmlUrl}");
            }
        }
    }
}
