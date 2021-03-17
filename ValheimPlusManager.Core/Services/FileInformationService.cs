using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using ValheimPlusManager.Core.Repositories;

namespace ValheimPlusManager.Core.Services
{
    public class FileInformationService : BaseService, IFileInformationService
    {
        #region Fields

        private IFileInformationRepository _fileInformationRepository;

        #endregion

        public FileInformationService(IFileInformationRepository fileInformationRepository)
        {
            _fileInformationRepository = fileInformationRepository;
        }

        #region Properties

        protected override ILogger Logger => NLog.LogManager.GetCurrentClassLogger();

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public Version GetProductVersion(Uri filepath)
        {
            Version version = null;

            try
            {
                var fileVersionInfo = _fileInformationRepository.GetFileVersionInfo(filepath);
                if (fileVersionInfo == null || !Version.TryParse(fileVersionInfo.ProductVersion, out version))
                {
                    return null;
                }

                return version;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        #endregion
    }
}
