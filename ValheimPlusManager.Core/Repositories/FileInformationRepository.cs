using System;
using System.Diagnostics;
using Serilog;

namespace ValheimPlusManager.Core.Repositories
{
    public class FileInformationRepository : BaseRepository<FileInformationRepository>, IFileInformationRepository
    {
        public FileInformationRepository()
        {
        }

        public override bool IsLoggerCreated => base.IsLoggerCreated;

        #region Methods
        /// <inheritdoc/>
        public string GetProductVersion(Uri filepath)
        {
            return FileVersionInfo.GetVersionInfo(filepath.LocalPath)?.ProductVersion;
        } 
        #endregion
    }
}
