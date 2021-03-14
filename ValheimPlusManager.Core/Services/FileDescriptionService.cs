using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using ValheimPlusManager.Core.Repositories;

namespace ValheimPlusManager.Core.Services
{
    public class FileDescriptionService : BaseService, IFileDescriptionService
    {
        #region Fields

        private IFileInformationRepository _fileInformationRepository;

        #endregion

        public FileDescriptionService(IFileInformationRepository fileInformationRepository)
        {
            _fileInformationRepository = fileInformationRepository;
        }

        protected override ILogger Logger => NLog.LogManager.GetCurrentClassLogger();

        #region Methods



        #endregion
    }
}
