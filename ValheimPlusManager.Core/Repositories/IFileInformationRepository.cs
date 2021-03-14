using System;
using System.Diagnostics;

namespace ValheimPlusManager.Core.Repositories
{
    public interface IFileInformationRepository
    {
        FileVersionInfo GetFileVersionInfo(Uri filepath);
    }
}
