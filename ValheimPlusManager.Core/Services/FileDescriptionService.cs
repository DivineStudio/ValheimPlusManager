using System;
using System.Collections.Generic;
using System.Text;
using ValheimPlusManager.Core.Repositories;

namespace ValheimPlusManager.Core.Services
{
    public class FileDescriptionService
    {
        #region Fields

        IFileInformationRepository _fileInformationRepository;

        #endregion

        public FileDescriptionService(IFileInformationRepository fileInformationRepository)
        {
            _fileInformationRepository = fileInformationRepository;
        }

        #region Methods



        #endregion
    }
}
