using System;
using System.Diagnostics;

namespace ValheimPlusManager.Core.Repositories
{
    public class FileInformationRepository : BaseRepository, IFileInformationRepository
    {
        public FileInformationRepository()
        {

        }

        public FileVersionInfo GetFileVersionInfo(Uri filepath)
        {
            FileVersionInfo fileVersionInfo = null;

            if (filepath.IsFile)
            {
                FileVersionInfo.GetVersionInfo(filepath.LocalPath); 
            }

            return fileVersionInfo;
        }
    }
}
